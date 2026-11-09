from sqlalchemy.schema import CreateTable
from sqlalchemy.dialects import mysql
from app import create_app
from app.models import db

app = create_app()

with app.app_context():
    dialect = mysql.dialect()
    tables = db.metadata.sorted_tables

    output = [
        "-- CollabSphere MySQL Schema Preview",
        "-- Generated from Flask SQLAlchemy models",
        "-- Preview only: NOT EXECUTED",
        "SET NAMES utf8mb4;",
        ""
    ]

    for table in tables:
        output.append(f"\n-- TABLE: {table.name}")
        output.append(
            str(CreateTable(table).compile(dialect=dialect)) + ";"
        )

    # MySQL-specific ON UPDATE CURRENT_TIMESTAMP
    output.extend([
        "",
        "-- MYSQL-SPECIFIC DEFAULT AND UPDATE RULES",
        "",
        "ALTER TABLE users",
        "MODIFY COLUMN updated_at TIMESTAMP NOT NULL",
        "DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;",
        "",
        "ALTER TABLE projects",
        "MODIFY COLUMN updated_at TIMESTAMP NOT NULL",
        "DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;",
        "",
        "ALTER TABLE workspace_documents",
        "MODIFY COLUMN updated_at TIMESTAMP NOT NULL",
        "DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;",
        "",
        "-- Normalize boolean default",
        "ALTER TABLE notifications",
        "MODIFY COLUMN is_read BOOLEAN NOT NULL DEFAULT 0;",
    ])

    path = r"..\..\database\schema.mysql.preview.sql"

    with open(path, "w", encoding="utf-8") as file:
        file.write("\n".join(output))

    print("TOTAL TABLES:", len(tables))
    print("PREVIEW CREATED:", path)
