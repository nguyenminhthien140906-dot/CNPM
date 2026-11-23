
import React from "react";
import { NavLink } from "react-router-dom";

const menuGroups = [
    {
        title: "TỔNG QUAN",
        items: [
            { label: "Dashboard", path: "/dashboard", icon: "📊" }
        ]
    },
    {
        title: "QUẢN LÝ HỌC TẬP",
        items: [
            { label: "Môn học", path: "/subjects", icon: "📚" },
            { label: "Lớp học", path: "/classes", icon: "🏫" },
            { label: "Dự án", path: "/projects", icon: "📁" },
            { label: "Nhóm", path: "/teams", icon: "👥" },
            { label: "Nhiệm vụ", path: "/tasks", icon: "✓" },
            { label: "Checkpoint", path: "/checkpoints", icon: "🎯" },
            { label: "Đánh giá", path: "/assessments", icon: "⭐" }
        ]
    },
    {
        title: "HỆ THỐNG",
        items: [
            { label: "Báo cáo", path: "/reports", icon: "📈" },
            { label: "Cài đặt", path: "/settings", icon: "⚙️" }
        ]
    }
];

const Sidebar = ({ isOpen, onClose }) => {
    return (
        <aside className={`app-sidebar ${isOpen ? "" : "closed"}`}>
            <div className="sidebar-brand">
                <h2>CollabSphere</h2>
                <button onClick={onClose} className="sidebar-close">
                    ×
                </button>
            </div>

            <nav className="sidebar-menu">
                {menuGroups.map(group => (
                    <div className="menu-group" key={group.title}>
                        <p className="menu-title">{group.title}</p>

                        {group.items.map(item => (
                            <NavLink
                                key={item.path}
                                to={item.path}
                                className={({ isActive }) =>
                                    `menu-item ${isActive ? "active" : ""}`
                                }
                                onClick={onClose}
                            >
                                <span>{item.icon}</span>
                                <span>{item.label}</span>
                            </NavLink>
                        ))}
                    </div>
                ))}
            </nav>
        </aside>
    );
};

export default Sidebar;
