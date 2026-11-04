from infrastructure.persistence import Persistence


class UserRepository:
    def __init__(self):
        self.database = Persistence()

    def create(self, username, email, role):
        query = """
            INSERT INTO users (username, email, role)
            VALUES (?, ?, ?)
        """

        self.database.execute(
            query, (username, email, role)
        )

        return "Thêm người dùng thành công!"

    def get_all(self):
        query = "SELECT * FROM users"
        return self.database.execute(query)

    def get_by_id(self, user_id):
        query = "SELECT * FROM users WHERE id = ?"
        return self.database.execute(query, (user_id,))

    def update(self, user_id, username, email, role):
        query = """
            UPDATE users
            SET username = ?, email = ?, role = ?
            WHERE id = ?
        """

        self.database.execute(
            query, (username, email, role, user_id)
        )

        return "Cập nhật người dùng thành công!"

    def delete(self, user_id):
        query = "DELETE FROM users WHERE id = ?"

        self.database.execute(query, (user_id,))

        return "Xóa người dùng thành công!"
