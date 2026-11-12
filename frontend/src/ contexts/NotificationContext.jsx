
import React, {
    createContext,
    useContext,
    useState,
    useCallback
} from "react";

const NotificationContext = createContext(null);

export const NotificationProvider = ({ children }) => {
    const [notifications, setNotifications] = useState([]);

    const removeNotification = useCallback((id) => {
        setNotifications((prev) =>
            prev.filter((item) => item.id !== id)
        );
    }, []);

    const addNotification = useCallback((
        message,
        type = "info",
        duration = 4000
    ) => {
        const id = crypto.randomUUID();

        const notification = {
            id,
            message,
            type,
            createdAt: new Date()
        };

        setNotifications((prev) => [
            ...prev,
            notification
        ]);

        if (duration > 0) {
            setTimeout(() => {
                removeNotification(id);
            }, duration);
        }

        return id;
    }, [removeNotification]);

    const success = (message, duration = 4000) =>
        addNotification(message, "success", duration);

    const error = (message, duration = 5000) =>
        addNotification(message, "error", duration);

    const warning = (message, duration = 4000) =>
        addNotification(message, "warning", duration);

    const info = (message, duration = 4000) =>
        addNotification(message, "info", duration);

    const clearNotifications = () => {
        setNotifications([]);
    };

    const icons = {
        success: "✓",
        error: "✕",
        warning: "⚠",
        info: "ℹ"
    };

    const colors = {
        success: {
            background: "#F0FDF4",
            border: "#86EFAC",
            icon: "#16A34A"
        },
        error: {
            background: "#FEF2F2",
            border: "#FCA5A5",
            icon: "#DC2626"
        },
        warning: {
            background: "#FFFBEB",
            border: "#FCD34D",
            icon: "#D97706"
        },
        info: {
            background: "#EFF6FF",
            border: "#93C5FD",
            icon: "#2563EB"
        }
    };

    return (
        <NotificationContext.Provider
            value={{
                notifications,
                addNotification,
                removeNotification,
                clearNotifications,
                success,
                error,
                warning,
                info
            }}
        >
            {children}

            <div style={{
                position: "fixed",
                top: 20,
                right: 20,
                zIndex: 9999,
                display: "flex",
                flexDirection: "column",
                gap: 12,
                width: 350,
                maxWidth: "calc(100vw - 40px)"
            }}>
                {notifications.map((item) => {
                    const color = colors[item.type];

                    return (
                        <div
                            key={item.id}
                            role="alert"
                            style={{
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "space-between",
                                padding: "14px 16px",
                                borderRadius: 10,
                                border: `1px solid ${color.border}`,
                                background: color.background,
                                boxShadow: "0 4px 15px rgba(0,0,0,0.1)"
                            }}
                        >
                            <div style={{
                                display: "flex",
                                alignItems: "center",
                                gap: 12
                            }}>
                                <span style={{
                                    color: color.icon,
                                    fontSize: 20,
                                    fontWeight: "bold"
                                }}>
                                    {icons[item.type]}
                                </span>

                                <span style={{
                                    fontSize: 14,
                                    color: "#334155"
                                }}>
                                    {item.message}
                                </span>
                            </div>

                            <button
                                type="button"
                                onClick={() =>
                                    removeNotification(item.id)
                                }
                                aria-label="Đóng thông báo"
                                style={{
                                    border: "none",
                                    background: "transparent",
                                    fontSize: 22,
                                    cursor: "pointer",
                                    color: "#64748B"
                                }}
                            >
                                ×
                            </button>
                        </div>
                    );
                })}
            </div>
        </NotificationContext.Provider>
    );
};

export const useNotification = () => {
    const context = useContext(NotificationContext);

    if (!context) {
        throw new Error(
            "useNotification phải được sử dụng bên trong NotificationProvider."
        );
    }

    return context;
};

export default NotificationContext;

