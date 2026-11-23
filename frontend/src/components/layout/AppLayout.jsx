
import React, { useState } from "react";
import Sidebar from "./Sidebar";
import Header from "./Header";
import "./Layout.css";

const AppLayout = ({ children }) => {
    const [sidebarOpen, setSidebarOpen] = useState(true);

    return (
        <div className="app-layout">
            <Sidebar
                isOpen={sidebarOpen}
                onClose={() => setSidebarOpen(false)}
            />

            <div className={`app-main ${sidebarOpen ? "" : "expanded"}`}>
                <Header
                    onMenuClick={() => setSidebarOpen(!sidebarOpen)}
                />

                <main className="app-content">
                    {children}
                </main>
            </div>
        </div>
    );
};

export default AppLayout;
