// CollabSphere Database Migration Environment
// Requires: MySqlConnector NuGet package

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MySqlConnector;

namespace CollabSphere.Database.Migrations;

public static class MigrationEnvironment
{
    private static string Env(string name, string fallback) =>
        Environment.GetEnvironmentVariable(name) ?? fallback;

    public static string BuildConnectionString()
    {
        var builder = new MySqlConnectionStringBuilder
        {
            Server = Env("MYSQL_HOST", "localhost"),
            Port = uint.TryParse(Env("MYSQL_PORT", "3306"), out var port)
                ? port : 3306,
            UserID = Env("MYSQL_USER", "root"),
            Password = Env("MYSQL_PASSWORD", ""),
            Database = Env("MYSQL_DATABASE", "collabsphere")
        };

        var connectionString =
            Environment.GetEnvironmentVariable(
                "COLLABSPHERE_CONNECTION_STRING");

        return string.IsNullOrWhiteSpace(connectionString)
            ? builder.ConnectionString
            : connectionString;
    }

    public static string FindVersionsDirectory()
    {
        var configured = Environment.GetEnvironmentVariable(
            "COLLABSPHERE_MIGRATIONS_DIR");

        if (!string.IsNullOrWhiteSpace(configured))
        {
            if (!Directory.Exists(configured))
                throw new DirectoryNotFoundException(
                    $"Migration directory not found: {configured}");

            return Path.GetFullPath(configured);
        }

        for (var current =
            new DirectoryInfo(AppContext.BaseDirectory);
            current != null;
            current = current.Parent)
        {
            var candidate = Path.Combine(
                current.FullName,
                "database",
                "migrations",
                "versions");

            if (Directory.Exists(candidate))
                return candidate;
        }

        throw new DirectoryNotFoundException(
            "Cannot find database/migrations/versions.");
    }

    public static async Task ApplyPendingAsync(
        CancellationToken cancellationToken = default)
    {
        var versionsDirectory = FindVersionsDirectory();

        var files = Directory.GetFiles(
                versionsDirectory,
                "*.up.sql",
                SearchOption.TopDirectoryOnly)
            .OrderBy(
                Path.GetFileName,
                StringComparer.Ordinal)
            .ToArray();

        if (files.Length == 0)
            throw new InvalidOperationException(
                "No migration files found.");

        await using var connection =
            new MySqlConnection(BuildConnectionString());

        await connection.OpenAsync(cancellationToken);

        // Create migration history table
        await using (var command = new MySqlCommand(
            """
            CREATE TABLE IF NOT EXISTS schema_migrations (
                version VARCHAR(150) PRIMARY KEY,
                applied_at TIMESTAMP NOT NULL
                    DEFAULT CURRENT_TIMESTAMP
            ) ENGINE=InnoDB;
            """,
            connection))
        {
            await command.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var file in files)
        {
            var filename = Path.GetFileName(file);
            var version = filename[..^".up.sql".Length];

            // Check migration status
            await using var check = new MySqlCommand(
                """
                SELECT EXISTS (
                    SELECT 1
                    FROM schema_migrations
                    WHERE version = @version
                )
                """,
                connection);

            check.Parameters.AddWithValue("version", version);

            var alreadyApplied =
                Convert.ToInt32(
                    await check.ExecuteScalarAsync(
                        cancellationToken)) == 1;

            if (alreadyApplied)
            {
                Console.WriteLine(
                    $"[Database] Skipping {version}");
                continue;
            }

            // Read SQL migration
            var sql = await File.ReadAllTextAsync(
                file,
                cancellationToken);

            await using var transaction =
                await connection.BeginTransactionAsync(
                    cancellationToken);

            try
            {
                await using (var apply = new MySqlCommand(
                    sql,
                    connection,
                    transaction))
                {
                    await apply.ExecuteNonQueryAsync(
                        cancellationToken);
                }

                // Record migration
                await using (var record = new MySqlCommand(
                    """
                    INSERT INTO schema_migrations(version)
                    VALUES (@version)
                    """,
                    connection,
                    transaction))
                {
                    record.Parameters.AddWithValue(
                        "version",
                        version);

                    await record.ExecuteNonQueryAsync(
                        cancellationToken);
                }

                await transaction.CommitAsync(cancellationToken);

                Console.WriteLine(
                    $"[Database] Applied {version}");
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        Console.WriteLine(
            "[Database] All migrations completed.");
    }
}
