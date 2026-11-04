import sqlite3
from infrastructure.configuration import config


class Persistence:
    def connect(self):
        return sqlite3.connect(config.DATABASE)

    def execute(self, query, parameters=()):
        connection = self.connect()
        cursor = connection.cursor()

        cursor.execute(query, parameters)

        connection.commit()
        result = cursor.fetchall()

        connection.close()

        return result
