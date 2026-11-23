
import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";

const ProtectedRoute = ({ children }) => {
    const { user, status } = useAuth();
    const location = useLocation();

    if (status === "loading") {
        return (
            <section className="access-message">
                <p>Đang xác thực tài khoản...</p>
            </section>
        );
    }

    if (!user) {
        return (
            <section className="access-message">
                <h2>Bạn chưa đăng nhập</h2>
                <p>
                    Vui lòng đăng nhập tài khoản CollabSphere
                    để tiếp tục sử dụng hệ thống.
                </p>
                <Navigate
                    to="/login"
                    state={{ from: location }}
                    replace
                />
            </section>
        );
    }

    return children;
};

export default ProtectedRoute;
