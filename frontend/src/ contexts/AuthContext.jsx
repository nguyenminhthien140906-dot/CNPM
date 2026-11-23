

import React, {
    createContext,
    useContext,
    useState,
    useEffect,
    useCallback
} from "react";

const AuthContext = createContext(null);

const TOKEN_KEY = "collabsphere_token";

const API_URL = "/api/Auth";

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(null);
    const [loading, setLoading] = useState(true);

    const isAuthenticated = Boolean(token && user);

    const getCurrentUser = useCallback(async (accessToken) => {
        const response = await fetch(`${API_URL}/me`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${accessToken}`,
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            throw new Error("Phiên đăng nhập không hợp lệ.");
        }

        const result = await response.json();

        return result?.data ?? result?.Data ?? null;
    }, []);

    const login = useCallback(async (usernameOrEmail, password) => {
        try {
            const response = await fetch(`${API_URL}/login`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    usernameOrEmail,
                    password
                })
            });

            const result = await response.json().catch(() => null);

            const authData = result?.data ?? result?.Data;
            const accessToken =
                authData?.accessToken ?? authData?.AccessToken;

            if (!response.ok) {
                return {
                    success: false,
                    message:
                        result?.message ??
                        result?.Message ??
                        "Tên đăng nhập hoặc mật khẩu không đúng."
                };
            }

            if (!accessToken) {
                return {
                    success: false,
                    message: "Không nhận được Access Token."
                };
            }

            const currentUser =
                authData?.user ?? authData?.User ?? null;

            localStorage.setItem(TOKEN_KEY, accessToken);

            setToken(accessToken);
            setUser(currentUser);

            return {
                success: true,
                user: currentUser
            };

        } catch (error) {
            return {
                success: false,
                message: error.message ||
                    "Không thể kết nối đến máy chủ."
            };
        }
    }, []);

    const logout = useCallback(() => {
        localStorage.removeItem(TOKEN_KEY);

        setToken(null);
        setUser(null);
    }, []);

    const initialize = useCallback(async () => {
        try {
            const savedToken = localStorage.getItem(TOKEN_KEY);

            if (!savedToken) {
                return;
            }

            const currentUser = await getCurrentUser(savedToken);

            if (!currentUser) {
                throw new Error("Không tìm thấy thông tin người dùng.");
            }

            setToken(savedToken);
            setUser(currentUser);

        } catch (error) {
            localStorage.removeItem(TOKEN_KEY);

            setToken(null);
            setUser(null);

        } finally {
            setLoading(false);
        }
    }, [getCurrentUser]);

    useEffect(() => {
        initialize();
    }, [initialize]);

    const hasRole = useCallback((roles) => {
        if (!user?.roleCode && !user?.RoleCode) {
            return false;
        }

        const userRole = user.roleCode ?? user.RoleCode;

        const roleList = Array.isArray(roles)
            ? roles
            : [roles];

        return roleList.some(
            role =>
                String(userRole).toLowerCase() ===
                String(role).toLowerCase()
        );
    }, [user]);

    const updateUser = useCallback((newUser) => {
        setUser(newUser);
    }, []);

    const value = {
        user,
        token,
        loading,
        isAuthenticated,
        login,
        logout,
        getCurrentUser,
        hasRole,
        setUser: updateUser
    };

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);

    if (!context) {
        throw new Error(
            "useAuth phải được sử dụng bên trong AuthProvider."
        );
    }

    return context;
};

export default AuthContext;
