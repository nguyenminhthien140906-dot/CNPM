
import {
    createContext,
    useCallback,
    useContext,
    useEffect,
    useRef,
    useState
} from "react";

export const NotificationType = Object.freeze({
    SUCCESS: "success",
    ERROR: "error",
    WARNING: "warning",
    INFO: "info"
});

export const NotificationContext = createContext(null);

export const useNotification = () => {
    const context = useContext(NotificationContext);

    if (!context) {
        throw new Error(
            "useNotification must be used within NotificationProvider"
        );
    }

    return context;
};

export const NotificationProvider = ({ children }) => {
    const [notifications, setNotifications] = useState([]);

    const timers = useRef(new Map());

    const removeNotification = useCallback((id) => {
        const timer = timers.current.get(id);

        if (timer) {
            clearTimeout(timer);
            timers.current.delete(id);
        }

        setNotifications((prev) =>
            prev.filter((item) => item.id !== id)
        );
    }, []);

    const addNotification = useCallback((
        message,
        type = NotificationType.INFO,
        duration = 4000
    ) => {
        if (!message || !message.trim()) {
            throw new Error("Message cannot be empty.");
        }

        const notification = {
            id: crypto.randomUUID(),
            message,
            type,
            createdAt: new Date().toISOString()
        };

        setNotifications((prev) => [
            ...prev,
            notification
        ]);

        if (duration > 0) {
            const timer = setTimeout(() => {
                removeNotification(notification.id);
            }, duration);

            timers.current.set(notification.id, timer);
        }

        return notification.id;
    }, [removeNotification]);

    const success = useCallback((message, duration = 4000) => {
        return addNotification(
            message,
            NotificationType.SUCCESS,
            duration
        );
    }, [addNotification]);

    const error = useCallback((message, duration = 5000) => {
        return addNotification(
            message,
            NotificationType.ERROR,
            duration
        );
    }, [addNotification]);

    const warning = useCallback((message, duration = 4000) => {
        return addNotification(
            message,
            NotificationType.WARNING,
            duration
        );
    }, [addNotification]);

    const info = useCallback((message, duration = 4000) => {
        return addNotification(
            message,
            NotificationType.INFO,
            duration
        );
    }, [addNotification]);

    const clearNotifications = useCallback(() => {
        timers.current.forEach((timer) => {
            clearTimeout(timer);
        });

        timers.current.clear();
        setNotifications([]);
    }, []);

    useEffect(() => {
        return () => {
            timers.current.forEach((timer) => {
                clearTimeout(timer);
            });

            timers.current.clear();
        };
    }, []);

    const value = {
        notifications,
        addNotification,
        removeNotification,
        clearNotifications,
        success,
        error,
        warning,
        info
    };

    return (
        <NotificationContext.Provider value={value}>
            {children}
        </NotificationContext.Provider>
    );
};

export default NotificationProvider;
