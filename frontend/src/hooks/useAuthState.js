
import { useState, useCallback } from "react";

export const createUserSession = (data = {}) => ({
    userId: data.userId || "",
    fullName: data.fullName || "",
    email: data.email || "",
    role: data.role || "",
    avatar: data.avatar || null,
    isAuthenticated: data.isAuthenticated ?? false
});

export const useAuthState = () => {
    const [currentUser, setCurrentUser] = useState(null);

    const isAuthenticated =
        currentUser !== null &&
        currentUser.isAuthenticated;

    const userId = currentUser?.userId || null;
    const fullName = currentUser?.fullName || null;
    const email = currentUser?.email || null;
    const role = currentUser?.role || null;

    const setUser = useCallback((user) => {
        if (!user) {
            throw new Error("User cannot be empty.");
        }

        setCurrentUser({
            ...createUserSession(user),
            isAuthenticated: true
        });
    }, []);

    const updateUser = useCallback((user) => {
        if (!user) {
            throw new Error("User cannot be empty.");
        }

        setCurrentUser((prev) => {
            if (!prev?.isAuthenticated) {
                return prev;
            }

            return {
                ...prev,
                ...user,
                isAuthenticated: true
            };
        });
    }, []);

    const clearUser = useCallback(() => {
        setCurrentUser(null);
    }, []);

    const hasRole = useCallback((targetRole) => {
        if (!isAuthenticated || !targetRole?.trim()) {
            return false;
        }

        return currentUser.role?.toLowerCase() ===
            targetRole.toLowerCase();
    }, [currentUser, isAuthenticated]);

    const hasAnyRole = useCallback((roles = []) => {
        if (!isAuthenticated || !Array.isArray(roles)) {
            return false;
        }

        return roles.some((item) => hasRole(item));
    }, [isAuthenticated, hasRole]);

    return {
        currentUser,
        isAuthenticated,
        userId,
        fullName,
        email,
        role,
        setUser,
        updateUser,
        clearUser,
        hasRole,
        hasAnyRole
    };
};

export default useAuthState;
