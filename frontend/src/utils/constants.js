
export const Constants = {
    Roles: {
        Admin: "Admin",
        Lecturer: "Lecturer",
        Student: "Student"
    },

    TaskStatus: {
        Pending: "Pending",
        InProgress: "InProgress",
        Review: "Review",
        Completed: "Completed"
    },

    ProjectStatus: {
        Planning: "Planning",
        Active: "Active",
        Completed: "Completed",
        Cancelled: "Cancelled"
    },

    CheckpointStatus: {
        Pending: "Pending",
        Submitted: "Submitted",
        Approved: "Approved",
        Rejected: "Rejected"
    },

    Api: {
        BaseUrl:
            import.meta.env.VITE_API_BASE_URL ||
            "https://localhost:7001/api",
        TimeoutSeconds: 30
    },

    Validation: {
        MinPasswordLength: 8,
        MaxNameLength: 100,
        MaxDescriptionLength: 1000
    }
};

export default Constants;
