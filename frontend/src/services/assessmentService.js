
import apiClient from "./apiClient";

const assessmentService = {
    getAll: async () => {
        return await apiClient.get("/assessments");
    },

    getById: async (id) => {
        return await apiClient.get(`/assessments/${id}`);
    },

    create: async (evaluation) => {
        if (evaluation.score < 0 || evaluation.score > 10) {
            throw new Error("Điểm đánh giá phải từ 0 đến 10.");
        }

        if (evaluation.evaluatorId === evaluation.targetId) {
            throw new Error("Không thể tự đánh giá bản thân.");
        }

        return await apiClient.post("/assessments", evaluation);
    },

    delete: async (id) => {
        return await apiClient.delete(`/assessments/${id}`);
    }
};

export default assessmentService;
