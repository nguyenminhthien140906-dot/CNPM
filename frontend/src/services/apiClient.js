
const API_BASE_URL =
    import.meta.env.VITE_API_BASE_URL || "https://localhost:7001/api";

const getToken = () => {
    return localStorage.getItem("authToken");
};

const request = async (endpoint, options = {}) => {
    const token = getToken();

    const headers = {
        "Content-Type": "application/json",
        ...options.headers
    };

    if (token) {
        headers.Authorization = `Bearer ${token}`;
    }

    const response = await fetch(
        `${API_BASE_URL}${endpoint}`,
        {
            ...options,
            headers
        }
    );

    if (response.status === 401) {
        localStorage.removeItem("authToken");
        window.dispatchEvent(new Event("auth:unauthorized"));
    }

    if (!response.ok) {
        const error = await response.json().catch(() => null);

        throw new Error(
            error?.message ||
            error?.title ||
            `API Error: ${response.status}`
        );
    }

    if (response.status === 204) {
        return null;
    }

    return response.json();
};

const apiClient = {
    get: (url) => request(url),

    post: (url, data) =>
        request(url, {
            method: "POST",
            body: JSON.stringify(data)
        }),

    put: (url, data) =>
        request(url, {
            method: "PUT",
            body: JSON.stringify(data)
        }),

    patch: (url, data) =>
        request(url, {
            method: "PATCH",
            body: JSON.stringify(data)
        }),

    delete: (url) =>
        request(url, {
            method: "DELETE"
        })
};

export default apiClient;
