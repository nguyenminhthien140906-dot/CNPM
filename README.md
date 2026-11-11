
# CollabSphere (COSRE)

### Hệ thống hỗ trợ học tập theo phương pháp Project-Based Learning (PBL)

CollabSphere là hệ thống hỗ trợ tổ chức, quản lý và thực hiện các hoạt động học tập theo dự án. Hệ thống hướng đến việc kết nối giảng viên và sinh viên trong toàn bộ quá trình học tập, từ xây dựng dự án, phân công nhiệm vụ, theo dõi tiến độ đến đánh giá kết quả.

Thông qua CollabSphere, giảng viên có thể quản lý lớp học, thiết kế dự án và theo dõi quá trình thực hiện. Sinh viên có thể thành lập nhóm, trao đổi thông tin, phối hợp thực hiện nhiệm vụ và nộp sản phẩm học tập trên cùng một nền tảng.

Dự án được xây dựng theo định hướng Project-Based Learning (PBL), trong đó người học chủ động tiếp thu kiến thức và phát triển kỹ năng thông qua việc giải quyết các vấn đề thực tế.

---

## 1. Mục tiêu dự án

CollabSphere được phát triển nhằm:

- Hỗ trợ giảng viên tổ chức và quản lý hoạt động học tập theo dự án.
- Hỗ trợ sinh viên làm việc nhóm, phân công nhiệm vụ và theo dõi tiến độ.
- Tạo môi trường trao đổi, cộng tác và chia sẻ tài nguyên học tập.
- Hỗ trợ quản lý các mốc tiến độ, điểm kiểm tra và bài nộp.
- Hỗ trợ đánh giá kết quả và phản hồi trong quá trình học tập.
- Tích hợp các công cụ cộng tác và hỗ trợ học tập bằng AI.
- Tập trung các hoạt động học tập theo dự án trên một hệ thống thống nhất.

## 2. Đối tượng sử dụng

| Đối tượng | Vai trò |
|---|---|
| Quản trị viên | Quản lý tài khoản và các thông tin chung của hệ thống. |
| Giảng viên | Quản lý môn học, lớp học, dự án, nhiệm vụ và đánh giá kết quả học tập. |
| Sinh viên | Tham gia lớp học, làm việc nhóm, thực hiện nhiệm vụ và nộp sản phẩm dự án. |

Các quyền truy cập và thao tác cụ thể được xác định theo yêu cầu nghiệp vụ của từng chức năng.

## 3. Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Frontend | ReactJS |
| Backend | ASP.NET Core Web API |
| Ngôn ngữ Backend | C# |
| Cơ sở dữ liệu | MySQL |
| Giao tiếp thời gian thực | WebRTC, Signal, Socket.IO |
| Hỗ trợ AI | AWS Bedrock |
| Cloud và lưu trữ | Azure, AWS, Cloudinary, Upstash |
| Quản lý mã nguồn | Git, GitHub |

Các công nghệ trên là định hướng công nghệ của dự án. Những thư viện, dịch vụ và cấu hình cụ thể cần được đối chiếu với mã nguồn triển khai thực tế.

## 4. Chức năng chính

Hệ thống được phân chia thành 12 chức năng chính.

| Mã | Chức năng |
|---|---|
| FE01 | Quản lý tài khoản người dùng |
| FE02 | Quản lý môn học và đề cương |
| FE03 | Quản lý lớp học |
| FE04 | Quản lý dự án |
| FE05 | Quản lý nhóm và không gian làm việc |
| FE06 | Quản lý cột mốc và điểm kiểm tra |
| FE07 | Giao tiếp và tổ chức cuộc họp |
| FE08 | Quản lý tài nguyên học tập |
| FE09 | Công cụ cộng tác thời gian thực |
| FE10 | Đánh giá và phản hồi |
| FE11 | Hỗ trợ học tập bằng AI |
| FE12 | Quản lý thông báo |

### 4.1. Quản lý và tổ chức học tập

- Quản lý tài khoản, thông tin người dùng và phân quyền.
- Quản lý môn học, đề cương và lớp học.
- Xây dựng dự án học tập, xác định mục tiêu và sản phẩm đầu ra.
- Phân công dự án cho lớp học và tổ chức sinh viên thành các nhóm.

### 4.2. Thực hiện và cộng tác dự án

- Quản lý nhóm, thành viên và không gian làm việc.
- Xây dựng cột mốc, điểm kiểm tra và nhiệm vụ.
- Phân công nhiệm vụ và theo dõi tiến độ thực hiện.
- Trao đổi thông tin, tổ chức cuộc họp và chia sẻ tài nguyên.
- Hỗ trợ cộng tác trong quá trình xây dựng sản phẩm dự án.

### 4.3. Đánh giá và hỗ trợ học tập

- Quản lý bài nộp và kết quả đánh giá.
- Hỗ trợ giảng viên đưa ra nhận xét, phản hồi.
- Hỗ trợ sinh viên tìm kiếm thông tin và giải đáp thắc mắc bằng AI.
- Gửi thông báo về nhiệm vụ, tiến độ và các hoạt động học tập.

Chi tiết nghiệp vụ được trình bày trong các tài liệu FE tương ứng tại thư mục `docs/`.

## 5. Cấu trúc thư mục dự án

```text
CollabSphere/
│
├── README.md
│
├── docs/
│   ├── README.md
│   ├── PROJECT-TREE.md
│   ├── 01-requirements/
│   ├── 02-use-case/
│   ├── 03-erd/
│   ├── FE01-user-account-management.md
│   ├── FE02-subject-syllabus-management.md
│   ├── FE03-class-management.md
│   ├── FE04-project-management.md
│   ├── FE05-team-workspace-management.md
│   ├── FE06-milestone-checkpoint-management.md
│   ├── FE07-communication-meetings.md
│   ├── FE08-resource-management.md
│   ├── FE09-realtime-collaboration-tools.md
│   ├── FE10-assessment-feedback.md
│   ├── FE11-ai-assistance.md
│   ├── FE12-notification-management.md
│   └── API-CONTRACT-GUIDE.md
│
├── database/
│   ├── 01-erd.md
│   ├── schema.sql
│   ├── scripts/
│   └── migrations/
│
├── backend/
│   ├── CollabSphere.API/
│   ├── CollabSphere.Application/
│   ├── CollabSphere.Domain/
│   ├── CollabSphere.Infrastructure/
│   ├── tests/
│   ├── CollabSphere.sln
│   └── README.md
│
├── frontend/
│   ├── public/
│   ├── src/
│   ├── package.json
│   └── README.md
│
└── figma/
    ├── assets/
    ├── design-system/
    └── prototypes/
```

### Ý nghĩa các thư mục

| Thư mục | Nội dung |
|---|---|
| `docs/` | Tài liệu yêu cầu, phân tích, thiết kế và mô tả chức năng. |
| `database/` | Thiết kế ERD, cấu trúc SQL, dữ liệu mẫu và migration. |
| `backend/` | Mã nguồn API, nghiệp vụ, thực thể và truy cập dữ liệu. |
| `frontend/` | Mã nguồn giao diện người dùng. |
| `figma/` | Tài nguyên thiết kế giao diện và prototype. |

Cấu trúc trên mô tả cách tổ chức dự án. Một số thư mục hoặc tệp có thể được bổ sung trong quá trình triển khai.

## 6. Kiến trúc hệ thống

Backend được tổ chức theo mô hình Clean Architecture nhằm phân tách trách nhiệm giữa các lớp, giúp hệ thống dễ bảo trì và mở rộng.

```text
Frontend (ReactJS)
        |
        | HTTP / API
        v
CollabSphere.API
        |
        v
CollabSphere.Application
        |
        v
CollabSphere.Domain
        ^
        |
CollabSphere.Infrastructure
        |
        v
PostgreSQL
```

### 6.1. API

Tiếp nhận yêu cầu từ Frontend, xử lý giao tiếp HTTP, xác thực đầu vào và trả kết quả cho người dùng.

### 6.2. Application

Điều phối các ca sử dụng, xử lý nghiệp vụ ứng dụng và kết nối các thành phần cần thiết để thực hiện chức năng.

### 6.3. Domain

Chứa các thực thể, quy tắc nghiệp vụ và những thành phần cốt lõi của hệ thống.

### 6.4. Infrastructure

Triển khai các thành phần truy cập cơ sở dữ liệu, repository và tích hợp với các dịch vụ bên ngoài.

### 6.5. Frontend

Cung cấp giao diện để giảng viên và sinh viên thao tác với các chức năng của hệ thống.

## 7. Thiết kế cơ sở dữ liệu

Cơ sở dữ liệu CollabSphere được thiết kế để quản lý các thông tin và quan hệ nghiệp vụ phát sinh trong quá trình học tập theo dự án.

Các nhóm dữ liệu chính gồm:

- Người dùng, vai trò, sinh viên và giảng viên.
- Môn học, đề cương và lớp học.
- Dự án, nhóm và thành viên.
- Cột mốc, điểm kiểm tra và nhiệm vụ.
- Bài nộp, tài nguyên và kết quả đánh giá.
- Cuộc họp, tin nhắn và không gian làm việc.
- Thông báo và hội thoại AI.

### Tài liệu cơ sở dữ liệu

| Tệp | Nội dung |
|---|---|
| [`01-erd.md`](database/01-erd.md) | Mô tả các thực thể, thuộc tính và quan hệ dữ liệu. |
| [`schema.sql`](database/schema.sql) | Định nghĩa bảng, khóa chính, khóa ngoại và các ràng buộc. |
| [`seed.sql`](database/seed.sql) | Dữ liệu mẫu phục vụ kiểm thử và phát triển. |
| `migrations/` | Lưu các thay đổi cấu trúc cơ sở dữ liệu theo từng phiên bản. |

Cấu trúc cơ sở dữ liệu cần được đối chiếu với các thực thể nghiệp vụ và mã nguồn Backend để bảo đảm tính thống nhất.

## 8. Hướng dẫn cài đặt môi trường

### 8.1. Yêu cầu

Trước khi chạy dự án, cần chuẩn bị:

- Git để tải mã nguồn.
- .NET SDK tương thích với Backend.
- Node.js và npm để chạy Frontend.
- PostgreSQL để lưu trữ dữ liệu.
- Visual Studio hoặc Visual Studio Code để chỉnh sửa mã nguồn.

### 8.2. Tải mã nguồn

```bash
git clone <repository-url>
cd CollabSphere
```

Thay `<repository-url>` bằng địa chỉ GitHub thực tế của dự án.

### 8.3. Cấu hình cơ sở dữ liệu

1. Cài đặt và khởi động PostgreSQL.
2. Tạo cơ sở dữ liệu theo cấu hình của Backend.
3. Kiểm tra các tệp SQL và migration hiện có.
4. Cấu hình chuỗi kết nối trong Backend.
5. Khởi tạo cơ sở dữ liệu theo hướng dẫn của dự án.

Không đưa mật khẩu hoặc thông tin kết nối thật lên GitHub. Các thông tin này nên được lưu trong biến môi trường hoặc tệp cấu hình không được commit.

### 8.4. Chạy Backend

Mở terminal tại thư mục gốc của dự án:

```bash
cd backend
dotnet restore CollabSphere.sln
dotnet build CollabSphere.sln
```

Sau khi build thành công, chạy project API theo cấu hình thực tế của solution:

```bash
dotnet run --project CollabSphere.API
```

Nếu project API có tên hoặc đường dẫn khác, cần điều chỉnh lệnh tương ứng.

### 8.5. Chạy Frontend

Mở terminal tại thư mục Frontend:

```bash
cd frontend
npm install
npm run dev
```

Sau khi chạy thành công, truy cập địa chỉ được hiển thị trong terminal.

## 9. Quy trình phát triển

Quá trình phát triển CollabSphere được thực hiện theo các bước:

1. Phân tích yêu cầu và xác định chức năng cần triển khai.
2. Cập nhật tài liệu chức năng và các quy tắc nghiệp vụ.
3. Thiết kế hoặc điều chỉnh ERD và cấu trúc cơ sở dữ liệu.
4. Xây dựng API và xử lý nghiệp vụ ở Backend.
5. Phát triển giao diện và kết nối API ở Frontend.
6. Kiểm thử chức năng và kiểm tra tính toàn vẹn dữ liệu.
7. Cập nhật tài liệu và hợp nhất mã nguồn vào nhánh chính.

Các thành viên cần thống nhất tên bảng, trường dữ liệu, API và quy tắc nghiệp vụ trước khi tích hợp các phần việc.

## 10. Kiểm thử và kiểm soát chất lượng

Việc kiểm thử cần tập trung vào:

- Kiểm tra tính hợp lệ của dữ liệu đầu vào.
- Kiểm tra các quy tắc nghiệp vụ và phân quyền.
- Kiểm tra các quan hệ khóa chính, khóa ngoại.
- Kiểm tra các trường hợp dữ liệu thiếu, trùng hoặc không hợp lệ.
- Kiểm tra luồng xử lý từ Frontend đến Backend và Database.
- Kiểm tra kết quả trả về của API.
- Kiểm tra khả năng xử lý lỗi và bảo vệ dữ liệu.

Các chức năng chỉ nên được xác nhận hoàn thành sau khi đã được triển khai và kiểm tra theo yêu cầu tương ứng.

## 11. Tài liệu tham khảo trong dự án

- [`docs/README.md`](docs/README.md): Hướng dẫn đọc và sử dụng tài liệu dự án.
- [`docs/PROJECT-TREE.md`](docs/PROJECT-TREE.md): Cấu trúc thư mục dự án.
- [`database/01-erd.md`](database/01-erd.md): Thiết kế cơ sở dữ liệu.
- [`database/schema.sql`](database/schema.sql): Cấu trúc các bảng SQL.
- [`docs/API-CONTRACT-GUIDE.md`](docs/API-CONTRACT-GUIDE.md): Quy ước giao tiếp API.

## 12. Định hướng phát triển

CollabSphere hướng đến việc xây dựng một môi trường học tập theo dự án có khả năng mở rộng, hỗ trợ tốt hơn cho giảng viên và sinh viên trong quá trình tổ chức, thực hiện và đánh giá dự án.

Trong các giai đoạn tiếp theo, dự án tập trung vào việc hoàn thiện các chức năng cốt lõi, tăng cường khả năng cộng tác, nâng cao tính ổn định và bảo đảm sự thống nhất giữa tài liệu, cơ sở dữ liệu, Backend và Frontend.

---

**CollabSphere — Học tập thông qua dự án, cộng tác để phát triển.**
