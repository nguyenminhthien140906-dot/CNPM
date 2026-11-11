import React, { useState } from 'react';

const DataTable = ({ columns, data, onEdit, onDelete, searchPlaceholder = "Tìm kiếm..." }) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 5;

  // Xử lý Tìm kiếm
  const filteredData = data.filter(item => 
    Object.values(item).some(val => 
      String(val).toLowerCase().includes(searchTerm.toLowerCase())
    )
  );

  // Xử lý Phân trang
  const totalPages = Math.ceil(filteredData.length / itemsPerPage);
  const currentData = filteredData.slice(
    (currentPage - 1) * itemsPerPage, 
    currentPage * itemsPerPage
  );

  return (
    <div className="card" style={{ marginTop: '20px' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '15px' }}>
        <input 
          type="text" 
          placeholder={searchPlaceholder} 
          value={searchTerm}
          onChange={(e) => { setSearchTerm(e.target.value); setCurrentPage(1); }}
          style={styles.searchInput}
        />
        {/* Nơi gắn các Filter Dropdown nếu cần */}
      </div>

      <table style={styles.table}>
        <thead>
          <tr>
            {columns.map((col, index) => (
              <th key={index} style={styles.th}>{col.label}</th>
            ))}
            <th style={styles.th}>Hành động</th>
          </tr>
        </thead>
        <tbody>
          {currentData.length > 0 ? currentData.map((row, rowIndex) => (
            <tr key={rowIndex} style={{ borderBottom: '1px solid #e2e8f0' }}>
              {columns.map((col, colIndex) => (
                <td key={colIndex} style={styles.td}>
                  {col.render ? col.render(row[col.key], row) : row[col.key]}
                </td>
              ))}
              <td style={styles.td}>
                {onEdit && <button onClick={() => onEdit(row)} style={styles.editBtn}>Sửa</button>}
                {onDelete && <button onClick={() => onDelete(row)} style={styles.deleteBtn}>Xóa</button>}
              </td>
            </tr>
          )) : (
            <tr><td colSpan={columns.length + 1} style={{ textAlign: 'center', padding: '20px' }}>Không có dữ liệu</td></tr>
          )}
        </tbody>
      </table>

      {/* Phân trang */}
      <div style={styles.pagination}>
        <button disabled={currentPage === 1} onClick={() => setCurrentPage(p => p - 1)} style={styles.pageBtn}>Trước</button>
        <span style={{ fontSize: '14px', color: 'var(--color-text-muted)' }}>
          Trang {currentPage} / {totalPages || 1}
        </span>
        <button disabled={currentPage === totalPages || totalPages === 0} onClick={() => setCurrentPage(p => p + 1)} style={styles.pageBtn}>Sau</button>
      </div>
    </div>
  );
};

const styles = {
  searchInput: { padding: '8px 12px', border: '1px solid #cbd5e1', borderRadius: '6px', width: '300px', outline: 'none' },
  table: { width: '100%', borderCollapse: 'collapse', textAlign: 'left', fontSize: '14px' },
  th: { padding: '12px', borderBottom: '2px solid #e2e8f0', color: 'var(--color-text-muted)' },
  td: { padding: '12px', color: 'var(--color-text-main)' },
  editBtn: { background: 'none', border: 'none', color: 'var(--color-primary-base)', cursor: 'pointer', marginRight: '10px', fontWeight: '500' },
  deleteBtn: { background: 'none', border: 'none', color: 'var(--color-status-danger)', cursor: 'pointer', fontWeight: '500' },
  pagination: { display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginTop: '15px' },
  pageBtn: { padding: '5px 10px', border: '1px solid #cbd5e1', borderRadius: '4px', background: '#fff', cursor: 'pointer' }
};

export default DataTable;
