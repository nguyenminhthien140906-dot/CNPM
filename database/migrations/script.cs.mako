
namespace CollabSphere.Database.Migrations.Generated;

internal static class MigrationTemplate
{
    public const string Version = "001";

    public const string Description = "Initial database schema";

    public const string UpSql = """
        CREATE TABLE roles (
            role_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
            role_code VARCHAR(30) NOT NULL UNIQUE,
            role_name VARCHAR(100) NOT NULL
        );

        CREATE TABLE users (
            user_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
            role_id BIGINT UNSIGNED NOT NULL,
            username VARCHAR(50) NOT NULL UNIQUE,
            email VARCHAR(255) NOT NULL UNIQUE,
            password_hash VARCHAR(255) NOT NULL,
            full_name VARCHAR(150) NOT NULL,
            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

            CONSTRAINT fk_users_roles
                FOREIGN KEY (role_id)
                REFERENCES roles(role_id)
        );
        """;

    public const string DownSql = """
        DROP TABLE IF EXISTS users;
        DROP TABLE IF EXISTS roles;
        """;
}

