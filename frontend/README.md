# CollabSphere – Frontend

## 1. Giới thiệu

**CollabSphere** là hệ thống hỗ trợ học tập theo phương pháp Project-Based Learning (PBL). Frontend là phần giao diện người dùng, cho phép giảng viên, sinh viên và các đối tượng quản lý tương tác với hệ thống thông qua trình duyệt web.

Frontend được xây dựng với mục tiêu cung cấp một môi trường học tập và làm việc nhóm tập trung, hỗ trợ quản lý môn học, lớp học, dự án, nhóm, nhiệm vụ và đánh giá kết quả học tập.

Giao diện Frontend kết nối với Backend thông qua RESTful API và hỗ trợ giao tiếp thời gian thực bằng SignalR.

## 2. Công nghệ sử dụng

| Công nghệ | Phiên bản | Mục đích |
|---|---|---|
| React | 18.3.1 | Xây dựng giao diện người dùng |
| React DOM | 18.3.1 | Hiển thị giao diện trên trình duyệt |
| React Router DOM | 6.28.0 | Quản lý điều hướng và định tuyến |
| Vite | 5.4.11 | Công cụ phát triển và đóng gói ứng dụng |
| JavaScript (JSX) | ES Modules | Xây dựng các thành phần giao diện |
| CSS | - | Thiết kế và định dạng giao diện |
| Microsoft SignalR | 11.0.0 | Hỗ trợ giao tiếp thời gian thực |
| Node.js | >= 18 | Môi trường chạy JavaScript |
| npm | - | Quản lý thư viện và dependencies |

## 3. Các chức năng chính

Frontend CollabSphere được tổ chức theo các nhóm chức năng nghiệp vụ:

### 3.1. Xác thực và quản lý tài khoản
- Đăng nhập hệ thống.
- Đăng ký tài khoản.
- Quản lý trạng thái đăng nhập.
- Lưu trữ và sử dụng Access Token.
- Kiểm soát quyền truy cập theo vai trò người dùng.

### 3.2. Quản lý học vụ
- Quản lý môn học.
- Quản lý đề cương môn học.
- Quản lý lớp học.
- Quản lý sinh viên và giảng viên.
- Hỗ trợ các nghiệp vụ quản lý và phê duyệt.

### 3.3. Quản lý dự án
- Hiển thị danh sách dự án.
- Theo dõi thông tin dự án.
- Quản lý nhóm thực hiện.
- Theo dõi tiến độ thực hiện dự án.

### 3.4. Quản lý công việc
- Quản lý nhiệm vụ.
- Theo dõi trạng thái công việc.
- Quản lý cột mốc và điểm kiểm tra.
- Hỗ trợ theo dõi tiến độ hoàn thành.

### 3.5. Không gian làm việc nhóm
- Quản lý thông tin nhóm.
- Theo dõi hoạt động của thành viên.
- Hỗ trợ phối hợp thực hiện dự án.
- Kết nối các chức năng phục vụ làm việc nhóm.

### 3.6. Đánh giá và báo cáo
- Quản lý hoạt động đánh giá.
- Theo dõi kết quả thực hiện.
- Hỗ trợ nhận xét và phản hồi.
- Hiển thị thông tin báo cáo.

### 3.7. Thông báo và giao tiếp thời gian thực
- Hiển thị thông báo hệ thống.
- Hỗ trợ kết nối SignalR.
- Hỗ trợ cập nhật thông tin theo thời gian thực.

## 4. Cấu trúc thư mục

```text
frontend/
│
├── public/
│   └── images/
│       ├── default-avatar.svg
│       ├── empty-projects.svg
│       ├── empty-tasks.svg
│       └── logo.svg
│
├── src/
│   ├── assets/
│   │   ├── icons/
│   │   │   ├── AppIcon.razor
│   │   │   ├── IconNames.cs
│   │   │   └── collabsphere.svg
│   │   │
│   │   └── styles/
│   │       ├── components.css
│   │       ├── global.css
│   │       ├── responsive.css
│   │       └── variables.css
│   │
│   ├── components/
│   │   ├── assessments/
│   │   ├── common/
│   │   ├── layout/
│   │   ├── projects/
│   │   ├── tasks/
│   │   ├── teams/
│   │   ├── DataTable.razor
│   │   └── Modal.razor
│   │
│   ├── contexts/
│   │   ├── AuthContext.cs
│   │   └── NotificationContext.cs
│   │
│   ├── hooks/
│   │   ├── AuthStateService.cs
│   │   ├── DataTableService.cs
│   │   └── ProjectStateService.cs
│   │
│   ├── pages/
│   │   ├── admin/
│   │   ├── assessments/
│   │   ├── auth/
│   │   ├── checkpoints/
│   │   ├── classes/
│   │   ├── dashboard/
│   │   ├── head/
│   │   ├── projects/
│   │   ├── reports/
│   │   ├── settings/
│   │   ├── staff/
│   │   ├── subjects/
│   │   ├── syllabus/
│   │   ├── tasks/
│   │   ├── teams/
│   │   └── workspace/
│   │
│   ├── routes/
│   │   ├── AppRoutes.razor
│   │   ├── ProtectedRoute.razor
│   │   └── RoleBasedRoute.razor
│   │
│   ├── services/
│   │   ├── ApiModels.cs
│   │   ├── CollabSphereApiService.cs
│   │   ├── apiClient.cs
│   │   ├── authService.cs
│   │   ├── assessmentService.cs
│   │   ├── checkpointService.cs
│   │   ├── projectService.cs
│   │   ├── taskService.cs
│   │   └── teamService.cs
│   │
│   ├── utils/
│   │   ├── constants.cs
│   │   ├── formatDate.cs
│   │   └── validators.cs
│   │
│   ├── App.jsx
│   └── main.jsx
│
├── ImageGenerator/
├── index.html
├── package.json
├── package-lock.json
├── Vite.config
├── test-realtime.mjs
└── README.md
```

## 5. Yêu cầu môi trường

Để chạy Frontend, cần chuẩn bị:

- Node.js phiên bản 18 trở lên.
- npm được cài đặt cùng Node.js.
- Visual Studio Code hoặc trình soạn thảo mã nguồn tương đương.
- Backend CollabSphere.API đang hoạt động để sử dụng các chức năng cần kết nối dữ liệu.

Kiểm tra phiên bản Node.js và npm:

```bash
node -v
npm -v
```

## 6. Cài đặt và chạy dự án

### Bước 1: Mở thư mục Frontend

Mở Terminal trong Visual Studio Code và di chuyển đến thư mục frontend:

```bash
cd frontend
```

### Bước 2: Cài đặt thư viện

Chạy lệnh:

```bash
npm install
```

Lệnh này sẽ cài đặt các thư viện được khai báo trong package.json.

### Bước 3: Cấu hình kết nối Backend

Tạo file `.env` trong thư mục frontend:

```env
VITE_API_URL=http://localhost:5000
```

Trong đó:
- `VITE_API_URL`: Địa chỉ Backend API.
- `localhost:5000`: Địa chỉ mặc định được khai báo trong cấu hình proxy hiện tại.

Nếu Backend sử dụng cổng khác, cần thay đổi URL cho phù hợp.

### Bước 4: Khởi chạy Frontend

```bash
npm run dev
```

Sau khi khởi chạy thành công, Vite sẽ cung cấp địa chỉ truy cập ứng dụng, mặc định:

```text
http://localhost:5173
```

Mở địa chỉ trên trình duyệt để truy cập giao diện.

## 7. Kết nối Backend API

Frontend giao tiếp với Backend thông qua HTTP API.

Các request được tổ chức theo các nhóm nghiệp vụ như:

| Nhóm API | Chức năng |
|---|---|
| `/api/Auth` | Xác thực và quản lý tài khoản |
| `/api/Projects` | Quản lý dự án |
| `/api/Teams` | Quản lý nhóm |
| `/api/Tasks` | Quản lý nhiệm vụ |
| `/api/Checkpoints` | Quản lý điểm kiểm tra |
| `/api/Assessments` | Quản lý đánh giá |

Địa chỉ và endpoint thực tế phụ thuộc vào cấu hình và các Controller được triển khai trong Backend.

### Cấu hình Proxy

Vite được cấu hình chuyển tiếp request đến Backend:

- `/api`: Chuyển tiếp các request REST API.
- `/hubs`: Chuyển tiếp kết nối SignalR và WebSocket.

Điều này giúp Frontend có thể giao tiếp với Backend trong quá trình phát triển mà không cần khai báo địa chỉ đầy đủ cho từng request.

## 8. Giao tiếp thời gian thực với SignalR

Dự án sử dụng thư viện `@microsoft/signalr` để hỗ trợ giao tiếp thời gian thực.

Các hoạt động dự kiến hỗ trợ bao gồm:

- Trao đổi thông tin giữa các thành viên.
- Nhận thông báo theo thời gian thực.
- Cập nhật thông tin liên quan đến hoạt động nhóm.
- Đồng bộ dữ liệu khi có sự kiện mới từ máy chủ.

Đường dẫn Hub chat được sử dụng trong mã kiểm thử:

```text
/hubs/chat
```

Frontend cần có Backend đang hoạt động và cung cấp SignalR Hub tương ứng để sử dụng chức năng này.

## 9. Kiểm tra và Build dự án

### Chạy môi trường phát triển

```bash
npm run dev
```

### Kiểm tra chức năng Realtime

```bash
npm run test:realtime
```

Lệnh kiểm thử yêu cầu Backend đang chạy, có dữ liệu tài khoản kiểm thử và biến môi trường `TEST_PASSWORD` được thiết lập.

### Build dự án

```bash
npm run build
```

Sau khi build thành công, các tệp triển khai sẽ được tạo trong thư mục:

```text
dist/
```

### Xem thử bản Build

```bash
npm run preview
```

## 10. Quy trình phát triển

Quy trình làm việc với Frontend:

1. Lấy mã nguồn mới nhất từ GitHub.
2. Cài đặt các dependencies cần thiết.
3. Kiểm tra cấu hình kết nối Backend.
4. Phát triển giao diện theo từng nhóm chức năng.
5. Kết nối giao diện với các API tương ứng.
6. Kiểm tra các chức năng và xử lý lỗi.
7. Build dự án để kiểm tra trước khi triển khai.
8. Commit và push mã nguồn lên GitHub.

## 11. Repository

**GitHub:** https://github.com/nguyenminhthien140906-dot/CNPM

Thư mục Frontend:

```text
CNPM/frontend/
```

Tài liệu này mô tả phần Frontend của dự án CollabSphere, phục vụ việc cài đặt môi trường, phát triển, kết nối Backend và kiểm tra ứng dụng.

---

**CollabSphere – Supporting Project-Based Learning Through Collaboration.**
