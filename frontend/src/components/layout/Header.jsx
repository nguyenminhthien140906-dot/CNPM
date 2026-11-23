
import React from "react";
import { useAuth } from "../../contexts/AuthContext";

const Header = ({ onMenuClick }) => {
    const { user, logout } = useAuth();

    const fullName = user?.fullName
        ?? user?.FullName
        ?? user?.username
        ?? user?.Username
        ?? "Người dùng";

    return (
        <header className="app-header">
            <button
                className="menu-toggle"
                onClick={onMenuClick}
            >
                ☰
            </button>

            <div className="header-right">
                <span className="header-user">
                    Xin chào, {fullName}
                </span>

                <button
                    className="logout-button"
                    onClick={logout}
                >
                    Đăng xuất
                </button>
            </div>
        </header>
    );
};

export default Header;
