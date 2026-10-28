FE7. Giao tiếp và họp trực tuyến1. Mục tiêuHỗ trợ giảng viên và sinh viên trao đổi, thảo luận thông suốt trong suốt quá trình thực hiện đồ án/dự án (DU_AN). Cung cấp các tính năng tích hợp gồm: chat thời gian thực (nhắn tin trao đổi), họp video/audio trực tuyến, chia sẻ màn hình và lên lịch cuộc họp.2. Vai trò liên quanGIANG_VIEN, SINH_VIEN3. Luồng nghiệp vụNgười dùng (Giảng viên hoặc Sinh viên) truy cập vào không gian dự án (DU_AN) hoặc phòng họp chung.Hệ thống kiểm tra quyền thành viên trong dự án/nhóm trước khi cho phép tham gia phòng chat hoặc phòng họp trực tuyến.Người dùng thực hiện các thao tác: gửi tin nhắn chat, tạo lịch họp mới, tham gia cuộc họp video/audio hoặc chia sẻ màn hình trong phiên họp.Hệ thống ghi nhận lịch sử tin nhắn, lưu trữ lịch họp và quản lý trạng thái phiên họp trực tuyến.4. Quy tắc nghiệp vụQuyền chat & họp: Chỉ các thành viên thuộc nhóm dự án và Giảng viên hướng dẫn mới có quyền gửi tin nhắn, tham gia phòng họp hoặc tạo lịch họp trong phạm vi dự án đó.Lên lịch cuộc họp (LICH_HOP): Người tạo lịch họp (Giảng viên hoặc Trưởng nhóm sinh viên) phải xác định rõ thời gian bắt đầu (ThoiGianBatDau), thời gian kết thúc (ThoiGianKetThuc) và danh sách người tham gia.Quản lý phòng họp trực tuyến: Sử dụng giao thức WebRTC hoặc tích hợp dịch vụ hội nghị (như Zoom, Jitsi, Agora) để đảm bảo bảo mật; người ngoài dự án không thể truy cập vào phòng họp.Lưu trữ lịch sử: Tin nhắn chat (TIN_NHAN) phải được lưu trữ theo thời gian thực và đồng bộ cho tất cả thành viên tham gia nhóm dự án.5. API dự kiếnĐây là hợp đồng API đề xuất để nhóm thống trước trước khi code, chưa xác nhận là đã được triển khai.Phương thứcEndpointMục đíchGET/api/projects/{id}/messagesLấy lịch sử tin nhắn chat của dự án (TIN_NHAN)POST/api/projects/{id}/messagesGửi tin nhắn chat thời gian thực mớiGET/api/projects/{id}/meetingsXem danh sách lịch họp (LICH_HOP) của dự ánPOST/api/projects/{id}/meetingsTạo lịch cuộc họp trực tuyến mớiPOST/api/meetings/{id}/joinTham gia phòng họp trực tuyến (Video/Audio/Share screen)Ví dụ dữ liệu JSON (Tạo lịch họp)JSON{
  "maDuAn": "DA01",
  "tieuDe": "Họp tiến độ tuần 3 - Đồ án tốt nghiệp",
  "nguoiTao": "GV001",
  "thoiGianBatDau": "2026-11-15T09:00:00",
  "thoiGianKetThuc": "2026-11-15T10:00:00",
  "phongHopUrl": "https://meet.jit.si/project-da01-room"
}
6. Sơ đồ luồng chức năngĐoạn mãflowchart TD
    A[Người dùng chọn tính năng Giao tiếp / Họp] --> B{Kiểm tra quyền thành viên dự án}
    B -- Không có quyền --> C[Từ chối truy cập / Báo lỗi]
    B -- Hợp lệ --> D{Lựa chọn hành động}
    D -- Nhắn tin Chat --> E[Gửi & Nhận tin nhắn thời gian thực qua WebSocket]
    D -- Lên lịch họp --> F[Nhập thông tin thời gian & Lưu vào LICH_HOP]
    D -- Tham gia họp Video --> G[Kết nối WebRTC / Truy cập phòng họp trực tuyến]
    G --> H[Chia sẻ Audio, Video & Màn hình trong phiên họp]
    E --> I[Hoàn tất trao đổi]
    F --> I
    H --> I
7. Tiêu chí nghiệm thuNgười dùng có quyền trong dự án truy cập được vào phòng chat và các buổi họp trực tuyến.Người ngoài nhóm/dự án bị từ chối truy cập hoàn toàn vào phòng chat và phòng họp.Tin nhắn được gửi và nhận theo thời gian thực (Real-time) không có độ trễ lớn.Tính năng gọi Video/Audio, chia sẻ màn hình và lên lịch hoạt động ổn định, không bị lỗi xung đột dữ liệu.
