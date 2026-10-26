QUẢN LÝ GIAO TIẾP VÀ HỌP TRỰC TUYẾNMã chức năng: FE07Tên chức năng: Giao tiếp và họp trực tuyếnHệ thống: CollabSphere (Project-Based Learning – PBL)Tác nhân chính: Giảng viên (Lecturer), Sinh viên (Student)1. Tổng quan & Phạm viChức năng hỗ trợ trao đổi thời gian thực, tổ chức họp video/audio, chia sẻ màn hình và lên lịch họp cho các nhóm dự án PBL.MãTên chức năngMô tả ngắnFE07.01Trò chuyện thời gian thựcNhắn tin, gửi tệp, biểu cảm (nhóm dự án, lớp, 1-1)FE07.02Lên lịch cuộc họpTạo lịch, chọn người tham gia, gửi thông báo & nhắc nhởFE07.03Họp Video / AudioPhòng họp trực tuyến truyền hình ảnh, âm thanhFE07.04Chia sẻ màn hìnhTrình chiếu màn hình/ứng dụng khi báo cáoPhân quyền nhanh:Giảng viên: Tạo/chủ trì cuộc họp lớp & nhóm, quản lý quyền mic/cam, chat.Sinh viên: Tạo cuộc họp nhóm dự án, tham gia họp, chia sẻ màn hình, chat.2. Quy trình nghiệp vụĐoạn mãflowchart TD
    A([Bắt đầu]) --> B{Chọn hình thức}
    B -->|Chat| C[Vào kênh Chat] --> D[Gửi tin/tệp qua Socket] --> Z([Kết thúc])
    B -->|Lên lịch| E[Tạo lịch họp] --> F[Gửi thông báo nhắc lịch] --> Z
    B -->|Họp video| G[Vào phòng họp WebRTC] --> H[Bật Mic/Cam & Chia sẻ màn hình] --> I[Rời/Kết thúc họp] --> Z
3. Quy tắc nghiệp vụ (Business Rules)BR07.01: Chỉ thành viên thuộc nhóm/lớp mới được vào kênh chat và phòng họp tương ứng.BR07.02: Sinh viên chỉ lên lịch cho nhóm mình; Giảng viên được lên lịch cho cả lớp/nhóm.BR07.03: File gửi qua chat tối đa 25MB (phải quét mã độc).BR07.04: Tin nhắn chỉ được sửa/xóa trong vòng 15 phút.BR07.05: Quyền Host thuộc về người tạo; phòng tự đóng sau 30 phút nếu không có người.4. API đề xuấtPhương thứcĐường dẫn APIMô tảGET/api/chat/channelsLấy danh sách kênh chatGET / POST/api/chat/channels/{id}/messagesLấy lịch sử / Gửi tin nhắnPOST/api/chat/uploadTải file đính kèmGET / POST/api/meetingsXem danh sách / Tạo lịch họpPATCH / DELETE/api/meetings/{id}Sửa / Hủy cuộc họpPOST/api/meetings/{id}/joinLấy Token WebRTC tham giaPOST/api/meetings/{id}/endKết thúc cuộc họp (Host)5. Mô hình dữ liệu (ERD)Đoạn mãerDiagram
    CHAT_CHANNELS ||--o{ CHAT_MESSAGES : contains
    CHAT_MESSAGES ||--o{ CHAT_ATTACHMENTS : includes
    MEETINGS ||--o{ MEETING_PARTICIPANTS : includes
    USERS ||--o{ CHAT_MESSAGES : sends
    USERS ||--o{ MEETINGS : creates

    CHAT_CHANNELS { int id PK, string channel_name, string channel_type }
    CHAT_MESSAGES { int id PK, int channel_id FK, int sender_id FK, string content }
    CHAT_ATTACHMENTS { int id PK, int message_id FK, string file_name, string file_path }
    MEETINGS { int id PK, string title, int created_by FK, datetime start_time, string room_code }
    MEETING_PARTICIPANTS { int meeting_id PK_FK, int user_id PK_FK, string role }
6. Tiêu chí nghiệm thu & Test CasesNghiệm thu: Chat thời gian thực (độ trễ <2s); gửi file thành công; họp video/audio rõ tiếng/hình; chia sẻ màn hình mượt; chặn người ngoài truy cập.Test cases chính:TC07.01: Gửi tin nhắn thành công trong nhóm.TC07.02: Chặn gửi file > 25MB.TC07.03: Tạo & nhận thông báo lịch họp.TC07.04: Bật/Tắt Mic, Cam, Chia sẻ màn hình ổn định.TC07.05: Host tắt mic thành viên khác thành công.7. Tài liệu liên quanFE01 - Quản lý tài khoản | FE03 - Quản lý lớp học | FE05 - Quản lý nhómiệu liên quanFE01 – Quản lý tài khoản người dùngFE03 – Quản lý lớp họcFE04 – Quản lý dự ánFE05 – Quản lý nhóm và thành viênFE12 – Quản lý phân quyền
