
# CollabSphere (COSRE)

### Hệ thống hỗ trợ học tập theo phương pháp Project-Based Learning (PBL)

CollabSphere là hệ thống hỗ trợ tổ chức, quản lý và thực hiện hoạt động học tập theo dự án. Nền tảng hướng đến việc kết nối quản trị viên, bộ phận quản lý học vụ, giảng viên và sinh viên trong toàn bộ quá trình học tập: từ quản lý môn học, đề cương và lớp học đến tổ chức dự án, làm việc nhóm, thực hiện nhiệm vụ, nộp sản phẩm và đánh giá kết quả.

Dự án được xây dựng theo định hướng **Project-Based Learning (PBL)**. Thay vì chỉ tiếp nhận kiến thức một chiều, sinh viên tham gia giải quyết vấn đề, phối hợp với các thành viên trong nhóm và tạo ra sản phẩm học tập cụ thể. Giảng viên theo dõi tiến độ, hướng dẫn và đánh giá quá trình thực hiện.

> **Tên dự án:** CollabSphere  
> **Tên viết tắt:** COSRE  
> **Định hướng:** Nền tảng hỗ trợ học tập và cộng tác theo dự án (PBL)

---

## Mục lục

- [1. Giới thiệu dự án](#1-giới-thiệu-dự-án)
- [2. Mục tiêu](#2-mục-tiêu)
- [3. Đối tượng sử dụng](#3-đối-tượng-sử-dụng)
- [4. Phạm vi chức năng](#4-phạm-vi-chức-năng)
- [5. Công nghệ và nền tảng](#5-công-nghệ-và-nền-tảng)
- [6. Kiến trúc hệ thống](#6-kiến-trúc-hệ-thống)
- [7. Cấu trúc thư mục](#7-cấu-trúc-thư-mục)
- [8. Thiết kế cơ sở dữ liệu](#8-thiết-kế-cơ-sở-dữ-liệu)
- [9. Backend và API](#9-backend-và-api)
- [10. Frontend](#10-frontend)
- [11. Cài đặt môi trường](#11-cài-đặt-môi-trường)
- [12. Khởi tạo và chạy Backend](#12-khởi-tạo-và-chạy-backend)
- [13. Kiểm tra API](#13-kiểm-tra-api)
- [14. Dữ liệu mẫu](#14-dữ-liệu-mẫu)
- [15. Kiểm thử](#15-kiểm-thử)
- [16. Quy trình phát triển và đóng góp](#16-quy-trình-phát-triển-và-đóng-góp)
- [17. Tài liệu liên quan](#17-tài-liệu-liên-quan)
- [18. Phạm vi triển khai hiện tại](#18-phạm-vi-triển-khai-hiện-tại)
- [19. Định hướng phát triển](#19-định-hướng-phát-triển)

---

## 1. Giới thiệu dự án

Trong quá trình học tập theo dự án, giảng viên và sinh viên cần phối hợp qua nhiều hoạt động khác nhau như quản lý lớp, chia nhóm, giao nhiệm vụ, theo dõi tiến độ, trao đổi tài liệu và đánh giá sản phẩm. Nếu các hoạt động này được thực hiện trên nhiều công cụ tách biệt, việc theo dõi tiến độ và tổng hợp thông tin có thể trở nên khó khăn.

CollabSphere được định hướng xây dựng thành một nền tảng tập trung để hỗ trợ các hoạt động trên. Hệ thống cung cấp môi trường quản lý học vụ và không gian cộng tác, giúp các bên tham gia theo dõi thông tin liên quan đến dự án trong cùng một hệ thống.

### 1.1. Vấn đề cần giải quyết

- Thông tin môn học, đề cương và lớp học cần được tổ chức thống nhất.
- Hoạt động học tập theo dự án cần có mục tiêu, phạm vi và sản phẩm đầu ra rõ ràng.
- Thành viên nhóm cần biết nhiệm vụ, người phụ trách và thời hạn thực hiện.
- Giảng viên cần theo dõi tiến độ và đánh giá kết quả của cá nhân hoặc nhóm.
- Quá trình trao đổi, chia sẻ tài nguyên và thông báo cần được tập trung.
- Người học cần có thêm công cụ hỗ trợ tra cứu và giải đáp trong quá trình thực hiện dự án.

### 1.2. Định hướng giải pháp

CollabSphere hướng đến việc kết nối các hoạt động quản lý học vụ, tổ chức dự án, cộng tác nhóm và đánh giá học tập. Các chức năng được chia thành từng nhóm nghiệp vụ để thuận tiện cho việc phân tích, phát triển, kiểm thử và mở rộng.

---

## 2. Mục tiêu

Dự án hướng đến các mục tiêu sau:

1. **Tập trung hóa thông tin học tập:** quản lý tài khoản, môn học, đề cương và lớp học trên một nền tảng.
2. **Hỗ trợ tổ chức dự án:** tạo dự án, xác định mục tiêu, phạm vi, thời gian và sản phẩm đầu ra.
3. **Tăng cường làm việc nhóm:** hỗ trợ tổ chức nhóm, quản lý thành viên và phân công công việc.
4. **Theo dõi tiến độ:** quản lý nhiệm vụ, cột mốc, điểm kiểm tra và trạng thái thực hiện.
5. **Hỗ trợ trao đổi:** tạo môi trường giao tiếp, họp và chia sẻ tài nguyên phục vụ dự án.
6. **Hỗ trợ đánh giá:** lưu trữ bài nộp, kết quả đánh giá và phản hồi.
7. **Hỗ trợ người học:** định hướng tích hợp công cụ AI để hỗ trợ tìm kiếm thông tin và giải đáp thắc mắc.
8. **Bảo đảm khả năng mở rộng:** phân chia hệ thống thành các lớp và nhóm chức năng có trách nhiệm rõ ràng.

---

## 3. Đối tượng sử dụng

| Đối tượng | Vai trò và phạm vi sử dụng |
|---|---|
| **Quản trị viên (ADMIN)** | Quản lý tài khoản, vai trò và các thông tin quản trị của hệ thống. |
| **Trưởng bộ phận (HEAD)** | Tham gia các nghiệp vụ quản lý và phê duyệt theo phân quyền. |
| **Nhân viên học vụ (STAFF)** | Quản lý thông tin môn học, đề cương, lớp học và phân công học vụ. |
| **Giảng viên (LECTURER)** | Theo dõi lớp học, tổ chức hoạt động học tập, hướng dẫn dự án và đánh giá kết quả. |
| **Sinh viên (STUDENT)** | Tham gia lớp, làm việc nhóm, thực hiện nhiệm vụ, trao đổi và nộp sản phẩm. |

Quyền truy cập được kiểm soát theo vai trò. Phạm vi quyền cụ thể của từng chức năng cần căn cứ vào tài liệu nghiệp vụ và chính sách phân quyền được triển khai trong API.

---

## 4. Phạm vi chức năng

Hệ thống được phân chia thành 12 nhóm chức năng (FE01–FE12). Đây là cách phân nhóm nghiệp vụ của dự án; không có nghĩa toàn bộ API và quy trình của tất cả nhóm đã hoàn thiện.

| Mã | Nhóm chức năng | Nội dung |
|---|---|---|
| **FE01** | Quản lý tài khoản người dùng | Tài khoản, thông tin cá nhân, vai trò và xác thực. |
| **FE02** | Quản lý môn học và đề cương | Thông tin môn học, phiên bản và nội dung đề cương. |
| **FE03** | Quản lý lớp học | Tạo và cập nhật lớp, phân công giảng viên, thêm hoặc xóa sinh viên khỏi lớp. |
| **FE04** | Quản lý dự án | Xây dựng dự án, mục tiêu, phạm vi, thời gian và sản phẩm đầu ra. |
| **FE05** | Quản lý nhóm và không gian làm việc | Tổ chức nhóm, thành viên và khu vực làm việc của nhóm. |
| **FE06** | Quản lý cột mốc và điểm kiểm tra | Thiết lập mốc tiến độ và các điểm kiểm tra trong dự án. |
| **FE07** | Giao tiếp và tổ chức cuộc họp | Trao đổi thông tin và quản lý hoạt động họp. |
| **FE08** | Quản lý tài nguyên học tập | Chia sẻ và quản lý tài liệu, tài nguyên phục vụ học tập. |
| **FE09** | Công cụ cộng tác thời gian thực | Hỗ trợ các hoạt động cộng tác trong quá trình làm việc nhóm. |
| **FE10** | Đánh giá và phản hồi | Quản lý bài nộp, đánh giá và nhận xét. |
| **FE11** | Hỗ trợ học tập bằng AI | Hỗ trợ tra cứu, giải đáp và một số tác vụ học tập. |
| **FE12** | Quản lý thông báo | Thông báo về nhiệm vụ, tiến độ và hoạt động liên quan. |

### 4.1. Luồng nghiệp vụ tổng quát

```text
Quản lý tài khoản và phân quyền
              |
              v
Quản lý môn học và đề cương
              |
              v
Quản lý lớp học và thành viên lớp
              |
              v
Tổ chức dự án học tập
              |
              v
Thành lập nhóm và phân công nhiệm vụ
              |
              v
Thực hiện nhiệm vụ - theo dõi tiến độ
              |
              v
Trao đổi - họp - chia sẻ tài nguyên
              |
              v
Nộp sản phẩm và đánh giá
              |
              v
Phản hồi và tổng hợp kết quả
```

Luồng trên mô tả định hướng nghiệp vụ tổng thể. Trình tự thực tế có thể thay đổi tùy quy trình và quyền của từng vai trò.

---

## 5. Công nghệ và nền tảng

Các công nghệ dưới đây được ghi nhận từ cấu hình và mã nguồn hiện có trong repository. Những dịch vụ chưa có cấu hình hoặc tích hợp thực tế được đánh dấu là định hướng, không xem là thành phần đã vận hành.

| Thành phần | Công nghệ / công cụ | Vai trò |
|---|---|---|
| Backend API | ASP.NET Core Web API | Tiếp nhận và xử lý yêu cầu HTTP. |
| Ngôn ngữ Backend | C# | Triển khai API và nghiệp vụ phía máy chủ. |
| Nền tảng .NET | .NET 10 | Runtime và framework của các project Backend. |
| ORM | Entity Framework Core | Ánh xạ entity và thao tác dữ liệu. |
| Database provider | `MySql.EntityFrameworkCore` | Kết nối EF Core với MySQL. |
| Database driver | `MySqlConnector` | Thực thi các lệnh SQL và migration. |
| Cơ sở dữ liệu | MySQL 8.0+ | Lưu trữ dữ liệu hệ thống. |
| Xác thực | JWT Bearer | Xác thực request bằng access token. |
| Mã hóa mật khẩu | ASP.NET Identity PasswordHasher | Băm và kiểm tra mật khẩu. |
| Giao tiếp thời gian thực | SignalR package có trong mã nguồn | Hỗ trợ kết nối thời gian thực ở phía client/server. |
| AI Agent | Python | Module AI được tổ chức riêng trong `backend/ai-agent/`. |
| Frontend | JSX và các thành phần `.razor`/C# | Mã giao diện hiện có đang ở trạng thái kết hợp/chuyển đổi. |
| Quản lý mã nguồn | Git, GitHub | Quản lý phiên bản và cộng tác phát triển. |

**Lưu ý:** Repository hiện có mã nguồn và cấu hình cho một số công nghệ, nhưng điều đó không đồng nghĩa mọi dịch vụ đã được cấu hình đầy đủ hoặc đang hoạt động. Các nền tảng như AWS Bedrock, Azure, Cloudinary hay Upstash chỉ nên được xem là lựa chọn tích hợp khi có cấu hình và mã triển khai tương ứng.

---

## 6. Kiến trúc hệ thống

### 6.1. Kiến trúc tổng quan

Backend được chia thành bốn project theo hướng phân lớp, gần với Clean Architecture. Mỗi project đảm nhiệm một nhóm trách nhiệm riêng.

```text
                    Người dùng
                        |
                        v
                 Frontend Client
                        |
                    HTTP / JSON
                        |
                        v
                CollabSphere.API
                 /             \
                v               v
      Application Layer    Infrastructure
                \               |
                 v              v
                 Domain      Entity Framework
                                |
                                v
                              MySQL

       AI Agent (Python) - module riêng trong backend/ai-agent
```

Sơ đồ thể hiện các thành phần chính. Quan hệ phụ thuộc cụ thể giữa các project Backend được mô tả như sau:

```text
CollabSphere.API
   |                 \
   v                  v
Application       Infrastructure
   |                  |
   v                  v
Domain <---------- Domain
```

### 6.2. Các lớp Backend

| Project | Trách nhiệm | Thành phần tiêu biểu |
|---|---|---|
| `CollabSphere.API` | Giao tiếp HTTP, controller, xác thực, phân quyền và khởi động ứng dụng. | Controllers, Middleware, JWT service, Program.cs. |
| `CollabSphere.Application` | DTO, interface và các thành phần điều phối nghiệp vụ ứng dụng. | Auth DTOs, service interfaces, use cases. |
| `CollabSphere.Domain` | Mô hình nghiệp vụ cốt lõi, entity, enum, exception và value object. | Entities.cs, Enums.cs, Exceptions.cs. |
| `CollabSphere.Infrastructure` | Kết nối dữ liệu, mapping, repository, dịch vụ và khởi chạy migration/seed. | Persistence.cs, Configurations.cs, Repositories.cs, MigrationEnvironment.cs. |

### 6.3. Luồng xử lý một request

1. Người dùng thao tác trên giao diện.
2. Frontend gửi request đến endpoint của `CollabSphere.API`.
3. API kiểm tra dữ liệu đầu vào và xác thực/phân quyền nếu endpoint yêu cầu.
4. Thành phần ứng dụng thực hiện hoặc điều phối nghiệp vụ.
5. Infrastructure truy cập MySQL thông qua các thành phần truy cập dữ liệu.
6. API trả kết quả theo định dạng JSON cho Frontend.

---

## 7. Cấu trúc thư mục

Cấu trúc dưới đây được rút gọn từ repository để thể hiện các khu vực chính:

```text
CNPM/
│
├── README.md
│
├── docs/
│   ├── README.md
│   ├── PROJECT-TREE.md
│   ├── API-CONTRACT-GUIDE.md
│   ├── api-hoc-vu.md
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
│   └── FE12-notification-management.md
│
├── database/
│   ├── 01-erd.md
│   ├── schema.sql
│   ├── migrations/
│   │   ├── MigrationEnvironment.cs
│   │   └── versions/
│   │       ├── 001_identity_academic.up.sql
│   │       ├── 001_identity_academic.down.sql
│   │       ├── 002_projects_teams.up.sql
│   │       ├── 002_projects_teams.down.sql
│   │       ├── 003_progress_tasks_submissions.up.sql
│   │       ├── 003_progress_tasks_submissions.down.sql
│   │       ├── 004_resources_communication.up.sql
│   │       ├── 004_resources_communication.down.sql
│   │       ├── 005_workspaces_documents.up.sql
│   │       ├── 005_workspaces_documents.down.sql
│   │       ├── 006_evaluations_notifications_ai.up.sql
│   │       ├── 006_evaluations_notifications_ai.down.sql
│   │       └── 007_seed_roles.up.sql
│   ├── scripts/
│   │   └── run_mysql.sh
│   └── seeds/
│       ├── 001_seed_roles.sql
│       ├── 002_seed_users.sql
│       ├── 003_seed_subjects.sql
│       ├── 004_seed_syllabi.sql
│       └── 005_seed_classes.sql
│
├── backend/
│   ├── CollabSphere.API/
│   ├── CollabSphere.Application/
│   ├── CollabSphere.Domain/
│   ├── CollabSphere.Infrastructure/
│   ├── tests/
│   ├── ai-agent/
│   ├── CollabSphere.Backend.sln
│   └── CollabSphere.sln
│
├── frontend/
   ├── public/
   ├── src/
   │   ├── assets/
   │   ├── components/
   │   ├── contexts/
   │   ├── hooks/
   │   ├── pages/
   │   ├── routes/
   │   ├── services/
   │   └── utils/
   ├── package.json
   └── README.md


### 7.1. Vai trò từng thư mục

| Thư mục | Nội dung |
|---|---|
| `docs/` | Tài liệu yêu cầu, mô tả chức năng, API contract và cấu trúc dự án. |
| `database/` | ERD, schema tổng hợp, migration, seed và script hỗ trợ database. |
| `backend/CollabSphere.API/` | Web API, controller, middleware, xác thực và cấu hình ứng dụng. |
| `backend/CollabSphere.Application/` | DTO, interface, use case và thành phần ứng dụng. |
| `backend/CollabSphere.Domain/` | Entity và quy tắc nghiệp vụ cốt lõi. |
| `backend/CollabSphere.Infrastructure/` | Persistence, mapping, repository, service, migration và seed. |
| `backend/ai-agent/` | Module AI viết bằng Python, được tách riêng khỏi các project .NET. |
| `backend/tests/` | Script kiểm thử API hiện có. |
| `frontend/` | Mã giao diện, component, trang, route, service và tài nguyên tĩnh. |
| `figma/` | Tài nguyên thiết kế, design system, prototype và wireframe. |

---

## 8. Thiết kế cơ sở dữ liệu

### 8.1. Hệ quản trị và quy ước

Cơ sở dữ liệu sử dụng **MySQL 8.0 trở lên**, với bộ ký tự `utf8mb4` và collation `utf8mb4_unicode_ci`. Các bảng sử dụng InnoDB để hỗ trợ khóa ngoại và giao dịch.

Database có tên mặc định:

```sql
CREATE DATABASE collabsphere
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;
```

### 8.2. Nhóm dữ liệu

Các bảng được chia thành những nhóm nghiệp vụ:

| Nhóm | Dữ liệu tiêu biểu |
|---|---|
| Định danh và học vụ | `roles`, `users`, `students`, `lecturers`, `subjects`, `syllabi`, `study_classes`, `class_students`, `class_lecturers` |
| Dự án và nhóm | `projects`, `teams`, `team_members` |
| Tiến độ và nhiệm vụ | `milestones`, `checkpoints`, `tasks`, `task_assignees`, `submissions` |
| Tài nguyên và giao tiếp | `resources`, `meetings`, `meeting_participants`, `messages` |
| Không gian làm việc | `workspaces`, `whiteboard_snapshots`, `workspace_documents`, `document_versions` |
| Đánh giá, thông báo và AI | `evaluations`, `notifications`, `ai_conversations`, `ai_messages` |

### 8.3. Quản lý cấu trúc bằng SQL Migration

Repository hiện có các migration được đánh số từ `001` đến `007`:

| Phiên bản | Tên migration | Phạm vi |
|---|---|---|
| 001 | `identity_academic` | Người dùng, vai trò và dữ liệu học vụ. |
| 002 | `projects_teams` | Dự án, nhóm và thành viên nhóm. |
| 003 | `progress_tasks_submissions` | Cột mốc, điểm kiểm tra, nhiệm vụ và bài nộp. |
| 004 | `resources_communication` | Tài nguyên, cuộc họp và tin nhắn. |
| 005 | `workspaces_documents` | Không gian làm việc, bảng cộng tác và tài liệu. |
| 006 | `evaluations_notifications_ai` | Đánh giá, thông báo và hội thoại AI. |
| 007 | `seed_roles` | Thêm các vai trò mặc định. |

Backend sử dụng bộ chạy SQL migration riêng trong `CollabSphere.Infrastructure/Database/MigrationEnvironment.cs`. Các file `*.up.sql` được sắp xếp theo tên và chỉ áp dụng nếu phiên bản chưa có trong bảng `schema_migrations`.

**Quy tắc quan trọng khi cập nhật database:**

- Không chỉnh sửa migration đã được áp dụng trên môi trường dùng chung.
- Khi cần thay đổi cấu trúc, tạo migration mới với số thứ tự tiếp theo.
- File `.down.sql` hiện được lưu để tham khảo hoặc chạy thủ công; Backend không tự động chạy rollback.
- Không dùng đồng thời `EnsureCreated()` hoặc `Database.Migrate()` cho schema đang được quản lý bằng bộ SQL migration riêng.
- Nên sao lưu dữ liệu trước khi chạy thay đổi schema trên môi trường có dữ liệu quan trọng.

### 8.4. Các tệp database

| Tệp / thư mục | Mục đích |
|---|---|
| `database/01-erd.md` | Mô tả thực thể, thuộc tính và quan hệ dữ liệu. |
| `database/schema.sql` | Schema MySQL tổng hợp để tham khảo hoặc khởi tạo theo hướng dẫn phù hợp. |
| `database/migrations/versions/` | Các file migration tăng/giảm cấu trúc theo phiên bản. |
| `database/migrations/MigrationEnvironment.cs` | Bộ chạy migration được sử dụng bởi Backend. |
| `database/seeds/` | Dữ liệu mẫu phục vụ phát triển và kiểm thử. |
| `database/scripts/run_mysql.sh` | Script hỗ trợ thao tác với MySQL. |

Trong quy trình chạy Backend hiện tại, migration là cơ chế khởi tạo schema chính. Không chạy đồng thời `schema.sql` rồi lại chạy toàn bộ migration trên cùng một database trống, vì có thể dẫn đến lỗi bảng đã tồn tại.

---

## 9. Backend và API

### 9.1. Tổng quan

Backend được phát triển bằng ASP.NET Core Web API trên .NET 10. Dữ liệu được truy cập thông qua Entity Framework Core và MySQL provider. Một số thao tác khởi tạo database sử dụng `MySqlConnector` để chạy file SQL migration.

### 9.2. Các thành phần chính

- **Controllers:** tiếp nhận request và trả response.
- **Contracts / DTOs:** định nghĩa dữ liệu request và response.
- **Services:** xử lý các tác vụ ứng dụng như xác thực và tạo token.
- **Middleware:** xử lý lỗi chưa được xử lý ở tầng request.
- **Persistence:** định nghĩa DbContext và DbSet.
- **Configurations:** ánh xạ entity với bảng, cột và ràng buộc database.
- **Repositories:** cung cấp lớp truy cập dữ liệu dùng chung.
- **MigrationEnvironment:** kiểm tra và áp dụng migration.
- **DatabaseSeeder:** chuẩn bị dữ liệu mẫu trong môi trường phát triển.

### 9.3. API hiện có

Theo mã nguồn Backend trong repository, các controller hiện có gồm:

| Controller | Nhóm endpoint / trách nhiệm |
|---|---|
| `AuthController` | Đăng nhập và thông tin người dùng hiện tại. |
| `AdminController` | Endpoint quản trị và kiểm tra quyền quản trị. |
| `SubjectsController` | Quản lý môn học. |
| `SyllabiController` | Quản lý đề cương môn học. |
| `StudyClassesController` | Quản lý lớp học. |
| `ClassAssignmentsController` | Phân công giảng viên và sinh viên vào lớp. |
| `NotificationsController` | Endpoint thông báo. |

Ngoài ra, API có các Hub phục vụ kết nối thời gian thực và endpoint hệ thống để kiểm tra trạng thái ứng dụng/database. Chi tiết route, request, response và quyền truy cập cần xem trong `backend/README.md` và `docs/API-CONTRACT-GUIDE.md`.

### 9.4. Xác thực và phân quyền

Backend sử dụng JWT Bearer. Sau khi đăng nhập thành công, client nhận access token và gửi token trong header của các request cần xác thực:

```http
Authorization: Bearer <access-token>
```

Các mã vai trò đang được sử dụng trong Backend:

| Mã vai trò | Ý nghĩa |
|---|---|
| `ADMIN` | Quản trị viên |
| `HEAD` | Trưởng bộ phận |
| `STAFF` | Nhân viên học vụ |
| `LECTURER` | Giảng viên |
| `STUDENT` | Sinh viên |

Mật khẩu được xử lý bằng ASP.NET Identity PasswordHasher; không lưu mật khẩu dạng văn bản thuần trong database.

---

## 10. Frontend

Thư mục `frontend/` chứa giao diện và các thành phần phục vụ tương tác với Backend.

### 10.1. Các nhóm thành phần

| Thư mục | Trách nhiệm |
|---|---|
| `assets/` | Icon, hình ảnh và CSS. |
| `components/` | Thành phần giao diện dùng lại như bảng, modal, nút, form và thẻ nội dung. |
| `contexts/` | Trạng thái dùng chung như xác thực và thông báo. |
| `hooks/` | Các service quản lý trạng thái và dữ liệu giao diện. |
| `pages/` | Các trang theo chức năng như đăng nhập, lớp học, môn học, dự án, nhóm và nhiệm vụ. |
| `routes/` | Khai báo điều hướng và kiểm soát route theo trạng thái/quyền. |
| `services/` | Client gọi API và các service theo nghiệp vụ. |
| `utils/` | Hằng số, định dạng dữ liệu và kiểm tra đầu vào. |
| `public/` | Tài nguyên tĩnh được phục vụ trực tiếp. |

### 10.2. Trạng thái mã nguồn

Frontend hiện chứa đồng thời các tệp `.jsx`, `.razor` và `.cs`. Vì vậy, không nên mô tả frontend là một ứng dụng React thuần hoặc một ứng dụng Blazor đã hoàn chỉnh. Đây là khu vực đang có mã nguồn kết hợp/chuyển đổi.

`frontend/package.json` hiện chưa khai báo đầy đủ các script chạy ứng dụng giao diện. Do đó, lệnh `npm run dev` chỉ có thể sử dụng sau khi package và cấu hình frontend tương ứng được hoàn thiện. Không nên xem việc cài npm package là đủ để khởi chạy toàn bộ giao diện hiện tại.

---

## 11. Cài đặt môi trường

### 11.1. Yêu cầu

| Công cụ | Phiên bản / yêu cầu |
|---|---|
| Git | Phiên bản phù hợp với hệ điều hành. |
| .NET SDK | .NET 10 SDK. |
| MySQL Server | MySQL 8.0 trở lên. |
| MySQL client | MySQL Workbench hoặc MySQL CLI để quản lý database. |
| IDE | Visual Studio hoặc Visual Studio Code. |
| PowerShell | Khuyến nghị nếu chạy script kiểm thử `.ps1`. |
| Python | Chỉ cần khi phát triển hoặc chạy module `backend/ai-agent/`. |

Kiểm tra cài đặt:

```bash
git --version
dotnet --version
mysql --version
```

### 11.2. Tải source code

```bash
git clone https://github.com/nguyenminhthien140906-dot/CNPM.git
cd CNPM
```

Nếu đã tải ZIP từ GitHub, giải nén source code và mở terminal tại thư mục gốc `CNPM`.

---

## 12. Khởi tạo và chạy Backend

Các bước sau được căn cứ theo hướng dẫn trong `backend/README.md`.

### Bước 1. Tạo database

Mở MySQL Workbench hoặc MySQL CLI và chạy:

```sql
CREATE DATABASE collabsphere
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;
```

Chỉ tạo database rỗng. Backend sẽ tự áp dụng các file migration để tạo bảng.

### Bước 2. Di chuyển đến project API

```bash
cd backend/CollabSphere.API
```

### Bước 3. Cấu hình User Secrets

User Secrets được dùng để lưu thông tin nhạy cảm trong môi trường phát triển, tránh ghi mật khẩu database và khóa JWT trực tiếp vào source code.

**Windows PowerShell:**

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=collabsphere;User=root;Password=MAT_KHAU_MYSQL;"
dotnet user-secrets set "Jwt:Key" ([Convert]::ToBase64String((1..48 | ForEach-Object { Get-Random -Maximum 256 })))
dotnet user-secrets set "Seed:DefaultPassword" "Collab@12345"
```

Thay `MAT_KHAU_MYSQL` bằng mật khẩu MySQL đang sử dụng trên máy. Mật khẩu mẫu ở trên chỉ dành cho môi trường phát triển; khi triển khai thật, hãy dùng mật khẩu riêng và bảo mật.

Nếu sử dụng PowerShell, chạy từng lệnh tại thư mục `backend/CollabSphere.API`.

### Bước 4. Chọn môi trường Development

Trong PowerShell:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
```

Seed dữ liệu mẫu chỉ chạy trong môi trường `Development` và khi đã cấu hình `Seed:DefaultPassword`.

### Bước 5. Restore và build

Đang ở thư mục `backend/CollabSphere.API`, chạy:

```powershell
dotnet restore
dotnet build
```

### Bước 6. Chạy API

```powershell
dotnet run --urls "http://localhost:5000"
```

Nếu khởi động thành công, terminal sẽ hiển thị địa chỉ ứng dụng đang lắng nghe. Giữ terminal này mở trong thời gian kiểm thử.

### Bước 7. Kiểm tra trạng thái

Mở trình duyệt hoặc gửi request:

```http
GET http://localhost:5000/api/system/status
```

Kiểm tra mapping và kết nối database:

```http
GET http://localhost:5000/api/system/database-check
```

Các endpoint này được mô tả trong tài liệu Backend. Nếu sử dụng cổng khác, thay `5000` bằng cổng được hiển thị trong terminal.

### Lưu ý khi khởi chạy

- Tài khoản MySQL phải có quyền tạo bảng và ghi dữ liệu trong database `collabsphere`.
- `Jwt:Key` phải là chuỗi Base64 hợp lệ và giải mã được tối thiểu 32 byte.
- Không commit User Secrets hoặc thông tin xác thực lên GitHub.
- Nếu migration lỗi giữa chừng, cần đọc tên migration và thông báo lỗi trước khi xử lý database.
- Trên database đã có dữ liệu, không tự ý xóa database để sửa lỗi. Hãy sao lưu và xác định nguyên nhân trước.

---

## 13. Kiểm tra API

Có thể sử dụng Postman, curl hoặc công cụ HTTP bất kỳ để kiểm tra API.

### 13.1. Kiểm tra trạng thái hệ thống

```bash
curl http://localhost:5000/api/system/status
```

### 13.2. Đăng nhập

```http
POST http://localhost:5000/api/auth/login
Content-Type: application/json
```

Body minh họa:

```json
{
  "usernameOrEmail": "admin",
  "password": "Collab@12345"
}
```

Tài khoản và mật khẩu chỉ hợp lệ nếu dữ liệu mẫu đã được seed thành công và mật khẩu trùng với `Seed:DefaultPassword` được cấu hình trên máy.

### 13.3. Gửi request có xác thực

Sau khi nhận access token, thêm header:

```http
Authorization: Bearer <access-token>
```

### 13.4. Một số endpoint được mô tả trong Backend

| Method | Endpoint | Mục đích |
|---|---|---|
| `GET` | `/api/system/status` | Kiểm tra trạng thái ứng dụng và database. |
| `GET` | `/api/system/database-check` | Kiểm tra kết nối và mapping database. |
| `POST` | `/api/auth/login` | Đăng nhập. |
| `GET` | `/api/auth/me` | Lấy thông tin người dùng từ token. |
| `GET` | `/api/subjects` | Lấy danh sách môn học. |
| `GET` | `/api/syllabi` | Truy xuất đề cương theo API được triển khai. |
| `GET` | `/api/classes` | Truy xuất danh sách lớp theo API được triển khai. |

Đây là danh sách tham khảo từ Backend hiện có; xem `backend/README.md` để biết đầy đủ tham số, request body, response và quyền của từng endpoint.

---

## 14. Dữ liệu mẫu

Các file seed nằm tại `database/seeds/`:

| File | Nội dung |
|---|---|
| `001_seed_roles.sql` | Các vai trò mặc định. |
| `002_seed_users.sql` | Tài khoản mẫu theo các vai trò. |
| `003_seed_subjects.sql` | Dữ liệu môn học. |
| `004_seed_syllabi.sql` | Dữ liệu đề cương. |
| `005_seed_classes.sql` | Dữ liệu lớp học và phân công liên quan. |

Seed được Backend gọi trong môi trường `Development` khi có cấu hình `Seed:DefaultPassword`.

Một số tài khoản mẫu được mô tả trong `backend/README.md`:

| Username | Email | Vai trò |
|---|---|---|
| `admin` | `admin@collabsphere.local` | ADMIN |
| `head01` | `head01@collabsphere.local` | HEAD |
| `staff01` | `staff01@collabsphere.local` | STAFF |
| `lecturer01` | `lecturer01@collabsphere.local` | LECTURER |
| `student01` | `student01@collabsphere.local` | STUDENT |
| `student02` | `student02@collabsphere.local` | STUDENT |

Mật khẩu dùng để đăng nhập là giá trị `Seed:DefaultPassword` được cấu hình trên máy phát triển. Không sử dụng các tài khoản mẫu này trong môi trường thật.

**Lưu ý:** File seed người dùng có nội dung placeholder cho password hash. Không chạy riêng file đó trực tiếp trong MySQL Workbench để tạo tài khoản đăng nhập; hãy để `DatabaseSeeder` xử lý theo quy trình của Backend.

---

## 15. Kiểm thử

Repository hiện có script kiểm thử API:

```text
backend/tests/api-smoke-test.ps1
```

Có thể chạy bằng PowerShell sau khi Backend đã khởi động:

```powershell
.\backend\tests\api-smoke-test.ps1
```

Nếu script cần tham số hoặc base URL riêng, kiểm tra nội dung file trước khi chạy.

### 15.1. Phạm vi cần kiểm thử

- Đăng nhập với thông tin hợp lệ và không hợp lệ.
- Kiểm tra token còn hạn, hết hạn và token không hợp lệ.
- Kiểm tra quyền truy cập theo từng vai trò.
- Tạo, xem, cập nhật và xóa dữ liệu học vụ theo quyền.
- Kiểm tra dữ liệu trùng mã và dữ liệu không hợp lệ.
- Kiểm tra khóa ngoại và ràng buộc dữ liệu.
- Kiểm tra kết nối giữa Frontend, API và MySQL.
- Kiểm tra xử lý lỗi khi database không khả dụng.

### 15.2. Nguyên tắc xác nhận hoàn thành

Một chức năng chỉ nên được đánh dấu hoàn thành khi:

1. Có mô tả yêu cầu và quy tắc nghiệp vụ.
2. Có phần triển khai tương ứng.
3. Có kiểm tra quyền truy cập nếu chức năng yêu cầu phân quyền.
4. Có kiểm thử các trường hợp thành công và thất bại.
5. Kết quả được đối chiếu với yêu cầu ban đầu.

---

## 16. Quy trình phát triển và đóng góp

Để hạn chế xung đột khi nhiều thành viên cùng làm việc, nên thống nhất quy trình sau:

### 16.1. Quy trình thực hiện chức năng

1. Đọc tài liệu chức năng tương ứng trong `docs/`.
2. Xác định vai trò sử dụng, dữ liệu đầu vào, kết quả và quy tắc nghiệp vụ.
3. Kiểm tra ERD và các bảng liên quan trong `database/`.
4. Nếu cần thay đổi database, tạo migration mới.
5. Triển khai Domain/Application/Infrastructure/API theo trách nhiệm từng lớp.
6. Kết nối giao diện với API theo API contract.
7. Kiểm thử và cập nhật tài liệu.
8. Tạo Pull Request để kiểm tra trước khi hợp nhất vào nhánh chính.

### 16.2. Quy tắc làm việc với database

- Thống nhất tên bảng, tên cột và kiểu dữ liệu trước khi triển khai.
- Mọi thay đổi schema cần có migration tương ứng.
- Không tự ý sửa migration đã chạy trên môi trường dùng chung.
- Không commit mật khẩu, JWT key hoặc thông tin kết nối thật.
- Cập nhật ERD và tài liệu khi thay đổi quan hệ dữ liệu.

### 16.3. Quy tắc làm việc với API

- Dùng endpoint và cấu trúc dữ liệu thống nhất trong API contract.
- Kiểm tra xác thực và phân quyền ở các endpoint cần bảo vệ.
- Trả mã trạng thái HTTP phù hợp.
- Không trả thông tin nhạy cảm trong response hoặc log.
- Cập nhật tài liệu khi thay đổi route hoặc request/response.

### 16.4. Git cơ bản

```bash
git status
git pull
git checkout -b feature/ten-chuc-nang
git add .
git commit -m "feat: mo ta thay doi"
git push -u origin feature/ten-chuc-nang
```

Tên nhánh và nội dung commit cần phản ánh đúng phần việc được thực hiện. Trước khi tạo Pull Request, cần kiểm tra thay đổi và chạy build/test phù hợp.

---

## 17. Tài liệu liên quan

| Tài liệu | Nội dung |
|---|---|
| [`docs/README.md`](docs/README.md) | Hướng dẫn đọc và sử dụng tài liệu dự án. |
| [`docs/PROJECT-TREE.md`](docs/PROJECT-TREE.md) | Cấu trúc thư mục chi tiết. |
| [`docs/API-CONTRACT-GUIDE.md`](docs/API-CONTRACT-GUIDE.md) | Quy ước giao tiếp giữa Frontend và Backend. |
| [`docs/api-hoc-vu.md`](docs/api-hoc-vu.md) | Tài liệu API học vụ. |
| [`docs/FE01-user-account-management.md`](docs/FE01-user-account-management.md) | Quản lý tài khoản người dùng. |
| [`docs/FE02-subject-syllabus-management.md`](docs/FE02-subject-syllabus-management.md) | Quản lý môn học và đề cương. |
| [`docs/FE03-class-management.md`](docs/FE03-class-management.md) | Quản lý lớp học. |
| [`docs/FE04-project-management.md`](docs/FE04-project-management.md) | Quản lý dự án. |
| [`docs/FE05-team-workspace-management.md`](docs/FE05-team-workspace-management.md) | Quản lý nhóm và không gian làm việc. |
| [`docs/FE06-milestone-checkpoint-management.md`](docs/FE06-milestone-checkpoint-management.md) | Quản lý cột mốc và điểm kiểm tra. |
| [`docs/FE07-communication-meetings.md`](docs/FE07-communication-meetings.md) | Giao tiếp và cuộc họp. |
| [`docs/FE08-resource-management.md`](docs/FE08-resource-management.md) | Quản lý tài nguyên. |
| [`docs/FE09-realtime-collaboration-tools.md`](docs/FE09-realtime-collaboration-tools.md) | Công cụ cộng tác thời gian thực. |
| [`docs/FE10-assessment-feedback.md`](docs/FE10-assessment-feedback.md) | Đánh giá và phản hồi. |
| [`docs/FE11-ai-assistance.md`](docs/FE11-ai-assistance.md) | Hỗ trợ học tập bằng AI. |
| [`docs/FE12-notification-management.md`](docs/FE12-notification-management.md) | Quản lý thông báo. |
| [`database/01-erd.md`](database/01-erd.md) | Mô hình thực thể và quan hệ dữ liệu. |
| [`database/schema.sql`](database/schema.sql) | Schema MySQL tổng hợp. |
| [`backend/README.md`](backend/README.md) | Hướng dẫn kỹ thuật chi tiết cho Backend. |
| [`frontend/README.md`](frontend/README.md) | Tài liệu Frontend. |

---

## 18. Phạm vi triển khai hiện tại

Phần này giúp phân biệt giữa **phạm vi chức năng của toàn dự án** và **những thành phần đã có trong repository** tại thời điểm cập nhật README.

### 18.1. Backend

Theo tài liệu Backend và cấu trúc source hiện tại:

- Đã có các project API, Application, Domain và Infrastructure trên .NET 10.
- Đã có cấu hình kết nối MySQL, JWT Bearer và bộ chạy SQL migration.
- Đã có các migration `001` đến `007` và nhóm file seed.
- Các API học vụ hiện có tập trung vào xác thực, môn học, đề cương, lớp học và phân công thành viên lớp.
- Database đã có cấu trúc cho nhiều nhóm nghiệp vụ khác; không đồng nghĩa API của toàn bộ các nhóm đó đã hoàn thiện.

### 18.2. Frontend

- Có cấu trúc trang, component, service, route, context và tiện ích.
- Source đang chứa kết hợp `.jsx`, `.razor` và `.cs`.
- Cấu hình package hiện tại chưa đủ để khẳng định toàn bộ giao diện có thể chạy bằng một lệnh npm tiêu chuẩn.
- Cần thống nhất hướng triển khai frontend và hoàn thiện cấu hình build/run trước khi xem đây là bản phát hành hoàn chỉnh.

### 18.3. AI Agent

- Có module Python riêng trong `backend/ai-agent/`.
- Có các nhóm model, route, service và agent.
- Cần kiểm tra cấu hình môi trường và luồng tích hợp thực tế trước khi xác nhận AI Agent đã kết nối hoàn chỉnh với API chính.

### 18.4. Nguyên tắc cập nhật trạng thái

README này không đánh dấu toàn bộ FE01–FE12 là đã hoàn thành. Khi một chức năng được triển khai, cần cập nhật trạng thái dựa trên code, API, giao diện và kết quả kiểm thử thực tế.

---

## 19. Định hướng phát triển

Các hướng phát triển tiếp theo của CollabSphere:

1. Hoàn thiện và thống nhất kiến trúc Frontend.
2. Mở rộng API cho các nghiệp vụ dự án, nhóm, nhiệm vụ, bài nộp và đánh giá.
3. Đồng bộ entity, mapping EF Core và schema MySQL.
4. Hoàn thiện kiểm soát quyền theo vai trò và theo phạm vi dữ liệu.
5. Hoàn thiện các chức năng cộng tác, thông báo và trao đổi thời gian thực.
6. Làm rõ cơ chế tích hợp AI Agent với Backend chính.
7. Bổ sung kiểm thử tự động và tài liệu API.
8. Chuẩn hóa cấu hình môi trường phát triển, kiểm thử và triển khai.

---

**CollabSphere — Học tập thông qua dự án, cộng tác để phát triển.**
