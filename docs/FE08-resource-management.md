QUẢN LÝ TÀI NGUYÊNMã chức năng: FE08Tên chức năng: Quản lý tài nguyênHệ thống: CollabSphere – Hệ thống hỗ trợ học tập theo phương pháp học tập dựa trên dự án (Project-Based Learning – PBL)   Tác nhân chính: Giảng viên (Lecturer), Sinh viên (Student)   1. Tổng quan chức năng1.1. Mục tiêuChức năng Quản lý tài nguyên cho phép giảng viên và sinh viên tải lên, lưu trữ, chia sẻ và tổ chức các tệp tin, tài liệu học tập, bài giảng, slide và mã nguồn phục vụ trực tiếp cho hoạt động dạy - học và thực hiện dự án PBL. Chức năng giúp tập trung dữ liệu, kiểm soát phân quyền truy cập và hỗ trợ theo dõi phiên bản tệp tin.   1.2. Phạm vi chức năngMã chức năngTên chức năngMô tảFE08.01Quản lý tài nguyên lớp họcGiảng viên tải lên, phân loại và chia sẻ bài giảng, đề tài, tài liệu tham khảo cho toàn lớpFE08.02Quản lý tài nguyên nhómSinh viên tải lên, chia sẻ tệp tin, tài liệu, slide, mã nguồn trong nội bộ nhóm dự án   FE08.03Tìm kiếm & Xem trực tuyếnTìm kiếm tệp tin theo từ khóa/phân loại, hỗ trợ xem trước (preview) và tải về tệpFE08.04Phân quyền & Quản lý phiên bảnKiểm soát quyền truy cập/chỉnh sửa tệp và lưu vết các phiên bản cập nhật của tài liệu1.3. Đối tượng sử dụngVai tròQuyền hạnLecturer (Giảng viên)Quản lý toàn bộ tài nguyên lớp; xem, tải về và quản lý tài nguyên của các nhóm dự án trực thuộc   Student (Sinh viên)Tải lên, chia sẻ, chỉnh sửa và xóa tài liệu trong nhóm dự án của mình; xem và tải tài nguyên lớp   Admin (Quản trị viên)Cấu hình dung lượng lưu trữ tối đa, định dạng tệp cho phép và dọn dẹp bộ nhớ hệ thống2. Mô tả chi tiết chức năng2.1. Quản lý tài nguyên lớp họcMục đích: Giúp giảng viên chia sẻ các tài nguyên học tập chung cho toàn bộ sinh viên trong lớp.   Quy trình thực hiện:Giảng viên truy cập vào lớp học và chọn mục "Tài nguyên lớp".Chọn "Tải tệp lên" hoặc tạo thư mục mới để tổ chức tài liệu.Chọn tệp từ máy tính, nhập tên hiển thị và mô tả tệp.Hệ thống kiểm tra định dạng, dung lượng và quét mã độc.Lưu tệp và hiển thị công khai cho tất cả sinh viên trong lớp học.2.2. Quản lý tài nguyên nhóm dự ánMục đích: Cho phép các thành viên trong nhóm dự án cùng đóng góp, lưu trữ và quản lý tài liệu, slide thuyết trình.   Các thao tác chính:Tải tệp tin/tài liệu/slide phục vụ dự án lên không gian lưu trữ của nhóm.   Tạo các thư mục con (ví dụ: Báo cáo, Slide, Source Code, Tài liệu thiết kế).Chia sẻ đường dẫn truy cập tệp nội bộ cho các thành viên hoặc giảng viên phụ trách.   Cập nhật phiên bản mới cho tệp tin đã tồn tại.Xóa tệp không còn sử dụng (chỉ người tải lên hoặc Nhóm trưởng/Giảng viên mới có quyền).2.3. Xem trực tuyến và Tìm kiếm tài liệuCác thao tác chính:Tìm kiếm tệp theo tên, người tải lên, định dạng hoặc khoảng thời gian.Lọc tài liệu theo danh mục (Bài giảng, Slide, Báo cáo, Tài liệu tham khảo).Xem trước trực tuyến các định dạng phổ biến (.pdf, .docx, .pptx, .png, .jpg) mà không cần tải về.Tải tệp tin về thiết bị cá nhân.3. Quy trình nghiệp vụĐoạn mãflowchart TD
    A([Bắt đầu]) --> B[Người dùng truy cập Quản lý tài nguyên]
    B --> C{Chọn không gian lưu trữ}
    C -->|Tài nguyên Lớp| D[Truy cập tài nguyên Lớp học]
    C -->|Tài nguyên Nhóm| E[Truy cập tài nguyên Nhóm dự án]
    
    D --> F{Chọn thao tác}
    E --> F
    
    F -->|Tải tệp lên| G[Chọn tệp & Nhập thông tin]
    G --> H[Hệ thống kiểm tra định dạng & dung lượng]
    H --> I{Tệp hợp lệ?}
    I -->|Không| J[Thông báo lỗi] --> G
    I -->|Có| K[Lưu tệp & Phân quyền truy cập]
    
    F -->|Tìm kiếm / Tải về| L[Nhập từ khóa / Lọc tài liệu]
    L --> M[Kiểm tra quyền truy cập]
    M --> N[Hiển thị bản xem trước / Cho phép tải về]
    
    F -->|Xóa / Cập nhật| O[Chọn tệp cần thao tác]
    O --> P{Kiểm tra quyền sở hữu}
    P -->|Không đủ quyền| Q[Báo lỗi từ chối]
    P -->|Có quyền| R[Cập nhật/Xóa tệp vào Thùng rác]
    
    K --> S([Kết thúc])
    N --> S
    R --> S
4. Quy tắc nghiệp vụMã quy tắcNội dungBR08.01Các định dạng tệp tin được phép tải lên: .pdf, .docx, .xlsx, .pptx, .zip, .rar, .png, .jpg (Nghiêm cấm tệp thực thi như .exe, .bat, .sh, .msi)BR08.02Dung lượng tối đa cho từng tệp tải lên là 50MB. Tổng dung lượng lưu trữ tối đa cho mỗi nhóm dự án là 2GBBR08.03Sinh viên chỉ có quyền sửa/xóa tệp tin do chính mình tải lên hoặc các tệp trong nhóm dự án của mình   BR08.04Giảng viên có toàn quyền xem, tải về và xóa/quản lý tài nguyên của lớp học và toàn bộ các nhóm dự án trong lớp   BR08.05Khi tải lên tệp trùng tên trong cùng thư mục, hệ thống sẽ tự động tạo phiên bản mới (Version 2, Version 3...) thay vì ghi đèBR08.06Tệp bị xóa sẽ nằm trong "Thùng rác" 30 ngày trước khi bị hệ thống xóa vĩnh viễn5. Giao diện lập trình ứng dụng (API) đề xuấtLưu ý: Các đường dẫn API dưới đây là thiết kế đề xuất cho chức năng FE08. Cần đối chiếu với mã nguồn phía máy chủ trước khi triển khai.Phương thứcĐường dẫn APIChức năngGET/api/resourcesLấy danh sách tài nguyên (hỗ trợ query class_id, team_id, folder_id)POST/api/resources/uploadTải lên tệp tin mới (multipart/form-data)GET/api/resources/{resourceId}Xem chi tiết thông tin và xem trước tệpGET/api/resources/{resourceId}/downloadTải về tệp tinPATCH/api/resources/{resourceId}Cập nhật tên/mô tả/di chuyển thư mục tệpDELETE/api/resources/{resourceId}Xóa tệp tin (Đưa vào thùng rác)POST/api/resources/{resourceId}/versionsTải lên phiên bản mới cho tệp tinGET/api/resources/{resourceId}/versionsLấy lịch sử các phiên bản của tệp5.1. Ví dụ dữ liệu tải lên tệp tin (Response)JSON{
  "id": 105,
  "file_name": "Slide_Bao_Cao_PBL_Nhom1.pptx",
  "file_path": "/uploads/teams/12/Slide_Bao_Cao_PBL_Nhom1.pptx",
  "file_size": 15420800,
  "file_type": "application/vnd.openxmlformats-officedocument.presentationml.presentation",
  "uploaded_by": 101,
  "class_id": 5,
  "team_id": 12,
  "version": 1,
  "created_at": "2026-11-02T14:30:00Z"
}
6. Mô hình dữ liệu đề xuấtCác bảng dưới đây là phương án thiết kế đề xuất. Cần đối chiếu với sơ đồ ERD và tệp database/schema.sql của dự án trước khi triển khai.Tên bảngChức năngresourcesLưu thông tin các tệp tin và thư mục tài nguyênresource_versionsLưu lịch sử các phiên bản của tệp tinresource_foldersLưu danh sách thư mục dùng để phân loại tài liệu6.1. Sơ đồ quan hệ dữ liệu đề xuấtĐoạn mãerDiagram
    USERS ||--o{ RESOURCES : uploads
    STUDY_CLASSES ||--o{ RESOURCES : contains
    PROJECT_TEAMS ||--o{ RESOURCES : contains
    RESOURCE_FOLDERS ||--o{ RESOURCES : stores
    RESOURCES ||--o{ RESOURCE_VERSIONS : tracks

    RESOURCES {
        int id PK
        string file_name
        string file_path
        int file_size
        string file_type
        int uploaded_by FK
        int class_id FK
        int team_id FK
        int folder_id FK
        string status
        datetime created_at
    }

    RESOURCE_FOLDERS {
        int id PK
        string folder_name
        int parent_id FK
        int class_id FK
        int team_id FK
    }

    RESOURCE_VERSIONS {
        int id PK
        int resource_id FK
        int version_number
        string file_path
        int uploaded_by FK
        datetime created_at
    }
7. Tiêu chí nghiệm thu[ ] Giảng viên tải lên và tổ chức được bài giảng, tài liệu cho toàn lớp.   [ ] Sinh viên tải lên, chỉnh sửa và chia sẻ được tệp tin, slide trong phạm vi nhóm dự án.   [ ] Giảng viên truy cập và xem/tải được tất cả tài nguyên của các nhóm trong lớp phụ trách.   [ ] Hệ thống ngăn chặn tải lên các tệp tin thực thi nguy hiểm (.exe, .bat...).[ ] Hệ thống báo lỗi rõ ràng khi tệp tải lên vượt quá 50MB hoặc nhóm vượt quá dung lượng 2GB.[ ] Xem trước được các tệp tài liệu dạng PDF và hình ảnh trực tiếp trên trình duyệt.[ ] Sinh viên nhóm này không thể truy cập hoặc xem tệp tin của nhóm khác nếu không được cấp quyền.8. Kịch bản kiểm thửMã kiểm thửTrường hợp kiểm thửKết quả mong đợiTC08.01Sinh viên tải tệp PDF 15MB vào nhóm dự ánTải lên thành công, xuất hiện trong danh sách tài nguyên nhóm   TC08.02Tải lên tệp chứa đuôi .exeHệ thống từ chối tải tệp và cảnh báo an toànTC08.03Tải lên tệp dung lượng 60MBTừ chối tải lên và thông báo vượt quá giới hạn 50MBTC08.04Giảng viên xem tài nguyên của Nhóm 1Xem và tải được toàn bộ tài liệu của Nhóm 1   TC08.05Sinh viên Nhóm 2 truy cập URL tệp của Nhóm 1Từ chối truy cập (HTTP 403 Forbidden)TC08.06Tải lên tệp trùng tênTạo phiên bản mới (Version 2) thành côngTC08.07Xóa tệp tin khỏi nhómTệp chuyển sang trạng thái chờ xóa trong thùng rác9. Kế hoạch triển khaiXây dựng dịch vụ lưu trữ tệp (Cloud Storage S3 / Local Storage Engine).Phát triển API Backend cho việc Upload, Download, Phân quyền và Quản lý thư mục.Xây dựng giao diện Quản lý tài nguyên Lớp & Nhóm (Upload drag-and-drop, danh sách tệp, xem trước).Tích hợp bộ xem trước tài liệu (PDF Viewer, Image Viewer) trên Frontend.Kết nối phân quyền theo Lớp (class_id) và Nhóm (team_id).Kiểm thử hiệu năng tải tệp lớn và kiểm thử bảo mật phân quyền truy cập.10. Tài liệu liên quanFE01 – Quản lý tài khoản người dùngFE03 – Quản lý lớp họcFE04 – Quản lý dự ánFE05 – Quản lý nhóm và thành viênFE12 – Quản lý phân quyền
