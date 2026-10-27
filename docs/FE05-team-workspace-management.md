FE5. Quản lý nhóm và không gian làm việc

1. Mục tiêu
Cung cấp chức năng giúp giảng viên khởi tạo, tổ chức không gian làm việc (workspace) và phân bổ danh sách thành viên vào các nhóm. Cung cấp biểu đồ/số liệu để giảng viên theo dõi tiến độ tổng thể và đánh giá mức độ đóng góp của từng cá nhân. Đồng thời, hỗ trợ trưởng nhóm cấp phát và quản trị công việc thực tế thông qua hệ thống thẻ (Card), cho phép chia nhỏ đầu việc thành Task, Subtask và các điểm kiểm tra (Checklist)[cite: 5].

2. Vai trò liên quan
Giảng viên (Lecturer), Trưởng nhóm (Leader), Thành viên (Member).

3. Luồng nghiệp vụ
1. Người dùng truy cập vào module quản lý không gian làm việc/nhóm.
2. Hệ thống kiểm tra vai trò để phân luồng giao diện (Giảng viên xem tổng quan; Sinh viên xem chi tiết nhóm).
3. Hệ thống trả về dữ liệu danh sách nhóm, dashboard tiến độ hoặc bảng công việc (board).
4. Khi người dùng thao tác nghiệp vụ (tạo nhóm, thêm Card, giao Task, tick Checklist...), hệ thống xác thực quyền, cập nhật DB và phản hồi kết quả lên UI.

4. Quy tắc nghiệp vụ
- Phân quyền cao nhất: Chỉ Giảng viên mới được phép tạo nhóm mới, gỡ/thêm sinh viên và xem báo cáo đóng góp của tất cả các nhóm.
- Phân quyền nhóm: Trưởng nhóm có toàn quyền cấu trúc công việc (tạo/xóa/chỉnh sửa Card, Task, Subtask, Checklist) trong phạm vi nhóm của mình[cite: 5].
- Thành viên bình thường chỉ có quyền thay đổi trạng thái (VD: kéo thả cột tiến độ, tick Done) đối với những công việc được phân công.
- Bảo mật: Chặn hoàn toàn việc người dùng can thiệp dữ liệu hoặc xem không gian làm việc của nhóm khác.

5. API dự kiến
> Đây là hợp đồng API đề xuất để nhóm thống nhất trước khi code, chưa xác nhận là đã được triển khai[cite: 2].

| Phương thức | Endpoint | Mục đích |
| :--- | :--- | :--- |
| GET | `/api/workspaces` | Load danh sách nhóm/không gian làm việc |
| POST | `/api/workspaces` | Giảng viên tạo không gian làm việc mới |
| GET | `/api/workspaces/{id}/reports` | Lấy data thống kê tiến độ và % đóng góp |
| POST | `/api/workspaces/{id}/cards` | Trưởng nhóm tạo thẻ công việc (Card) mới |
| PATCH | `/api/cards/{id}` | Cập nhật tiến độ của Task, Subtask, Checklist |

Ví dụ dữ liệu JSON (Payload cập nhật Card)
```json
{
  "cardTitle": "Thiết kế UI/UX module FE5",
  "assigneeId": "SE15000",
  "tasks": [
    {
      "name": "Lên wireframe",
      "isDone": true
    },
    {
      "name": "Design bản final",
      "isDone": false,
      "subtasks": [
        { "name": "Màn hình tạo Card", "isDone": false },
        { "name": "Màn hình Checklist", "isDone": false }
      ]
    }
  ],
  "checklists": [
    { "item": "Xin feedback Giảng viên", "checked": false }
  ]
}
