
import apiClient from "./apiClient";

const authService = {
    async login(usernameOrEmail, password) {
        const response = await apiClient.post(
            "/api/Auth/login",
            {
                usernameOrEmail,
                password
            }
        );

        const data = response?.data ?? response?.Data;

        if (!data?.accessToken && !data?.AccessToken) {
            throw new Error("Không nhận được Access Token.");
        }

        const token = data.accessToken ?? data.AccessToken;

        apiClient.setToken(token);

        return {
            accessToken: token,
            user: data.user ?? data.User
        };
    },

    async getCurrentUser() {
        const response = await apiClient.get("/api/Auth/me");

        return response?.data ?? response?.Data ?? null;
    },

    logout() {
        apiClient.setToken(null);
    },

    isAuthenticated() {
        return Boolean(apiClient.getToken());
    }
};

export default authService;
