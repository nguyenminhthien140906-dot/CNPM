GIAO TIẾP VÀ HỌP TRỰC TUYẾN
Mã chức năng: FE07
Tên chức năng: Giao tiếp và họp trực tuyến
Hệ thống: CollabSphere – Hệ thống hỗ trợ học tập theo phương pháp học tập dựa trên dự án (Project-Based Learning – PBL)
Tác nhân chính: Giảng viên (Lecturer), Sinh viên (Student)
1. Tổng quan chức năng
1.1. Mục tiêu

Chức năng Giao tiếp và họp trực tuyến cho phép giảng viên và sinh viên trao đổi thông tin, tổ chức các buổi họp trực tuyến, chia sẻ màn hình và lên lịch cuộc họp trong quá trình thực hiện dự án.

Chức năng này giúp tăng cường khả năng cộng tác giữa các thành viên, hỗ trợ giảng viên hướng dẫn sinh viên, theo dõi tiến độ dự án và giải quyết các vấn đề phát sinh trong quá trình học tập.

1.2. Phạm vi chức năng
Mã chức năng	Tên chức năng	Mô tả
FE07.01	Chat thời gian thực	Gửi và nhận tin nhắn cá nhân hoặc tin nhắn nhóm theo dự án
FE07.02	Họp trực tuyến	Tạo phòng họp và tham gia cuộc họp bằng video/audio
FE07.03	Chia sẻ màn hình	Chia sẻ màn hình hoặc cửa sổ ứng dụng trong cuộc họp
FE07.04	Lên lịch cuộc họp	Tạo, xem, cập nhật và hủy lịch họp
FE07.05	Quản lý người tham gia	Mời thành viên, kiểm soát quyền tham gia và theo dõi trạng thái tham gia
FE07.06	Quản lý lịch sử và biên bản họp	Xem thông tin cuộc họp, kết luận và công việc cần thực hiện sau cuộc họp
1.3. Đối tượng sử dụng
Vai trò	Quyền hạn
Student (Sinh viên)	Gửi tin nhắn, tham gia cuộc họp, chia sẻ màn hình khi được phép và xem lịch họp của dự án mình tham gia
Lecturer (Giảng viên)	Trao đổi với sinh viên, tổ chức họp hướng dẫn, lên lịch họp và quản lý cuộc họp trong phạm vi được phân công
Staff (Nhân viên quản lý)	Xem thông tin liên quan đến hoạt động học tập theo quyền hạn được cấp; không mặc nhiên có quyền đọc nội dung trao đổi riêng tư
Admin (Quản trị viên)	Quản lý quyền truy cập, cấu hình hệ thống và hỗ trợ xử lý sự cố kỹ thuật
2. Mô tả chi tiết chức năng
2.1. Chat thời gian thực

Mục đích: Hỗ trợ giảng viên và sinh viên trao đổi thông tin tức thời trong quá trình thực hiện dự án.

Quy trình thực hiện:

Người dùng đăng nhập vào hệ thống CollabSphere.
Người dùng mở cuộc trò chuyện cá nhân hoặc nhóm dự án mà mình có quyền tham gia.
Người dùng nhập nội dung tin nhắn và gửi đi.
Hệ thống kiểm tra quyền truy cập và tính hợp lệ của nội dung.
Hệ thống lưu tin nhắn và chuyển tin nhắn đến các thành viên liên quan.
Hệ thống cập nhật giao diện người nhận và thông báo tin nhắn mới.

Yêu cầu xử lý:

Gửi và nhận tin nhắn theo thời gian thực.
Hiển thị người gửi, nội dung và thời điểm gửi tin nhắn.
Lưu lịch sử tin nhắn để người dùng có thể xem lại.
Hỗ trợ trò chuyện cá nhân và nhóm dự án.
Hiển thị trạng thái gửi tin nhắn và thông báo tin nhắn mới.
Kiểm tra quyền truy cập trước khi đọc hoặc gửi tin nhắn.
Xử lý lỗi kết nối và hỗ trợ đồng bộ tin nhắn khi kết nối được khôi phục.
Kiểm tra loại và kích thước tệp đính kèm nếu hệ thống hỗ trợ gửi tệp.
2.2. Họp trực tuyến qua video/audio

Mục đích: Cho phép giảng viên và sinh viên tổ chức các buổi trao đổi trực tiếp mà không cần gặp mặt tại cùng một địa điểm.

Các thao tác chính:

Tạo phòng họp trực tuyến.
Tham gia phòng họp bằng liên kết hoặc từ giao diện dự án.
Bật/tắt micro và camera.
Xem danh sách người tham gia.
Rời cuộc họp hoặc kết thúc cuộc họp theo quyền hạn.
Hiển thị trạng thái kết nối của người tham gia.
Thông báo khi có thành viên tham gia hoặc rời phòng họp.

Yêu cầu xử lý:

Kiểm tra quyền tham gia phòng họp.
Yêu cầu cấp quyền sử dụng micro và camera khi cần.
Hiển thị thông báo nếu trình duyệt không hỗ trợ hoặc người dùng từ chối cấp quyền thiết bị.
Xử lý tình trạng mất kết nối và cho phép người dùng thử kết nối lại.
Kiểm soát quyền tạo, quản lý và kết thúc phòng họp.
Không tự động ghi âm hoặc ghi hình khi chưa có cơ chế thông báo và cho phép phù hợp.
2.3. Chia sẻ màn hình

Mục đích: Hỗ trợ người dùng trình bày báo cáo, sản phẩm dự án, mã nguồn hoặc tài liệu trong các buổi họp trực tuyến.

Các thao tác chính:

Bắt đầu chia sẻ màn hình.
Chọn toàn bộ màn hình, cửa sổ ứng dụng hoặc tab trình duyệt tùy khả năng của nền tảng.
Dừng chia sẻ màn hình.
Theo dõi nội dung được chia sẻ trong cuộc họp.
Kiểm soát quyền chia sẻ màn hình theo vai trò hoặc thiết lập của người tổ chức.

Yêu cầu xử lý:

Chỉ bắt đầu chia sẻ sau khi người dùng chủ động lựa chọn nội dung và cấp quyền.
Hiển thị trạng thái đang chia sẻ màn hình.
Cho phép người chia sẻ dừng chia sẻ bất cứ lúc nào.
Giải phóng tài nguyên chia sẻ khi cuộc họp kết thúc hoặc kết nối bị ngắt.
Thông báo rõ ràng nếu trình duyệt hoặc thiết bị không hỗ trợ chức năng chia sẻ.
2.4. Lên lịch và quản lý cuộc họp

Mục đích: Giúp giảng viên và sinh viên chủ động sắp xếp thời gian trao đổi, hướng dẫn và đánh giá tiến độ dự án.

Các thao tác chính:

Tạo lịch họp với tiêu đề, thời gian và nội dung.
Chọn dự án hoặc nhóm liên quan.
Mời thành viên tham gia cuộc họp.
Xem lịch họp sắp diễn ra và lịch sử các cuộc họp.
Cập nhật thời gian, nội dung hoặc danh sách người tham gia.
Hủy lịch họp.
Gửi thông báo khi tạo, thay đổi hoặc hủy lịch họp.
Gửi lời nhắc trước thời điểm bắt đầu cuộc họp.

Thông tin cuộc họp bao gồm:

Trường dữ liệu	Ý nghĩa
meeting_id	Mã định danh cuộc họp
title	Tiêu đề cuộc họp
description	Nội dung hoặc mục đích cuộc họp
project_id	Dự án liên quan, nếu có
organizer_id	Người tổ chức cuộc họp
start_time	Thời điểm bắt đầu
end_time	Thời điểm kết thúc dự kiến
meeting_link	Liên kết tham gia cuộc họp
status	Trạng thái cuộc họp
created_at	Thời điểm tạo cuộc họp
2.5. Quản lý người tham gia

Mục đích: Kiểm soát danh sách thành viên được mời và tham gia cuộc họp.

Các thao tác chính:

Mời giảng viên hoặc sinh viên tham gia.
Xem danh sách người được mời.
Theo dõi trạng thái lời mời nếu được hỗ trợ.
Xem danh sách người đang tham gia cuộc họp.
Cho phép người tổ chức hoặc người có quyền loại thành viên khỏi phòng họp.
Kiểm tra quyền tham gia dựa trên dự án, nhóm và lời mời.

Yêu cầu xử lý:

Chỉ tài khoản hợp lệ mới được mời hoặc tham gia theo quy định của hệ thống.
Không cho phép người không có quyền truy cập vào phòng họp riêng tư.
Kiểm tra quyền quản lý người tham gia ở phía máy chủ.
Ghi nhận thông tin tham gia và rời cuộc họp khi cần phục vụ quản lý hoạt động học tập.
2.6. Quản lý lịch sử và biên bản họp

Mục đích: Lưu lại thông tin cần thiết để giảng viên và sinh viên theo dõi các nội dung đã trao đổi và công việc sau cuộc họp.

Các thao tác chính:

Xem danh sách cuộc họp đã diễn ra.
Xem thời gian, người tổ chức và danh sách người tham gia.
Ghi nhận nội dung chính và kết luận cuộc họp.
Ghi nhận nhiệm vụ cần thực hiện sau cuộc họp.
Liên kết biên bản họp với dự án hoặc nhóm tương ứng.
Cập nhật biên bản theo quyền hạn được cấp.

Yêu cầu xử lý:

Chỉ người dùng được cấp quyền mới được xem hoặc chỉnh sửa biên bản.
Phân biệt trạng thái cuộc họp đã lên lịch, đang diễn ra, đã kết thúc hoặc đã hủy.
Không mặc định lưu nội dung âm thanh, video hoặc bản ghi cuộc họp.
Hỗ trợ liên kết các công việc sau cuộc họp với chức năng quản lý dự án nếu hệ thống có tích hợp.
3. Quy trình nghiệp vụ
mermaid
flowchart TD
    A([Bắt đầu]) --> B[Người dùng đăng nhập]
    B --> C[Truy cập chức năng giao tiếp và họp trực tuyến]
    C --> D{Chọn chức năng}
    D -->|Chat| E[Mở cuộc trò chuyện]
    E --> F[Nhập và gửi tin nhắn]
    F --> G[Kiểm tra quyền và lưu tin nhắn]
    G --> H[Chuyển tin nhắn đến người nhận]
    D -->|Họp ngay| I[Tạo hoặc tham gia phòng họp]
    I --> J[Kiểm tra quyền tham gia]
    J --> K{Được phép tham gia?}
    K -->|Không| L[Thông báo từ chối truy cập]
    K -->|Có| M[Tham gia họp video/audio]
    M --> N[Chia sẻ màn hình nếu cần]
    N --> O[Kết thúc cuộc họp]
    D -->|Lên lịch| P[Nhập thông tin cuộc họp]
    P --> Q[Kiểm tra dữ liệu và quyền hạn]
    Q --> R{Thông tin hợp lệ?}
    R -->|Không| S[Thông báo lỗi]
    S --> P
    R -->|Có| T[Lưu lịch họp]
    T --> U[Gửi lời mời và nhắc lịch]
    O --> V[Ghi nhận lịch sử và biên bản]
    V --> W([Kết thúc])
    U --> W
    H --> W
    L --> W

4. Quy tắc nghiệp vụ
Mã quy tắc	Nội dung
BR07.01	Chỉ người dùng đã xác thực mới được sử dụng chức năng giao tiếp và họp trực tuyến
BR07.02	Người dùng chỉ được truy cập cuộc trò chuyện thuộc phạm vi quyền hạn của mình
BR07.03	Tin nhắn phải được kiểm tra quyền gửi và quyền nhận trước khi xử lý
BR07.04	Mỗi cuộc họp phải có người tổ chức hợp lệ
BR07.05	Thời điểm kết thúc cuộc họp phải sau thời điểm bắt đầu
BR07.06	Người dùng chỉ được tham gia phòng họp khi đáp ứng điều kiện truy cập
BR07.07	Chức năng micro, camera và chia sẻ màn hình phải tuân thủ quyền cấp thiết bị của người dùng
BR07.08	Chỉ người có quyền mới được thay đổi lịch họp hoặc kết thúc cuộc họp theo quy định
BR07.09	Khi lịch họp thay đổi hoặc bị hủy, hệ thống phải thông báo cho những người liên quan
BR07.10	Không tạo nhiều bản ghi thành viên trùng lặp cho cùng một cuộc họp
BR07.11	Tin nhắn, lịch họp và biên bản phải được bảo vệ theo quyền truy cập tương ứng
BR07.12	Hệ thống không mặc định ghi âm, ghi hình hoặc lưu màn hình khi chưa có cơ chế thông báo và cho phép phù hợp
BR07.13	Thời gian họp phải được lưu trữ nhất quán và hiển thị theo múi giờ phù hợp
BR07.14	Hệ thống phải xử lý lỗi kết nối và thông báo rõ nguyên nhân khi thao tác thất bại
5. Giao diện lập trình ứng dụng (API) đề xuất
Lưu ý: Các đường dẫn API dưới đây là thiết kế đề xuất cho chức năng FE07. Cần đối chiếu với mã nguồn phía máy chủ và kiến trúc hiện tại trước khi triển khai.
Phương thức	Đường dẫn API	Chức năng
GET	/api/conversations	Lấy danh sách cuộc trò chuyện của người dùng
POST	/api/conversations	Tạo cuộc trò chuyện
GET	/api/conversations/{conversationId}/messages	Lấy lịch sử tin nhắn
POST	/api/conversations/{conversationId}/messages	Gửi tin nhắn
POST	/api/conversations/{conversationId}/attachments	Tải tệp đính kèm lên cuộc trò chuyện
POST	/api/meetings	Tạo cuộc họp hoặc lịch họp
GET	/api/meetings	Lấy danh sách cuộc họp
GET	/api/meetings/{meetingId}	Xem chi tiết cuộc họp
PATCH	/api/meetings/{meetingId}	Cập nhật thông tin cuộc họp
DELETE	/api/meetings/{meetingId}	Hủy cuộc họp theo quyền hạn
POST	/api/meetings/{meetingId}/join	Kiểm tra quyền và yêu cầu tham gia cuộc họp
POST	/api/meetings/{meetingId}/leave	Ghi nhận người dùng rời cuộc họp
GET	/api/meetings/{meetingId}/participants	Lấy danh sách người tham gia
POST	/api/meetings/{meetingId}/participants	Mời thành viên tham gia
DELETE	/api/meetings/{meetingId}/participants/{userId}	Hủy lời mời hoặc loại thành viên theo quyền
GET	/api/meetings/{meetingId}/minutes	Xem biên bản cuộc họp
PUT	/api/meetings/{meetingId}/minutes	Tạo hoặc cập nhật biên bản cuộc họp
5.1. Ví dụ dữ liệu tạo cuộc họp
json
{
  "title": "Hop nhom du an PBL",
  "description": "Thao luan tien do va phan cong cong viec",
  "project_id": 15,
  "start_time": "2026-11-02T09:00:00+07:00",
  "end_time": "2026-11-02T10:00:00+07:00",
  "participant_ids": [101, 102, 103]
}


Trong đó:

title: Tiêu đề cuộc họp.
description: Nội dung cuộc họp.
project_id: Dự án liên quan.
start_time, end_time: Thời gian bắt đầu và kết thúc.
participant_ids: Danh sách tài khoản được mời.
5.2. Ví dụ dữ liệu gửi tin nhắn
json
{
  "content": "Nhom da hoan thanh phan phan tich yeu cau.",
  "message_type": "text"
}


Trong đó, message_type xác định loại tin nhắn. Nếu hỗ trợ tệp đính kèm, thông tin tệp cần được xử lý và kiểm tra theo quy định riêng.

5.3. Ví dụ dữ liệu biên bản họp
json
{
  "summary": "Thong nhat pham vi va tien do du an.",
  "decisions": [
    "Hoan thanh thiet ke giao dien",
    "Cap nhat tien do truoc buoi hop tiep theo"
  ],
  "action_items": [
    {
      "title": "Hoan thien giao dien",
      "assignee_id": 101,
      "due_date": "2026-11-05"
    }
  ]
}


Lưu ý triển khai: Các API báo hiệu kết nối WebRTC, truyền media và chia sẻ màn hình có thể cần dịch vụ signaling hoặc nền tảng họp trực tuyến chuyên dụng. Những hoạt động media không nên được xử lý như các yêu cầu REST thông thường.

6. Mô hình dữ liệu đề xuất

Các bảng dưới đây là phương án thiết kế đề xuất. Cần đối chiếu với sơ đồ ERD và tệp database/schema.sql của dự án trước khi triển khai.

Tên bảng	Chức năng
users	Lưu thông tin tài khoản người dùng hiện có
conversations	Lưu thông tin cuộc trò chuyện
conversation_members	Liên kết người dùng với cuộc trò chuyện
messages	Lưu nội dung tin nhắn
message_attachments	Lưu metadata của tệp đính kèm, nếu hỗ trợ
meetings	Lưu thông tin cuộc họp và lịch họp
meeting_participants	Lưu danh sách người được mời và người tham gia
meeting_minutes	Lưu biên bản và kết luận cuộc họp
meeting_action_items	Lưu các công việc phát sinh sau cuộc họp, nếu cần
6.1. Sơ đồ quan hệ dữ liệu đề xuất
mermaid
erDiagram
    USERS ||--o{ CONVERSATION_MEMBERS : joins
    CONVERSATIONS ||--o{ CONVERSATION_MEMBERS : includes
    CONVERSATIONS ||--o{ MESSAGES : contains
    USERS ||--o{ MESSAGES : sends
    MESSAGES ||--o{ MESSAGE_ATTACHMENTS : has
    USERS ||--o{ MEETINGS : organizes
    MEETINGS ||--o{ MEETING_PARTICIPANTS : includes
    USERS ||--o{ MEETING_PARTICIPANTS : participates
    MEETINGS ||--o{ MEETING_MINUTES : records
    MEETING_MINUTES ||--o{ MEETING_ACTION_ITEMS : contains

    USERS {
        int id PK
        string role
        string status
    }

    CONVERSATIONS {
        int id PK
        string conversation_type
        int project_id
        datetime created_at
    }

    CONVERSATION_MEMBERS {
        int conversation_id PK, FK
        int user_id PK, FK
        datetime joined_at
    }

    MESSAGES {
        int id PK
        int conversation_id FK
        int sender_id FK
        text content
        string message_type
        datetime sent_at
    }

    MEETINGS {
        int id PK
        int organizer_id FK
        int project_id
        string title
        datetime start_time
        datetime end_time
        string meeting_link
        string status
    }

    MEETING_PARTICIPANTS {
        int meeting_id PK, FK
        int user_id PK, FK
        string invitation_status
        datetime joined_at
        datetime left_at
    }

    MEETING_MINUTES {
        int id PK
        int meeting_id FK
        int author_id FK
        text summary
        datetime updated_at
    }

    MEETING_ACTION_ITEMS {
        int id PK
        int minutes_id FK
        string title
        int assignee_id FK
        date due_date
        string status
    }

6.2. Một số ràng buộc dữ liệu quan trọng
conversation_members: Không cho phép một người dùng được thêm nhiều lần vào cùng một cuộc trò chuyện.
meeting_participants: Không cho phép trùng người tham gia trong cùng một cuộc họp.
messages: Người gửi phải là thành viên có quyền gửi tin nhắn trong cuộc trò chuyện.
meetings: end_time phải lớn hơn start_time.
meeting_minutes: Chỉ người có quyền mới được tạo hoặc chỉnh sửa biên bản.
Các khóa ngoại phải tham chiếu đến tài khoản và thực thể hợp lệ trong hệ thống.
Nếu sử dụng dịch vụ họp bên ngoài, thông tin định danh phòng họp và liên kết tham gia cần được quản lý an toàn.
7. Tiêu chí nghiệm thu
Người dùng đăng nhập có thể truy cập chức năng giao tiếp theo quyền hạn.
Người dùng có thể gửi và nhận tin nhắn trong cuộc trò chuyện được phép.
Tin nhắn được lưu và có thể xem lại theo quyền truy cập.
Người dùng có thể tạo hoặc tham gia cuộc họp hợp lệ.
Người tham gia có thể bật/tắt micro và camera khi thiết bị, trình duyệt và quyền truy cập cho phép.
Người có quyền có thể chia sẻ và dừng chia sẻ màn hình.
Người dùng có thể tạo, xem, cập nhật và hủy lịch họp theo quyền hạn.
Hệ thống gửi thông báo khi lịch họp được tạo, thay đổi hoặc hủy.
Hệ thống kiểm tra quyền tham gia và quyền quản lý phòng họp.
Danh sách người tham gia không phát sinh bản ghi trùng.
Người dùng có quyền có thể xem và cập nhật biên bản cuộc họp.
Hệ thống xử lý lỗi kết nối và hiển thị thông báo phù hợp.
Hệ thống bảo vệ tin nhắn, liên kết phòng họp và biên bản khỏi truy cập trái phép.
Chức năng video/audio và chia sẻ màn hình hoạt động trong môi trường trình duyệt được hỗ trợ.
8. Kịch bản kiểm thử
Mã kiểm thử	Trường hợp kiểm thử	Kết quả mong đợi
TC07.01	Gửi tin nhắn trong cuộc trò chuyện hợp lệ	Tin nhắn được gửi và hiển thị cho người nhận
TC07.02	Người không thuộc cuộc trò chuyện yêu cầu đọc tin nhắn	Hệ thống từ chối truy cập
TC07.03	Gửi tin nhắn khi mất kết nối	Hệ thống thông báo trạng thái gửi thất bại hoặc chờ đồng bộ
TC07.04	Tạo cuộc họp với dữ liệu hợp lệ	Cuộc họp được tạo thành công
TC07.05	Tạo cuộc họp có thời gian kết thúc trước thời gian bắt đầu	Hệ thống từ chối và thông báo lỗi
TC07.06	Người không có quyền tham gia mở liên kết phòng họp	Hệ thống từ chối hoặc yêu cầu xác thực quyền
TC07.07	Người tham gia bật/tắt micro và camera	Trạng thái thiết bị được cập nhật phù hợp
TC07.08	Chia sẻ màn hình khi trình duyệt cho phép	Nội dung được chia sẻ trong cuộc họp
TC07.09	Người dùng từ chối quyền chia sẻ màn hình	Hệ thống thông báo và không bắt đầu chia sẻ
TC07.10	Cập nhật lịch họp	Thông tin mới được lưu và người liên quan được thông báo
TC07.11	Hủy lịch họp	Cuộc họp được chuyển sang trạng thái hủy và có thông báo
TC07.12	Mời trùng một người vào cùng cuộc họp	Không tạo bản ghi thành viên trùng
TC07.13	Người không có quyền sửa biên bản	Hệ thống từ chối thao tác
TC07.14	Người tham gia rời cuộc họp	Hệ thống cập nhật trạng thái tham gia phù hợp
9. Kế hoạch triển khai
Khảo sát mã nguồn, kiến trúc hiện tại và cơ sở dữ liệu của CollabSphere.
Xây dựng giao diện danh sách cuộc trò chuyện và khung chat.
Xây dựng cơ chế gửi/nhận tin nhắn thời gian thực.
Xây dựng giao diện tạo lịch họp và danh sách cuộc họp.
Tích hợp dịch vụ video/audio và signaling phù hợp.
Xây dựng chức năng chia sẻ màn hình và quản lý người tham gia.
Xây dựng chức năng lịch sử cuộc họp và biên bản họp.
Kết nối API với cơ sở dữ liệu và dịch vụ thông báo.
Kiểm tra phân quyền, bảo mật và xử lý lỗi kết nối.
Kiểm thử các trường hợp thành công, thất bại và nhiều người tham gia đồng thời.
10. Tài liệu liên quan
FE01 – Quản lý tài khoản người dùng.
FE03 – Quản lý lớp học.
FE04 – Quản lý dự án.
FE05 – Quản lý nhóm và thành viên.
FE12 – Quản lý phân quyền.
Lưu ý: Tên và mã các chức năng liên quan cần được đối chiếu với danh mục chức năng chính thức của dự án CollabSphere trước khi chốt tài liệu.
