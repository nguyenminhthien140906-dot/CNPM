class DomainException(Exception):
    """Lớp gốc cho các ngoại lệ của Domain"""
    pass

class InvalidTaskStatusException(DomainException):
    def __init__(self, status):
        super().__init__(f"Trạng thái nhiệm vụ không hợp lệ: {status}")

class UnauthorizedException(DomainException):
    def __init__(self, message="Người dùng không có quyền thực hiện hành động này."):
        super().__init__(message)

class UserNotFoundException(DomainException):
    def __init__(self, user_id):
        super().__init__(f"Không tìm thấy người dùng với ID: {user_id}")
