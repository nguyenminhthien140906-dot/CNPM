# FE7. Giao tiếp và họp trực tuyến
## 1. Mục tiêu
Cung cấp công cụ giao tiếp thời gian thực bao gồm chat, họp trực tuyến qua video/audio, chia sẻ màn hình và lên lịch cuộc họp; hỗ trợ Giảng viên, Sinh viên và các thành viên trao đổi hiệu quả trong quá trình thực hiện dự án.
## 2. Vai trò liên quan
Admin, Head Department, Staff, Lecturer, Student
## 3. Luồng nghiệp vụ
Người dùng mở giao diện chat hoặc danh sách cuộc họp trong dự án.
Hệ thống kiểm tra quyền truy cập và vai trò đối với dự án/phòng họp.
Hệ thống trả về lịch sử tin nhắn, danh sách cuộc họp hoặc chi tiết phòng họp trực tuyến.
Khi người dùng lên lịch họp, gửi tin nhắn hoặc bắt đầu/kết thúc cuộc họp, hệ thống cập nhật trạng thái và ghi nhận kết quả thời gian thực
## 4. Quy tắc nghiệp vụ
Thành viên thuộc dự án (Lecturer, Student) mới có quyền truy cập vào phòng chat và cuộc họp của dự án đó.
Chỉ Giảng viên (Lecturer) hoặc Trưởng nhóm/Thành viên được cấp quyền mới có thể lên lịch họp hoặc bắt đầu cuộc họp video/audio.
Tính năng chia sẻ màn hình chỉ khả dụng khi người dùng đang tham gia vào cuộc họp video/audio hoạt động.
Người không có quyền hoặc không thuộc dự án sẽ bị từ chối truy cập
## 5. API dự kiến
> Đây là hợp đồng API đề xuất để nhóm thống nhất trước khi code, chưa xác nhận là đã được triển khai.
|Phương thức  |   | Endpoint  |   | Mục đích  |
| GET |    | '/api/projects/{id}/messages   |   | Lấy lịch sử tin nhắn trò chuyện của dự án                                |
|POST |    | ' /api/projects/{id}/messages  |   |Gửi tin nhắn mới trong kênh chat (CHAT_MESSAGE)                           |
| GET |    | '/api/projects/{id}/meetings   |   |Lấy danh sách cuộc họp đã lên lịch của dự án                              |
|POST |    | '/api/meetings                 |   |Tạo/Lên lịch cuộc họp trực tuyến mới (CUOC_HOP)                           |
|POST |    | '/api/meetings/{id}/join       |   |Tham gia cuộc họp trực tuyến (lấy thông tin token/room)Ví dụ dữ liệu JSON |

Tạo cuộc họp mới (POST /api/projects/{projectId}/meetings):
JSON{
  "title": "Họp tiến độ Dự án Tuần 4",
  "startTime": "2026-11-01T09:00:00Z",
  "durationMinutes": 60,
  "isOnline": true
}
Cập nhật trạng thái cuộc họp (PATCH /api/meetings/{id}/status):
JSON{
  "status": "ONGOING"
}
## 6. Sơ đồ luồng chức năng
Đoạn mã
flowchart TD
    A[Người dùng bắt đầu] --> B{Có quyền thực hiện?}
    B -- Không --> C[Trả về lỗi quyền truy cập]
    B -- Có --> D[Kiểm tra dữ liệu đầu vào]
    D --> E{Dữ liệu hợp lệ?}
    E -- Không --> F[Thông báo lỗi và yêu cầu chỉnh sửa]
    E -- Có --> G[Thực hiện nghiệp vụ FE7]
    G --> H[Lưu kết quả và phản hồi]
## 7. Tiêu chí nghiệm thu
Người dùng có quyền thực hiện được các thao tác được mô tả trong tài liệu (chat, tạo cuộc họp, tham gia họp, chia sẻ màn hình).
Người dùng không thuộc dự án hoặc không có quyền bị từ chối truy cập hoặc thao tác.
Dữ liệu không hợp lệ (ví dụ: thời gian họp ở quá khứ, tiêu đề trống) được báo lỗi rõ ràng và không lưu trữ.
Phản hồi và trạng thái cuộc họp/tin nhắn được cập nhật thời gian thực cho tất cả người dùng liên quan.
