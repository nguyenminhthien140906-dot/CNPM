# CollabSphere — Tài liệu dự án

## 1. Giới thiệu

CollabSphere là hệ thống hỗ trợ việc học theo phương pháp Project-Based Learning (PBL). Phương pháp này giúp người học tiếp thu kiến thức thông qua quá trình thực hiện dự án, giải quyết vấn đề, làm việc nhóm và tạo ra sản phẩm học tập.

Hệ thống hướng đến việc hỗ trợ giảng viên tổ chức hoạt động học tập theo dự án và hỗ trợ sinh viên tham gia, cộng tác, theo dõi tiến độ và nộp kết quả. Các thông tin liên quan đến môn học, lớp học, dự án, nhóm, nhiệm vụ và đánh giá được tổ chức trên cùng một hệ thống.

Thư mục `docs/` lưu trữ tài liệu phân tích, thiết kế và mô tả chức năng của CollabSphere. Tài liệu giúp các thành viên trong nhóm thống nhất cách hiểu về yêu cầu trước khi triển khai giao diện, API và cơ sở dữ liệu.

## 2. Mục đích của thư mục docs

Thư mục `docs/` được sử dụng để:

- Lưu trữ tài liệu tổng quan về dự án.
- Mô tả các chức năng và yêu cầu nghiệp vụ của hệ thống.
- Trình bày luồng xử lý chính của từng chức năng.
- Làm rõ dữ liệu đầu vào, dữ liệu đầu ra và các quy tắc nghiệp vụ.
- Làm cơ sở trao đổi giữa các thành viên phụ trách phân tích, thiết kế, frontend, backend và cơ sở dữ liệu.
- Theo dõi và cập nhật tài liệu khi yêu cầu hoặc cách triển khai thay đổi.

Tài liệu trong thư mục này không thay thế mã nguồn. Một chức năng chỉ được xem là đã triển khai khi có phần mã nguồn tương ứng và đã được kiểm tra theo yêu cầu.

## 3. Danh mục tài liệu

| Tài liệu | Nội dung |
|---|---|
| `README.md` | Giới thiệu thư mục tài liệu và hướng dẫn tra cứu |
| `PROJECT.md` | Tổng quan, mục tiêu, phạm vi và các thành phần của dự án |
| `PROJECT-TREE.md` | Cây thư mục và vai trò của các thư mục, tệp chính |
| `API-CONTRACT-GUIDE.md` | Quy ước mô tả API, request, response và lỗi |
| Tài liệu FE01–FE12 | Mô tả yêu cầu và quy trình nghiệp vụ của các chức năng |

Tên tệp và danh mục chức năng cần được cập nhật theo các tệp thực tế trong thư mục `docs/`.

## 4. Cách tra cứu tài liệu

Có thể tra cứu tài liệu theo nhu cầu:

- Muốn tìm hiểu tổng quan dự án: đọc `PROJECT.md`.
- Muốn xem cấu trúc thư mục: đọc `PROJECT-TREE.md`.
- Muốn tìm hiểu một chức năng cụ thể: mở tài liệu FE tương ứng.
- Muốn thống nhất cách mô tả API: đọc `API-CONTRACT-GUIDE.md`.
- Muốn xem thiết kế dữ liệu: tham khảo tài liệu ERD và các tệp SQL trong thư mục `database/`.

## 5. Quy ước khi cập nhật tài liệu

Khi bổ sung hoặc chỉnh sửa tài liệu, cần tuân thủ các nguyên tắc sau:

1. Sử dụng tên chức năng và thuật ngữ thống nhất trong toàn bộ dự án.
2. Mô tả rõ mục tiêu, đối tượng sử dụng, dữ liệu đầu vào và kết quả đầu ra.
3. Trình bày quy trình theo thứ tự xử lý thực tế.
4. Nêu rõ các điều kiện hợp lệ, trường hợp lỗi và giới hạn của chức năng.
5. Không ghi một chức năng là đã hoàn thành nếu chức năng đó mới chỉ có trong tài liệu.
6. Khi thay đổi yêu cầu, cần cập nhật tài liệu liên quan để tránh mâu thuẫn.
7. Đối với API, tên endpoint, phương thức HTTP và cấu trúc dữ liệu cần thống nhất với mã nguồn backend.
8. Đối với cơ sở dữ liệu, tên bảng và cột trong tài liệu cần đối chiếu với ERD và schema SQL.

## 6. Phạm vi tài liệu

Tài liệu trong `docs/` tập trung vào yêu cầu và thiết kế hệ thống, bao gồm:

- Mô tả bài toán và mục tiêu xây dựng hệ thống.
- Đối tượng sử dụng và trách nhiệm của từng vai trò.
- Các chức năng nghiệp vụ của hệ thống.
- Quy trình xử lý chính và các trường hợp ngoại lệ.
- Dữ liệu cần lưu trữ và mối quan hệ giữa các đối tượng.
- Các quy tắc nghiệp vụ và yêu cầu bảo mật liên quan.

Các hướng dẫn cài đặt, cấu hình và chạy ứng dụng cần được xây dựng dựa trên mã nguồn và cấu hình thực tế của từng thành phần.
