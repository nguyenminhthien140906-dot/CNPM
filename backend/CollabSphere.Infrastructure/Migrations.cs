import sqlite3
from infrastructure.configuration import config


def migrate_database():
    connection = sqlite3.connect(config.DATABASE)
    cursor = connection.cursor()

    cursor.execute("""
        CREATE TABLE IF NOT EXISTS users (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            username TEXT NOT NULL,
            email TEXT UNIQUE NOT NULL,
            role TEXT DEFAULT 'Member'
        )
    """)

    connection.commit()
    connection.close()

    print("Di cư cơ sở dữ liệu thành công!")
