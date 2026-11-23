
import apiClient from "./apiClient";

const teamService = {
    getAll: async () => {
        return await apiClient.get("/teams");
    },

    getById: async (id) => {
        return await apiClient.get(`/teams/${id}`);
    },

    create: async (team) => {
        if (!team.name?.trim()) {
            throw new Error("Tên nhóm không được để trống.");
        }

        return await apiClient.post("/teams", team);
    },

    addMember: async (groupId, studentId) => {
        return await apiClient.post(
            `/teams/${groupId}/members`,
            { studentId }
        );
    },

    removeMember: async (groupId, studentId) => {
        return await apiClient.delete(
            `/teams/${groupId}/members/${studentId}`
        );
    }
};

export default teamService;
