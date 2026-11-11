
using MySqlConnector;

namespace CollabSphere.Infrastructure.Database;

public class MigrationEnvironment
{
    private readonly string _connectionString;
    private readonly string _migrationFolder;

    public MigrationEnvironment(
        string connectionString,
        string migrationFolder)
    {
        _connectionString = connectionString;
        _migrationFolder = migrationFolder;
    }

    public async Task RunAsync()
    {
        if (!Directory.Exists(_migrationFolder))
        {
            throw new DirectoryNotFoundException(
                $"Không tìm thấy thư mục migration: {_migrationFolder}");
        }

        await using var connection =
            new MySqlConnection(_connectionString);

        await connection.OpenAsync();

        const string createTableSql = """
            CREATE TABLE IF NOT EXISTS schema_migrations
            (
                version VARCHAR(255) PRIMARY KEY,
                applied_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            );
            """;

        await using (var command =
            new MySqlCommand(createTableSql, connection))
        {
            await command.ExecuteNonQueryAsync();
        }

        var migrationFiles = Directory
            .GetFiles(_migrationFolder, "*.up.sql")
            .OrderBy(file => Path.GetFileName(file))
            .ToList();

        foreach (var file in migrationFiles)
        {
            var version = Path.GetFileName(file);

            const string checkSql = """
                SELECT COUNT(*)
                FROM schema_migrations
                WHERE version = @version;
                """;

            await using var checkCommand =
                new MySqlCommand(checkSql, connection);

            checkCommand.Parameters.AddWithValue(
                "@version", version);

            var result = await checkCommand.ExecuteScalarAsync();

            if (Convert.ToInt32(result) > 0)
            {
                continue;
            }

            var sql = await File.ReadAllTextAsync(file);

            await using var transaction =
                await connection.BeginTransactionAsync();

            try
            {
                await using var migrationCommand =
                    new MySqlCommand(sql, connection, transaction);

                await migrationCommand.ExecuteNonQueryAsync();

                const string insertSql = """
                    INSERT INTO schema_migrations (version)
                    VALUES (@version);
                    """;

                await using var insertCommand =
                    new MySqlCommand(
                        insertSql, connection, transaction);

                insertCommand.Parameters.AddWithValue(
                    "@version", version);

                await insertCommand.ExecuteNonQueryAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

