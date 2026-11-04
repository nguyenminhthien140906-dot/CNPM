namespace CollabSphere.Domain.Enums
{
    public enum UserRole
    {
        Admin,
        Head,
        Staff,
        Lecturer,
        Student
    }

    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Review,
        Done
    }

    public enum EvaluationType
    {
        LecturerToGroup,
        PeerReview
    }
}
