// Explicitly fictional sample data for offline UI demonstration; never represented as database records.
export const demoSeed = {
  subjects: [
    { subjectId: 1, subjectCode: 'SE301', subjectName: 'Công nghệ phần mềm', description: 'Phát triển phần mềm theo nhóm' },
    { subjectId: 2, subjectCode: 'AI201', subjectName: 'Trí tuệ nhân tạo', description: 'Ứng dụng AI trong thực tế' },
    { subjectId: 3, subjectCode: 'DB202', subjectName: 'Cơ sở dữ liệu', description: 'Thiết kế và quản trị dữ liệu' },
    { subjectId: 4, subjectCode: 'UX101', subjectName: 'Thiết kế trải nghiệm người dùng', description: 'Tư duy thiết kế và nguyên mẫu' },
  ],
  syllabi: [
    { syllabusId: 1, subjectId: 1, subjectCode: 'SE301', version: '2026.1', title: 'Đề cương Công nghệ phần mềm', content: 'Yêu cầu: xây dựng ứng dụng web theo phương pháp PBL.' },
    { syllabusId: 2, subjectId: 2, subjectCode: 'AI201', version: '2026.1', title: 'Đề cương Trí tuệ nhân tạo', content: 'Phân tích, huấn luyện và triển khai mô hình.' },
  ],
  classes: [
    { classId: 1, classCode: 'SE301-01', className: 'Công nghệ phần mềm – Nhóm 01', subjectId: 1, subjectName: 'Công nghệ phần mềm', semester: 'HK1', academicYear: '2026-2027', capacity: 45, studentCount: 38, lecturerCount: 1, status: 'OPEN' },
    { classId: 2, classCode: 'AI201-02', className: 'Trí tuệ nhân tạo – Nhóm 02', subjectId: 2, subjectName: 'Trí tuệ nhân tạo', semester: 'HK1', academicYear: '2026-2027', capacity: 40, studentCount: 34, lecturerCount: 1, status: 'OPEN' },
    { classId: 3, classCode: 'DB202-01', className: 'Cơ sở dữ liệu – Nhóm 01', subjectId: 3, subjectName: 'Cơ sở dữ liệu', semester: 'HK1', academicYear: '2026-2027', capacity: 42, studentCount: 42, lecturerCount: 1, status: 'CLOSED' },
  ],
  users: [
    { userId: 1, fullName: 'Nguyễn Minh Anh', username: 'lecturer01', email: 'lecturer01@example.edu.vn', roleCode: 'LECTURER', isActive: true },
    { userId: 2, fullName: 'Trần Gia Bảo', username: 'student01', email: 'student01@example.edu.vn', roleCode: 'STUDENT', isActive: true },
    { userId: 3, fullName: 'Lê Thanh Hà', username: 'student02', email: 'student02@example.edu.vn', roleCode: 'STUDENT', isActive: true },
    { userId: 4, fullName: 'Phạm Quang Huy', username: 'staff01', email: 'staff01@example.edu.vn', roleCode: 'STAFF', isActive: true },
    { userId: 5, fullName: 'Đỗ Hoàng Phúc', username: 'head01', email: 'head01@example.edu.vn', roleCode: 'HEAD', isActive: true },
    { userId: 6, fullName: 'Vũ Ngọc Lan', username: 'admin', email: 'admin@example.edu.vn', roleCode: 'ADMIN', isActive: true },
  ],
  projects: [
    { id: 1, name: 'CollabSphere – Nền tảng học tập PBL', code: 'PBL-001', classId: 1, subject: 'Công nghệ phần mềm', description: 'Xây dựng hệ thống quản lý dự án học tập, giao tiếp và AI trong một không gian.', status: 'IN_PROGRESS', progress: 72, deadline: '2026-12-20', owner: 'Nguyễn Minh Anh', teamId: 1 },
    { id: 2, name: 'Trợ lý học tập thông minh', code: 'PBL-002', classId: 2, subject: 'Trí tuệ nhân tạo', description: 'Ứng dụng AI hỗ trợ lên kế hoạch và giải đáp thắc mắc.', status: 'PENDING', progress: 22, deadline: '2027-01-10', owner: 'Nguyễn Minh Anh', teamId: 2 },
    { id: 3, name: 'Thư viện số cho sinh viên', code: 'PBL-003', classId: 1, subject: 'Công nghệ phần mềm', description: 'Nền tảng chia sẻ tài liệu và tìm kiếm tri thức.', status: 'APPROVED', progress: 46, deadline: '2027-01-15', owner: 'Nguyễn Minh Anh', teamId: 3 },
    { id: 4, name: 'Hệ thống phân tích dữ liệu học tập', code: 'PBL-004', classId: 3, subject: 'Cơ sở dữ liệu', description: 'Báo cáo tiến độ và kết quả học tập trực quan.', status: 'DRAFT', progress: 12, deadline: '2027-02-05', owner: 'Nguyễn Minh Anh', teamId: 2 },
  ],
  teams: [
    { id: 1, name: 'Nhóm Innovators', projectId: 1, leader: 'Trần Gia Bảo', members: ['Trần Gia Bảo', 'Lê Thanh Hà', 'Nguyễn Hoàng Nam', 'Phạm Thu Thảo', 'Bùi Minh Khang'], color: '#6258e8' },
    { id: 2, name: 'Nhóm AI Pioneers', projectId: 2, leader: 'Lê Thanh Hà', members: ['Lê Thanh Hà', 'Trần Gia Bảo', 'Hoàng Phúc Long'], color: '#12a58b' },
    { id: 3, name: 'Nhóm Creators', projectId: 3, leader: 'Nguyễn Hoàng Nam', members: ['Nguyễn Hoàng Nam', 'Phạm Thu Thảo', 'Trần Gia Bảo'], color: '#f4a340' },
  ],
  tasks: [
    { id: 1, title: 'Thiết kế cơ sở dữ liệu', projectId: 1, teamId: 1, assignee: 'Trần Gia Bảo', status: 'DONE', priority: 'HIGH', due: '2026-11-15' },
    { id: 2, title: 'Xây dựng API xác thực', projectId: 1, teamId: 1, assignee: 'Lê Thanh Hà', status: 'DONE', priority: 'HIGH', due: '2026-11-18' },
    { id: 3, title: 'Tích hợp AI Chatbot', projectId: 1, teamId: 1, assignee: 'Trần Gia Bảo', status: 'IN_PROGRESS', priority: 'HIGH', due: '2026-11-28' },
    { id: 4, title: 'Thiết kế giao diện Dashboard', projectId: 1, teamId: 1, assignee: 'Phạm Thu Thảo', status: 'IN_PROGRESS', priority: 'MEDIUM', due: '2026-11-30' },
    { id: 5, title: 'Kiểm thử Realtime Notification', projectId: 1, teamId: 1, assignee: 'Bùi Minh Khang', status: 'REVIEW', priority: 'HIGH', due: '2026-12-02' },
    { id: 6, title: 'Viết tài liệu hướng dẫn', projectId: 1, teamId: 1, assignee: 'Nguyễn Hoàng Nam', status: 'TODO', priority: 'LOW', due: '2026-12-10' },
    { id: 7, title: 'Nghiên cứu bộ dữ liệu', projectId: 2, teamId: 2, assignee: 'Lê Thanh Hà', status: 'IN_PROGRESS', priority: 'MEDIUM', due: '2026-12-05' },
    { id: 8, title: 'Thiết kế kiến trúc chatbot', projectId: 2, teamId: 2, assignee: 'Hoàng Phúc Long', status: 'TODO', priority: 'HIGH', due: '2026-12-12' },
  ],
  milestones: [
    { id: 1, projectId: 1, title: 'Phân tích yêu cầu & thiết kế', deadline: '2026-11-18', status: 'DONE', description: 'Hoàn thiện tài liệu SRS, UML và database.' },
    { id: 2, projectId: 1, title: 'Xây dựng chức năng cốt lõi', deadline: '2026-12-02', status: 'IN_PROGRESS', description: 'Backend API, giao diện, xác thực và dự án.' },
    { id: 3, projectId: 1, title: 'Tích hợp Realtime & AI', deadline: '2026-12-12', status: 'TODO', description: 'Chat, meeting, thông báo và AI assistant.' },
    { id: 4, projectId: 1, title: 'Kiểm thử & bàn giao', deadline: '2026-12-20', status: 'TODO', description: 'Kiểm thử tích hợp, tài liệu và demo.' },
  ],
  checkpoints: [
    { id: 1, milestoneId: 1, title: 'Nộp sơ đồ ERD', assignee: 'Trần Gia Bảo', status: 'SUBMITTED', due: '2026-11-18', link: '' },
    { id: 2, milestoneId: 2, title: 'Demo API và giao diện', assignee: 'Lê Thanh Hà', status: 'IN_PROGRESS', due: '2026-12-02', link: '' },
    { id: 3, milestoneId: 3, title: 'Kiểm thử SignalR', assignee: 'Trần Gia Bảo', status: 'TODO', due: '2026-12-12', link: '' },
  ],
  meetings: [
    { meetingId: 1, teamId: 1, title: 'Họp Sprint Planning', description: 'Phân chia công việc sprint tiếp theo', startsAt: '2026-11-25T14:00:00', meetingUrl: 'https://meet.google.com/' },
    { meetingId: 2, teamId: 1, title: 'Review tiến độ tuần', description: 'Báo cáo và xử lý vấn đề', startsAt: '2026-11-28T09:00:00', meetingUrl: 'https://meet.google.com/' },
  ],
  messages: [
    { id: 1, teamId: 1, sender: 'Lê Thanh Hà', content: 'Mình đã cập nhật API và tài liệu tích hợp nhé.', createdAt: '2026-11-23T08:30:00' },
    { id: 2, teamId: 1, sender: 'Trần Gia Bảo', content: 'Tuyệt, mình sẽ kiểm tra realtime và kết nối giao diện.', createdAt: '2026-11-23T08:42:00' },
    { id: 3, teamId: 1, sender: 'Phạm Thu Thảo', content: 'Chiều nay cả nhóm cùng review dashboard nha!', createdAt: '2026-11-23T09:05:00' },
  ],
  resources: [
    { id: 1, name: 'Đề cương dự án CollabSphere.pdf', type: 'PDF', scope: 'Dự án PBL-001', owner: 'Nguyễn Minh Anh', date: '2026-11-20', url: '' },
    { id: 2, name: 'Sơ đồ kiến trúc hệ thống.png', type: 'Hình ảnh', scope: 'Nhóm Innovators', owner: 'Trần Gia Bảo', date: '2026-11-22', url: '' },
    { id: 3, name: 'Kế hoạch Sprint 03.docx', type: 'Word', scope: 'Nhóm Innovators', owner: 'Lê Thanh Hà', date: '2026-11-23', url: '' },
  ],
  reviews: [
    { id: 1, subject: 'Nhóm Innovators', evaluator: 'Nguyễn Minh Anh', score: 9.0, comment: 'Tiến độ tốt, cần hoàn thiện kiểm thử tích hợp.', type: 'Giảng viên', date: '2026-11-22' },
    { id: 2, subject: 'Trần Gia Bảo', evaluator: 'Lê Thanh Hà', score: 9.5, comment: 'Chủ động hỗ trợ các thành viên.', type: 'Đồng cấp', date: '2026-11-21' },
  ],
  notifications: [
    { notificationId: 1, title: 'Có công việc mới được giao', message: 'Bạn được giao nhiệm vụ Tích hợp AI Chatbot.', isRead: false, createdAt: '2026-11-23T09:30:00' },
    { notificationId: 2, title: 'Nhắc lịch họp nhóm', message: 'Sprint Planning diễn ra vào 14:00 ngày 25/11.', isRead: false, createdAt: '2026-11-23T09:00:00' },
    { notificationId: 3, title: 'Đã nhận phản hồi', message: 'Giảng viên đã đánh giá nhóm Innovators.', isRead: true, createdAt: '2026-11-22T14:20:00' },
  ],
  documents: [{ id: 1, title: 'Biên bản họp nhóm', content: '<h2>Biên bản họp nhóm</h2><p>Nội dung và quyết định quan trọng của sprint.</p>' }],
};
