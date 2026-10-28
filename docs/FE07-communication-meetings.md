QUẢN LÝ GIAO TIẾP VÀ HỌP TRỰC TUYẾNMã chức năng: FE07Tên chức năng: Quản lý giao tiếp và họp trực tuyếnHệ thống: CollabSphere – Hệ thống hỗ trợ học tập theo phương pháp học tập dựa trên dự án (Project-Based Learning – PBL)Tác nhân chính: Giảng viên (Lecturer), Sinh viên (Student)1. Tổng quan chức năng1.1. Mục tiêuChức năng Quản lý giao tiếp và họp trực tuyến cung cấp môi trường tương tác thời gian thực cho giảng viên và sinh viên trong quá trình thực hiện dự án (PBL). Chức năng tích hợp trò chuyện văn bản, cuộc gọi video/audio, chia sẻ màn hình và lên lịch cuộc họp giúp tối ưu hóa việc trao đổi thông tin, phản hồi đồ án và theo dõi tiến độ nhóm.1.2. Phạm vi chức năngMã chức năngTên chức năngMô tảFE07.01Trò chuyện thời gian thựcChat nhóm dự án, chat riêng, chia sẻ tệp tin và emoji thời gian thựcFE07.02Lên lịch cuộc họpTạo, cập nhật, hủy và nhận thông báo lịch họp nhóm hoặc phản hồi với giảng viênFE07.03Họp Video/Audio trực tuyếnKhởi tạo và tham gia phòng họp trực tuyến truyền hình ảnh, âm thanh chất lượng caoFE07.04Chia sẻ màn hình & Tương tácChia sẻ màn hình làm việc, hỗ trợ trình chiếu báo cáo và ghi hình/tóm tắt cuộc họp1.3. Đối tượng sử dụngVai tròQuyền hạnSinh viên (Student)Tạo nhóm chat dự án, nhắn tin, lên lịch họp nhóm, khởi tạo/tham gia họp video, chia sẻ màn hìnhGiảng viên (Lecturer)Tham gia chat nhóm dự án, đặt lịch họp tư vấn/duyệt tiến độ, chủ trì phòng họp trực tuyếnNhân viên (Staff)Xem lịch sử/thống kê hoạt động giao tiếp khi có yêu cầu hỗ trợ hoặc kiểm traQuản trị viên (Admin)Cấu hình tích hợp dịch vụ (WebRTC/Media Server/Socket), phân quyền và khắc phục sự cố kết nối2. Mô tả chi tiết chức năng2.1. Trò chuyện thời gian thực (Real-time Chat)Mục đích: Tương tác, trao đổi công việc hằng ngày giữa các thành viên trong dự án và với giảng viên hướng dẫn.Quy trình thực hiện:Người dùng truy cập vào kênh chat của Dự án/Nhóm.Soạn tin nhắn văn bản, gắn đính kèm tệp (tài liệu, hình ảnh, mã nguồn) hoặc biểu cảm.Hệ thống truyền tải tin nhắn tức thời đến các thành viên đang hoạt động thông qua WebSocket.Lưu trữ lịch sử tin nhắn để tra cứu lại.Yêu cầu xử lý:Tự động tạo kênh chat riêng khi một Dự án (FE04) hoặc Nhóm (FE05) được khởi tạo.Hỗ trợ tải tệp đính kèm với giới hạn dung lượng theo quy định.Hiển thị trạng thái tin nhắn (đã gửi, đã xem) và trạng thái online/offline của người dùng.2.2. Lên lịch cuộc họp (Meeting Scheduling)Mục đích: Đặt lịch các buổi họp định kỳ của nhóm hoặc các buổi review đồ án với giảng viên.Các thao tác chính:Tạo lịch họp mới: Điền tiêu đề, mô tả, thời gian bắt đầu/kết thúc, liên kết phòng họp tự động sinh.Mời người tham gia (thành viên nhóm, giảng viên).Gửi thông báo nhắc nhở trước giờ họp (qua notification hệ thống hoặc email).Cập nhật hoặc hủy lịch họp đã tạo.2.3. Họp Video/Audio trực tuyến (Video/Audio Conferencing)Mục đích: Tổ chức các buổi báo cáo tiến độ, thảo luận dự án trực tiếp qua video và micro.Các thao tác chính:Tham gia phòng họp từ liên kết lịch họp hoặc khởi tạo cuộc gọi nhanh (Instant Meeting).Bật/Tắt Micro (Mute/Unmute) và Camera.Quản lý quyền trong phòng họp (Giảng viên/Trưởng nhóm có thể tắt tiếng người tham gia).Giơ tay phát biểu, danh sách thành viên tham dự trong phòng.2.4. Chia sẻ màn hình & Tương tác (Screen Sharing & Interaction)Mục đích: Hỗ trợ trình chiếu slide báo cáo, demo sản phẩm hoặc sửa lỗi mã nguồn trực tiếp.Các thao tác chính:Chia sẻ toàn bộ màn hình, cửa sổ ứng dụng hoặc thẻ trình duyệt.Trò chuyện văn bản song song trong phòng họp (In-meeting Chat).Ghi âm/ghi hình cuộc họp (Recording) và lưu vào kho lưu trữ dự án (nếu cấu hình).3. Quy tắc nghiệp vụ (Business Rules)Mã quy tắcNội dungBR07.01Chỉ thành viên thuộc Dự án/Nhóm hoặc Giảng viên phụ trách mới có quyền truy cập kênh chat và phòng họp của dự án đó.BR07.02Tin nhắn đã gửi chỉ được sửa hoặc xóa trong vòng 15 phút kể từ khi gửi.BR07.03Tệp đính kèm trong chat không vượt quá dung lượng tối đa quy định (ví dụ: 25MB/tệp) và phải qua kiểm tra loại tệp an toàn.BR07.04Lịch họp không được đặt trong quá khứ và thời gian kết thúc phải sau thời gian bắt đầu.BR07.05Người tạo cuộc họp hoặc Giảng viên có quyền Chủ trì (Host) để điều phối phòng họp (tắt mic, mời ra khỏi phòng).BR07.06Mọi kết nối Real-time (Chat/Video) phải được xác thực thông qua Access Token hợp lệ của hệ thống.BR07.07Khi cuộc họp kết thúc, hệ thống cần cập nhật trạng thái phòng và lưu nhật ký tham gia (khi có yêu cầu).4. Giao diện lập trình ứng dụng (API) đề xuấtLưu ý: Các đường dẫn API dưới đây là thiết kế đề xuất cho chức năng FE07. Cần đối chiếu với cấu trúc máy chủ WebSocket/Media Server trước khi triển khai.Phương thứcĐường dẫn APIChức năngGET/api/chat/channels/{channelId}/messagesLấy lịch sử tin nhắn của kênh chatPOST/api/chat/channels/{channelId}/messagesGửi tin nhắn mới (kèm tệp đính kèm)DELETE/api/chat/messages/{messageId}Xóa tin nhắnGET/api/meetingsLấy danh sách lịch họp của người dùng/dự ánPOST/api/meetingsTạo lịch họp mớiGET/api/meetings/{meetingId}Lấy thông tin chi tiết cuộc họpPATCH/api/meetings/{meetingId}Cập nhật thông tin lịch họpDELETE/api/meetings/{meetingId}Hủy lịch họpPOST/api/meetings/{meetingId}/join-tokenLấy Token/Credential để truy cập phòng họp (WebRTC/Third-party)4.1. Ví dụ dữ liệu tạo lịch họpJSON{
  "project_id": 501,
  "title": "Báo cáo tiến độ Sprint 2 - Nhóm 1",
  "description": "Thảo luận về thiết kế giao diện và kết nối API FE07",
  "start_time": "2026-11-05T09:00:00Z",
  "end_time": "2026-11-05T10:30:00Z",
  "participant_ids": [101, 102, 103, 12]
}
4.2. Ví dụ dữ liệu gửi tin nhắn chatJSON{
  "channel_id": "proj-501-chat",
  "content": "Chào thầy, nhóm em đã tải lên tài liệu SRS bản mới nhất ạ.",
  "attachments": [
    {
      "file_name": "SRS_FE07_v1.pdf",
      "file_url": "https://storage.collabsphere.edu.vn/files/SRS_FE07_v1.pdf",
      "file_type": "application/pdf"
    }
  ]
}
5. Mô hình dữ liệu đề xuấtCác bảng dưới đây là phương án thiết kế đề xuất bổ sung vào schema cơ sở dữ liệu (database/schema.sql).Tên bảngChức năngchat_channelsLưu thông tin các kênh chat (theo dự án, nhóm hoặc hội thoại riêng)chat_messagesLưu nội dung tin nhắn, thời gian gửi và người gửichat_attachmentsLưu thông tin tệp đính kèm trong tin nhắnmeetingsLưu thông tin lịch họp và liên kết phòng họpmeeting_participantsLiên kết danh sách người tham gia cuộc họp5.1. Sơ đồ cơ sở dữ liệu SQL (Minh họa)SQL-- 1. Bảng Kênh Chat
CREATE TABLE chat_channels (
    id VARCHAR(100) PRIMARY KEY,
    project_id BIGINT NULL,
    channel_name VARCHAR(255) NOT NULL,
    type ENUM('PROJECT', 'GROUP', 'DIRECT') DEFAULT 'PROJECT',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (project_id) REFERENCES projects(id) ON DELETE CASCADE
);

-- 2. Bảng Tin Nhắn Chat
CREATE TABLE chat_messages (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    channel_id VARCHAR(100) NOT NULL,
    sender_id BIGINT NOT NULL,
    content TEXT,
    is_edited BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (channel_id) REFERENCES chat_channels(id) ON DELETE CASCADE,
    FOREIGN KEY (sender_id) REFERENCES users(id)
);

-- 3. Bảng Lịch Họp
CREATE TABLE meetings (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT NOT NULL,
    created_by BIGINT NOT NULL,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL,
    room_code VARCHAR(100) NOT NULL UNIQUE,
    status ENUM('SCHEDULED', 'ONGOING', 'COMPLETED', 'CANCELLED') DEFAULT 'SCHEDULED',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (project_id) REFERENCES projects(id) ON DELETE CASCADE,
    FOREIGN KEY (created_by) REFERENCES users(id)
);

-- 4. Bảng Người Tham Gia Cuộc Họp
CREATE TABLE meeting_participants (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    meeting_id BIGINT NOT NULL,
    user_id BIGINT NOT NULL,
    status ENUM('PENDING', 'ACCEPTED', 'DECLINED') DEFAULT 'PENDING',
    joined_at DATETIME NULL,
    FOREIGN KEY (meeting_id) REFERENCES meetings(id) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id),
    UNIQUE KEY uq_meeting_user (meeting_id, user_id)
);
6. Tiêu chí nghiệm thuSinh viên và Giảng viên trong cùng dự án gửi và nhận được tin nhắn thời gian thực mà không cần tải lại trang.Người dùng tải được tệp đính kèm trong kênh chat đúng dung lượng cho phép.Tạo được lịch họp mới và tự động gửi thông báo đến các thành viên được mời.Khởi tạo phòng họp video/audio hoạt động ổn định, mượt mà với âm thanh và hình ảnh rõ ràng.Giảng viên và sinh viên thực hiện chia sẻ màn hình thành công trong buổi họp.Người dùng không thuộc dự án không thể truy cập vào kênh chat hoặc phòng họp riêng tư.Hệ thống hiển thị thông báo rõ ràng khi kết nối mạng bị gián đoạn hoặc cuộc họp bị hủy.7. Kịch bản kiểm thửMã kiểm thửTrường hợp kiểm thửKết quả mong đợiTC07.01Gửi tin nhắn văn bản hợp lệTin nhắn xuất hiện lập tức trên màn hình người nhậnTC07.02Gửi tệp đính kèm vượt dung lượngHệ thống chặn gửi và thông báo lỗi vượt quá giới hạnTC07.03Tạo lịch họp hợp lệLịch họp được lưu, hiển thị trên lịch chung và gửi thông báoTC07.04Tạo lịch họp có thời gian bắt đầu trong quá khứHệ thống từ chối tạo và thông báo lỗi thời gian không hợp lệTC07.05Tham gia phòng họp bằng liên kết hợp lệKết nối thành công vào phòng video/audioTC07.06Chia sẻ màn hình trong phòng họpMàn hình được chia sẻ hiển thị rõ ràng cho tất cả người tham dựTC07.07Truy cập phòng họp khi không có quyềnTừ chối truy cập và thông báo không đủ quyền hạnTC07.08Bật/Tắt Micro và CameraTrạng thái thiết bị thay đổi chính xác và cập nhật cho các thành viên8. Kế hoạch triển khaiThiết kế giao diện Chat UI, Lịch họp và Phòng họp trực tuyến (Frontend).Xây dựng dịch vụ WebSocket / SignalR phục vụ truyền tải tin nhắn thời gian thực.Tích hợp giải pháp Media Server (WebRTC / LiveKit / Jitsi / Agora) cho họp video và chia sẻ màn hình.Xây dựng các API quản lý tin nhắn, tệp đính kèm và lịch họp.Kết nối dịch vụ thông báo (Notification Service) để nhắc lịch họp.Kiểm tra phân quyền truy cập theo Dự án (FE04) và Nhóm (FE05).Kiểm thử hiệu năng truyền tải dữ liệu real-time và khả năng chịu tải của phòng họp.Hoàn thiện tài liệu hướng dẫn và đóng gói chức năng.9. Tài liệu liên quanFE01 – Quản lý tài khoản người dùngFE04 – Quản lý dự ánFE05 – Quản lý nhóm và thành viênFE12 – Quản lý phân quyền
