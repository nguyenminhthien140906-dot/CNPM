# CollabSphere — Tài liệu dự án

## 1. Giới thiệu

CollabSphere là hệ thống hỗ trợ học tập theo phương pháp Project-Based Learning (PBL). Hệ thống giúp giảng viên tổ chức dự án học tập, giao nhiệm vụ, theo dõi tiến độ và đánh giá kết quả. Đồng thời, sinh viên có thể làm việc nhóm, trao đổi thông tin, thực hiện nhiệm vụ và nộp sản phẩm dự án.

Dự án hướng đến việc tập trung các hoạt động học tập theo dự án trên một hệ thống thống nhất, giúp giảng viên và sinh viên thuận tiện hơn trong quá trình tổ chức, thực hiện và theo dõi kết quả học tập.

## 2. Mục đích thư mục docs

Thư mục `docs/` lưu trữ tài liệu phân tích, thiết kế và mô tả các chức năng của hệ thống CollabSphere.

Các tài liệu trong thư mục này được sử dụng để:

- Thống nhất yêu cầu nghiệp vụ giữa các thành viên trong nhóm.
- Mô tả chức năng, đối tượng sử dụng và quy trình xử lý.
- Làm rõ dữ liệu đầu vào, kết quả đầu ra và các quy tắc nghiệp vụ.
- Làm cơ sở triển khai giao diện người dùng, API và cơ sở dữ liệu.
- Hỗ trợ kiểm tra, cập nhật và bảo trì hệ thống trong quá trình phát triển.

## 3. Danh sách chức năng

| Mã | Chức năng | Tài liệu |
|---|---|---|
| FE01 | Quản lý tài khoản người dùng | [FE01](FE01-user-account-management.md) |
| FE02 | Quản lý môn học và đề cương | [FE02](FE02-subject-syllabus-management.md) |
| FE03 | Quản lý lớp học | [FE03](FE03-class-management.md) |
| FE04 | Quản lý dự án | [FE04](FE04-project-management.md) |
| FE05 | Quản lý nhóm và không gian làm việc | [FE05](FE05-team-workspace-management.md) |
| FE06 | Quản lý cột mốc và điểm kiểm tra | [FE06](FE06-milestone-checkpoint-management.md) |
| FE07 | Giao tiếp và cuộc họp | [FE07](FE07-communication-meetings.md) |
| FE08 | Quản lý tài nguyên học tập | [FE08](FE08-resource-management.md) |
| FE09 | Công cụ cộng tác thời gian thực | [FE09](FE09-realtime-collaboration-tools.md) |
| FE10 | Đánh giá và phản hồi | [FE10](FE10-assessment-feedback.md) |
| FE11 | Hỗ trợ học tập bằng AI | [FE11](FE11-ai-assistance.md) |
| FE12 | Quản lý thông báo | [FE12](FE12-notification-management.md) |

## 4. Cách xem và sử dụng tài liệu

- Mở thư mục dự án CollabSphere bằng Visual Studio Code.
- Mở thư mục `docs/` và chọn tài liệu cần xem.
- Nhấn `Ctrl + Shift + V` để mở bản xem trước Markdown.
- Có thể xem trực tiếp các tệp Markdown trên GitHub.
- Các sơ đồ Mermaid có thể được xem trên những nền tảng hỗ trợ Mermaid.

Khi triển khai một chức năng, nhóm nên tham khảo tài liệu FE tương ứng để nắm rõ yêu cầu, quy trình xử lý và các quy tắc nghiệp vụ liên quan.

## 5. Quy ước cập nhật tài liệu

Khi bổ sung hoặc chỉnh sửa tài liệu, cần đảm bảo:

- Tên chức năng và thuật ngữ được sử dụng thống nhất.
- Quy trình xử lý được trình bày theo đúng thứ tự nghiệp vụ.
- Dữ liệu đầu vào, đầu ra và các trường hợp lỗi được mô tả rõ ràng.
- Thay đổi về yêu cầu được cập nhật trong các tài liệu liên quan.
- Tên API, bảng dữ liệu và các trường thông tin được đối chiếu với mã nguồn thực tế.

## 6. Lưu ý

Các tài liệu trong thư mục `docs/` mô tả yêu cầu và thiết kế của hệ thống, không thay thế cho mã nguồn triển khai.

Các luồng nghiệp vụ, API và ví dụ dữ liệu cần được kiểm tra, đối chiếu với mã nguồn thực tế. Một chức năng chỉ được xem là đã triển khai khi có phần mã nguồn tương ứng và đã được kiểm tra hoạt động.
