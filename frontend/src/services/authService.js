
import apiClient from "./apiClient";

const authService = {
    login: async (email, password) => {
        return await apiClient.post("/auth/login", {
            email,
            password
        });
    },

    register: async (data) => {
        return await apiClient.post("/auth/register", {
            fullName: data.fullName,
            email: data.email,
            password: data.password
        });
    },

    getCurrentUser: async () => {
        return await apiClient.get("/auth/me");
    },

    logout: () => {
        localStorage.removeItem("authToken");
        localStorage.removeItem("user");
    },

    saveSession: (token, user) => {
        localStorage.setItem("authToken", token);
        localStorage.setItem("user", JSON.stringify(user));
    },

    getSession: () => {
        const token = localStorage.getItem("authToken");
        const user = localStorage.getItem("user");

        return {
            token,
            user: user ? JSON.parse(user) : null
        };
    },

    clearSession: () => {
        localStorage.removeItem("authToken");
        localStorage.removeItem("user");
    }
};

export default authService;
