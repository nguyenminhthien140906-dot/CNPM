# CollabSphere – Backend

Backend của **CollabSphere**, hệ thống hỗ trợ tổ chức học tập theo phương pháp Project-Based Learning (PBL). Backend cung cấp API phục vụ xác thực và các nghiệp vụ học vụ.

## 1. Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Framework | ASP.NET Core Web API |
| Ngôn ngữ | C# |
| Runtime | .NET 10 |
| ORM | Entity Framework Core |
| Cơ sở dữ liệu | MySQL 8.0+ |
| SQL Connector | MySqlConnector |
| Xác thực | JWT Bearer |
| Mã hóa mật khẩu | ASP.NET Identity PasswordHasher |
| AI Agent | Python |

## 2. Chức năng chính

- Đăng nhập và xác thực người dùng bằng JWT.
- Quản lý môn học.
- Quản lý đề cương môn học.
- Quản lý lớp học.
- Phân công giảng viên vào lớp.
- Thêm và xóa sinh viên khỏi lớp.
- Kiểm tra trạng thái API và kết nối cơ sở dữ liệu.

Cơ sở dữ liệu còn có cấu trúc cho các nghiệp vụ dự án, nhóm, nhiệm vụ, bài nộp, đánh giá, thông báo và AI. Các nghiệp vụ này cần được triển khai API tương ứng.

## 3. Kiến trúc Backend

Backend được tổ chức thành 4 project theo kiến trúc phân lớp:

```text
backend/
│
├── CollabSphere.API/
│   ├── Controllers/
│   ├── Middleware/
│   ├── Properties/
│   ├── Program.cs
│   └── appsettings.json
│
├── CollabSphere.Application/
│   ├── DTOs/
│   ├── Interfaces/
│   └── Services/
│
├── CollabSphere.Domain/
│   ├── Entities/
│   ├── Enums/
│   └── Interfaces/
│
├── CollabSphere.Infrastructure/
│   ├── Persistence/
│   ├── Repositories/
│   ├── Services/
│   └── Migrations/
│
├── tests/
├── ai-agent/
│
├── CollabSphere.sln
├── CollabSphere.Backend.sln
└── README.md
```

### Vai trò các project

- **API:** Tiếp nhận HTTP Request, xử lý Controller, Middleware và xác thực.
- **Application:** Chứa DTO, interface và xử lý nghiệp vụ ứng dụng.
- **Domain:** Chứa Entity và các thành phần cốt lõi của hệ thống.
- **Infrastructure:** Xử lý kết nối MySQL, Entity Framework Core, Repository và Migration.

## 4. Yêu cầu môi trường

- .NET SDK 10.
- MySQL Server 8.0 trở lên.
- Git.
- Visual Studio hoặc Visual Studio Code.

Kiểm tra môi trường:

```bash
dotnet --version
mysql --version
```

## 5. Cài đặt và chạy Backend

### Bước 1. Mở thư mục Backend

```bash
cd backend
```

### Bước 2. Tạo Database

Chạy câu lệnh sau trong MySQL:

```sql
CREATE DATABASE collabsphere
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;
```

### Bước 3. Cấu hình User Secrets

Mở terminal tại thư mục `CollabSphere.API`:

```bash
cd CollabSphere.API
```

Cấu hình chuỗi kết nối MySQL:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=collabsphere;User=root;Password=YOUR_MYSQL_PASSWORD;"
```

Cấu hình JWT:

```bash
dotnet user-secrets set "Jwt:Key" "YOUR_BASE64_SECRET_KEY"
```

Cấu hình mật khẩu tài khoản mẫu:

```bash
dotnet user-secrets set "Seed:DefaultPassword" "YOUR_SEED_PASSWORD"
```

Không đưa mật khẩu MySQL, khóa JWT hoặc thông tin bí mật lên GitHub.

### Bước 4. Khôi phục và Build

```bash
dotnet restore
dotnet build
```

### Bước 5. Chạy Backend

```bash
dotnet run --urls "http://localhost:5000"
```

## 6. Kiểm tra API

Sau khi chạy Backend, có thể kiểm tra các endpoint:

| Method | Endpoint | Chức năng |
|---|---|---|
| GET | `/api/system/status` | Kiểm tra trạng thái API |
| GET | `/api/system/database-check` | Kiểm tra kết nối Database |
| POST | `/api/auth/login` | Đăng nhập |
| GET | `/api/auth/me` | Lấy thông tin người dùng hiện tại |
| GET | `/api/subjects` | Lấy danh sách môn học |
| GET | `/api/syllabi` | Lấy danh sách đề cương |
| GET | `/api/classes` | Lấy danh sách lớp học |

## 7. Database Migration

Các migration SQL được quản lý trong thư mục:

```text
database/
└── migrations/
    └── versions/
        ├── 001_*.up.sql
        ├── 002_*.up.sql
        ├── 003_*.up.sql
        └── ...
```

Cơ chế migration:

- Backend tự chạy các migration chưa được áp dụng khi khởi động.
- Phiên bản migration được lưu trong bảng `schema_migrations`.
- File `.up.sql` dùng để cập nhật cấu trúc Database.
- File `.down.sql` dùng để rollback thủ công.
- Không sử dụng `Database.MigrateAsync()` hoặc `EnsureCreatedAsync()` cho cơ chế migration SQL hiện tại.

Khi thay đổi cấu trúc Database, nên tạo migration phiên bản mới thay vì chỉnh sửa migration đã được áp dụng.

## 8. Kiểm thử

Chạy Backend trước, sau đó mở PowerShell tại thư mục `backend`:

```powershell
powershell -ExecutionPolicy Bypass -File .\tests\api-smoke-test.ps1 -BaseUrl "http://localhost:5000" -Password "YOUR_SEED_PASSWORD"
```

## 9. Solution

| File | Mục đích |
|---|---|
| `CollabSphere.sln` | Solution chính của Backend |
| `CollabSphere.Backend.sln` | Solution riêng cho các project Backend |

Build solution:

```bash
dotnet build CollabSphere.sln
```

## 10. Lưu ý phát triển

- Giữ thống nhất tên bảng, cột SQL và cấu hình ánh xạ trong C#.
- Tên bảng và cột Database sử dụng `snake_case`.
- Tên class và thuộc tính C# sử dụng `PascalCase`.
- Kiểm tra phân quyền đối với các API có thao tác ghi dữ liệu.
- Không commit thông tin bí mật lên GitHub.
- Build và kiểm thử trước khi tạo Pull Request.
