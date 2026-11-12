import React, { useState } from 'react';
import DataTable from '../../components/DataTable';

const HeadApproval = () => {
  const [projects] = useState([
    { id: 'PRJ-01', name: 'Hệ thống CollabSphere', group: 'Nhóm 1', status: 'Chờ duyệt' },
    { id: 'PRJ-02', name: 'App Quản lý Bán hàng', group: 'Nhóm 5', status: 'Đã duyệt' },
  ]);

  const columns = [
    { key: 'id', label: 'Mã Dự án' },
    { key: 'name', label: 'Tên Dự án' },
    { key: 'group', label: 'Nhóm thực hiện' },
    { 
      key: 'status', label: 'Trạng thái',
      render: (val) => (
        <span style={{ color: val === 'Chờ duyệt' ? 'var(--color-status-warning)' : 'var(--color-status-success)' }}>
          {val}
        </span>
      )
    }
  ];

  // Head Department thay vì Sửa/Xóa thì sẽ là Duyệt/Từ chối
  const handleApprove = (row) => {
    alert(`Đã phê duyệt dự án: ${row.name}`);
    // Gọi API PATCH /api/projects/{id}/approve
  };

  return (
    <div style={{ padding: '20px' }}>
      <h2>Phê duyệt Dự án (Head Department)</h2>
      <DataTable 
        columns={columns} 
        data={projects} 
        onEdit={handleApprove} // Tái sử dụng nút Edit thành nút Duyệt (Bạn có thể custom lại icon/text trong DataTable)
        searchPlaceholder="Tìm theo tên dự án, mã nhóm..."
      />
    </div>
  );
};

export default HeadApproval;
