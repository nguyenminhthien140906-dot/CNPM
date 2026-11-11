import React, { useState, useEffect } from 'react';
import DataTable from '../../components/DataTable';
import Modal from '../../components/Modal';

const AdminUsers = () => {
  const [users, setUsers] = useState([
    { id: 'U01', name: 'Nguyễn Văn A', role: 'Staff', status: 'Hoạt động' },
    { id: 'U02', name: 'Trần Thị B', role: 'Head', status: 'Bị khóa' },
  ]);
  
  const [isModalOpen, setModalOpen] = useState(false);
  const [formData, setFormData] = useState({ name: '', role: 'Staff' });

  // Nơi gọi API của Thiện & Doãn Duy
  useEffect(() => {
    // fetch('/api/users').then(res => setUsers(res.data));
  }, []);

  const columns = [
    { key: 'id', label: 'ID' },
    { key: 'name', label: 'Họ Tên' },
    { key: 'role', label: 'Vai trò' },
    { 
      key: 'status', label: 'Trạng thái',
      render: (val) => (
        <span style={{ color: val === 'Hoạt động' ? 'var(--color-status-success)' : 'var(--color-status-danger)', fontWeight: 'bold' }}>
          {val}
        </span>
      )
    }
  ];

  const handleSave = () => {
    // Gọi API POST /api/users
    console.log("Saving...", formData);
    setModalOpen(false);
  };

  const handleDelete = (row) => {
    if(window.confirm(`Bạn có chắc muốn vô hiệu hóa tài khoản ${row.name}?`)) {
      // Gọi API PATCH /api/users/{id}/status
      console.log("Deactivating...", row.id);
    }
  };

  return (
    <div style={{ padding: '20px' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <h2>Quản lý Tài khoản (Admin)</h2>
        <button className="btn btn-primary" onClick={() => setModalOpen(true)}>+ Thêm Tài khoản</button>
      </div>

      <DataTable 
        columns={columns} 
        data={users} 
        onEdit={(row) => { setFormData(row); setModalOpen(true); }}
        onDelete={handleDelete}
        searchPlaceholder="Tìm theo tên, ID, vai trò..."
      />

      <Modal isOpen={isModalOpen} title="Thông tin Tài khoản" onClose={() => setModalOpen(false)} onSubmit={handleSave}>
        <div style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
          <input type="text" placeholder="Họ và tên" value={formData.name} onChange={e => setFormData({...formData, name: e.target.value})} style={{ padding: '8px', borderRadius: '4px', border: '1px solid #ccc' }} />
          <select value={formData.role} onChange={e => setFormData({...formData, role: e.target.value})} style={{ padding: '8px', borderRadius: '4px', border: '1px solid #ccc' }}>
            <option value="Staff">Giáo vụ (Staff)</option>
            <option value="Head">Trưởng khoa (Head)</option>
            <option value="Lecturer">Giảng viên (Lecturer)</option>
          </select>
        </div>
      </Modal>
    </div>
  );
};

export default AdminUsers;
