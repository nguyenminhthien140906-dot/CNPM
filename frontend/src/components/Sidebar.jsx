import React from 'react';

// Cập nhật danh sách chức năng theo yêu cầu quản lý môn học, đề cương, lớp học, tài khoản và phê duyệt dự án.
const menuConfig = {
  admin: [
    { path: '/admin/dashboard', label: 'Dashboard Admin' },
    { path: '/admin/accounts', label: 'Quản lý Tài khoản' },
    { path: '/admin/subjects', label: 'Quản lý Môn học' },
    { path: '/admin/classes', label: 'Quản lý Lớp học' },
  ],
  head: [
    { path: '/head/dashboard', label: 'Dashboard Head' },
    { path: '/head/syllabuses', label: 'Quản lý Đề cương' },
    { path: '/head/classes', label: 'Phân công Lớp học' },
    { path: '/head/approvals', label: 'Phê duyệt Dự án' },
  ],
  staff: [
    { path: '/staff/dashboard', label: 'Dashboard Staff' },
    { path: '/staff/classes', label: 'Lớp học của tôi' },
    { path: '/staff/syllabuses', label: 'Biên soạn Đề cương' },
    { path: '/staff/projects', label: 'Đề xuất Dự án' },
  ]
};

const Sidebar = ({ currentUser }) => {
  const role = currentUser?.role || 'admin'; // Giả lập role admin để test UI
  const activeMenu = menuConfig[role] || [];

  return (
    <div style={styles.sidebar}>
      <div style={styles.logoContainer}>
        <h2 style={styles.logoText}>CollabSphere</h2>
      </div>
      
      <div style={styles.userInfo}>
        <div style={styles.userName}>{currentUser?.name || 'Nguyễn Duy'}</div>
        <span style={styles.roleBadge}>{role.toUpperCase()}</span>
      </div>

      <nav style={styles.nav}>
        <ul style={styles.menuList}>
          {activeMenu.map((item, index) => (
            <li key={index} style={styles.menuItem}>
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

const styles = {
  sidebar: { width: '260px', height: '100vh', backgroundColor: '#1e293b', color: '#f8fafc', display: 'flex', flexDirection: 'column' },
  logoContainer: { padding: '20px', borderBottom: '1px solid #334155', textAlign: 'center' },
  logoText: { margin: 0, fontSize: '24px', fontWeight: 'bold', color: '#38bdf8' },
  userInfo: { padding: '15px 20px', backgroundColor: '#0f172a', display: 'flex', flexDirection: 'column', gap: '5px' },
  userName: { fontWeight: '600', fontSize: '15px' },
  roleBadge: { backgroundColor: '#ef4444', padding: '2px 8px', borderRadius: '12px', fontSize: '11px', fontWeight: 'bold' },
  nav: { flex: 1, overflowY: 'auto' },
  menuList: { listStyleType: 'none', padding: 0, margin: 0 },
  menuItem: { borderBottom: '1px solid #334155' },
  link: { color: '#cbd5e1', textDecoration: 'none', display: 'block', padding: '15px 20px', fontSize: '14px' }
};

export default Sidebar;
