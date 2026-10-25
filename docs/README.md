# CollabSphere (COSRE) — Tài liệu chức năng

## 1. Mục đích

Thư mục `docs/` mô tả các chức năng chính của CollabSphere theo mục **6.1 — Major Features**. Tài liệu được chia thành FE1–FE12 để nhóm thống nhất yêu cầu trước khi triển khai frontend, backend và cơ sở dữ liệu.

## 2. Danh sách chức năng

| Mã | Chức năng | Tài liệu |
|---|---|---|
| FE1 | Quản lý tài khoản và phân quyền | [`FE01-user-account-management.md`](FE01-user-account-management.md) |
| FE2 | Quản lý môn học và đề cương | [`FE02-subject-syllabus-management.md`](FE02-subject-syllabus-management.md) |
| FE3 | Quản lý lớp học | [`FE03-class-management.md`](FE03-class-management.md) |
| FE4 | Quản lý dự án | [`FE04-project-management.md`](FE04-project-management.md) |
| FE5 | Quản lý nhóm và không gian làm việc | [`FE05-team-workspace-management.md`](FE05-team-workspace-management.md) |
| FE6 | Quản lý Milestone và Checkpoint | [`FE06-milestone-checkpoint-management.md`](FE06-milestone-checkpoint-management.md) |
| FE7 | Giao tiếp và họp trực tuyến | [`FE07-communication-meetings.md`](FE07-communication-meetings.md) |
| FE8 | Quản lý tài nguyên | [`FE08-resource-management.md`](FE08-resource-management.md) |
| FE9 | Công cụ cộng tác thời gian thực | [`FE09-realtime-collaboration-tools.md`](FE09-realtime-collaboration-tools.md) |
| FE10 | Đánh giá và phản hồi | [`FE10-assessment-feedback.md`](FE10-assessment-feedback.md) |
| FE11 | Hỗ trợ bằng trí tuệ nhân tạo (AI) | [`FE11-ai-assistance.md`](FE11-ai-assistance.md) |
| FE12 | Quản lý thông báo | [`FE12-notification-management.md`](FE12-notification-management.md) |

## 3. Cách đọc tài liệu

Mỗi tài liệu chức năng gồm mục tiêu, vai trò, luồng nghiệp vụ, quy tắc chính, API dự kiến, ví dụ JSON và sơ đồ Mermaid. Các endpoint và payload trong tài liệu là **đề xuất hợp đồng API để nhóm thống nhất**, không khẳng định rằng chúng đã được cài đặt trong source code.

## 4. Hiển thị và sử dụng trong VS Code/GitHub

- Mở file `.md` trong VS Code và dùng **Markdown: Open Preview** (`Ctrl+Shift+V`) để xem định dạng.
- Các khối `mermaid` hiển thị sơ đồ trong các nền tảng hỗ trợ Mermaid (GitHub hỗ trợ Mermaid trong Markdown).
- Các khối `json`, `http` và `text` là ví dụ dữ liệu/hợp đồng để tham khảo khi viết code; Markdown tự nó không chạy backend hay frontend.
- Trước khi code tích hợp, nhóm cần thống nhất tên endpoint, DTO, trạng thái và quyền truy cập.

## 5. Phạm vi vai trò

- **Admin:** xem tài khoản, vô hiệu hóa tài khoản, nhận báo cáo hệ thống.
- **Staff:** quản lý môn học, syllabus, lớp và tài khoản Lecturer/Student; phân công người dùng vào lớp.
- **Head Department:** xem project, duyệt/từ chối, cập nhật project đã duyệt và phân project cho lớp.
- **Lecturer:** quản lý project, lớp/team được giao, milestone, checkpoint, tài nguyên, đánh giá, giao tiếp và công cụ cộng tác.
- **Student:** tham gia team, làm task/checkpoint, quản lý tài nguyên, cộng tác, chat/họp và đánh giá đồng cấp theo quyền.

## 6. Nguyên tắc

- Đọc tài liệu chức năng trước khi triển khai.
- Không tự ý đổi tên entity/API sau khi nhóm đã thống nhất hợp đồng.
- Không đưa mật khẩu, API key hoặc thông tin bí mật thật vào tài liệu hay Git.
- Nếu một yêu cầu chưa được nêu trong đề tài, cần thống nhất với nhóm/giảng viên trước khi coi là yêu cầu chính thức.
