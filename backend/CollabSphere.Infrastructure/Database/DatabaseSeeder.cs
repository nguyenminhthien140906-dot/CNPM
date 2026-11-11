using Microsoft.AspNetCore.Identity;
using MySqlConnector;

namespace CollabSphere.Infrastructure.Database;

public static class DatabaseSeeder
{
    private const string Placeholder = "REPLACE_WITH_VALID_PASSWORD_HASH";

    public static async Task SeedAsync(
        string connectionString,
        string seedDirectory,
        string defaultPassword,
        CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(seedDirectory))
            throw new DirectoryNotFoundException($"Không tìm thấy thư mục seed: {seedDirectory}");

        if (string.IsNullOrWhiteSpace(defaultPassword))
            throw new InvalidOperationException("Seed:DefaultPassword đang trống.");

        var hasher = new PasswordHasher<object>();
        var passwordHash = hasher.HashPassword(new object(), defaultPassword);

        var files = Directory.GetFiles(seedDirectory, "*.sql")
            .OrderBy(Path.GetFileName, StringComparer.Ordinal)
            .ToArray();

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        foreach (var file in files)
        {
            var sql = (await File.ReadAllTextAsync(file, cancellationToken))
                .Replace(Placeholder, passwordHash);

            try
            {
                await using var command = new MySqlCommand(sql, connection);
                await command.ExecuteNonQueryAsync(cancellationToken);
                Console.WriteLine($"[Seed] OK: {Path.GetFileName(file)}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Seed {Path.GetFileName(file)} thất bại: {ex.Message}", ex);
            }
        }

        Console.WriteLine("[Seed] Hoàn tất.");
    }
}
