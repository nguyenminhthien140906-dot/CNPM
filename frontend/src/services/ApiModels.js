
export const ApiResult = {
    ok: (message = "Thành công") => ({
        success: true,
        message
    }),

    fail: (message) => ({
        success: false,
        message
    })
};

export const createUserDto = (data = {}) => ({
    id: data.id || "",
    fullName: data.fullName || "",
    email: data.email || "",
    role: data.role || "STUDENT",
    status: data.status || "Hoạt động"
});

export const createDashboardSummaryDto = (data = {}) => ({
    totalProjects: data.totalProjects || 0,
    totalClasses: data.totalClasses || 0,
    totalTeams: data.totalTeams || 0,
    totalTasks: data.totalTasks || 0,
    completedTasks: data.completedTasks || 0,
    inProgressTasks: data.inProgressTasks || 0,
    pendingTasks: data.pendingTasks || 0,
    recentProjects: data.recentProjects || [],
    upcomingCheckpoints: data.upcomingCheckpoints || []
});

export const createDashboardProjectDto = (data = {}) => ({
    name: data.name || "",
    description: data.description || "",
    status: data.status || ""
});

export const createDashboardCheckpointDto = (data = {}) => ({
    name: data.name || "",
    project: data.project || "",
    dueDate: data.dueDate || null,
    status: data.status || ""
});

export const createSubjectDto = (data = {}) => ({
    subjectCode: data.subjectCode || "",
    subjectName: data.subjectName || "",
    credits: data.credits ?? 3,
    description: data.description || "",
    status: data.status || "ACTIVE"
});

export const createSyllabusDto = (data = {}) => ({
    syllabusCode: data.syllabusCode || "",
    subjectCode: data.subjectCode || "",
    subjectName: data.subjectName || "",
    version: data.version || "2026.1",
    objectives: data.objectives || "",
    contentSummary: data.contentSummary || "",
    status: data.status || "DRAFT"
});

export const createClassDto = (data = {}) => ({
    code: data.code || "",
    name: data.name || "",
    semester: data.semester || "1",
    academicYear: data.academicYear || "2026-2027",
    capacity: data.capacity ?? 40,
    studentCount: data.studentCount || 0,
    status: data.status || "ACTIVE"
});

export const createProjectApprovalDto = (data = {}) => ({
    id: data.id || "",
    name: data.name || "",
    lecturer: data.lecturer || "",
    submittedDate: data.submittedDate || "",
    description: data.description || "",
    status: data.status || "PENDING",
    rejectionReason: data.rejectionReason || ""
});
