namespace CollabSphere.API.Authorization;

public static class RoleNames
{
    public const string Admin = "ADMIN";
    public const string Head = "HEAD";
    public const string Staff = "STAFF";
    public const string Lecturer = "LECTURER";
    public const string Student = "STUDENT";
}

public static class RolePolicies
{
    public const string AdminOnly = "AdminOnly";
    public const string HeadOnly = "HeadOnly";
    public const string StaffOnly = "StaffOnly";
    public const string LecturerOnly = "LecturerOnly";
    public const string StudentOnly = "StudentOnly";
}
