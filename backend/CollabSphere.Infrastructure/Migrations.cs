
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CollabSphere.Infrastructure.Persistence;

namespace CollabSphere.Infrastructure;

public static class Migrations
{
    public static async Task ApplyMigrationsAsync(
        IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var logger = scope.ServiceProvider
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("DatabaseMigrations");

        var dbContext = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        try
        {
            logger.LogInformation(
                "Starting database migration for CollabSphere..."
            );

            var pendingMigrations = await dbContext.Database
                .GetPendingMigrationsAsync();

            var pendingList = pendingMigrations.ToList();

            if (pendingList.Count == 0)
            {
                logger.LogInformation(
                    "Database is already up to date."
                );

                return;
            }

            logger.LogInformation(
                "Found {Count} pending migrations.",
                pendingList.Count
            );

            foreach (var migration in pendingList)
            {
                logger.LogInformation(
                    "Pending migration: {MigrationName}",
                    migration
                );
            }

            await dbContext.Database.MigrateAsync();

            logger.LogInformation(
                "Database migrations completed successfully."
            );
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "An error occurred while applying database migrations."
            );

            throw;
        }
    }

    public static async Task<List<string>> GetAppliedMigrationsAsync(
        IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        var migrations = await dbContext.Database
            .GetAppliedMigrationsAsync();

        return migrations.ToList();
    }

    public static async Task<List<string>> GetPendingMigrationsAsync(
        IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        var migrations = await dbContext.Database
            .GetPendingMigrationsAsync();

        return migrations.ToList();
    }

    public static async Task<bool> CanConnectAsync(
        IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        try
        {
            return await dbContext.Database.CanConnectAsync();
        }
        catch
        {
            return false;
        }
    }

    public static async Task<bool> EnsureDatabaseCreatedAsync(
        IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        try
        {
            return await dbContext.Database.EnsureCreatedAsync();
        }
        catch
        {
            return false;
        }
    }
}
