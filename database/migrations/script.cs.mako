namespace CollabSphere.Database.Migrations.Generated;

internal static class MigrationTemplate
{
    public const string Version = "001";
    public const string Description = "Initial database schema";

    public const string UpSql = """
        CREATE TABLE roles (
            role_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
            role_code VARCHAR(30) NOT NULL UNIQUE,
            role_name VARCHAR(100) NOT NULL,
            description TEXT,
            created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        ) ENGINE=InnoDB
          DEFAULT CHARSET=utf8mb4
          COLLATE=utf8mb4_unicode_ci;

        CREATE TABLE users (
            user_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
            role_id BIGINT UNSIGNED NOT NULL,
            username VARCHAR(50) NOT NULL UNIQUE,
            email VARCHAR(255) NOT NULL UNIQUE,
            password_hash VARCHAR(255) NOT NULL,
            full_name VARCHAR(150) NOT NULL,
            avatar_url VARCHAR(500),
            phone VARCHAR(20),
            is_active BOOLEAN NOT NULL DEFAULT TRUE,
            created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
            updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
                ON UPDATE CURRENT_TIMESTAMP,

            CONSTRAINT fk_users_roles
                FOREIGN KEY (role_id)
                REFERENCES roles(role_id)
                ON UPDATE CASCADE
                ON DELETE RESTRICT
        ) ENGINE=InnoDB
          DEFAULT CHARSET=utf8mb4
          COLLATE=utf8mb4_unicode_ci;

        CREATE TABLE students (
            student_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
            user_id BIGINT UNSIGNED NOT NULL UNIQUE,
            student_code VARCHAR(30) NOT NULL UNIQUE,

            CONSTRAINT fk_students_users
                FOREIGN KEY (user_id)
                REFERENCES users(user_id)
                ON UPDATE CASCADE
                ON DELETE CASCADE
        ) ENGINE=InnoDB
          DEFAULT CHARSET=utf8mb4
          COLLATE=utf8mb4_unicode_ci;

        CREATE TABLE lecturers (
            lecturer_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
            user_id BIGINT UNSIGNED NOT NULL UNIQUE,
            lecturer_code VARCHAR(30) NOT NULL UNIQUE,

            CONSTRAINT fk_lecturers_users
                FOREIGN KEY (user_id)
                REFERENCES users(user_id)
                ON UPDATE CASCADE
                ON DELETE CASCADE
        ) ENGINE=InnoDB
          DEFAULT CHARSET=utf8mb4
          COLLATE=utf8mb4_unicode_ci;
        """;

    public const string DownSql = """
        DROP TABLE IF EXISTS lecturers;
        DROP TABLE IF EXISTS students;
        DROP TABLE IF EXISTS users;
        DROP TABLE IF EXISTS roles;
        """;
}
