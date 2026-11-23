
import { Navigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";

const RoleBasedRoute = ({ roles = [], children }) => {
    const { user, status } = useAuth();

    if (status === "loading") {
        return (
            <section className="role-message">
                <p>Đang kiểm tra vai trò...</p>
            </section>
        );
    }

    if (!user) {
        return <Navigate to="/login" replace />;
    }

    const userRole = user.roleCode || user.role;

    const hasPermission = roles.some(
        (role) =>
            role.toLowerCase() ===
            userRole?.toLowerCase()
    );

    if (!hasPermission) {
        return (
            <section className="role-message">
                <h2>Truy cập bị từ chối</h2>
                <p>
                    Bạn không có quyền sử dụng chức năng này.
                </p>
                <a href="/dashboard">
                    Quay về Dashboard
                </a>
            </section>
        );
    }

    return children;
};

export default RoleBasedRoute;
