
import {
    Routes,
    Route,
    Navigate
} from "react-router-dom";

import ProtectedRoute from "./ProtectedRoute";
import RoleBasedRoute from "./RoleBasedRoute";

import Login from "../pages/auth/Login";
import Dashboard from "../pages/Dashboard";

const AppRoutes = () => {
    return (
        <Routes>
            <Route
                path="/login"
                element={<Login />}
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
                path="/admin"
                element={
                    <RoleBasedRoute roles={["ADMIN"]}>
                        <Dashboard />
                    </RoleBasedRoute>
                }
            />

            <Route
                path="/lecturer"
                element={
                    <RoleBasedRoute roles={["LECTURER"]}>
                        <Dashboard />
                    </RoleBasedRoute>
                }
            />

            <Route
                path="/student"
                element={
                    <RoleBasedRoute roles={["STUDENT"]}>
                        <Dashboard />
                    </RoleBasedRoute>
                }
            />

            <Route
                path="/"
                element={
                    <Navigate
                        to="/dashboard"
                        replace
                    />
                }
            />

            <Route
                path="*"
                element={
                    <section className="access-message">
                        <h1>404</h1>
                        <p>
                            Không tìm thấy trang bạn yêu cầu.
                        </p>
                        <a href="/dashboard">
                            Quay về Dashboard
                        </a>
                    </section>
                }
            />
        </Routes>
    );
};

export default AppRoutes;
