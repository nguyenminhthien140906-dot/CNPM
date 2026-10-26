const express = require('express');
const multer = require('multer');
const app = express();

app.use(express.json());

// 1. Cấu hình nơi lưu trữ tệp tin tải lên
const storage = multer.diskStorage({
  destination: (req, file, cb) => cb(null, 'uploads/'),
  filename: (req, file, cb) => cb(null, Date.now() + '-' + file.originalname)
});
const upload = multer({ storage });

// 2. CSDL mô phỏng dạng SQL (Ví dụ cấu trúc bảng 'resources')
/*
CREATE TABLE resources (
  id INT AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(255) NOT NULL,
  file_path VARCHAR(255) NOT NULL,
  scope ENUM('CLASS', 'GROUP') NOT NULL,
  class_id INT NOT NULL,
  group_id INT DEFAULT NULL,
  uploaded_by INT NOT NULL,
  role ENUM('TEACHER', 'STUDENT') NOT NULL
);
*/

// Mảng giả lập CSDL để chạy trực tiếp
let resourcesDB = [];

// 3. API Tải lên tài nguyên (Dành cho cả Giảng viên và Sinh viên)
app.post('/api/resources/upload', upload.single('file'), (req, res) => {
  const { title, scope, class_id, group_id, user_id, role } = req.body;

  if (!req.file) {
    return res.status(400).json({ error: 'Vui lòng chọn tệp tin để tải lên!' });
  }

  const newResource = {
    id: resourcesDB.length + 1,
    title: title || req.file.originalname,
    file_path: req.file.path,
    scope, // 'CLASS' hoặc 'GROUP'
    class_id: Number(class_id),
    group_id: group_id ? Number(group_id) : null,
    uploaded_by: Number(user_id),
    role // 'TEACHER' hoặc 'STUDENT'
  };

  resourcesDB.push(newResource);
  res.status(201).json({ message: 'Tải tài nguyên thành công!', data: newResource });
});

// 4. API Xem danh sách tài nguyên (Lọc theo Lớp hoặc Nhóm)
app.get('/api/resources', (req, res) => {
  const { class_id, group_id, scope } = req.query;

  const results = resourcesDB.filter(item => {
    if (scope === 'CLASS') {
      return item.class_id === Number(class_id) && item.scope === 'CLASS';
    }
    if (scope === 'GROUP') {
      return item.group_id === Number(group_id) && item.scope === 'GROUP';
    }
    return false;
  });

  res.json(results);
});

// 5. API Xóa tài nguyên (Giảng viên quản lý toàn bộ / Sinh viên xóa file của mình)
app.delete('/api/resources/:id', (req, res) => {
  const resourceId = Number(req.params.id);
  const { user_id, role } = req.body;

  const index = resourcesDB.findIndex(item => item.id === resourceId);
  if (index === -1) {
    return res.status(404).json({ error: 'Không tìm thấy tài nguyên!' });
  }

  const resource = resourcesDB[index];

  // Kiểm tra quyền xóa: Giảng viên được xóa hết, Sinh viên chỉ xóa tệp do mình đăng
  if (role !== 'TEACHER' && resource.uploaded_by !== Number(user_id)) {
    return res.status(403).json({ error: 'Bạn không có quyền xóa tài nguyên này!' });
  }

  resourcesDB.splice(index, 1);
  res.json({ message: 'Đã xóa tài nguyên thành công!' });
});

app.listen(3000, () => console.log('Server đang chạy tại port 3000'));
