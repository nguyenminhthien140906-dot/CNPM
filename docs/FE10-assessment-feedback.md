import React, { useState } from 'react';

const EvaluationSystem = ({ currentUser }) => {
  // Giả lập role: 'lecturer' (Giảng viên) hoặc 'student' (Sinh viên)
  // Trong thực tế, bạn lấy từ Context/Redux
  const role = currentUser?.role || 'lecturer'; 
  
  // State cho Giảng viên
  const [teacherEval, setTeacherEval] = useState({ target: 'group', score: '', feedback: '' });
  
  // State cho Sinh viên (Đánh giá đồng cấp)
  const [peerReviews, setPeerReviews] = useState([
    { id: 1, name: 'Nguyễn Văn A', contribution: 100, comment: '' },
    { id: 2, name: 'Lê Thị B', contribution: 80, comment: '' },
  ]);

  // Xử lý Giảng viên submit
  const handleTeacherSubmit = (e) => {
    e.preventDefault();
    alert(`Đã gửi đánh giá Checkpoint/Milestone!\nĐiểm: ${teacherEval.score}\nNhận xét: ${teacherEval.feedback}`);
  };

  // Xử lý Sinh viên submit đánh giá đồng cấp
  const handlePeerSubmit = (e) => {
    e.preventDefault();
    console.log("Dữ liệu đánh giá đồng cấp:", peerReviews);
    alert('Đã gửi đánh giá đồng cấp thành công!');
  };

  // Cập nhật state đánh giá đồng cấp
  const handlePeerChange = (id, field, value) => {
    setPeerReviews(peerReviews.map(peer => 
      peer.id === id ? { ...peer, [field]: value } : peer
    ));
  };

  return (
    <div style={styles.container}>
      <h2 style={styles.title}>
        {role === 'lecturer' ? 'Hệ thống Đánh giá & Phản hồi (Giảng viên)' : 'Đánh giá Đồng cấp (Sinh viên)'}
      </h2>

      {/* ================= GIAO DIỆN GIẢNG VIÊN ================= */}
      {role === 'lecturer' && (
        <div style={styles.card}>
          <h3 style={styles.cardTitle}>Đánh giá Checkpoint / Milestone</h3>
          <form onSubmit={handleTeacherSubmit} style={styles.form}>
            <div style={styles.formGroup}>
              <label style={styles.label}>Đối tượng đánh giá:</label>
              <select 
                style={styles.input} 
                value={teacherEval.target}
                onChange={(e) => setTeacherEval({...teacherEval, target: e.target.value})}
              >
                <option value="group">Toàn bộ Nhóm 1</option>
                <option value="member_1">Thành viên: Duy Nguyễn</option>
                <option value="member_2">Thành viên: Nguyễn Văn A</option>
              </select>
            </div>

            <div style={styles.formGroup}>
              <label style={styles.label}>Điểm số (0-10):</label>
              <input 
                type="number" min="0" max="10" style={styles.input} required
                value={teacherEval.score}
                onChange={(e) => setTeacherEval({...teacherEval, score: e.target.value})}
              />
            </div>

            <div style={styles.formGroup}>
              <label style={styles.label}>Phản hồi / Nhận xét:</label>
              <textarea 
                rows="4" style={styles.textarea} required
                placeholder="Nhập phản hồi chi tiết cho bài nộp Checkpoint hoặc câu trả lời Milestone..."
                value={teacherEval.feedback}
                onChange={(e) => setTeacherEval({...teacherEval, feedback: e.target.value})}
              ></textarea>
            </div>

            <button type="submit" style={styles.btnPrimary}>Gửi Đánh Giá</button>
          </form>
        </div>
      )}

      {/* ================= GIAO DIỆN SINH VIÊN (PEER REVIEW) ================= */}
      {role === 'student' && (
        <div style={styles.card}>
          <h3 style={styles.cardTitle}>Đánh giá mức độ đóng góp (Peer Review)</h3>
          <p style={{marginBottom: '20px', color: '#64748b', fontSize: '14px'}}>
            Vui lòng đánh giá mức độ hoàn thành công việc của các thành viên trong nhóm.
          </p>
          <form onSubmit={handlePeerSubmit}>
            {peerReviews.map((peer) => (
              <div key={peer.id} style={styles.peerCard}>
                <div style={{fontWeight: 'bold', marginBottom: '10px'}}>{peer.name}</div>
                <div style={styles.formGroup}>
                  <label style={styles.label}>Mức độ đóng góp (%):</label>
                  <input 
                    type="number" min="0" max="100" style={styles.input}
                    value={peer.contribution}
                    onChange={(e) => handlePeerChange(peer.id, 'contribution', e.target.value)}
                  />
                </div>
                <div style={styles.formGroup}>
                  <label style={styles.label}>Nhận xét thái độ / Kỹ năng:</label>
                  <input 
                    type="text" style={styles.input}
                    placeholder="VD: Code tốt, đúng deadline..."
                    value={peer.comment}
                    onChange={(e) => handlePeerChange(peer.id, 'comment', e.target.value)}
                  />
                </div>
              </div>
            ))}
            <button type="submit" style={styles.btnPrimary}>Xác nhận Đánh Giá</button>
          </form>
        </div>
      )}
    </div>
  );
};

// CSS in JS
const styles = {
  container: { padding: '30px', backgroundColor: '#f1f5f9', minHeight: '100vh', fontFamily: 'system-ui, sans-serif' },
  title: { color: '#1e293b', marginBottom: '20px', fontSize: '24px' },
  card: { backgroundColor: '#fff', padding: '24px', borderRadius: '8px', boxShadow: '0 1px 3px rgba(0,0,0,0.1)', maxWidth: '700px' },
  cardTitle: { borderBottom: '2px solid #e2e8f0', paddingBottom: '10px', marginBottom: '20px', color: '#334155' },
  form: { display: 'flex', flexDirection: 'column', gap: '15px' },
  formGroup: { display: 'flex', flexDirection: 'column', gap: '5px' },
  label: { fontWeight: '600', fontSize: '14px', color: '#475569' },
  input: { padding: '10px', borderRadius: '6px', border: '1px solid #cbd5e1', fontSize: '14px', outline: 'none' },
  textarea: { padding: '10px', borderRadius: '6px', border: '1px solid #cbd5e1', fontSize: '14px', outline: 'none', resize: 'vertical' },
  btnPrimary: { backgroundColor: '#3b82f6', color: '#fff', padding: '12px 20px', border: 'none', borderRadius: '6px', cursor: 'pointer', fontWeight: 'bold', marginTop: '10px' },
  peerCard: { backgroundColor: '#f8fafc', padding: '15px', borderRadius: '6px', border: '1px solid #e2e8f0', marginBottom: '15px' }
};

export default EvaluationSystem;
