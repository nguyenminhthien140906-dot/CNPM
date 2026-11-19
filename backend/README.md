# CollabSphere – Backend

Backend của **CollabSphere**, hệ thống hỗ trợ học tập theo phương pháp Project-Based Learning (PBL). Backend cung cấp REST API, xác thực và phân quyền người dùng, truy cập MySQL, một số chức năng học vụ, giao tiếp thời gian thực và điểm tích hợp với AI Agent.

> Phạm vi triển khai cần phân biệt rõ: các API xác thực và học vụ có Controller cụ thể trong source; không phải toàn bộ module được mô tả trong cơ sở dữ liệu hoặc tài liệu yêu cầu đều đã có API hoàn chỉnh.

## 1. Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Framework | ASP.NET Core Web API |
| Ngôn ngữ | C# |
| Target framework | .NET 10 (`net10.0`) |
| ORM | Entity Framework Core |
| Database | MySQL 8.0+ |
| MySQL provider/driver | Pomelo/EF provider theo cấu hình project và MySqlConnector cho migration/seed |
| Authentication | JWT Bearer |
| Password hashing | ASP.NET Core Identity `PasswordHasher` |
| Realtime | SignalR |
| API description | ASP.NET Core OpenAPI (Development) |
| AI service | Python Flask service riêng |

## 2. Chức năng hiện có

Các nhóm chức năng có Controller hoặc service trong source hiện tại:

- **Xác thực:** đăng nhập bằng username/email và mật khẩu; lấy thông tin tài khoản hiện tại từ JWT.
- **Quản lý môn học:** danh sách, tìm kiếm, xem chi tiết, tạo, cập nhật và xóa môn học.
- **Quản lý đề cương:** danh sách, lọc theo môn, xem nội dung, tạo, cập nhật và xóa đề cương.
- **Quản lý lớp học:** danh sách, lọc, xem chi tiết, tạo, cập nhật toàn bộ/một phần, đổi trạng thái và xóa lớp.
- **Phân công và ghi danh:** xem danh sách giảng viên/sinh viên, phân công giảng viên, ghi danh và gỡ thành viên khỏi lớp.
- **Quản trị người dùng:** xem danh sách/chi tiết và khóa hoặc mở tài khoản (ADMIN).
- **Kiểm tra hệ thống:** kiểm tra trạng thái API và khả năng truy cập database.
- **Realtime:** khai báo SignalR Hub cho chat và thông báo.
- **Thông báo, cuộc họp và AI:** có Controller riêng; cần đối chiếu luồng nghiệp vụ và cấu hình dịch vụ phụ thuộc khi tích hợp.

Database có các migration cho dự án, nhóm, nhiệm vụ, tài nguyên, workspace, đánh giá, thông báo và AI. Việc có bảng hoặc migration không tự động có nghĩa toàn bộ API nghiệp vụ tương ứng đã được triển khai.

## 3. Kiến trúc và cấu trúc thư mục

Backend được chia thành các project theo hướng phân lớp:

```text
backend/
├── CollabSphere.API/
│   ├── Authorization/
│   ├── Controllers/
│   ├── Hubs/
│   ├── Services/
│   ├── Program.cs
│   └── appsettings.json
│
├── CollabSphere.Application/
│   ├── Admin/
│   ├── Auth/
│   ├── DTOs.cs
│   ├── Interfaces.cs
│   ├── Services.cs
│   └── Validators.cs
│
├── CollabSphere.Domain/
│   ├── Entities.cs
│   ├── Enums.cs
│   ├── Exceptions.cs
│   └── ValueObjects.cs
│
├── CollabSphere.Infrastructure/
│   ├── Database/
│   ├── Services/
│   ├── Configurations.cs
│   ├── Persistence.cs
│   └── Repositories.cs
│
├── ai-agent/
├── tests/
├── CollabSphere.sln
├── CollabSphere.Backend.sln
└── README.md
```

### Vai trò các project

- **CollabSphere.API:** điểm vào HTTP, Controllers, middleware, JWT authentication/authorization, SignalR Hub và cấu hình ứng dụng.
- **CollabSphere.Application:** DTO, interface và các thành phần xử lý nghiệp vụ ứng dụng.
- **CollabSphere.Domain:** entity, enum, exception và các mô hình lõi.
- **CollabSphere.Infrastructure:** DbContext, cấu hình ánh xạ, repository, dịch vụ truy cập dữ liệu, chạy SQL migration và seed.
- **ai-agent:** dịch vụ Python chạy riêng, không phải project .NET trong solution Backend.
- **tests:** script kiểm thử API bằng PowerShell.

Hai solution `.sln` nằm trong thư mục `backend`. Có thể build solution chính bằng `CollabSphere.sln`.

## 4. Yêu cầu môi trường

- .NET SDK 10.
- MySQL Server 8.0 trở lên.
- Git.
- Visual Studio 2022 phiên bản hỗ trợ .NET 10 hoặc Visual Studio Code kèm C# Dev Kit.
- PowerShell nếu chạy script smoke test.
- Python 3 nếu cần chạy AI Agent.

Kiểm tra cài đặt:

```bash
dotnet --version
mysql --version
python --version
```

## 5. Cài đặt và chạy Backend API

Thực hiện các bước sau từ thư mục gốc repository.

### Bước 1. Tạo database

Khởi động MySQL và tạo database:

```sql
CREATE DATABASE collabsphere
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
```

### Bước 2. Cấu hình User Secrets

Mở terminal tại thư mục API:

```bash
cd backend/CollabSphere.API
```

Thiết lập chuỗi kết nối:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=collabsphere;User=root;Password=YOUR_MYSQL_PASSWORD;"
```

Tạo JWT key dạng Base64 có tối thiểu 32 bytes sau khi giải mã. Ví dụ trên PowerShell:

```powershell
$keyBytes = New-Object byte[] 48
[System.Security.Cryptography.RandomNumberGenerator]::Fill($keyBytes)
$key = [Convert]::ToBase64String($keyBytes)
dotnet user-secrets set "Jwt:Key" $key
```

Trong `appsettings.json`, cấu hình hiện tại đã có:

```json
{
  "Jwt": {
    "Issuer": "CollabSphere",
    "Audience": "CollabSphereUsers"
  }
}
```

Không cần ghi JWT key vào `appsettings.json`. Không commit mật khẩu MySQL, JWT key hoặc User Secrets lên GitHub.

### Bước 3. Cấu hình dữ liệu mẫu (Development)

Nếu muốn chạy seed, đặt mật khẩu chung cho các tài khoản mẫu:

```bash
dotnet user-secrets set "Seed:DefaultPassword" "YOUR_SEED_PASSWORD"
```

Seeder chỉ chạy khi ASP.NET Core đang ở môi trường `Development` và cấu hình `Seed:DefaultPassword` không rỗng. Mật khẩu hash được tạo lúc seed bằng `PasswordHasher`; không lưu mật khẩu dạng rõ trong file seed.

### Bước 4. Restore và build

Từ thư mục `backend`:

```bash
dotnet restore CollabSphere.sln
dotnet build CollabSphere.sln
```

### Bước 5. Chạy API

Từ thư mục `backend/CollabSphere.API`:

```bash
dotnet run --urls "http://localhost:5000"
```

Mở `http://localhost:5000/api/system/status` để kiểm tra trạng thái API và database. Khi môi trường là Development, OpenAPI được ánh xạ tại `/openapi/v1.json`.

## 6. Database migration và seed

SQL migration được lưu tại repository root:

```text
database/
├── migrations/
│   └── versions/
│       ├── 001_identity_academic.up.sql
│       ├── 001_identity_academic.down.sql
│       ├── 002_projects_teams.up.sql
│       ├── 002_projects_teams.down.sql
│       └── ...
└── seeds/
    ├── 001_seed_roles.sql
    ├── 002_seed_users.sql
    ├── 003_seed_subjects.sql
    ├── 004_seed_syllabi.sql
    └── 005_seed_classes.sql
```

Cơ chế hiện tại:

- Khi khởi động, `MigrationEnvironment.ApplyPendingAsync` tìm các file `*.up.sql`, sắp xếp theo tên và chạy những phiên bản chưa có trong bảng `schema_migrations`.
- Mỗi migration thành công được ghi nhận trong `schema_migrations`.
- Các file `*.down.sql` là script rollback thủ công; startup không tự chạy chúng.
- Seed chạy riêng trong Development khi có `Seed:DefaultPassword`.
- Schema được quản lý bằng SQL migration. Không gọi `Database.MigrateAsync()` hoặc `EnsureCreatedAsync()` để thay thế cơ chế này.

Khi thay đổi schema, tạo migration phiên bản mới gồm file `.up.sql` và `.down.sql`; không sửa migration đã được áp dụng ở môi trường dùng chung nếu không có kế hoạch migration rõ ràng.

## 7. API và phân quyền

Một số endpoint chính:

| Method | Endpoint | Mô tả |
|---|---|---|
| POST | `/api/auth/login` | Đăng nhập, nhận JWT |
| GET | `/api/auth/me` | Thông tin người dùng từ JWT |
| GET | `/api/system/status` | Trạng thái API và database |
| GET | `/api/system/database-check` | Kiểm tra truy cập bảng identity |
| GET/POST/PUT/DELETE | `/api/subjects` | Quản lý môn học |
| GET/POST/PUT/DELETE | `/api/syllabi` | Quản lý đề cương |
| GET/POST/PUT/PATCH/DELETE | `/api/classes` | Quản lý lớp học |
| GET/POST/DELETE | `/api/classes/{id}/lecturers` | Phân công giảng viên |
| GET/POST/DELETE | `/api/classes/{id}/students` | Ghi danh sinh viên |
| GET/PATCH | `/api/admin/users` | Quản lý tài khoản (ADMIN) |

Các endpoint được bảo vệ yêu cầu header `Authorization: Bearer <token>`. Nhóm quyền quản lý học vụ là `ADMIN`, `HEAD`, `STAFF`; API quản trị người dùng yêu cầu `ADMIN`. Chi tiết route, request body và điều kiện nghiệp vụ xem tại [`docs/api-hoc-vu.md`](../docs/api-hoc-vu.md).

## 8. SignalR – giao tiếp thời gian thực

Backend đăng ký hai Hub:

| Hub | Đường dẫn |
|---|---|
| ChatHub | `/hubs/chat` |
| NotificationHub | `/hubs/notifications` |

JWT Bearer được đọc từ query parameter `access_token` cho các kết nối đến hai Hub trên. Client cần truyền access token khi thiết lập SignalR connection. Hub yêu cầu xác thực theo cấu hình hiện tại.

## 9. AI Agent

Thư mục `backend/ai-agent` chứa dịch vụ Python riêng. API .NET đăng ký một `HttpClient` tên `FlaskAI` với địa chỉ cơ sở `http://127.0.0.1:5001` và timeout 30 giây. Vì vậy, khi sử dụng chức năng phụ thuộc AI, cần chạy AI Agent tại địa chỉ/cổng tương ứng.

Cài dependencies (từ thư mục `backend/ai-agent`):

```bash
pip install -r requirements.txt
```

Lệnh khởi chạy và cấu hình chi tiết phụ thuộc `run.py`, `config/` và biến môi trường của AI Agent. Không coi AI Agent là tự động chạy cùng tiến trình ASP.NET Core.

## 10. Kiểm thử API

Script smoke test: `backend/tests/api-smoke-test.ps1`.

Khởi động MySQL, cấu hình seed và chạy Backend trước. Sau đó từ thư mục repository chạy PowerShell:

```powershell
powershell -ExecutionPolicy Bypass -File .\backend\tests\api-smoke-test.ps1 -BaseUrl "http://localhost:5000" -Password "YOUR_SEED_PASSWORD"
```

Script hiện có 46 assertion kiểm tra các luồng database, đăng nhập, phân quyền, môn học, đề cương, lớp học, phân công giảng viên và ghi danh sinh viên. Script tạo dữ liệu thử nghiệm và có thao tác dọn dữ liệu; chỉ chạy trên database phát triển/test, không chạy trực tiếp trên production.

## 11. Cấu hình và lưu ý phát triển

- `ConnectionStrings:DefaultConnection` phải trỏ đến MySQL đang hoạt động.
- `Jwt:Key` phải là Base64 và giải mã tối thiểu 32 bytes; Issuer/Audience phải khớp cấu hình phát hành token.
- Không commit secrets, mật khẩu, token hoặc dữ liệu cá nhân thật.
- Kiểm tra role trên mọi API có thao tác ghi hoặc truy cập dữ liệu theo người dùng.
- Duy trì nhất quán giữa SQL schema, Domain Entity và cấu hình mapping.
- Khi thêm API, cập nhật request/response contract, kiểm thử và tài liệu tương ứng.
- Không đánh dấu module hoàn thành chỉ vì đã có bảng dữ liệu hoặc giao diện mẫu; cần kiểm thử luồng tích hợp thực tế.

## 12. Repository

- Repository: https://github.com/nguyenminhthien140906-dot/CNPM
- Backend: `backend/`
- API contract và nghiệp vụ học vụ: `docs/api-hoc-vu.md`
- Hướng dẫn quy ước API: `docs/API-CONTRACT-GUIDE.md`
- Database schema/migration: `database/`
