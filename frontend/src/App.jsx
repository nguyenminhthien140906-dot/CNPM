import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";

import { useAuth } from "./contexts/AuthContext";
import Layout from "./components/Layout";

import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import { Subjects, Syllabi, Classes, Users } from "./pages/Academic";
import { Chat, Meetings, Resources, Workspace } from "./pages/Collaboration";
import { Evaluations, AI, Notifications, Reports } from "./pages/Insights";
import { Projects, Teams, Tasks, Milestones } from "./pages/ProjectWorkspace";

export default function App() {
    const { user } = useAuth();

    if (!user) {
        return (
            <Routes>
                <Route path="/login" element={<Login />} />
                <Route path="*" element={<Navigate to="/login" replace />} />
            </Routes>
        );
    }

    return (
        <Layout>
            <Routes>
                <Route path="/" element={<Dashboard />} />
                <Route path="/projects" element={<Projects />} />
                <Route path="/teams" element={<Teams />} />
                <Route path="/tasks" element={<Tasks />} />
                <Route path="/milestones" element={<Milestones />} />
                <Route path="/chat" element={<Chat />} />
                <Route path="/meetings" element={<Meetings />} />
                <Route path="/resources" element={<Resources />} />
                <Route path="/workspace" element={<Workspace />} />
                <Route path="/ai" element={<AI />} />
                <Route path="/subjects" element={<Subjects />} />
                <Route path="/syllabi" element={<Syllabi />} />
                <Route path="/classes" element={<Classes />} />
                <Route path="/evaluations" element={<Evaluations />} />
                <Route path="/reports" element={<Reports />} />
                <Route path="/users" element={<Users />} />
                <Route path="/notifications" element={<Notifications />} />
                <Route path="*" element={<Navigate to="/" replace />} />
            </Routes>
        </Layout>
    );
}

