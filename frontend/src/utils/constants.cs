
namespace CollabSphere.Web.Utils
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Admin = "Admin";
            public const string Lecturer = "Lecturer";
            public const string Student = "Student";
        }

        public static class TaskStatus
        {
            public const string Pending = "Pending";
            public const string InProgress = "InProgress";
            public const string Review = "Review";
            public const string Completed = "Completed";
        }

        public static class ProjectStatus
        {
            public const string Planning = "Planning";
            public const string Active = "Active";
            public const string Completed = "Completed";
            public const string Cancelled = "Cancelled";
        }

        public static class CheckpointStatus
        {
            public const string Pending = "Pending";
            public const string Submitted = "Submitted";
            public const string Approved = "Approved";
            public const string Rejected = "Rejected";
        }

        public static class Api
        {
            public const string BaseUrl = "https://localhost:7001/api";
            public const int TimeoutSeconds = 30;
        }

        public static class Validation
        {
            public const int MinPasswordLength = 8;
            public const int MaxNameLength = 100;
            public const int MaxDescriptionLength = 1000;
        }
    }
}

