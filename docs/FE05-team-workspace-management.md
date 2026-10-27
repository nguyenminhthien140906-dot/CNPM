# FE5. Quản lý nhóm và không gian làm việc

## 1. Mục tiêu
Quản lý nhóm và không gian làm việc cho phép giảng viên khởi tạo nhóm, quản lý danh sách thành viên, theo dõi tiến độ công việc và đánh giá mức độ đóng góp của từng sinh viên. Đồng thời, hỗ trợ trưởng nhóm phân công công việc thông qua việc tạo thẻ công việc (Card), nhiệm vụ (Task), nhiệm vụ con (Subtask) và danh sách kiểm tra (Checklist).

## 2. Vai trò liên quan
Lecturer, Leader, Student

## 3. Luồng nghiệp vụ
1. Người dùng có quyền truy cập danh sách nhóm hoặc không gian làm việc.
2. Hệ thống kiểm tra quyền truy cập.
3. Hệ thống trả về danh sách nhóm, không gian làm việc hoặc chi tiết công việc.
4. Khi Giảng viên hoặc Trưởng nhóm cập nhật thông tin (tạo nhóm, giao thẻ công việc, cập nhật task, subtask, checklist), hệ thống cập nhật trạng thái và ghi nhận kết quả.

## 4. Quy tắc nghiệp vụ
- Chỉ Giảng viên mới có quyền tạo nhóm, phân công thành viên và xem thống kê tiến độ, đóng góp của tất cả các nhóm.
- Trưởng nhóm chỉ quản lý danh sách công việc trong phạm vi nhóm được giao (tạo Card, Task, Subtask, Checklist).
- Student chỉ được xem công việc và cập nhật trạng thái các task được phân công.
- Không cho phép người dùng không thuộc nhóm thực hiện các thao tác quản trị hoặc chỉnh sửa công việc.

## 5. API dự kiến
> Đây là hợp đồng API đề xuất để nhóm thống nhất trước khi code, chưa xác nhận là đã được triển khai.

| Phương thức | Endpoint | Mục đích |
| :--- | :--- | :--- |
| GET | `/api/workspaces` | Xem danh sách không gian làm việc |
| GET | `/api/workspaces/{id}` | Xem chi tiết nhóm và tiến độ công việc |
| POST | `/api/workspaces` | Giảng viên tạo không gian làm việc mới |
| POST | `/api/workspaces/{id}/cards` | Trưởng nhóm tạo thẻ công việc (Card) |
| PATCH | `/api/cards/{id}` | Cập nhật trạng thái Card, Task, Subtask, Checklist |

### Ví dụ dữ liệu JSON
```json
{
  "cardTitle": "Thiết kế cơ sở dữ liệu",
  "assigneeId": "SV12345",
  "tasks": [
    {
      "taskName": "Tạo ERD",
      "isCompleted": true,
      "subtasks": [
        {
          "title": "Vẽ bảng NHIEM_VU",
          "isCompleted": true
        }
      ]
    }
  ],
  "checklists": [
    {
      "item": "Review bài với Giảng viên",
      "isDone": false
    }
  ]
}
6. Sơ đồ luồng chức năng
Đoạn mã
flowchart TD
    A[Người dùng bắt đầu] --> B{Có quyền thực hiện?}
    B -- Không --> C[Trả về lỗi quyền truy cập]
    B -- Có --> D[Kiểm tra dữ liệu đầu vào]
    D --> E{Dữ liệu hợp lệ?}
    E -- Không --> F[Thông báo lỗi và yêu cầu chỉnh sửa]
    E -- Có --> G[Thực hiện nghiệp vụ FE5]
    G --> H[Lưu kết quả và phản hồi]
7. Tiêu chí nghiệm thu
Người dùng có quyền thực hiện được các thao tác được mô tả trong tài liệu.

Người dùng không có quyền bị từ chối truy cập hoặc thao tác.

Dữ liệu không hợp lệ được báo lỗi rõ ràng và không làm sai lệch dữ liệu đã lưu.

Kết quả thao tác được trả về để giao diện hiển thị cho người dùng.
