
const TOKEN_KEY = "collabsphere_token";

class ApiClient {
    constructor() {
        this.baseUrl = "";
    }

    setBaseUrl(url) {
        this.baseUrl = url?.replace(/\/+$/, "") || "";
    }

    getToken() {
        return localStorage.getItem(TOKEN_KEY);
    }

    setToken(token) {
        if (token) {
            localStorage.setItem(TOKEN_KEY, token);
        } else {
            localStorage.removeItem(TOKEN_KEY);
        }
    }

    async request(url, options = {}) {
        const token = this.getToken();

        const headers = {
            Accept: "application/json",
            ...options.headers
        };

        if (options.body && !(options.body instanceof FormData)) {
            headers["Content-Type"] = "application/json";
        }

        if (token) {
            headers.Authorization = `Bearer ${token}`;
        }

        const response = await fetch(
            `${this.baseUrl}${url}`,
            {
                ...options,
                headers
            }
        );

        if (response.status === 204) {
            if (!response.ok) {
                throw new Error("Yêu cầu không thành công.");
            }
            return null;
        }

        const contentType = response.headers.get("content-type");

        const data = contentType?.includes("application/json")
            ? await response.json()
            : await response.text();

        if (!response.ok) {
            const message =
                data?.message ||
                data?.Message ||
                data?.title ||
                data?.Title ||
                (typeof data === "string" ? data : null) ||
                `Lỗi HTTP ${response.status}`;

            throw new Error(message);
        }

        return data;
    }

    get(url) {
        return this.request(url, {
            method: "GET"
        });
    }

    post(url, data) {
        return this.request(url, {
            method: "POST",
            body: JSON.stringify(data)
        });
    }

    put(url, data) {
        return this.request(url, {
            method: "PUT",
            body: JSON.stringify(data)
        });
    }

    patch(url, data) {
        return this.request(url, {
            method: "PATCH",
            body: JSON.stringify(data)
        });
    }

    delete(url) {
        return this.request(url, {
            method: "DELETE"
        });
    }
}

const apiClient = new ApiClient();

export default apiClient;
