#FE7. Giao tiếp và họp trực tuyến
## 1. Mục tiêu
Hỗ trợ giảng viên và sinh viên trao đổi, tương tác trực tiếp trong quá trình thực hiện dự án (DU_AN). Chức năng cung cấp công cụ trò chuyện thời gian thực (CHAT_MESSAGE), tổ chức cuộc họp video/audio trực tuyến (CUOC_HOP), chia sẻ màn hình và lên lịch danh sách cuộc họp để đảm bảo tiến độ công việc.
# 2. Vai trò liên quan
GIANG_VIEN, SINH_VIEN
# 3. Luồng nghiệp vụ
1. Người dùng truy cập vào không gian giao tiếp của dự án (DU_AN).
2. Hệ thống kiểm tra quyền truy cập của người dùng đối với dự án.
3. Người dùng có thể gửi/nhận tin nhắn thời gian thực, lên lịch cuộc họp hoặc tham gia phiên họp video/audio.
4. Trong cuộc họp, người dùng có thể bật/tắt camera, mic và thực hiện chia sẻ màn hình.
5. Hệ thống ghi nhận trạng thái cuộc họp, lịch sử tin nhắn và danh sách thành viên tham gia.
## 4. Quy tắc nghiệp vụ
- Chỉ thành viên (Giảng viên, Sinh viên) thuộc nhóm dự án mới có quyền truy cập kênh chat và tham gia/tạo cuộc họp trực tuyến của dự án đó.
- Người tạo cuộc họp (hoặc Giảng viên) có quyền lên lịch (ThoiGianBatDau, ThoiGianKetThuc), chỉnh sửa hoặc hủy cuộc họp.
- Thời gian bắt đầu cuộc họp khi lên lịch phải lớn hơn hoặc bằng thời gian hiện tại.
- Tất cả tin nhắn chat (NoiDung, ThoiGianGui) và thông tin cuộc họp phải được lưu trữ và đồng bộ thời gian thực (Real-time) cho các thành viên trong dự án.
## 5. API dự kiến
> Đây là hợp đồng API đề xuất để nhóm thống nhất trước khi code, chưa xác nhận là đã được triển khai.
|Phương thức  |   | Endpoint  |   | Mục đích  |
| GET |    | '/api/projects/{id}/messages   |   | Lấy lịch sử tin nhắn trò chuyện của dự án                                |
|POST |    | ' /api/projects/{id}/messages  |   |Gửi tin nhắn mới trong kênh chat (CHAT_MESSAGE)                           |
| GET |    | '/api/projects/{id}/meetings   |   |Lấy danh sách cuộc họp đã lên lịch của dự án                              |
|POST |    | '/api/meetings                 |   |Tạo/Lên lịch cuộc họp trực tuyến mới (CUOC_HOP)                           |
|POST |    | '/api/meetings/{id}/join       |   |Tham gia cuộc họp trực tuyến (lấy thông tin token/room)Ví dụ dữ liệu JSON |
## 6. Sơ đồ luồng chức năng
Đoạn mã
flowchart TD
    A[Người dùng bắt đầu truy cập Kênh giao tiếp] --> B{Kiểm tra quyền thuộc Nhóm/Dự án}
    B -- Không thuộc nhóm --> C[Từ chối truy cập]
    B -- Đúng nhóm --> D{Chọn hình thức giao tiếp}
    
    D -- Chat thời gian thực --> E[Gửi/Nhận tin nhắn CHAT_MESSAGE]
    E --> F[Đồng bộ tin nhắn real-time cho các thành viên]
    
    D -- Họp trực tuyến --> G{Chọn thao tác họp}
    G -- Lên lịch cuộc họp --> H{Kiểm tra thời gian hợp lệ}
    H -- Không hợp lệ --> I[Báo lỗi thời gian]
    H -- Hợp lệ --> J[Lưu thông tin CUOC_HOP & Thông báo]
    
    G -- Tham gia họp ngay --> K[Kết nối Video/Audio & Chia sẻ màn hình]
    K --> L[Kết thúc cuộc họp & Cập nhật trạng thái]
## 7. Tiêu chí nghiệm thu
Người dùng có quyền thực hiện được các thao tác được mô tả trong tài liệu.
Người dùng không có quyền bị từ chối truy cập hoặc thao tác.
Dữ liệu không hợp lệ được báo lỗi rõ ràng và không làm sai lệch dữ liệu đã lưu.
Kết quả thao tác được trả về để giao diện hiển thị cho người dùng.
