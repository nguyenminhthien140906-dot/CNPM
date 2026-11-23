
import apiClient from "./apiClient";

const ALLOWED_STATUSES = [
    "Pending",
    "InProgress",
    "Submitted",
    "Approved",
    "Rejected"
];

const checkpointService = {
    getAll: async () => {
        return await apiClient.get("/checkpoints");
    },

    getById: async (id) => {
        return await apiClient.get(`/checkpoints/${id}`);
    },

    create: async (checkpoint) => {
        if (!checkpoint.title?.trim()) {
            throw new Error(
                "Tên checkpoint không được để trống."
            );
        }

        return await apiClient.post("/checkpoints", {
            ...checkpoint,
            status: "Pending"
        });
    },

    updateStatus: async (id, status) => {
        if (!ALLOWED_STATUSES.includes(status)) {
            throw new Error(
                "Trạng thái checkpoint không hợp lệ."
            );
        }

        return await apiClient.patch(
            `/checkpoints/${id}/status`,
            { status }
        );
    },

    delete: async (id) => {
        return await apiClient.delete(`/checkpoints/${id}`);
    }
};

export default checkpointService;
