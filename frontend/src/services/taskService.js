
import apiClient from "./apiClient";

const ALLOWED_STATUSES = [
    "Pending",
    "InProgress",
    "Review",
    "Completed"
];

const taskService = {
    getByProject: async (projectId) => {
        return await apiClient.get(
            `/tasks/project/${projectId}`
        );
    },

    create: async (task) => {
        if (!task.title?.trim()) {
            throw new Error(
                "Tên công việc không được để trống."
            );
        }

        return await apiClient.post("/tasks", {
            ...task,
            status: "Pending"
        });
    },

    updateStatus: async (id, status) => {
        if (!ALLOWED_STATUSES.includes(status)) {
            throw new Error(
                "Trạng thái công việc không hợp lệ."
            );
        }

        return await apiClient.patch(
            `/tasks/${id}/status`,
            { status }
        );
    },

    delete: async (id) => {
        return await apiClient.delete(`/tasks/${id}`);
    }
};

export default taskService;
