import React, { useState } from 'react';
import Sidebar from '../../components/Sidebar';

const AccountManagement = () => {
  // Mock data tạm thời để hiển thị UI trong lúc chờ tích hợp Backend API
  const [users] = useState([
    { id: 1, name: 'Nguyễn Duy', email: 'duy@example.com', role: 'Admin', status: 'Active' },
    { id: 2, name: 'Trần Văn A', email: 'vana@example.com', role: 'Head', status: 'Active' },
    { id: 3, name: 'Lê Thị B', email: 'thib@example.com', role: 'Staff', status: 'Inactive' },
  ]);

  return (
    <div style={{ display: 'flex', minHeight: '100vh', backgroundColor: '#f1f5f9' }}>
      {/* Cột 1: Sidebar điều hướng */}
      <Sidebar currentUser={{ name: 'Nguyễn Duy', role: 'admin' }} />

      {/* Cột 2: Nội dung chính thực tế */}
      <div style={{ flex: 1, padding: '30px' }}>
        <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '20px' }}>
          <h2>Quản lý Tài khoản</h2>
          <button style={styles.primaryBtn}>+ Thêm tài khoản mới</button>
        </div>

        {/* Bảng dữ liệu người dùng */}
        <div style={styles.tableContainer}>
          <table style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'left' }}>
            <thead>
              <tr style={{ borderBottom: '2px solid #e2e8f0', color: '#64748b' }}>
                <th style={styles.th}>ID</th>
                <th style={styles.th}>Họ Tên</th>
                <th style={styles.th}>Email</th>
                <th style={styles.th}>Vai trò</th>
                <th style={styles.th}>Trạng thái</th>
                <th style={styles.th}>Hành động</th>
              </tr>
            </thead>
            <tbody>
              {users.map((user) => (
                <tr key={user.id} style={{ borderBottom: '1px solid #e2e8f0' }}>
                  <td style={styles.td}>{user.id}</td>
                  <td style={styles.td}>{user.name}</td>
                  <td style={styles.td}>{user.email}</td>
                  <td style={styles.td}><strong>{user.role}</strong></td>
                  <td style={styles.td}>
                    <span style={{
                      ...styles.statusBadge, 
                      backgroundColor: user.status === 'Active' ? '#dcfce3' : '#fee2e2',
                      color: user.status === 'Active' ? '#166534' : '#991b1b'
                    }}>
                      {user.status}
                    </span>
                  </td>
                  <td style={styles.td}>
                    <button style={styles.actionBtn}>Sửa</button>
                    <button style={{...styles.actionBtn, color: '#ef4444'}}>Khóa</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

const styles = {
  primaryBtn: { backgroundColor: '#3b82f6', color: '#fff', border: 'none', padding: '10px 15px', borderRadius: '6px', cursor: 'pointer', fontWeight: 'bold' },
  tableContainer: { backgroundColor: '#fff', padding: '20px', borderRadius: '8px', boxShadow: '0 1px 3px rgba(0,0,0,0.1)' },
  th: { padding: '12px 10px' },
  td: { padding: '12px 10px', color: '#334155' },
  statusBadge: { padding: '4px 8px', borderRadius: '12px', fontSize: '12px', fontWeight: 'bold' },
  actionBtn: { background: 'none', border: 'none', color: '#3b82f6', cursor: 'pointer', marginRight: '10px', fontWeight: 'bold' }
};

export default AccountManagement;
