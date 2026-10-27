# FE5. Quản lý nhóm và không gian làm việc

## 1. Mục tiêu
Cho phép Giảng viên tạo và quản lý nhóm, danh sách thành viên, theo dõi tiến độ và mức độ đóng góp của từng thành viên trong không gian làm việc. Đồng thời cho phép Trưởng nhóm quản lý công việc, tạo thẻ công việc (Card), nhiệm vụ (Task), nhiệm vụ con (Subtask) và các điểm kiểm tra (Checklist).

## 2. Vai trò liên quan
Lecturer (Giảng viên), Team Leader (Trưởng nhóm), Student/Member (Sinh viên / Thành viên nhóm)

## 3. Luồng nghiệp vụ
1. Người dùng có quyền truy cập vào danh sách nhóm và không gian làm việc.
2. Hệ thống kiểm tra quyền truy cập tương ứng với vai trò (Giảng viên, Trưởng nhóm, Thành viên).
3. Hệ thống trả về thông tin danh sách nhóm, không gian làm việc, tiến độ công việc hoặc chi tiết thẻ công việc.
4. Khi Giảng viên hoặc Trưởng nhóm thực hiện thao tác (tạo nhóm, giao việc, cập nhật thẻ công việc, nhiệm vụ con, điểm kiểm tra), hệ thống cập nhật trạng thái và ghi nhận kết quả.

## 4. Quy tắc nghiệp vụ
* Chỉ Giảng viên có quyền tạo nhóm, phân công/quản lý thành viên và theo dõi tiến độ, mức độ đóng góp của các nhóm thuộc phạm vi quản lý.
* Trưởng nhóm chỉ quản lý công việc trong phạm vi nhóm được giao, bao gồm tạo và quản lý các thẻ công việc (Card), nhiệm vụ (Task), nhiệm vụ con (Subtask) và điểm kiểm tra (Checklist).
* Thành viên thông thường chỉ được xem tiến độ và cập nhật trạng thái các nhiệm vụ được phân công.
* Không cho phép người dùng không có quyền thực hiện các thao tác quản trị nhóm hoặc chỉnh sửa công việc của người khác.

## 5. API dự kiến
> Đây là hợp đồng API đề xuất để nhóm thống nhất trước khi code, chưa xác nhận là đã được triển khai.

| Phương thức | Endpoint | Mục đích |
| :--- | :--- | :--- |
| **GET** | `/api/workspaces` | Xem danh sách không gian làm việc / nhóm |
| **GET** | `/api/workspaces/{id}` | Xem chi tiết không gian làm việc và tiến độ thành viên |
| **POST** | `/api/workspaces` | Giảng viên tạo nhóm / không gian làm việc mới |
| **POST** | `/api/workspaces/{id}/cards` | Trưởng nhóm tạo thẻ công việc (Card) mới |
| **PATCH** | `/api/cards/{id}` | Cập nhật trạng thái công việc, Task, Subtask, Checklist |

### Ví dụ dữ liệu JSON
```json
{
  "title": "Thiết kế cơ sở dữ liệu",
  "description": "Tạo các bảng cho tính năng quản lý nhóm",
  "assigneeId": "ST12345",
  "tasks": [
    {
      "taskName": "Tạo ERD",
      "isCompleted": true,
      "subtasks": [
        {
          "title": "Vẽ bảng User",
          "isCompleted": true
        }
      ]
    }
  ],
  "checklist": [
    {
      "item": "Review với giảng viên",
      "isDone": false
    }
  ]
}
