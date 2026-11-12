
import React, {
    createContext,
    useContext,
    useEffect,
    useState,
    useCallback
} from "react";

const AuthContext = createContext(null);

const API_URL =
    import.meta.env.VITE_API_URL || "http://localhost:5000";

const TOKEN_KEY = "collabsphere_token";

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(
        () => localStorage.getItem(TOKEN_KEY)
    );
    const [loading, setLoading] = useState(true);

    const login = async (usernameOrEmail, password) => {
        try {
            const response = await fetch(
                `${API_URL}/api/Auth/login`,
                {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        usernameOrEmail,
                        password
                    })
                }
            );

            const result = await response.json();

            if (!response.ok) {
                throw new Error(
                    result.message ||
                    "Tên đăng nhập hoặc mật khẩu không đúng."
                );
            }

            const authData = result.data;

            if (!authData?.accessToken) {
                throw new Error("Không nhận được Access Token.");
            }

            localStorage.setItem(
                TOKEN_KEY,
                authData.accessToken
            );

            setToken(authData.accessToken);
            setUser(authData.user);

            return {
                success: true,
                user: authData.user
            };

        } catch (error) {
            return {
                success: false,
                message: error.message
            };
        }
    };

    const logout = useCallback(() => {
        localStorage.removeItem(TOKEN_KEY);
        setToken(null);
        setUser(null);
    }, []);

    const getCurrentUser = useCallback(async (accessToken) => {
        const response = await fetch(
            `${API_URL}/api/Auth/me`,
            {
                method: "GET",
                headers: {
                    Authorization: `Bearer ${accessToken}`
                }
            }
        );

        if (!response.ok) {
            throw new Error("Phiên đăng nhập không hợp lệ.");
        }

        const result = await response.json();

        return result.data;
    }, []);

    useEffect(() => {
        let cancelled = false;

        const checkAuthentication = async () => {
            if (!token) {
                setLoading(false);
                return;
            }

            try {
                const currentUser =
                    await getCurrentUser(token);

                if (!cancelled) {
                    setUser(currentUser);
                }

            } catch {
                if (!cancelled) {
                    localStorage.removeItem(TOKEN_KEY);
                    setToken(null);
                    setUser(null);
                }

            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        };

        checkAuthentication();

        return () => {
            cancelled = true;
        };
    }, [token, getCurrentUser]);

    const hasRole = (role) => {
        if (!user) return false;

        if (Array.isArray(role)) {
            return role.includes(user.role);
        }

        return user.role === role;
    };

    const value = {
        user,
        token,
        loading,
        isAuthenticated: Boolean(token && user),
        login,
        logout,
        hasRole,
        setUser
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
