import React, { useState } from 'react';
import DataTable from '../../components/DataTable';

const StaffClasses = () => {
  const [classes] = useState([
    { id: 'SE114', name: 'Nhập môn CNPM', syllabus: 'Đã duyệt', lecturer: 'Lê Văn C' },
    { id: 'IT007', name: 'Cơ sở dữ liệu', syllabus: 'Đang soạn thảo', lecturer: 'Nguyễn Thị D' },
  ]);

  const columns = [
    { key: 'id', label: 'Mã Môn' },
    { key: 'name', label: 'Tên Môn Học' },
    { key: 'lecturer', label: 'Giảng viên phụ trách' },
    { 
      key: 'syllabus', label: 'Trạng thái Syllabus',
      render: (val) => <span className={val === 'Đã duyệt' ? 'badge-success' : 'badge-warning'}>{val}</span>
    }
  ];

  return (
    <div style={{ padding: '20px' }}>
      <h2>Quản lý Môn học & Đề cương (Staff)</h2>
      <div className="card" style={{ marginBottom: '20px', display: 'flex', gap: '20px' }}>
        <div><strong>Tổng số môn:</strong> 120</div>
        <div><strong>Đề cương cần duyệt:</strong> 5</div>
      </div>
      
      <DataTable 
        columns={columns} 
        data={classes} 
        onEdit={(row) => console.log("Cập nhật môn học", row)}
        searchPlaceholder="Tìm kiếm môn học..."
      />
    </div>
  );
};

export default StaffClasses;
