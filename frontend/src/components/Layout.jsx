import React, { useState } from 'react';
import { NavLink, useLocation, useNavigate } from 'react-router-dom';
import { useAuth, ROLE_LABELS } from '../contexts/AuthContext';
import { useNotifications } from '../contexts/NotificationContext';
import { Avatar, Badge, Icon, timeLabel } from './ui';

const GROUPS = [
  { label: 'KHÔNG GIAN LÀM VIỆC', links: [
    ['/', 'dashboard', 'Tổng quan'], ['/projects', 'folder', 'Dự án PBL'], ['/teams', 'users', 'Nhóm học tập'], ['/tasks', 'tasks', 'Bảng công việc'], ['/milestones', 'target', 'Milestone & Checkpoint'],
  ] },
  { label: 'HỌC TẬP & CỘNG TÁC', links: [
    ['/chat', 'message', 'Chat nhóm'], ['/meetings', 'video', 'Cuộc họp'], ['/resources', 'resources', 'Tài nguyên'], ['/workspace', 'pen', 'Không gian sáng tạo'], ['/ai', 'sparkles', 'Trợ lý AI'],
  ] },
  { label: 'QUẢN LÝ & THEO DÕI', links: [
    ['/subjects', 'book', 'Môn học'], ['/syllabi', 'file', 'Đề cương'], ['/classes', 'classes', 'Lớp học'], ['/evaluations', 'star', 'Đánh giá'], ['/reports', 'chart', 'Báo cáo tiến độ'], ['/users', 'shield', 'Tài khoản'], ['/notifications', 'bell', 'Thông báo'],
  ] },
];
const titles = Object.fromEntries(GROUPS.flatMap((g) => g.links.map(([path,,label]) => [path,label])));

export default function Layout({ children }) {
  const { user, isDemo, mode, logout, switchDemoRole } = useAuth();
  const { unread, notifications, markRead, connection, toast, setToast } = useNotifications();
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [showNotifications, setShowNotifications] = useState(false);
  const [search, setSearch] = useState('');
  const navigate = useNavigate(); const location = useLocation();
  const title = titles[location.pathname] || 'CollabSphere';
  return <div className="app-shell">
    {sidebarOpen && <button className="sidebar-scrim" onClick={() => setSidebarOpen(false)} aria-label="Đóng menu"/>}
    <aside className={`sidebar ${sidebarOpen ? 'open' : ''}`}>
      <div className="brand"><div className="brand-mark"><Icon name="layers" size={25} strokeWidth={2.4}/></div><div><strong>Collab<span>Sphere</span></strong><small>PROJECT-BASED LEARNING</small></div></div>
      <div className="sidebar-context"><span className="context-dot"/><div><strong>Workspace PBL</strong><small>Năm học 2026 – 2027</small></div><Icon name="chevron" size={16}/></div>
      <div className="sidebar-scroll">{GROUPS.map((g) => <nav className="nav-group" key={g.label} aria-label={g.label}><div className="nav-label">{g.label}</div>{g.links.filter(([, , label]) => label.toLowerCase().includes(search.toLowerCase())).map(([path, icon, label]) => <NavLink key={path} to={path} end={path === '/'} onClick={() => setSidebarOpen(false)} className={({ isActive }) => `nav-link ${isActive ? 'active' : ''}`}><Icon name={icon} size={18}/><span>{label}</span>{path === '/notifications' && unread > 0 && <span className="nav-count">{unread}</span>}</NavLink>)}</nav>)}</div>
      <div className="sidebar-bottom"><div className="help-card"><div className="help-icon"><Icon name="rocket" size={20}/></div><strong>Học tập hiệu quả hơn</strong><p>Quản lý dự án, cộng tác và phát triển cùng đội nhóm.</p><button onClick={() => navigate('/ai')}>Khám phá AI <Icon name="arrow" size={15}/></button></div><div className="sidebar-foot">COLLABSPHERE · V1.0</div></div>
    </aside>
    <div className="main-shell"><header className="topbar"><div className="topbar-left"><button className="icon-btn mobile-menu" onClick={() => setSidebarOpen(true)} aria-label="Mở menu"><Icon name="menu"/></button><div className="breadcrumbs"><span>Không gian học tập</span><span className="breadcrumb-separator">/</span><strong>{title}</strong></div></div><div className="topbar-right"><div className="topbar-search"><Icon name="search" size={18}/><input placeholder="Tìm nhanh chức năng..." value={search} onChange={(e) => setSearch(e.target.value)} onKeyDown={(e) => { if (e.key === 'Enter') { const match = GROUPS.flatMap((g) => g.links).find((x) => x[2].toLowerCase().includes(search.toLowerCase())); if (match) { navigate(match[0]); setSearch(''); } } }}/><kbd>↵</kbd></div><div className="topbar-divider"/><button className="icon-btn notification-trigger" aria-label="Thông báo" onClick={() => setShowNotifications(!showNotifications)}><Icon name="bell" size={20}/>{unread > 0 && <span className="unread-dot"/>}</button><div className="topbar-divider"/><div className="profile"><Avatar name={user?.fullName || user?.username}/><div className="profile-text"><strong>{user?.fullName || user?.username}</strong><span>{ROLE_LABELS[user?.roleCode] || user?.roleCode}</span></div><Icon name="chevron" size={14}/></div></div></header>
      <main className="main-content">{isDemo && <div className="demo-ribbon"><span><Icon name="info" size={15}/> Bạn đang dùng <strong>bản trải nghiệm với dữ liệu mẫu</strong>. Thay đổi được lưu trên trình duyệt.</span><label>Vai trò: <select value={user?.roleCode || 'STUDENT'} onChange={(e) => switchDemoRole(e.target.value)}>{Object.entries(ROLE_LABELS).map(([key,label]) => <option key={key} value={key}>{label}</option>)}</select></label></div>}{children}</main>
      <footer className="app-footer"><span>© 2026 CollabSphere · Nền tảng học tập theo dự án</span><div><span className={`connection-dot ${connection}`}/>{isDemo ? 'Chế độ mẫu' : connection === 'online' ? 'SignalR đã kết nối' : 'Realtime chưa kết nối'}<button onClick={logout}><Icon name="logout" size={15}/> Đăng xuất</button></div></footer>
    </div>
    {showNotifications && <div className="notification-popover"><div className="popover-head"><strong>Thông báo</strong><button onClick={() => { setShowNotifications(false); navigate('/notifications'); }}>Xem tất cả</button></div>{notifications.slice(0, 5).map((n) => <button key={n.notificationId ?? n.NotificationId} className={`popover-notice ${!(n.isRead ?? n.IsRead) ? 'unread' : ''}`} onClick={async () => { try { await markRead(n.notificationId ?? n.NotificationId); } catch {} setShowNotifications(false); navigate('/notifications'); }}><span className="popover-notice-icon"><Icon name="bell" size={17}/></span><span><strong>{n.title ?? n.Title}</strong><small>{n.message ?? n.Message}</small><em>{timeLabel(n.createdAt ?? n.CreatedAt)}</em></span></button>)}{notifications.length === 0 && <p className="popover-empty">Chưa có thông báo.</p>}</div>}
    {toast && <div className="toast"><Icon name="bell" size={19}/><span>{toast}</span><button onClick={() => setToast(null)} aria-label="Đóng"><Icon name="close" size={16}/></button></div>}
  </div>;
}
