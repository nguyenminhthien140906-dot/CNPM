using System;

namespace CollabSphere.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }

    public class InvalidTaskStatusException : DomainException
    {
        public InvalidTaskStatusException(string status) 
            : base($"Trạng thái nhiệm vụ không hợp lệ: {status}") { }
    }

    public class UnauthorizedException : DomainException
    {
        public UnauthorizedException(string message = "Người dùng không có quyền thực hiện hành động này.") 
            : base(message) { }
    }

    public class UserNotFoundException : DomainException
    {
        public UserNotFoundException(string userId) 
            : base($"Không tìm thấy người dùng với ID: {userId}") { }
    }
}
