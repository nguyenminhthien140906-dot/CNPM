from infrastructure.repository import UserRepository


class UserService:
    def __init__(self):
        self.repository = UserRepository()

    def add_user(self, username, email, role):
        if not username or not email:
            return "Vui lòng nhập đầy đủ thông tin!"

        return self.repository.create(
            username, email, role
        )

    def show_users(self):
        return self.repository.get_all()

    def search_user(self, user_id):
        return self.repository.get_by_id(user_id)

    def update_user(self, user_id, username, email, role):
        if not username or not email:
            return "Thông tin không được để trống!"

        return self.repository.update(
            user_id, username, email, role
        )

    def delete_user(self, user_id):
        return self.repository.delete(user_id)
