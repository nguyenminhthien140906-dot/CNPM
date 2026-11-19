# CollabSphere – Tài liệu API học vụ

Tài liệu này mô tả các API học vụ hiện được triển khai trong `backend/CollabSphere.API`. Phạm vi gồm môn học, đề cương, lớp học, phân công giảng viên, ghi danh sinh viên và quản lý tài khoản. Các endpoint được liệt kê dựa trên Controller hiện có; những chức năng chưa có Controller không được xem là API đã hoàn thành.

> Base URL phụ thuộc cấu hình chạy Backend. Ví dụ bên dưới dùng `http://localhost:5000`.

## 1. Môi trường và khởi chạy

Yêu cầu:
- .NET SDK 10.
- MySQL Server 8.0 trở lên, sử dụng InnoDB và `utf8mb4`.
- Git; PowerShell nếu chạy script smoke test.

Tạo database nếu chưa có:

```sql
CREATE DATABASE collabsphere
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
```

Mở terminal tại `backend/CollabSphere.API`, sau đó cấu hình User Secrets:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=collabsphere;User=root;Password=YOUR_MYSQL_PASSWORD;"
dotnet user-secrets set "Jwt:Key" "YOUR_BASE64_SECRET_KEY"
```

`Jwt:Key` phải là chuỗi Base64 giải mã thành ít nhất 32 bytes. Issuer và Audience hiện được đọc từ `appsettings.json` (`CollabSphere` và `CollabSphereUsers`). Không commit mật khẩu, JWT key hoặc dữ liệu bí mật lên GitHub.

Trong môi trường Development, muốn tạo dữ liệu mẫu thì cấu hình thêm:

```bash
dotnet user-secrets set "Seed:DefaultPassword" "YOUR_SEED_PASSWORD"
```

Chạy API:

```bash
dotnet run --urls "http://localhost:5000"
```

Khi khởi động, Backend tự áp dụng các file `*.up.sql` chưa chạy trong `database/migrations/versions`. Seed dữ liệu chỉ chạy khi môi trường là Development và có `Seed:DefaultPassword`.

## 2. Xác thực và phân quyền

Các endpoint học vụ (trừ đăng nhập và endpoint kiểm tra hệ thống) yêu cầu JWT Bearer. Đăng nhập để nhận token, sau đó gửi token trong header:

```http
Authorization: Bearer <accessToken>
```

Vai trò được sử dụng trong Backend:

| Role | Ý nghĩa |
|---|---|
| `ADMIN` | Quản trị hệ thống |
| `HEAD` | Trưởng bộ môn/khoa |
| `STAFF` | Nhân viên học vụ |
| `LECTURER` | Giảng viên |
| `STUDENT` | Sinh viên |

Quyền đọc danh sách/chi tiết môn học, đề cương và lớp học: người dùng đã đăng nhập. Các thao tác tạo, sửa, xóa và phân công/ghi danh: `ADMIN`, `HEAD`, `STAFF`. API quản trị người dùng chỉ dành cho `ADMIN`. Riêng API chọn danh sách giảng viên/sinh viên cũng giới hạn `ADMIN`, `HEAD`, `STAFF`.

## 3. Quy ước phản hồi

Các Controller học vụ sử dụng cấu trúc phản hồi thành công chung:

```json
{
  "success": true,
  "message": "Thành công",
  "data": {}
}
```

Một số endpoint đăng nhập hoặc quản trị có thể trả về các thuộc tính `Success`, `Message`, `Data` trong mã C#; JSON mặc định được serialize theo camelCase.

Lỗi chuẩn từ `ApiErrorResponse` có dạng:

```json
{
  "success": false,
  "error": {
    "code": "NOT_FOUND",
    "message": "Không tìm thấy dữ liệu."
  }
}
```

Với lỗi kiểm tra dữ liệu, `error.details` có thể chứa danh sách lỗi theo từng trường.

| HTTP | Mã lỗi thường dùng | Ý nghĩa |
|---:|---|---|
| 400 | `VALIDATION_ERROR` | Request không hợp lệ |
| 401 | `UNAUTHORIZED` | Thiếu hoặc sai token; thông tin đăng nhập không đúng |
| 403 | `FORBIDDEN` | Không đủ quyền |
| 404 | `NOT_FOUND` | Không tìm thấy tài nguyên |
| 409 | `CONFLICT` | Trùng dữ liệu hoặc vi phạm điều kiện nghiệp vụ |
| 500 | `INTERNAL_ERROR` | Lỗi máy chủ |
| 503 | `SERVICE_UNAVAILABLE` | Dịch vụ/cơ sở dữ liệu không sẵn sàng (endpoint trạng thái) |

## 4. API xác thực và kiểm tra hệ thống

| Method | Endpoint | Quyền | Mô tả |
|---|---|---|---|
| POST | `/api/auth/login` | Công khai | Đăng nhập bằng username hoặc email và mật khẩu |
| GET | `/api/auth/me` | Đã đăng nhập | Lấy UserId, Username, Email và Role từ token |
| GET | `/api/system/status` | Công khai | Kiểm tra API và khả năng kết nối database; trả 503 nếu database không kết nối được |
| GET | `/api/system/database-check` | Công khai | Kiểm tra khả năng truy cập bảng Roles và Users cùng mapping |

Ví dụ đăng nhập:

```http
POST /api/auth/login
Content-Type: application/json
```

```json
{
  "usernameOrEmail": "admin",
  "password": "YOUR_SEED_PASSWORD"
}
```

Kết quả thành công chứa `data.accessToken`. Dùng token đó cho các endpoint được bảo vệ.

## 5. API môn học – Subjects

Base path: `/api/subjects`

| Method | Endpoint | Quyền | Mô tả |
|---|---|---|---|
| GET | `/api/subjects?keyword=` | Đã đăng nhập | Danh sách môn; tìm theo mã hoặc tên |
| GET | `/api/subjects/{id}` | Đã đăng nhập | Chi tiết môn, kèm số syllabus và số lớp |
| GET | `/api/subjects/{id}/syllabus` | Đã đăng nhập | Thông tin môn và danh sách đề cương, mới nhất trước |
| POST | `/api/subjects` | ADMIN, HEAD, STAFF | Tạo môn học |
| PUT | `/api/subjects/{id}` | ADMIN, HEAD, STAFF | Cập nhật môn học |
| DELETE | `/api/subjects/{id}` | ADMIN, HEAD, STAFF | Xóa môn nếu chưa có lớp liên quan |

Body tạo/cập nhật:

```json
{
  "subjectCode": "CNPM",
  "subjectName": "Công nghệ phần mềm",
  "description": "Mô tả môn học"
}
```

Quy tắc chính: `subjectCode` không được trùng. Xóa môn đang được lớp học tham chiếu sẽ bị từ chối với HTTP 409.

## 6. API đề cương – Syllabi

Base path: `/api/syllabi`

| Method | Endpoint | Quyền | Mô tả |
|---|---|---|---|
| GET | `/api/syllabi?subjectId=` | Đã đăng nhập | Danh sách đề cương; có thể lọc theo môn |
| GET | `/api/syllabi/{id}` | Đã đăng nhập | Chi tiết đề cương, bao gồm nội dung |
| POST | `/api/syllabi` | ADMIN, HEAD, STAFF | Tạo đề cương |
| PUT | `/api/syllabi/{id}` | ADMIN, HEAD, STAFF | Cập nhật đề cương |
| DELETE | `/api/syllabi/{id}` | ADMIN, HEAD, STAFF | Xóa đề cương nếu không bị dữ liệu khác sử dụng |

Body tạo/cập nhật:

```json
{
  "subjectId": 1,
  "version": "v1",
  "title": "Đề cương học phần",
  "content": "Nội dung đề cương..."
}
```

`subjectId` phải tham chiếu môn học tồn tại. Cặp môn học và phiên bản không được trùng; vi phạm sẽ trả HTTP 409.

## 7. API lớp học – Classes

Base path: `/api/classes`

| Method | Endpoint | Quyền | Mô tả |
|---|---|---|---|
| GET | `/api/classes?subjectId=&status=` | Đã đăng nhập | Danh sách lớp, có thể lọc theo môn và trạng thái |
| GET | `/api/classes/{id}` | Đã đăng nhập | Chi tiết lớp |
| POST | `/api/classes` | ADMIN, HEAD, STAFF | Tạo lớp |
| PUT | `/api/classes/{id}` | ADMIN, HEAD, STAFF | Cập nhật toàn bộ thông tin lớp |
| PATCH | `/api/classes/{id}` | ADMIN, HEAD, STAFF | Cập nhật một phần thông tin lớp |
| PATCH | `/api/classes/{id}/status` | ADMIN, HEAD, STAFF | Thay đổi trạng thái lớp |
| DELETE | `/api/classes/{id}` | ADMIN, HEAD, STAFF | Xóa lớp nếu không có dự án liên quan |

Ví dụ body tạo lớp:

```json
{
  "subjectId": 1,
  "classCode": "CNPM-01",
  "className": "Công nghệ phần mềm - Lớp 01",
  "semester": "HK1",
  "academicYear": "2026-2027",
  "capacity": 40,
  "status": "OPEN"
}
```

Trạng thái lớp được chấp nhận: `PLANNED`, `OPEN`, `CLOSED`, `ARCHIVED`.

Ví dụ PATCH một phần:

```json
{
  "className": "Công nghệ phần mềm - Nhóm 01"
}
```

Ví dụ đổi trạng thái:

```json
{
  "status": "CLOSED"
}
```

Lưu ý nghiệp vụ:
- Mã lớp không được trùng.
- Sức chứa không được nhỏ hơn số sinh viên đã ghi danh.
- Không thể thêm sinh viên vào lớp đã đóng hoặc lớp đã đủ sức chứa.
- Xóa lớp đang có dự án liên quan bị từ chối.
- `PUT` cập nhật toàn bộ; `PATCH` chỉ cập nhật trường được gửi.

## 8. API phân công giảng viên và ghi danh sinh viên

Các endpoint này được khai báo trong `ClassAssignmentsController`, dùng base path `/api`.

| Method | Endpoint | Quyền | Mô tả |
|---|---|---|---|
| GET | `/api/lecturers` | ADMIN, HEAD, STAFF | Danh sách giảng viên để chọn |
| GET | `/api/students` | ADMIN, HEAD, STAFF | Danh sách sinh viên để chọn |
| GET | `/api/classes/{classId}/lecturers` | Đã đăng nhập | Danh sách giảng viên của lớp |
| POST | `/api/classes/{classId}/lecturers` | ADMIN, HEAD, STAFF | Phân công giảng viên |
| DELETE | `/api/classes/{classId}/lecturers/{lecturerId}` | ADMIN, HEAD, STAFF | Gỡ giảng viên khỏi lớp |
| GET | `/api/classes/{classId}/students` | Đã đăng nhập | Danh sách sinh viên của lớp |
| POST | `/api/classes/{classId}/students` | ADMIN, HEAD, STAFF | Ghi danh sinh viên vào lớp |
| DELETE | `/api/classes/{classId}/students/{studentId}` | ADMIN, HEAD, STAFF | Xóa sinh viên khỏi lớp |

Body phân công giảng viên:

```json
{
  "lecturerId": 1,
  "assignmentRole": "LECTURER"
}
```

Body ghi danh sinh viên:

```json
{
  "studentId": 1
}
```

Phân công trùng hoặc ghi danh trùng bị từ chối. Việc ghi danh cũng kiểm tra lớp tồn tại, trạng thái lớp và sức chứa.

## 9. API quản lý người dùng

Base path: `/api/admin`; Controller áp dụng policy `ADMIN` cho toàn bộ endpoint.

| Method | Endpoint | Quyền | Mô tả |
|---|---|---|---|
| GET | `/api/admin/test` | ADMIN | Endpoint kiểm tra quyền quản trị |
| GET | `/api/admin/users?page=&pageSize=&search=` | ADMIN | Danh sách người dùng có phân trang và tìm kiếm |
| GET | `/api/admin/users/{id}` | ADMIN | Chi tiết người dùng |
| PATCH | `/api/admin/users/{id}/status` | ADMIN | Khóa hoặc mở tài khoản |

Ví dụ cập nhật trạng thái:

```json
{
  "isActive": false
}
```

Danh sách người dùng hỗ trợ tìm theo thông tin tài khoản; `pageSize` tối đa 100. Không được tự khóa tài khoản đang đăng nhập.

## 10. Kiểm thử API

Script kiểm thử hiện có: `backend/tests/api-smoke-test.ps1`. Script gồm 46 kiểm tra theo các luồng kết nối database, đăng nhập, phân quyền, môn học, đề cương, lớp học, phân công giảng viên, ghi danh sinh viên và xóa dữ liệu kiểm thử.

Khởi động Backend trước, sau đó chạy PowerShell tại thư mục repository:

```powershell
powershell -ExecutionPolicy Bypass -File .\backend\tests\api-smoke-test.ps1 -BaseUrl "http://localhost:5000" -Password "YOUR_SEED_PASSWORD"
```

Script tạo dữ liệu kiểm thử (mã môn/lớp bắt đầu bằng `TEST`) và có bước dọn dữ liệu. Nên chạy trên database phát triển, không chạy trên dữ liệu production.

Các trường hợp kiểm tra thủ công nên bổ sung:
1. Gọi endpoint cần xác thực mà không gửi token: phải bị từ chối 401.
2. Dùng tài khoản `STUDENT` gọi endpoint ghi học vụ: phải bị từ chối 403.
3. Gửi request thiếu trường bắt buộc hoặc trạng thái không hợp lệ: kiểm tra lỗi 400.
4. Tạo mã môn, mã lớp hoặc cặp môn/phiên bản đề cương trùng: kiểm tra lỗi 409.
5. Ghi danh sinh viên vào lớp đóng/đầy: kiểm tra lỗi 409.

## 11. Vị trí mã nguồn liên quan

| Thành phần | Đường dẫn |
|---|---|
| Auth Controller | `backend/CollabSphere.API/Controllers/AuthController.cs` |
| Subjects Controller | `backend/CollabSphere.API/Controllers/SubjectsController.cs` |
| Syllabi Controller | `backend/CollabSphere.API/Controllers/SyllabiController.cs` |
| Study Classes Controller | `backend/CollabSphere.API/Controllers/StudyClassesController.cs` |
| Phân công lớp | `backend/CollabSphere.API/Controllers/ClassAssignmentsController.cs` |
| Request models học vụ | `backend/CollabSphere.API/Controllers/AcademicContracts.cs` |
| Admin Controller | `backend/CollabSphere.API/Controllers/AdminController.cs` |
| Cấu hình JWT, DI, Hub và startup | `backend/CollabSphere.API/Program.cs` |
| Định dạng lỗi | `backend/CollabSphere.API/ApiErrorResponse.cs` |
| Migration runner | `backend/CollabSphere.Infrastructure/Database/MigrationEnvironment.cs` |
| Seed runner | `backend/CollabSphere.Infrastructure/Database/DatabaseSeeder.cs` |
| SQL migration | `database/migrations/versions/` |
| SQL seed | `database/seeds/` |
| Smoke test | `backend/tests/api-smoke-test.ps1` |

## 12. Quy tắc cập nhật tài liệu

Khi thay đổi API, cần cập nhật đồng thời Controller, request/response model, phân quyền, kiểm thử và tài liệu này. Chỉ đánh dấu endpoint là hoàn thành khi route thực tế đã được triển khai và kiểm thử; chức năng mới nằm trong ERD hoặc tài liệu thiết kế không đồng nghĩa API đã sẵn sàng.
