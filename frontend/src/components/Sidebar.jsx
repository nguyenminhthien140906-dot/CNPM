import React from 'react';

// 1. Cấu hình danh sách menu theo từng vai trò (Role-based Menu Configuration)
const menuConfig = {
  admin: [
    { path: '/admin/dashboard', label: 'Dashboard Tổng' },
    { path: '/admin/users', label: 'Quản lý Người dùng' },
    { path: '/admin/departments', label: 'Quản lý Phòng ban' },
    { path: '/admin/settings', label: 'Cài đặt Hệ thống' },
  ],
  head: [
    { path: '/head/dashboard', label: 'Tổng quan Phòng ban' },
    { path: '/head/staff', label: 'Quản lý Nhân sự' },
    { path: '/head/tasks', label: 'Quản lý Công việc' },
    { path: '/head/approvals', label: 'Phê duyệt Yêu cầu' },
  ],
  staff: [
    { path: '/staff/dashboard', label: 'Dashboard Cá nhân' },
    { path: '/staff/my-tasks', label: 'Bảng Công việc' },
    { path: '/staff/requests', label: 'Đề xuất / Yêu cầu' },
  ]
};

const Sidebar = ({ currentUser }) => {
  // Lấy vai trò của người dùng (giả sử mặc định là 'staff' nếu chưa có dữ liệu)
  const role = currentUser?.role || 'staff'; 
  
  // Lấy danh sách menu tương ứng với vai trò
  const activeMenu = menuConfig[role] || [];

  return (
    <div style={styles.sidebar}>
      {/* Phần Logo của dự án */}
      <div style={styles.logoContainer}>
        <h2 style={styles.logoText}>CollabSphere</h2>
      </div>
      
      {/* Thông tin người dùng đăng nhập */}
      <div style={styles.userInfo}>
        <div style={styles.userName}>{currentUser?.name || 'Người dùng'}</div>
        <span style={styles.roleBadge}>{role.toUpperCase()}</span>
      </div>

      {/* Render danh sách menu động */}
      <nav style={styles.nav}>
        <ul style={styles.menuList}>
          {activeMenu.map((item, index) => (
            <li key={index} style={styles.menuItem}>
              {/* Trong thực tế, bạn nên dùng <Link to={item.path}> của react-router-dom */}
              <a href={item.path} style={styles.link}>
                {item.label}
              </a>
            </li>
          ))}
        </ul>
      </nav>
    </div>
  );
};

// 2. CSS in JS (Styles tĩnh cơ bản để bạn dễ hình dung UI)
const styles = {
  sidebar: {
    width: '260px',
    height: '100vh',
    backgroundColor: '#1e293b', // Màu nền tối hiện đại
    color: '#f8fafc',
    display: 'flex',
    flexDirection: 'column',
    boxShadow: '2px 0 5px rgba(0,0,0,0.1)',
  },
  logoContainer: {
    padding: '20px',
    borderBottom: '1px solid #334155',
    textAlign: 'center',
  },
  logoText: {
    margin: 0,
    fontSize: '24px',
    fontWeight: 'bold',
    color: '#38bdf8', // Điểm nhấn màu xanh
  },
  userInfo: {
    padding: '15px 20px',
    backgroundColor: '#0f172a',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'flex-start',
    gap: '5px',
  },
  userName: {
    fontWeight: '600',
    fontSize: '15px',
  },
  roleBadge: {
    backgroundColor: '#ef4444', // Đỏ cho nổi bật vai trò
    padding: '2px 8px',
    borderRadius: '12px',
    fontSize: '11px',
    fontWeight: 'bold',
    letterSpacing: '0.5px',
  },
  nav: {
    flex: 1,
    overflowY: 'auto',
  },
  menuList: {
    listStyleType: 'none',
    padding: 0,
    margin: 0,
  },
  menuItem: {
    borderBottom: '1px solid #334155',
  },
  link: {
    color: '#cbd5e1',
    textDecoration: 'none',
    display: 'block',
    padding: '15px 20px',
    fontSize: '14px',
    transition: 'background 0.2s, color 0.2s',
  }
};

export default Sidebar;
