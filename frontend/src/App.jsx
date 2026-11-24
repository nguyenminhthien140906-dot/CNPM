import React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import { useAuth } from './contexts/AuthContext';
import Layout from './components/Layout.jsx';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import { Subjects, Syllabi, Classes, Users } from './pages/Academic';
import { Projects, Teams, Tasks, Milestones } from './pages/ProjectWorkspace';
import { Chat, Meetings, Resources, Workspace } from './pages/Collaboration';
import { Evaluations, AI, Notifications, Reports } from './pages/Insights';

export default function App() {
  const { user } = useAuth();
  return <Routes>
    <Route path="/login" element={user ? <Navigate to="/" replace/> : <Login/>}/>
    <Route path="/*" element={user ? <Layout><Routes>
      <Route index element={<Dashboard/>}/>
      <Route path="subjects" element={<Subjects/>}/>
      <Route path="syllabi" element={<Syllabi/>}/>
      <Route path="classes" element={<Classes/>}/>
      <Route path="users" element={<Users/>}/>
      <Route path="projects" element={<Projects/>}/>
      <Route path="teams" element={<Teams/>}/>
      <Route path="tasks" element={<Tasks/>}/>
      <Route path="milestones" element={<Milestones/>}/>
      <Route path="chat" element={<Chat/>}/>
      <Route path="meetings" element={<Meetings/>}/>
      <Route path="resources" element={<Resources/>}/>
      <Route path="workspace" element={<Workspace/>}/>
      <Route path="evaluations" element={<Evaluations/>}/>
      <Route path="ai" element={<AI/>}/>
      <Route path="notifications" element={<Notifications/>}/>
      <Route path="reports" element={<Reports/>}/>
      <Route path="*" element={<Navigate to="/" replace/>}/>
    </Routes></Layout> : <Navigate to="/login" replace/>}/>
  </Routes>;
}

