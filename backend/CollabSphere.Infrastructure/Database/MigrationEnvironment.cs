using MySqlConnector;

namespace CollabSphere.Infrastructure.Database;

public static class MigrationEnvironment
{
    public static async Task ApplyPendingAsync(
        string connectionString,
        string migrationDirectory,
        CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(migrationDirectory))
            throw new DirectoryNotFoundException(
                $"Không tìm thấy thư mục migration: {migrationDirectory}");

        var files = Directory.GetFiles(migrationDirectory, "*.up.sql")
            .OrderBy(Path.GetFileName, StringComparer.Ordinal)
            .ToArray();

        if (files.Length == 0)
            throw new InvalidOperationException("Không tìm thấy file *.up.sql nào.");

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await using (var create = new MySqlCommand(
            @"CREATE TABLE IF NOT EXISTS schema_migrations (
                version VARCHAR(150) PRIMARY KEY,
                applied_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
              ) ENGINE=InnoDB;", connection))
        {
            await create.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var file in files)
        {
            var version = Path.GetFileName(file)[..^".up.sql".Length];

            await using (var check = new MySqlCommand(
                "SELECT COUNT(*) FROM schema_migrations WHERE version = @v;", connection))
            {
                check.Parameters.AddWithValue("@v", version);
                if (Convert.ToInt32(await check.ExecuteScalarAsync(cancellationToken)) > 0)
                {
                    Console.WriteLine($"[Migration] Skipped: {version}");
                    continue;
                }
            }

            var sql = await File.ReadAllTextAsync(file, cancellationToken);

            try
            {
                await using (var run = new MySqlCommand(sql, connection))
                    await run.ExecuteNonQueryAsync(cancellationToken);

                await using (var record = new MySqlCommand(
                    "INSERT INTO schema_migrations (version) VALUES (@v);", connection))
                {
                    record.Parameters.AddWithValue("@v", version);
                    await record.ExecuteNonQueryAsync(cancellationToken);
                }

                Console.WriteLine($"[Migration] Applied: {version}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Migration {version} thất bại: {ex.Message}", ex);
            }
        }

        Console.WriteLine("[Migration] Hoàn tất.");
    }
}
