# CollabSphere - Backend

Backend của hệ thống CollabSphere (nền tảng hỗ trợ học tập theo dự án), viết bằng ASP.NET Core Web API trên .NET 10, dùng MySQL làm cơ sở dữ liệu và JWT để xác thực.

## Mục lục

1. [Tổng quan](#1-tổng-quan)
2. [Kiến trúc và cấu trúc thư mục](#2-kiến-trúc-và-cấu-trúc-thư-mục)
3. [Yêu cầu hệ thống](#3-yêu-cầu-hệ-thống)
4. [Cài đặt và chạy](#4-cài-đặt-và-chạy)
5. [Cấu hình](#5-cấu-hình)
6. [Quy trình khởi động](#6-quy-trình-khởi-động)
7. [Cơ sở dữ liệu](#7-cơ-sở-dữ-liệu)
8. [Dữ liệu mẫu](#8-dữ-liệu-mẫu)
9. [Xác thực và phân quyền](#9-xác-thực-và-phân-quyền)
10. [Tham chiếu API](#10-tham-chiếu-api)
11. [Quy tắc nghiệp vụ và kiểm tra dữ liệu](#11-quy-tắc-nghiệp-vụ-và-kiểm-tra-dữ-liệu)
12. [Kiểm thử](#12-kiểm-thử)
13. [Hướng dẫn phát triển](#13-hướng-dẫn-phát-triển)
14. [Xử lý sự cố](#14-xử-lý-sự-cố)
15. [Phạm vi hiện tại và hạn chế đã biết](#15-phạm-vi-hiện-tại-và-hạn-chế-đã-biết)

---

## 1. Tổng quan

| Hạng mục | Nội dung |
|---|---|
| Framework | ASP.NET Core Web API, .NET 10 |
| Truy cập dữ liệu | Entity Framework Core với `MySql.EntityFrameworkCore` |
| Quản lý schema | File SQL (`.up.sql`, `.down.sql`) chạy bằng `MySqlConnector` |
| Xác thực | JWT Bearer |
| Mã hóa mật khẩu | ASP.NET Identity `PasswordHasher` |
| Cơ sở dữ liệu | MySQL 8.0 trở lên, bộ ký tự `utf8mb4` |
| Định dạng phản hồi | JSON, tên thuộc tính dạng camelCase |

Các nhóm chức năng đã có API:

- Xác thực: đăng nhập, kiểm tra token.
- Học vụ: môn học, syllabus, lớp học, phân công giảng viên, đăng ký sinh viên vào lớp.

Database đã có sẵn 29 bảng cho các nhóm chức năng khác (dự án, nhóm, nhiệm vụ, nộp bài, họp, tin nhắn, workspace, đánh giá, thông báo, AI), nhưng API cho các nhóm này chưa được làm.

---

## 2. Kiến trúc và cấu trúc thư mục

Backend chia thành 4 project theo kiểu phân lớp:

```
CollabSphere.API  ──►  CollabSphere.Infrastructure  ──►  CollabSphere.Domain
        │                         │
        └──────────►  CollabSphere.Application  ◄───────┘
                                  │
                                  └──►  CollabSphere.Domain
```

| Project | Vai trò |
|---|---|
| `CollabSphere.Domain` | Entity (`User`, `Subject`, `StudyClass`...), enum, exception, value object. Không phụ thuộc project nào khác |
| `CollabSphere.Application` | DTO và giao diện của nghiệp vụ xác thực (`IAuthService`, `LoginRequestDto`...) |
| `CollabSphere.Infrastructure` | `ApplicationDbContext`, mapping EF Core, `AuthService`, bộ chạy migration và seed |
| `CollabSphere.API` | Controller, middleware xử lý lỗi, sinh JWT, cấu hình khởi động |

Cấu trúc thư mục:

```
backend/
├── CollabSphere.API/
│   ├── Controllers/
│   │   ├── ApiControllerBase.cs          Lớp cha: Success, CreatedOk, Failure
│   │   ├── AuthController.cs             /api/auth
│   │   ├── AdminController.cs            /api/admin
│   │   ├── SubjectsController.cs         /api/subjects
│   │   ├── SyllabiController.cs          /api/syllabi
│   │   ├── StudyClassesController.cs     /api/classes
│   │   ├── ClassAssignmentsController.cs /api/classes/{id}/lecturers|students, /api/lecturers, /api/students
│   │   └── AcademicContracts.cs          Request model của học vụ
│   ├── Services/JwtTokenService.cs       Sinh JWT
│   ├── Contracts.cs                      ApiResponse<T> và các model chung
│   ├── Controllers.cs                    SystemController: /api/system
│   ├── Middleware.cs                     Bắt lỗi chưa xử lý, trả 500
│   ├── Program.cs                        Điểm khởi động
│   └── appsettings.json
├── CollabSphere.Application/
│   └── Auth/                             IAuthService, AuthDtos
├── CollabSphere.Domain/
│   └── Entities.cs                       Toàn bộ entity
├── CollabSphere.Infrastructure/
│   ├── Database/
│   │   ├── MigrationEnvironment.cs       Chạy file SQL migration
│   │   └── DatabaseSeeder.cs             Chạy file seed, tạo password hash
│   ├── Services/AuthService.cs           Kiểm tra đăng nhập
│   ├── Configurations.cs                 Mapping bảng và cột cho EF Core
│   └── Persistence.cs                    ApplicationDbContext
├── tests/api-smoke-test.ps1              Script kiểm thử API
└── ai-agent/                             Module Python, tách riêng khỏi backend .NET
```

Thư mục nằm ngoài `backend/` mà backend sử dụng:

| Đường dẫn | Nội dung |
|---|---|
| `database/migrations/versions/` | 7 cặp file SQL migration (001 đến 007) |
| `database/seeds/` | 5 file dữ liệu mẫu |
| `database/schema.sql` | Schema tổng hợp, chỉ để tham khảo |
| `database/01-erd.md` | Mô tả ERD |

Hai file `.csproj` được cấu hình để chép `migrations/versions/*.up.sql` và `seeds/*.sql` vào thư mục output khi build, nên chạy được từ bất kỳ thư mục nào.

---

## 3. Yêu cầu hệ thống

- .NET SDK 10
- MySQL 8.0 trở lên (đã bật InnoDB, hỗ trợ `CHECK` constraint)
- Git
- Tùy chọn: PowerShell (để chạy script test), Postman hoặc curl

Kiểm tra phiên bản:

```bash
dotnet --version
mysql --version
```

---

## 4. Cài đặt và chạy

### Bước 1. Lấy mã nguồn

```bash
git clone <địa-chỉ-repository>
cd CNPM/backend
```

### Bước 2. Tạo database trống

```sql
CREATE DATABASE collabsphere
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
```

Không tạo bảng thủ công. Backend tự chạy migration.

### Bước 3. Đặt cấu hình bí mật

Không ghi mật khẩu hoặc khóa JWT vào file đưa lên GitHub. Dùng User Secrets:

```bash
cd CollabSphere.API

dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=collabsphere;User=root;Password=MAT_KHAU;"
dotnet user-secrets set "Jwt:Key" "$(openssl rand -base64 48)"
dotnet user-secrets set "Seed:DefaultPassword" "Collab@12345"
```

Trên Windows PowerShell không có `openssl`, dùng lệnh này cho `Jwt:Key`:

```powershell
dotnet user-secrets set "Jwt:Key" ([Convert]::ToBase64String((1..48 | ForEach-Object { Get-Random -Maximum 256 })))
```

### Bước 4. Build và chạy

```bash
dotnet build
dotnet run
```

Cổng được in ở dòng `Now listening on: http://localhost:XXXX`. Repository chưa có `launchSettings.json` nên cổng do .NET tự chọn. Muốn cố định cổng:

```bash
dotnet run --urls "http://localhost:5000"
```

### Bước 5. Kiểm tra

```bash
curl http://localhost:5000/api/system/status
curl http://localhost:5000/api/system/database-check
```

Kết quả đúng: `Status: Running`, `Database: Connected`, `ColumnMapping: Passed`.

---

## 5. Cấu hình

Thứ tự ưu tiên của .NET: biến môi trường, rồi User Secrets (chỉ ở Development), rồi `appsettings.json`.

| Khóa | Bắt buộc | Mặc định | Ý nghĩa |
|---|---|---|---|
| `ConnectionStrings:DefaultConnection` | Có | không | Chuỗi kết nối MySQL |
| `Jwt:Key` | Có | không | Khóa ký JWT, chuỗi Base64, giải mã ra tối thiểu 32 byte |
| `Jwt:Issuer` | Có | `CollabSphere` (trong `appsettings.json`) | Bên phát hành token |
| `Jwt:Audience` | Có | `CollabSphereUsers` (trong `appsettings.json`) | Đối tượng nhận token |
| `Jwt:ExpiresInMinutes` | Không | `60` | Thời hạn token, phải lớn hơn 0 |
| `Seed:DefaultPassword` | Không | không | Mật khẩu chung cho tài khoản mẫu. Thiếu thì bỏ qua seed |
| `ASPNETCORE_ENVIRONMENT` | Không | `Production` khi chạy trực tiếp | Seed chỉ chạy khi giá trị là `Development` |

Dùng biến môi trường thay cho User Secrets (dấu `:` đổi thành `__`):

```bash
export ConnectionStrings__DefaultConnection="Server=localhost;Port=3306;Database=collabsphere;User=root;Password=MAT_KHAU;"
export Jwt__Key="..."
export ASPNETCORE_ENVIRONMENT=Development
```

Lưu ý về chuỗi kết nối: ứng dụng dùng cùng một chuỗi cho hai thư viện (`MySql.EntityFrameworkCore` và `MySqlConnector`). Chỉ dùng các tham số cơ bản (`Server`, `Port`, `Database`, `User`, `Password`) để tránh khác biệt giữa hai thư viện.

---

## 6. Quy trình khởi động

Khi chạy `dotnet run`, `Program.cs` thực hiện theo thứ tự:

1. Đọc cấu hình, kiểm tra `DefaultConnection`, `Jwt:Key` (Base64, từ 32 byte), `Jwt:Issuer`, `Jwt:Audience`. Thiếu hoặc sai thì dừng ngay với thông báo lỗi.
2. Đăng ký `ApplicationDbContext` (MySQL), `AuthService`, `JwtTokenService`, JWT Bearer, Authorization.
3. Build ứng dụng.
4. Chạy migration: `MigrationEnvironment.ApplyPendingAsync`.
5. Nếu môi trường là Development và có `Seed:DefaultPassword`: chạy seed bằng `DatabaseSeeder.SeedAsync`.
6. Gắn middleware: bắt lỗi, `UseHttpsRedirection`, `UseAuthentication`, `UseAuthorization`, rồi map controller.

Nếu migration hoặc seed lỗi, ứng dụng dừng và không nhận request.

---

## 7. Cơ sở dữ liệu

### 7.1. Cơ chế migration

Schema được quản lý bằng file SQL trong `database/migrations/versions/`, không dùng EF Core migration.

`MigrationEnvironment.ApplyPendingAsync` làm như sau:

1. Tìm tất cả file `*.up.sql`, sắp xếp theo tên (nên đặt tên có số thứ tự đầu).
2. Tạo bảng `schema_migrations(version, applied_at)` nếu chưa có.
3. Với từng file: nếu `version` (tên file bỏ đuôi `.up.sql`) đã có trong `schema_migrations` thì bỏ qua (`Skipped`). Nếu chưa thì chạy toàn bộ nội dung file bằng một lệnh, rồi ghi `version` vào bảng (`Applied`).
4. Một file lỗi thì dừng toàn bộ và ném `InvalidOperationException` kèm tên migration.

Quy tắc khi viết migration:

- Không sửa file đã được áp dụng. Muốn đổi cấu trúc, thêm cặp file mới, ví dụ `008_ten_thay_doi.up.sql` và `008_ten_thay_doi.down.sql`.
- Không dùng `DELIMITER` trong file (mỗi file chạy bằng một lệnh).
- MySQL không rollback được lệnh DDL. Nếu file lỗi giữa chừng, các bảng đã tạo vẫn còn và migration đó chưa được ghi nhận. Trên môi trường dev, cách sạch nhất là xóa database rồi chạy lại.
- File `.down.sql` hiện chỉ để tham khảo và chạy tay, backend không tự chạy.
- Không gọi `Database.MigrateAsync()` hay `EnsureCreatedAsync()` vì chúng thuộc cơ chế khác và sẽ xung đột với migration SQL.

### 7.2. Danh sách 29 bảng

| Migration | Bảng |
|---|---|
| 001_identity_academic | `roles`, `users`, `students`, `lecturers`, `subjects`, `syllabi`, `study_classes`, `class_students`, `class_lecturers` |
| 002_projects_teams | `projects`, `teams`, `team_members` |
| 003_progress_tasks_submissions | `milestones`, `checkpoints`, `tasks`, `task_assignees`, `submissions` |
| 004_resources_communication | `resources`, `meetings`, `meeting_participants`, `messages` |
| 005_workspaces_documents | `workspaces`, `whiteboard_snapshots`, `workspace_documents`, `document_versions` |
| 006_evaluations_notifications_ai | `evaluations`, `notifications`, `ai_conversations`, `ai_messages` |
| 007_seed_roles | Chèn 5 vai trò mặc định, không tạo bảng |

Ngoài ra có bảng `schema_migrations` do bộ chạy migration tự tạo.

### 7.3. Các bảng học vụ

| Bảng | Khóa chính | Ràng buộc đáng chú ý |
|---|---|---|
| `subjects` | `subject_id` | `subject_code` UNIQUE |
| `syllabi` | `syllabus_id` | UNIQUE (`subject_id`, `version`). Xóa môn thì xóa theo (CASCADE) |
| `study_classes` | `class_id` | `class_code` UNIQUE. `capacity >= 0`. `status` chỉ nhận PLANNED, OPEN, CLOSED, ARCHIVED. Xóa môn bị chặn (RESTRICT) |
| `students` | `student_id` | `user_id` UNIQUE, `student_code` UNIQUE |
| `lecturers` | `lecturer_id` | `user_id` UNIQUE, `lecturer_code` UNIQUE |
| `class_students` | (`class_id`, `student_id`) | Xóa lớp hoặc sinh viên thì xóa theo |
| `class_lecturers` | (`class_id`, `lecturer_id`) | `assignment_role` mặc định `LECTURER` |

### 7.4. Mapping EF Core

Mapping nằm ở `CollabSphere.Infrastructure/Configurations.cs`. Hiện `ApplicationDbContext` mới map các bảng: `roles`, `users`, `projects`, `tasks`, `subjects`, `syllabi`, `study_classes`, `students`, `lecturers`, `class_students`, `class_lecturers`. Các bảng còn lại chỉ tồn tại ở database.

Một số quy ước trong mapping:

- Cột `created_at`, `updated_at` do MySQL đặt giá trị (`DEFAULT CURRENT_TIMESTAMP`). EF cấu hình `HasDefaultValueSql`, nên không cần gán trong code.
- Cột `is_active` của `users` không khai báo giá trị mặc định ở EF, vì giá trị CLR mặc định `false` sẽ làm EF bỏ qua và gây sai. Giá trị mặc định `true` nằm trong entity.
- `Project.Tasks` đang bị `Ignore` vì entity `WorkTask` chưa có khóa ngoại tới `Project`. Cần cập nhật khi làm API dự án.

---

## 8. Dữ liệu mẫu

Seed nằm ở `database/seeds/`, chạy theo thứ tự tên file khi ở Development.

| File | Nội dung |
|---|---|
| 001_seed_roles.sql | 5 vai trò (ghi đè mô tả nếu đã có) |
| 002_seed_users.sql | 6 người dùng, 1 giảng viên (`GV001`), 2 sinh viên (`SV001`, `SV002`) |
| 003_seed_subjects.sql | Môn `CNPM`, `CTDL`, `PBL` |
| 004_seed_syllabi.sql | Syllabus phiên bản `1.0` cho các môn |
| 005_seed_classes.sql | Lớp `CNPM-01`, `CTDL-01`, `PBL-01`, phân công giảng viên và đăng ký sinh viên |

Tính chất:

- Chạy lại nhiều lần an toàn: file dùng `NOT EXISTS` hoặc `ON DUPLICATE KEY`.
- File `002_seed_users.sql` chứa chuỗi `REPLACE_WITH_VALID_PASSWORD_HASH`. `DatabaseSeeder` thay chuỗi này bằng hash thật của `Seed:DefaultPassword` trước khi chạy. Không chạy file này trực tiếp trong MySQL Workbench, nếu không người dùng sẽ có hash giả và không đăng nhập được.
- Các file seed bắt đầu bằng `USE collabsphere;`. Đổi tên database thì phải sửa các file này.

### Tài khoản mẫu

Mật khẩu chung là giá trị của `Seed:DefaultPassword`.

| Username | Email | Vai trò |
|---|---|---|
| admin | admin@collabsphere.local | ADMIN |
| head01 | head01@collabsphere.local | HEAD |
| staff01 | staff01@collabsphere.local | STAFF |
| lecturer01 | lecturer01@collabsphere.local | LECTURER |
| student01 | student01@collabsphere.local | STUDENT |
| student02 | student02@collabsphere.local | STUDENT |

Chỉ dùng cho môi trường phát triển.

---

## 9. Xác thực và phân quyền

### 9.1. Luồng đăng nhập

1. Client gửi `POST /api/auth/login` với `usernameOrEmail` và `password`.
2. `AuthService` tìm người dùng theo `username` hoặc `email`, kiểm tra `is_active` và có vai trò, rồi so mật khẩu bằng `PasswordHasher`.
3. Đúng thì `JwtTokenService` sinh token ký HS256, thời hạn `Jwt:ExpiresInMinutes`.
4. Client gửi token ở mọi request sau: `Authorization: Bearer <token>`.

Nội dung token (claims): `sub`, `NameIdentifier` (user id), `Name` (username), `email`, `Role` (mã vai trò), `jti`.

Token hết hạn tính chính xác với độ lệch đồng hồ cho phép 30 giây.

### 9.2. Vai trò

| Mã | Ý nghĩa |
|---|---|
| ADMIN | Quản trị hệ thống |
| HEAD | Trưởng bộ phận |
| STAFF | Nhân viên quản lý (giáo vụ) |
| LECTURER | Giảng viên |
| STUDENT | Sinh viên |

### 9.3. Ma trận quyền của API học vụ

| Thao tác | ADMIN | HEAD | STAFF | LECTURER | STUDENT |
|---|:-:|:-:|:-:|:-:|:-:|
| Xem môn, syllabus, lớp, danh sách giảng viên và sinh viên của lớp | Có | Có | Có | Có | Có |
| Tạo, sửa, xóa môn, syllabus, lớp | Có | Có | Có | Không | Không |
| Phân công giảng viên, thêm hoặc xóa sinh viên khỏi lớp | Có | Có | Có | Không | Không |
| `GET /api/lecturers`, `GET /api/students` | Có | Có | Có | Không | Không |

Chưa đăng nhập: 401. Đăng nhập nhưng thiếu quyền: 403.

---

## 10. Tham chiếu API

### 10.1. Quy ước chung

Mọi phản hồi (trừ lỗi 500 từ middleware và lỗi 400 của validation tự động) có dạng:

```json
{
  "success": true,
  "message": "Thành công",
  "data": { }
}
```

Khi lỗi nghiệp vụ: `success` là `false`, `message` mô tả lỗi, `data` là `null`.

Lỗi 400 do validation (thiếu trường, sai độ dài, sai định dạng) do ASP.NET Core trả về theo chuẩn `ProblemDetails`, có trường `errors` liệt kê từng trường sai.

Mã trạng thái:

| Mã | Ý nghĩa |
|---|---|
| 200 | Thành công |
| 201 | Tạo mới thành công |
| 400 | Dữ liệu gửi lên không hợp lệ |
| 401 | Chưa đăng nhập hoặc token sai, hết hạn |
| 403 | Không đủ quyền |
| 404 | Không tìm thấy tài nguyên |
| 409 | Xung đột: trùng mã, lớp đủ sĩ số, còn dữ liệu liên quan |
| 500 | Lỗi máy chủ chưa xử lý. Nội dung lỗi chỉ có trong log |
| 503 | Mất kết nối database (`/api/system/status`) |

Tham số id trong đường dẫn có kiểu số nguyên (`{id:long}`). Truyền chuỗi không phải số sẽ trả 404.

### 10.2. Hệ thống

| Method | Đường dẫn | Quyền | Mô tả |
|---|---|---|---|
| GET | `/api/system/status` | Công khai | Trạng thái ứng dụng và kết nối database. Trả 503 khi mất kết nối |
| GET | `/api/system/database-check` | Công khai | Đếm `roles`, `users` để kiểm tra mapping cột |

### 10.3. Xác thực

**POST `/api/auth/login`** (công khai)

Yêu cầu:

```json
{ "usernameOrEmail": "admin", "password": "Collab@12345" }
```

Phản hồi 200:

```json
{
  "success": true,
  "message": "Login successful.",
  "data": {
    "accessToken": "eyJ...",
    "tokenType": "Bearer",
    "expiresIn": 3600,
    "user": {
      "userId": 1,
      "username": "admin",
      "email": "admin@collabsphere.local",
      "fullName": "System Administrator",
      "roleCode": "ADMIN"
    }
  }
}
```

Sai tài khoản hoặc mật khẩu, tài khoản bị khóa: 401.

**GET `/api/auth/me`** (cần đăng nhập): trả `userId`, `username`, `email`, `role` lấy từ token.

**GET `/api/admin/test`** (chỉ ADMIN): dùng kiểm tra phân quyền.

### 10.4. Môn học (`/api/subjects`)

| Method | Đường dẫn | Quyền | Mô tả |
|---|---|---|---|
| GET | `/api/subjects?keyword=` | Đăng nhập | Danh sách, lọc theo mã hoặc tên chứa `keyword`, sắp xếp theo mã |
| GET | `/api/subjects/{id}` | Đăng nhập | Chi tiết, có `syllabusCount`, `classCount` |
| POST | `/api/subjects` | ADMIN, HEAD, STAFF | Tạo môn. 409 nếu trùng mã |
| PUT | `/api/subjects/{id}` | ADMIN, HEAD, STAFF | Sửa môn. 409 nếu trùng mã với môn khác |
| DELETE | `/api/subjects/{id}` | ADMIN, HEAD, STAFF | Xóa môn. 409 nếu môn đang có lớp |

Body POST và PUT:

| Trường | Kiểu | Bắt buộc | Giới hạn |
|---|---|---|---|
| `subjectCode` | chuỗi | Có | Tối đa 30 ký tự, được cắt khoảng trắng đầu cuối |
| `subjectName` | chuỗi | Có | Tối đa 150 ký tự |
| `description` | chuỗi | Không | Không giới hạn |

Phản hồi GET danh sách (rút gọn):

```json
{
  "success": true,
  "message": "Thành công",
  "data": [
    {
      "subjectId": 1,
      "subjectCode": "CNPM",
      "subjectName": "Cong nghe phan mem",
      "description": "Nghien cuu quy trinh phat trien phan mem",
      "createdAt": "2026-11-11T10:00:00",
      "syllabusCount": 1,
      "classCount": 1
    }
  ]
}
```

### 10.5. Syllabus (`/api/syllabi`)

| Method | Đường dẫn | Quyền | Mô tả |
|---|---|---|---|
| GET | `/api/syllabi?subjectId=` | Đăng nhập | Danh sách, lọc theo môn. Không kèm nội dung `content` |
| GET | `/api/syllabi/{id}` | Đăng nhập | Chi tiết, có `content` |
| POST | `/api/syllabi` | ADMIN, HEAD, STAFF | 404 nếu môn không tồn tại. 409 nếu trùng (môn, phiên bản) |
| PUT | `/api/syllabi/{id}` | ADMIN, HEAD, STAFF | Kiểm tra tương tự POST |
| DELETE | `/api/syllabi/{id}` | ADMIN, HEAD, STAFF | 409 nếu có dự án đang dùng syllabus này |

Body POST và PUT:

| Trường | Kiểu | Bắt buộc | Giới hạn |
|---|---|---|---|
| `subjectId` | số | Có | Phải tồn tại |
| `version` | chuỗi | Có | Tối đa 30 ký tự |
| `title` | chuỗi | Có | Tối đa 200 ký tự |
| `content` | chuỗi | Không | Không giới hạn |

### 10.6. Lớp học (`/api/classes`)

| Method | Đường dẫn | Quyền | Mô tả |
|---|---|---|---|
| GET | `/api/classes?subjectId=&status=` | Đăng nhập | Danh sách, có `studentCount`, `lecturerCount`. `status` không phân biệt hoa thường |
| GET | `/api/classes/{id}` | Đăng nhập | Chi tiết |
| POST | `/api/classes` | ADMIN, HEAD, STAFF | 404 nếu môn không tồn tại. 409 nếu trùng mã lớp |
| PUT | `/api/classes/{id}` | ADMIN, HEAD, STAFF | 409 nếu trùng mã, hoặc `capacity` nhỏ hơn số sinh viên hiện có |
| DELETE | `/api/classes/{id}` | ADMIN, HEAD, STAFF | 409 nếu lớp có dự án |

Body POST và PUT:

| Trường | Kiểu | Bắt buộc | Giới hạn |
|---|---|---|---|
| `subjectId` | số | Có | Phải tồn tại |
| `classCode` | chuỗi | Có | Tối đa 30 ký tự, duy nhất |
| `className` | chuỗi | Có | Tối đa 150 ký tự |
| `semester` | chuỗi | Không | Tối đa 30 ký tự |
| `academicYear` | chuỗi | Không | Tối đa 20 ký tự |
| `capacity` | số nguyên | Không | Từ 0 trở lên. Bỏ trống là không giới hạn |
| `status` | chuỗi | Không | `PLANNED`, `OPEN`, `CLOSED`, `ARCHIVED`. Mặc định `OPEN` |

Ví dụ:

```json
{
  "subjectId": 1,
  "classCode": "CNPM-02",
  "className": "Công nghệ phần mềm - Lớp 02",
  "semester": "HK1",
  "academicYear": "2026-2027",
  "capacity": 40,
  "status": "OPEN"
}
```

Khi sửa lớp, `status` được gửi lại cùng body. Nếu bỏ trống, giá trị mặc định `OPEN` sẽ được áp dụng, nên cần gửi đủ trạng thái hiện tại khi cập nhật.

### 10.7. Giảng viên và sinh viên của lớp

| Method | Đường dẫn | Quyền | Mô tả |
|---|---|---|---|
| GET | `/api/lecturers` | ADMIN, HEAD, STAFF | Danh sách giảng viên để chọn |
| GET | `/api/students` | ADMIN, HEAD, STAFF | Danh sách sinh viên để chọn |
| GET | `/api/classes/{classId}/lecturers` | Đăng nhập | Giảng viên của lớp |
| POST | `/api/classes/{classId}/lecturers` | ADMIN, HEAD, STAFF | Phân công. 404 nếu lớp hoặc giảng viên không tồn tại, 409 nếu đã phân công |
| DELETE | `/api/classes/{classId}/lecturers/{lecturerId}` | ADMIN, HEAD, STAFF | Gỡ giảng viên. 404 nếu chưa được phân công |
| GET | `/api/classes/{classId}/students` | Đăng nhập | Sinh viên của lớp |
| POST | `/api/classes/{classId}/students` | ADMIN, HEAD, STAFF | Thêm sinh viên (xem điều kiện ở mục 11) |
| DELETE | `/api/classes/{classId}/students/{studentId}` | ADMIN, HEAD, STAFF | Xóa sinh viên khỏi lớp. 404 nếu không có trong lớp |

Body phân công giảng viên:

```json
{ "lecturerId": 1, "assignmentRole": "LECTURER" }
```

`assignmentRole` tối đa 50 ký tự, mặc định `LECTURER`.

Body thêm sinh viên:

```json
{ "studentId": 1 }
```

### 10.8. Ví dụ gọi bằng curl

```bash
# 1. Đăng nhập, lấy token
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"usernameOrEmail":"admin","password":"Collab@12345"}'

# 2. Dùng token
TOKEN="dán-accessToken-vào-đây"

curl http://localhost:5000/api/subjects -H "Authorization: Bearer $TOKEN"

curl -X POST http://localhost:5000/api/classes \
  -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \
  -d '{"subjectId":1,"classCode":"CNPM-02","className":"Lop 02","capacity":30}'

curl -X POST http://localhost:5000/api/classes/1/students \
  -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \
  -d '{"studentId":1}'
```

---

## 11. Quy tắc nghiệp vụ và kiểm tra dữ liệu

### Tính duy nhất

| Đối tượng | Quy tắc |
|---|---|
| Môn học | `subjectCode` duy nhất |
| Syllabus | (`subjectId`, `version`) duy nhất |
| Lớp học | `classCode` duy nhất |
| Phân công giảng viên | Mỗi giảng viên được phân công vào một lớp tối đa một lần |
| Đăng ký sinh viên | Mỗi sinh viên có trong một lớp tối đa một lần |

### Ràng buộc xóa

| Thao tác | Điều kiện bị chặn (409) |
|---|---|
| Xóa môn học | Môn đang có lớp. Syllabus của môn bị xóa theo |
| Xóa syllabus | Có dự án đang tham chiếu |
| Xóa lớp | Lớp đang có dự án. Danh sách giảng viên và sinh viên của lớp bị xóa theo |

### Đăng ký sinh viên vào lớp

Theo thứ tự kiểm tra:

1. Lớp tồn tại, nếu không: 404.
2. Lớp không ở trạng thái `CLOSED` hoặc `ARCHIVED`, nếu không: 409.
3. Sinh viên tồn tại, nếu không: 404.
4. Sinh viên chưa có trong lớp, nếu không: 409.
5. Nếu lớp có `capacity`: số sinh viên hiện tại phải nhỏ hơn `capacity`, nếu không: 409 "Lớp đã đủ sĩ số".

### Sửa lớp

Nếu gửi `capacity`, giá trị phải lớn hơn hoặc bằng số sinh viên hiện có trong lớp.

### Chuẩn hóa dữ liệu

Mã môn, mã lớp, phiên bản, tên được cắt khoảng trắng đầu cuối trước khi lưu và so sánh. So sánh trùng mã phụ thuộc collation của MySQL (`utf8mb4_unicode_ci` không phân biệt hoa thường).

---

## 12. Kiểm thử

### 12.1. Script tự động

Chạy backend ở một terminal, sau đó ở terminal khác (Windows PowerShell):

```powershell
cd backend/tests
powershell -ExecutionPolicy Bypass -File .\api-smoke-test.ps1 -BaseUrl "http://localhost:XXXX" -Password "Collab@12345"
```

Script chạy khoảng 38 kiểm tra theo nhóm:

| Nhóm | Nội dung |
|---|---|
| Kết nối | `database-check` trả 200 |
| Đăng nhập | Admin, student đăng nhập đúng. Sai mật khẩu trả 401 |
| Phân quyền | Không token trả 401. Student tạo môn trả 403 |
| Môn học | Danh sách, tạo, trùng mã (409), sửa, lấy theo id, không tồn tại (404) |
| Syllabus | Tạo, trùng phiên bản (409), môn không tồn tại (404), lọc theo môn |
| Lớp học | Tạo, trùng mã (409), status sai (400), danh sách |
| Giảng viên | Phân công, trùng (409), danh sách |
| Sinh viên | Thêm, trùng (409), đủ sĩ số (409), giảm capacity quá thấp (409) |
| Ràng buộc xóa | Xóa môn đang có lớp trả 409 |
| Dọn dẹp | Xóa dữ liệu test (TEST01, TEST-C1) |

Kết thúc in `Ket qua: N PASS, M FAIL`. Có dòng FAIL thì script thoát với mã 1.

### 12.2. Kiểm thử bằng tay

Các tình huống nên thử thêm:

1. Đăng nhập `lecturer01` hoặc `student01`, gọi API ghi: phải ra 403.
2. Gửi body thiếu trường bắt buộc: phải ra 400.
3. Thêm sinh viên vào lớp `CLOSED`: phải ra 409.
4. Dừng MySQL rồi gọi `GET /api/system/status`: phải ra 503.
5. Dùng token đã hết hạn (đặt `Jwt:ExpiresInMinutes` rất nhỏ): phải ra 401.

### 12.3. Kiểm tra trực tiếp trong MySQL

```sql
USE collabsphere;

SELECT * FROM schema_migrations ORDER BY version;
SELECT role_code, role_name FROM roles;
SELECT username, LEFT(password_hash, 12) AS hash_prefix FROM users;
```

Cột `hash_prefix` phải bắt đầu bằng `AQAAAAIAAYag` (định dạng của ASP.NET Identity).

---

## 13. Hướng dẫn phát triển

### Thêm một API mới theo mẫu hiện có

1. Nếu cần bảng mới: thêm cặp file `NNN_ten.up.sql` và `NNN_ten.down.sql` vào `database/migrations/versions/`.
2. Thêm entity vào `CollabSphere.Domain/Entities.cs`.
3. Thêm lớp `XxxConfiguration` vào `CollabSphere.Infrastructure/Configurations.cs`, ánh xạ đúng tên cột trong SQL (snake_case).
4. Trong `Persistence.cs`: thêm `DbSet<Xxx>` và dòng `modelBuilder.ApplyConfiguration(new XxxConfiguration())`.
5. Thêm request model vào `AcademicContracts.cs` (hoặc file contracts mới) với DataAnnotations (`[Required]`, `[StringLength]`...).
6. Tạo controller kế thừa `ApiControllerBase`, đặt `[Route("api/...")]` và `[Authorize]`. Với thao tác ghi, thêm `[Authorize(Roles = "ADMIN,HEAD,STAFF")]`.
7. Trả kết quả bằng `Success(data)`, `CreatedOk(data)` hoặc `Failure(statusCode, message)`.
8. Thêm kiểm tra vào `tests/api-smoke-test.ps1`.

### Quy ước code

- Tên cột database dạng `snake_case`, thuộc tính C# dạng `PascalCase`, ánh xạ rõ ràng bằng `HasColumnName`.
- Kiểu khóa là `long` (khớp `BIGINT`). Riêng `AuthUserDto.UserId` là `ulong`, nên cần ép kiểu `(ulong)` khi gán.
- Dùng `AsNoTracking()` cho truy vấn chỉ đọc, và `Select` để chỉ lấy cột cần thiết.
- Kiểm tra trùng và ràng buộc trước khi ghi để trả 409 rõ ràng thay vì để database ném lỗi.
- Không commit mật khẩu, khóa JWT hoặc chuỗi kết nối thật.

### Quy trình Git đề xuất

Làm trên nhánh riêng (ví dụ `feature/ten-chuc-nang`), chạy `dotnet build` và script test trước khi mở Pull Request vào `main`.

---

## 14. Xử lý sự cố

| Hiện tượng | Nguyên nhân và cách xử lý |
|---|---|
| `Không tìm thấy DefaultConnection` | Chưa đặt User Secrets hoặc biến môi trường. Lệnh `dotnet user-secrets` phải chạy trong thư mục `CollabSphere.API` |
| `Thiếu Jwt:Key` | Chưa đặt `Jwt:Key` |
| `JWT signing key phải có ít nhất 32 bytes` hoặc `FormatException` | `Jwt:Key` không phải Base64 hợp lệ hoặc quá ngắn. Tạo lại bằng 48 byte ngẫu nhiên |
| `Access denied for user` | Sai mật khẩu hoặc user MySQL |
| `Unknown database 'collabsphere'` | Chưa tạo database |
| `Unable to connect to any of the specified MySQL hosts` | MySQL chưa chạy, sai host hoặc port |
| `Không tìm thấy thư mục migration` | Chưa build lại sau khi sửa `.csproj`. Chạy `dotnet clean` rồi `dotnet build`, kiểm tra `bin/Debug/net10.0/migrations/versions` |
| `Migration ... thất bại: Table '...' already exists` | Database đã có bảng do chạy tay file SQL hoặc migration lỗi giữa chừng. Xóa database rồi chạy lại |
| Không thấy dòng `[Seed]` nào | Môi trường không phải Development. Chạy với `ASPNETCORE_ENVIRONMENT=Development` |
| `[Seed] Bỏ qua: chưa đặt Seed:DefaultPassword` | Đặt `Seed:DefaultPassword` bằng User Secrets |
| Đăng nhập tài khoản mẫu trả 401 | Seed chưa chạy, hoặc `users.password_hash` vẫn là `REPLACE_WITH_VALID_PASSWORD_HASH`. Xóa dữ liệu users rồi chạy lại seed |
| API trả 401 dù có token | Token hết hạn, hoặc `Jwt:Key`, `Issuer`, `Audience` khi chạy khác lúc sinh token |
| API trả 403 | Vai trò của tài khoản không nằm trong danh sách được phép (xem mục 9.3) |
| API trả 500 với thông báo chung | Middleware ẩn chi tiết lỗi. Xem log ở terminal đang chạy backend |
| `/api/system/database-check` báo lỗi mapping | Tên cột trong `Configurations.cs` khác với SQL. Lỗi cụ thể nằm trong trường `error` của phản hồi |
| Cảnh báo `Failed to determine the https port for redirect` | Đang chạy HTTP mà chưa cấu hình HTTPS. Chỉ là cảnh báo, không ảnh hưởng chức năng khi phát triển |

---

## 15. Phạm vi hiện tại và hạn chế đã biết

Đã hoàn thành:

- Database 29 bảng, migration SQL 001 đến 007, bộ chạy migration, dữ liệu mẫu.
- Mapping EF Core cho nhóm danh tính và học vụ.
- Xác thực JWT, phân quyền theo vai trò.
- API môn học, syllabus, lớp học, phân công giảng viên, đăng ký sinh viên.

Chưa có hoặc cần lưu ý:

- Chưa có API cho dự án, nhóm, nhiệm vụ, nộp bài, họp, tin nhắn, workspace, đánh giá, thông báo, AI. Các bảng đã có trong database nhưng chưa được map trong EF Core.
- Chưa có API quản lý người dùng (tạo, sửa, khóa tài khoản). Tài khoản hiện chỉ có từ seed.
- Chưa có phân trang cho các API danh sách. Phù hợp dữ liệu nhỏ, cần bổ sung khi dữ liệu lớn.
- Phân quyền hiện theo vai trò, chưa kiểm tra theo sở hữu (ví dụ giảng viên chỉ xem lớp mình phụ trách). Mọi người dùng đăng nhập đều xem được toàn bộ môn và lớp.
- Chưa cấu hình CORS. Frontend chạy ở origin khác sẽ bị trình duyệt chặn cho đến khi bổ sung.
- Chưa có `launchSettings.json` và cấu hình HTTPS cho môi trường phát triển.
- Middleware hiện trả lỗi 500 chung cho mọi ngoại lệ chưa xử lý, kể cả lỗi dữ liệu. Chi tiết chỉ có trong log.
- Khi hai request đăng ký sinh viên cùng lúc vào lớp sắp đầy, kiểm tra sĩ số có thể bị vượt (chưa khóa ở mức database).
- Chưa có kiểm thử đơn vị. Kiểm thử hiện là script gọi API thật.
- File `.down.sql` chưa được backend sử dụng.
