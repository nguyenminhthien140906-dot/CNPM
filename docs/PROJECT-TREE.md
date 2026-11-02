# CollabSphere — Tổng quan dự án

## 1. Thông tin chung

- **Tên dự án:** CollabSphere
- **Tên tiếng Việt:** Hệ thống hỗ trợ việc học theo phương pháp Project-Based Learning (PBL)
- **Lĩnh vực:** Giáo dục và hỗ trợ học tập
- **Loại hệ thống:** Ứng dụng hỗ trợ tổ chức và quản lý hoạt động học tập theo dự án
- **Đối tượng sử dụng chính:** Cán bộ quản lý, giảng viên và sinh viên

CollabSphere được định hướng là hệ thống hỗ trợ quá trình học tập thông qua dự án. Hệ thống tập trung các hoạt động tổ chức lớp học, tạo dự án, phân công công việc, cộng tác, theo dõi tiến độ và đánh giá kết quả.

## 2. Bối cảnh và vấn đề cần giải quyết

Trong quá trình học tập theo dự án, giảng viên và sinh viên cần phối hợp nhiều hoạt động khác nhau. Nếu thông tin được lưu trữ rời rạc ở nhiều công cụ, việc theo dõi tiến độ, quản lý nhiệm vụ và tổng hợp kết quả có thể gặp khó khăn.

Một số vấn đề cần giải quyết gồm:

- Thông tin lớp học, dự án và nhóm cần được tổ chức rõ ràng.
- Sinh viên cần biết nhiệm vụ được giao, thời hạn và yêu cầu hoàn thành.
- Giảng viên cần theo dõi tiến độ thực hiện của các nhóm.
- Thành viên cần có nơi trao đổi và chia sẻ tài nguyên phục vụ dự án.
- Kết quả thực hiện cần được lưu lại để phục vụ việc đánh giá và phản hồi.
- Các hoạt động cần được liên kết với lớp học, môn học và dự án tương ứng.

CollabSphere hướng đến việc tập trung các hoạt động này trong một hệ thống thống nhất, giúp quá trình tổ chức và thực hiện dự án rõ ràng hơn.

## 3. Mục tiêu hệ thống

### 3.1. Mục tiêu tổng quát

Xây dựng hệ thống hỗ trợ giảng viên và sinh viên tổ chức, thực hiện, theo dõi và đánh giá hoạt động học tập theo phương pháp Project-Based Learning.

### 3.2. Mục tiêu cụ thể

- Hỗ trợ quản lý tài khoản và vai trò người dùng.
- Hỗ trợ quản lý môn học, đề cương và lớp học.
- Hỗ trợ tạo, cập nhật và tổ chức dự án học tập.
- Hỗ trợ phân nhóm và giao nhiệm vụ cho thành viên.
- Hỗ trợ theo dõi tiến độ dự án thông qua nhiệm vụ, cột mốc và điểm kiểm tra.
- Hỗ trợ trao đổi, cộng tác và chia sẻ tài nguyên trong quá trình thực hiện dự án.
- Hỗ trợ nộp sản phẩm, đánh giá và phản hồi kết quả học tập.
- Hỗ trợ cung cấp thông báo liên quan đến hoạt động của người dùng.

## 4. Phạm vi hệ thống

### 4.1. Phạm vi chức năng

Hệ thống được định hướng hỗ trợ các nhóm chức năng sau:

- Quản lý người dùng và phân quyền.
- Quản lý môn học, đề cương và lớp học.
- Quản lý dự án học tập.
- Quản lý nhóm và nhiệm vụ.
- Quản lý cột mốc và điểm kiểm tra.
- Hỗ trợ giao tiếp và cộng tác.
- Quản lý tài nguyên học tập.
- Hỗ trợ nộp bài, đánh giá và phản hồi.
- Hỗ trợ thông báo cho người dùng.

Danh sách chức năng chi tiết được trình bày trong các tài liệu FE tương ứng trong thư mục `docs/`.

### 4.2. Ngoài phạm vi

Các chức năng chưa được mô tả trong tài liệu yêu cầu hoặc chưa có trong mã nguồn không được xem là thuộc phạm vi triển khai hiện tại. Việc bổ sung chức năng mới cần được thống nhất và cập nhật vào tài liệu dự án.

## 5. Đối tượng sử dụng và vai trò

### 5.1. Admin

Admin phụ trách các hoạt động quản trị hệ thống, chẳng hạn như quản lý tài khoản và hỗ trợ kiểm soát hoạt động của hệ thống theo quyền được cấp.

### 5.2. Staff

Staff hỗ trợ các hoạt động quản lý học vụ như môn học, đề cương và lớp học theo phạm vi trách nhiệm được phân công.

### 5.3. Head Department

Head Department tham gia xem xét hoặc phê duyệt dự án theo quy trình nghiệp vụ được quy định trong hệ thống.

### 5.4. Lecturer

Lecturer tổ chức hoạt động học tập theo dự án, tạo và cập nhật dự án, giao nhiệm vụ, theo dõi tiến độ và đánh giá kết quả của sinh viên.

### 5.5. Student

Student tham gia lớp học và nhóm dự án, tiếp nhận nhiệm vụ, phối hợp với các thành viên, cập nhật tiến độ và nộp sản phẩm học tập.

Quyền truy cập cụ thể của từng vai trò cần được xác định trong tài liệu chức năng và được kiểm tra ở cả giao diện lẫn backend.

## 6. Quy trình nghiệp vụ tổng quát

Quy trình học tập theo dự án có thể được mô tả khái quát như sau:

1. Nhà trường hoặc bộ phận phụ trách thiết lập thông tin môn học và lớp học.
2. Giảng viên xây dựng dự án dựa trên mục tiêu học tập và nội dung môn học.
3. Dự án được xem xét hoặc phê duyệt theo quy trình của hệ thống.
4. Dự án được phân bổ cho lớp học hoặc nhóm sinh viên phù hợp.
5. Giảng viên tổ chức nhóm, nhiệm vụ, thời hạn và các cột mốc.
6. Sinh viên thực hiện nhiệm vụ, cộng tác và cập nhật tiến độ.
7. Giảng viên theo dõi quá trình thực hiện và hỗ trợ khi cần thiết.
8. Sinh viên nộp sản phẩm hoặc kết quả thực hiện.
9. Giảng viên đánh giá, phản hồi và ghi nhận kết quả.
10. Thông tin dự án được lưu lại để phục vụ tra cứu và theo dõi.

Quy trình chi tiết có thể khác nhau tùy chức năng và quy định nghiệp vụ được thống nhất trong nhóm.

## 7. Công nghệ và kiến trúc dự kiến

| Thành phần | Công nghệ |
|---|---|
| Frontend | ReactJS |
| Backend | ASP.NET Core Web API |
| Cơ sở dữ liệu | PostgreSQL |

Hệ thống được định hướng theo mô hình frontend–backend–database:

- **Frontend:** cung cấp giao diện để người dùng thao tác với hệ thống.
- **Backend:** tiếp nhận yêu cầu, kiểm tra quyền, xử lý nghiệp vụ và truy xuất dữ liệu.
- **Database:** lưu trữ thông tin người dùng, môn học, lớp học, dự án và các dữ liệu liên quan.

Công nghệ, thư viện và cấu hình cụ thể cần được xác nhận theo mã nguồn hiện tại. Không xem các thành phần mới chỉ được đề xuất trong tài liệu là đã được tích hợp.

## 8. Cấu trúc tài liệu và mã nguồn

Thư mục `docs/` chứa tài liệu tổng quan, tài liệu chức năng và các tài liệu phân tích, thiết kế liên quan.

Thư mục `database/` dùng để lưu tài liệu thiết kế dữ liệu, schema SQL và các tệp phục vụ thay đổi cơ sở dữ liệu.

Thư mục `backend/` chứa mã nguồn xử lý nghiệp vụ và cung cấp API cho ứng dụng.

Thư mục frontend, nếu có trong phiên bản dự án hiện tại, chứa mã nguồn giao diện người dùng.

Cây thư mục chi tiết và tên tệp thực tế được trình bày trong `PROJECT-TREE.md`.

## 9. Nguyên tắc thiết kế

- **Phân quyền rõ ràng:** người dùng chỉ được truy cập chức năng phù hợp với vai trò.
- **Dữ liệu nhất quán:** dữ liệu giữa giao diện, API và cơ sở dữ liệu cần thống nhất.
- **Nghiệp vụ rõ ràng:** mỗi chức năng cần có điều kiện đầu vào, quy trình xử lý và kết quả đầu ra.
- **Dễ bảo trì:** các thành phần cần được tổ chức rõ ràng để thuận tiện cập nhật.
- **An toàn:** dữ liệu và thao tác cần được kiểm tra ở backend, không chỉ dựa vào giao diện.
- **Có thể mở rộng:** cấu trúc hệ thống cần hỗ trợ việc bổ sung chức năng khi yêu cầu thay đổi.

## 10. Tài liệu liên quan

- [Danh mục tài liệu dự án](README.md)
- [Cây thư mục dự án](PROJECT-TREE.md)
- [Hướng dẫn quy ước API](API-CONTRACT-GUIDE.md)
- [Thiết kế ERD](../database/01-erd.md)
- [Database schema](../database/schema.sql)

## 11. Trạng thái tài liệu

Tài liệu này mô tả định hướng, mục tiêu và phạm vi của CollabSphere. Nội dung cần được cập nhật khi nhóm thống nhất thay đổi về nghiệp vụ, kiến trúc, công nghệ hoặc cấu trúc mã nguồn.

Thông tin trong tài liệu không thay thế việc kiểm tra mã nguồn, cấu hình và kết quả chạy thử của hệ thống.
