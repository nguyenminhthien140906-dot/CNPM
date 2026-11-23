
import {
    BrowserRouter,
    Routes,
    Route,
    Navigate,
    useLocation
} from "react-router-dom";

import { useAuth } from "./contexts/AuthContext";

import Login from "./pages/auth/Login";
import Dashboard from "./pages/Dashboard";

const ProtectedRoute = ({ children }) => {
    const { user, status } = useAuth();
    const location = useLocation();

    if (status === "loading") {
        return (
            <section className="route-message">
                <p>Đang kiểm tra quyền truy cập...</p>
            </section>
        );
    }

    if (!user) {
        return (
            <Navigate
                to="/login"
                state={{ from: location }}
                replace
            />
        );
    }

    return children;
};

const PublicRoute = ({ children }) => {
    const { user, status } = useAuth();

    if (status === "loading") {
        return (
            <section className="route-message">
                <p>Đang kiểm tra quyền truy cập...</p>
            </section>
        );
    }

    if (user) {
        return <Navigate to="/dashboard" replace />;
    }

    return children;
};

const NotFound = () => {
    return (
        <section className="route-message">
            <h1>404</h1>
            <p>Không tìm thấy trang bạn yêu cầu.</p>
            <a href="/dashboard">Quay về Dashboard</a>
        </section>
    );
};

const AppRoutes = () => {
    const { user } = useAuth();

    return (
        <Routes>
            <Route
                path="/login"
                element={
                    <PublicRoute>
                        <Login />
                    </PublicRoute>
                }
            />

            <Route
                path="/dashboard"
                element={
                    <ProtectedRoute>
                        <Dashboard />
                    </ProtectedRoute>
                }
            />

            <Route
                path="*"
                element={<NotFound />}
            />
        </Routes>
    );
};

function App() {
    return (
        <BrowserRouter>
            <AppRoutes />
        </BrowserRouter>
    );
}

export default App;
