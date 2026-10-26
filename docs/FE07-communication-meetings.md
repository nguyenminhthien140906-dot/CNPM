QUẢN LÝ GIAO TIẾP VÀ HỌP TRỰC TUYẾNMã chức năng: FE07Tên chức năng: Giao tiếp và họp trực tuyếnHệ thống: CollabSphere – Hệ thống hỗ trợ học tập theo phương pháp học tập dựa trên dự án (Project-Based Learning – PBL)Tác nhân chính: Giảng viên (Lecturer), Sinh viên (Student)1. Tổng quan chức năng1.1. Mục tiêuChức năng Giao tiếp và họp trực tuyến cho phép giảng viên và sinh viên tương tác trực tiếp trong quá trình thực hiện dự án PBL. Chức năng cung cấp kênh chat thời gian thực (Real-time Chat), cuộc họp trực tuyến (Video/Audio Call), chia sẻ màn hình và lên lịch cuộc họp theo nhóm dự án hoặc lớp học. Điều này giúp tối ưu hóa việc trao đổi, báo cáo tiến độ và cố vấn dự án từ xa.1.2. Phạm vi chức năngMã chức năngTên chức năngMô tảFE07.01Trò chuyện thời gian thựcNhắn tin văn bản, chia sẻ tệp tin, biểu cảm trong nhóm dự án hoặc tin nhắn riêngFE07.02Lên lịch cuộc họpTạo, cập nhật, xem lịch và gửi thông báo nhắc nhở lịch họp cho thành viênFE07.03Họp Video / Audio trực tuyếnKhởi tạo và tham gia phòng họp trực tuyến với tính năng truyền âm thanh và hình ảnhFE07.04Chia sẻ màn hìnhCho phép người tham gia chia sẻ màn hình làm việc để báo cáo và thuyết trình1.3. Đối tượng sử dụngVai tròQuyền hạnLecturer (Giảng viên)Tạo cuộc họp lớp/nhóm, chủ trì phòng họp, quản lý quyền phát biểu/chia sẻ, tham gia chat nhóm/riêngStudent (Sinh viên)Tạo cuộc họp nhóm dự án, tham gia phòng họp, chia sẻ màn hình, trao đổi qua kênh chatStaff (Nhân viên quản lý)Xem danh sách và lịch sử các cuộc họp trực tuyến trên hệ thống (khi có yêu cầu hỗ trợ)Admin (Quản trị viên)Cấu hình tích hợp WebRTC/SFU server, giới hạn tài nguyên cuộc họp và quản lý quyền truy cập2. Mô tả chi tiết chức năng2.1. Trò chuyện thời gian thực (Real-time Chat)Mục đích: Cung cấp kênh trao đổi tức thì giữa các thành viên trong cùng nhóm dự án, giữa sinh viên với giảng viên phụ trách.Quy trình thực hiện:Người dùng chọn kênh chat (kênh nhóm dự án, kênh lớp học hoặc chat 1-1).Hệ thống tải lịch sử tin nhắn và danh sách thành viên đang trực tuyến (Online status).Người dùng nhập tin nhắn hoặc tải lên tệp đính kèm.Hệ thống truyền tải tin nhắn tức thì qua Socket/WebSocket đến những người dùng liên quan.Hiển thị trạng thái đã gửi/đã nhận/đã xem của tin nhắn.Yêu cầu xử lý:Hỗ trợ định dạng tin nhắn văn bản, emoji, hình ảnh, tài liệu (pdf, docx, zip...).Giới hạn dung lượng tệp tải lên (tối đa 25MB/tệp).Mã hóa tin nhắn truyền tải trên đường truyền (HTTPS/WSS).Lưu trữ lịch sử chat và hỗ trợ tìm kiếm tin nhắn cũ.2.2. Lên lịch cuộc họp (Meeting Scheduling)Mục đích: Giúp giảng viên hoặc nhóm trưởng lên kế hoạch cho các buổi họp review dự án, báo cáo tiến độ.Các thao tác chính:Tạo cuộc họp mới (tiêu đề, thời gian bắt đầu, thời gian dự kiến, mô tả, liên kết tham gia).Chọn phạm vi mời (toàn bộ lớp, nhóm dự án cụ thể hoặc các cá nhân chọn lọc).Gửi thông báo tự động (Email và Notification trên hệ thống) cho thành viên được mời.Tích hợp tính năng nhắc nhở trước thời điểm họp (15 phút, 30 phút).Cập nhật hoặc hủy lịch họp kèm lý do thông báo.2.3. Họp Video / Audio trực tuyến (Online Video/Audio Call)Mục đích: Tổ chức các buổi thảo luận trực tiếp, bảo vệ báo cáo giai đoạn mà không cần gặp mặt trực tiếp.Các thao tác chính:Khởi tạo phòng họp ngay lập tức (Instant Meeting) hoặc tham gia theo lịch đã tạo.Bật/tắt Micro và Camera cá nhân.Danh sách người tham gia (Participant list) và trạng thái kết nối.Quyền chủ trì (Host control): Duyệt người vào phòng, tắt tiếng (Mute) thành viên, kết thúc cuộc họp.Tính năng giơ tay phát biểu (Raise hand) và khung chat nội bộ phòng họp.2.4. Chia sẻ màn hình (Screen Sharing)Mục đích: Cho phép thành viên trình bày slide, demo sản phẩm hoặc mã nguồn trong buổi họp.Các thao tác chính:Lựa chọn chia sẻ toàn màn hình, cửa sổ ứng dụng hoặc tab trình duyệt.Cho phép cấp quyền chia sẻ cho 1 người tại một thời điểm (hoặc nhiều người tùy cấu hình).Giảng viên có quyền ưu tiên ngắt chia sẻ màn hình của sinh viên để chuyển quyền điều hành.3. Quy trình nghiệp vụĐoạn mãflowchart TD
    A([Bắt đầu]) --> B[Người dùng truy cập chức năng Giao tiếp & Họp]
    B --> C{Chọn hình thức tương tác}
    C -->|Trò chuyện| D[Truy cập kênh Chat]
    D --> E[Gửi tin nhắn / tệp đính kèm]
    E --> F[Truyền qua WebSocket đến người nhận]
    
    C -->|Lên lịch họp| G[Nhập thông tin cuộc họp]
    G --> H[Chọn danh sách tham gia]
    H --> I[Lưu và gửi thông báo nhắc lịch]
    
    C -->|Họp trực tuyến| J{Nguồn tham gia}
    J -->|Tham gia theo lịch| K[Chọn cuộc họp từ lịch]
    J -->|Tạo họp ngay| L[Khởi tạo phòng họp]
    K --> M[Kết nối Server truyền thông WebRTC]
    L --> M
    M --> N[Bật/Tắt Mic, Cam, Chia sẻ màn hình]
    N --> O[Thực hiện buổi họp & Báo cáo]
    O --> P[Thoát/Kết thúc cuộc họp]
    
    F --> Q([Kết thúc])
    I --> Q
    P --> Q
4. Quy tắc nghiệp vụMã quy tắcNội dungBR07.01Chỉ thành viên trong cùng nhóm dự án, lớp học hoặc giảng viên phụ trách mới có quyền tham gia kênh chat/phòng họp tương ứngBR07.02Sinh viên chỉ được tạo lịch họp cho nhóm dự án của mình; Giảng viên có quyền tạo lịch họp cho nhóm hoặc toàn bộ lớpBR07.03Phòng họp trực tuyến chỉ cho phép tối đa sĩ số theo quy định hệ thống (ví dụ: 100 người/phòng) để đảm bảo băng thôngBR07.04Quyền Chủ trì (Host) thuộc về người tạo cuộc họp. Nếu Chủ trì rời phòng, quyền Host được chuyển giao hoặc phòng giữ nguyên trạng thái tùy cấu hìnhBR07.05Tin nhắn đã gửi chỉ được sửa/xóa trong vòng 15 phút kể từ thời điểm gửiBR07.06Tệp tải lên qua kênh chat phải được kiểm tra định dạng và quét mã độc trước khi lưu trữBR07.07Cuộc họp tự động đóng sau 30 phút nếu không có bất kỳ người tham gia nào trong phòng5. Giao diện lập trình ứng dụng (API) đề xuấtLưu ý: Các đường dẫn API dưới đây là thiết kế đề xuất cho chức năng FE07. Cần đối chiếu với mã nguồn phía máy chủ trước khi triển khai.Phương thứcĐường dẫn APIChức năngGET/api/chat/channelsLấy danh sách các kênh chat của người dùngGET/api/chat/channels/{channelId}/messagesLấy lịch sử tin nhắn của một kênhPOST/api/chat/channels/{channelId}/messagesGửi tin nhắn mớiPOST/api/chat/uploadTải lên tệp đính kèm trong chatGET/api/meetingsLấy danh sách lịch họp của người dùngPOST/api/meetingsTạo cuộc họp/lên lịch họp mớiGET/api/meetings/{meetingId}Xem thông tin chi tiết cuộc họpPATCH/api/meetings/{meetingId}Cập nhật thông tin/lịch cuộc họpDELETE/api/meetings/{meetingId}Hủy cuộc họpPOST/api/meetings/{meetingId}/joinYêu cầu tham gia phòng họp (Lấy Token WebRTC)POST/api/meetings/{meetingId}/endKết thúc cuộc họp (Dành cho Host)5.1. Ví dụ dữ liệu lên lịch cuộc họpJSON{
  "title": "Báo cáo tiến độ Sprint 2 - Nhóm 1",
  "description": "Giảng viên review giao diện và chức năng quản lý lớp học",
  "class_id": 5,
  "project_team_id": 12,
  "start_time": "2026-11-05T09:00:00Z",
  "estimated_duration": 60,
  "attendee_ids": [101, 102, 103, 12]
}
5.2. Ví dụ dữ liệu gửi tin nhắn chatJSON{
  "channel_id": "team-12-chat",
  "message_type": "text",
  "content": "Chào thầy, nhóm em đã cập nhật xong sơ đồ ERD ạ.",
  "attachments": []
}
5.3. Ví dụ dữ liệu phản hồi Token tham gia phòng họpJSON{
  "meeting_id": "mtg-8923-4412",
  "room_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "ice_servers": [
    {
      "urls": "stun:stun.l.google.com:19302"
    }
  ],
  "user_role": "host"
}
6. Mô hình dữ liệu đề xuấtCác bảng dưới đây là phương án thiết kế đề xuất. Cần đối chiếu với sơ đồ ERD và tệp database/schema.sql của dự án trước khi triển khai.Tên bảngChức năngchat_channelsLưu thông tin các kênh chat (Kênh nhóm, kênh lớp, chat 1-1)chat_messagesLưu chi tiết các tin nhắn trong kênhchat_attachmentsLưu thông tin tệp đính kèm tin nhắnmeetingsLưu thông tin và lịch trình các cuộc họpmeeting_participantsLiên kết thành viên được mời và tham gia cuộc họp6.1. Sơ đồ quan hệ dữ liệu đề xuấtĐoạn mãerDiagram
    CHAT_CHANNELS ||--o{ CHAT_MESSAGES : contains
    USERS ||--o{ CHAT_MESSAGES : sends
    CHAT_MESSAGES ||--o{ CHAT_ATTACHMENTS : includes
    MEETINGS ||--o{ MEETING_PARTICIPANTS : includes
    USERS ||--o{ MEETING_PARTICIPANTS : joins
    USERS ||--o{ MEETINGS : creates

    CHAT_CHANNELS {
        int id PK
        string channel_name
        string channel_type
        int reference_id
    }

    CHAT_MESSAGES {
        int id PK
        int channel_id FK
        int sender_id FK
        string content
        string message_type
        datetime created_at
    }

    CHAT_ATTACHMENTS {
        int id PK
        int message_id FK
        string file_name
        string file_path
        int file_size
    }

    MEETINGS {
        int id PK
        string title
        string description
        int created_by FK
        datetime start_time
        int estimated_duration
        string status
        string room_code UK
    }

    MEETING_PARTICIPANTS {
        int meeting_id PK, FK
        int user_id PK, FK
        string role
        datetime joined_at
    }
7. Tiêu chí nghiệm thu[ ] Giảng viên và sinh viên gửi/nhận tin nhắn thời gian thực trong kênh nhóm dự án không bị trễ quá 2 giây.[ ] Người dùng tải lên được tệp đính kèm đúng định dạng và dung lượng cho phép trong khung chat.[ ] Nhóm trưởng và Giảng viên tạo/lên lịch cuộc họp thành công và gửi thông báo tới các thành viên liên quan.[ ] Người dùng tham gia được phòng họp trực tuyến truyền nhận thành công Video và Audio.[ ] Người dùng thực hiện được thao tác chia sẻ màn hình mượt mà trong khi đang họp.[ ] Host có thể thực hiện quyền tắt tiếng (Mute) thành viên khác hoặc kết thúc cuộc họp.[ ] Hệ thống ngăn chặn thành viên ngoài nhóm/lớp truy cập trái phép vào kênh chat hoặc phòng họp.[ ] Hiển thị thông báo lỗi rõ ràng khi kết nối mạng gián đoạn hoặc thiết bị (Cam/Mic) không khả dụng.8. Kịch bản kiểm thửMã kiểm thửTrường hợp kiểm thửKết quả mong đợiTC07.01Gửi tin nhắn văn bản trong kênh nhómTin nhắn xuất hiện tức thì ở giao diện tất cả thành viên trong nhómTC07.02Gửi tệp đính kèm vượt quá 25MBTừ chối tải lên và hiển thị thông báo lỗi dung lượngTC07.03Tạo lịch họp nhóm hợp lệCuộc họp tạo thành công, xuất hiện trên lịch và gửi thông báoTC07.04Tham gia phòng họp videoKết nối thành công, nhận diện đúng Cam/Mic của người dùngTC07.05Chia sẻ màn hình trong phòng họpCác thành viên khác thấy được màn hình đang trình chiếuTC07.06Giảng viên tắt mic của sinh viênSinh viên bị tắt mic và nhận thông báo bị Mute bởi HostTC07.07Người ngoài truy cập link họp không được mờiTừ chối truy cập hoặc yêu cầu phòng chờ (Waiting room)TC07.08Hủy lịch họp đã tạoCuộc họp đổi trạng thái Canceled và gửi thông báo hủy cho thành viên9. Kế hoạch triển khaiTích hợp hạ tầng Real-time (Socket.io/WebSocket) và Server truyền thông (WebRTC/LiveKit/Agora).Xây dựng giao diện Chat nhóm, Chat cá nhân và quản lý danh sách cuộc họp.Xây dựng chức năng tạo lịch họp và gửi thông báo (Email/Notification).Phát triển giao diện phòng họp WebRTC (Video Grid, Controls Bar, Share Screen).Xây dựng các API backend phục vụ quản lý lịch họp, kênh chat và phân quyền phòng họp.Kết nối Frontend với hạ tầng Socket và WebRTC server.Kiểm thử hiệu năng truyền tải Video/Audio và khả năng chịu tải của Socket.Kiểm thử bảo mật phân quyền truy cập phòng họp và tệp tin.10. Tài liệu liên quanFE01 – Quản lý tài khoản người dùngFE03 – Quản lý lớp họcFE04 – Quản lý dự ánFE05 – Quản lý nhóm và thành viênFE12 – Quản lý phân quyền
