
import apiClient from "./apiClient";

const projectService = {
    getAll: async () => {
        return await apiClient.get("/projects");
    },

    getById: async (id) => {
        return await apiClient.get(`/projects/${id}`);
    },

    create: async (project) => {
        if (!project.name?.trim()) {
            throw new Error(
                "Tên dự án không được để trống."
            );
        }

        return await apiClient.post("/projects", project);
    },

    update: async (id, data) => {
        if (!data.name?.trim()) {
            throw new Error(
                "Tên dự án không được để trống."
            );
        }

        return await apiClient.put(
            `/projects/${id}`,
            data
        );
    },

    delete: async (id) => {
        return await apiClient.delete(`/projects/${id}`);
    }
};

export default projectService;
