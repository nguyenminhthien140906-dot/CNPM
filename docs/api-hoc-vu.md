# CollabSphere - Bàn giao Database và API học vụ
 
Phụ trách: Doãn Duy. Người kiểm thử: Thiện.
 
## 1. Cài đặt môi trường
 
Yêu cầu: .NET SDK 10, MySQL 8.0 trở lên.
 
1. Tạo database trống:
```sql
CREATE DATABASE collabsphere CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```
 
2. Đặt cấu hình bí mật (không ghi vào file đưa lên GitHub):
```bash
cd backend/CollabSphere.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=collabsphere;User=root;Password=MAT_KHAU;"
dotnet user-secrets set "Jwt:Key" "$(openssl rand -base64 48)"
dotnet user-secrets set "Seed:DefaultPassword" "Collab@12345"
```
 
Trên PowerShell không có `openssl`, dùng lệnh này cho `Jwt:Key`:
 
```powershell
dotnet user-secrets set "Jwt:Key" ([Convert]::ToBase64String((1..48 | ForEach-Object { Get-Random -Maximum 256 })))
```
 
3. Chạy:
```bash
dotnet run
```
 
Lần chạy đầu tiên, backend tự chạy 7 file SQL migration (001 đến 007) rồi seed dữ liệu mẫu. Log phải có 7 dòng `[Migration] Applied` và 5 dòng `[Seed] OK`. Chạy lại thì migration hiện `Skipped`.
 
Lưu ý: seed chỉ chạy khi môi trường là Development.
 
## 2. Tài khoản mẫu
 
Mật khẩu chung là giá trị đã đặt ở `Seed:DefaultPassword`.
 
| Username | Vai trò |
|---|---|
| admin | ADMIN |
| head01 | HEAD |
| staff01 | STAFF |
| lecturer01 | LECTURER |
| student01 | STUDENT |
| student02 | STUDENT |
 
## 3. Đăng nhập
 
`POST /api/auth/login`
 
```json
{ "usernameOrEmail": "admin", "password": "Collab@12345" }
```
 
Lấy `data.accessToken` trong kết quả, gửi kèm mọi request sau theo dạng header `Authorization: Bearer <token>`.
 
## 4. Định dạng phản hồi
 
```json
{ "success": true, "message": "Thành công", "data": { } }
```
 
Mã trạng thái:
 
| Mã | Ý nghĩa |
|---|---|
| 200, 201 | Thành công |
| 400 | Dữ liệu gửi lên không hợp lệ |
| 401 | Chưa đăng nhập hoặc token sai |
| 403 | Không đủ quyền |
| 404 | Không tìm thấy |
| 409 | Xung đột (trùng mã, đủ sĩ số, còn dữ liệu liên quan) |
| 500 | Lỗi máy chủ, xem log của backend |
 
## 5. Danh sách API
 
Quyền đọc: mọi người dùng đã đăng nhập. Quyền ghi (POST, PUT, DELETE): ADMIN, HEAD, STAFF.
 
### Môn học
 
| Method | Đường dẫn | Ghi chú |
|---|---|---|
| GET | /api/subjects?keyword= | Tìm theo mã hoặc tên |
| GET | /api/subjects/{id} | Có số syllabus và số lớp |
| POST | /api/subjects | 409 nếu trùng mã |
| PUT | /api/subjects/{id} | |
| DELETE | /api/subjects/{id} | 409 nếu môn đang có lớp |
 
Body POST và PUT:
 
```json
{ "subjectCode": "CNPM", "subjectName": "Công nghệ phần mềm", "description": "..." }
```
 
### Syllabus
 
| Method | Đường dẫn | Ghi chú |
|---|---|---|
| GET | /api/syllabi?subjectId= | |
| GET | /api/syllabi/{id} | Có nội dung đầy đủ |
| POST | /api/syllabi | 409 nếu trùng (môn, phiên bản) |
| PUT | /api/syllabi/{id} | |
| DELETE | /api/syllabi/{id} | 409 nếu dự án đang dùng |
 
Body:
 
```json
{ "subjectId": 1, "version": "v1", "title": "Đề cương", "content": "..." }
```
 
### Lớp học
 
| Method | Đường dẫn | Ghi chú |
|---|---|---|
| GET | /api/classes?subjectId=&status= | Có số sinh viên, số giảng viên |
| GET | /api/classes/{id} | |
| POST | /api/classes | 409 nếu trùng mã lớp |
| PUT | /api/classes/{id} | 409 nếu sĩ số tối đa nhỏ hơn số sinh viên hiện có |
| DELETE | /api/classes/{id} | 409 nếu lớp đang có dự án |
 
Body:
 
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
 
`status` chỉ nhận: PLANNED, OPEN, CLOSED, ARCHIVED.
 
### Phân công giảng viên và sinh viên
 
| Method | Đường dẫn | Ghi chú |
|---|---|---|
| GET | /api/lecturers | Danh sách giảng viên để chọn (ADMIN, HEAD, STAFF) |
| GET | /api/students | Danh sách sinh viên để chọn (ADMIN, HEAD, STAFF) |
| GET | /api/classes/{id}/lecturers | |
| POST | /api/classes/{id}/lecturers | Body: `{ "lecturerId": 1, "assignmentRole": "LECTURER" }`, 409 nếu trùng |
| DELETE | /api/classes/{id}/lecturers/{lecturerId} | |
| GET | /api/classes/{id}/students | |
| POST | /api/classes/{id}/students | Body: `{ "studentId": 1 }`, 409 nếu trùng, lớp đóng hoặc đủ sĩ số |
| DELETE | /api/classes/{id}/students/{studentId} | |
 
## 6. Kiểm thử
 
Chạy script tự động (Windows PowerShell), thay cổng theo dòng `Now listening on` của backend:
 
```powershell
cd backend/tests
powershell -ExecutionPolicy Bypass -File .\api-smoke-test.ps1 -BaseUrl "http://localhost:XXXX" -Password "Collab@12345"
```
 
Script tự tạo rồi xóa dữ liệu test (môn TEST01, lớp TEST-C1). Kết quả đúng: mọi dòng PASS, 0 FAIL.
 
Các tình huống Thiện nên thử thêm bằng tay:
 
1. Đăng nhập bằng `lecturer01` và `student01`, gọi các API ghi, phải ra 403.
2. Gửi body thiếu trường bắt buộc, phải ra 400.
3. Thêm sinh viên vào lớp `CLOSED`, phải ra 409.
4. Xóa môn đang có lớp, phải ra 409.
5. Dừng MySQL rồi gọi `GET /api/system/status`, phải ra 503.
## 7. Cấu trúc code
 
| Thành phần | Vị trí |
|---|---|
| Migration SQL (001 đến 007) | database/migrations/versions |
| Bộ chạy migration | backend/CollabSphere.Infrastructure/Database/MigrationEnvironment.cs |
| Bộ chạy seed | backend/CollabSphere.Infrastructure/Database/DatabaseSeeder.cs |
| Dữ liệu mẫu | database/seeds |
| Entity học vụ | backend/CollabSphere.Domain/Entities.cs |
| Mapping EF Core | backend/CollabSphere.Infrastructure/Configurations.cs |
| DbContext | backend/CollabSphere.Infrastructure/Persistence.cs |
| Controller học vụ | backend/CollabSphere.API/Controllers |
| Request model | backend/CollabSphere.API/Contracts/AcademicContracts.cs |
 
Schema được quản lý bằng file SQL, không dùng EF Core migration. Không gọi `MigrateAsync()` hay `EnsureCreatedAsync()`. Muốn đổi cấu trúc bảng, thêm file `008_*.up.sql` và `008_*.down.sql` vào `database/migrations/versions`.
