# FE12 – Quản lý thông báo

## 1. Mô tả chức năng

Chức năng quản lý thông báo cho phép hệ thống tự động gửi và quản lý các thông báo liên quan đến hoạt động học tập theo dự án (PBL).

Thông qua chức năng này, sinh viên và giảng viên có thể cập nhật kịp thời các thông tin về dự án, nhiệm vụ, hoạt động nhóm, bài nộp và kết quả đánh giá.

Hệ thống hỗ trợ người dùng xem danh sách thông báo, lọc thông báo theo trạng thái, đánh dấu đã đọc và xóa thông báo không còn cần thiết.

### 1.1. Đối tượng sử dụng

- Sinh viên: Nhận thông báo về dự án, nhiệm vụ, hoạt động nhóm, thời hạn nộp bài và kết quả đánh giá.
- Giảng viên: Nhận thông báo về bài nộp, tiến độ thực hiện dự án và các hoạt động của sinh viên.
- Quản trị viên: Quản lý các thông báo chung và thông báo hệ thống.

### 1.2. Dữ liệu đầu vào

- Thông tin sự kiện phát sinh từ các chức năng khác trong hệ thống.
- Mã người nhận thông báo.
- Tiêu đề và nội dung thông báo.
- Loại thông báo.
- Thời gian tạo thông báo.
- Trạng thái đọc của thông báo.

### 1.3. Dữ liệu đầu ra

- Danh sách thông báo của người dùng.
- Số lượng thông báo chưa đọc.
- Thông tin chi tiết của từng thông báo.
- Trạng thái thông báo sau khi người dùng thao tác.
- Kết quả thực hiện các thao tác xem, đánh dấu đã đọc và xóa thông báo.

## 2. Các chức năng chi tiết

### 2.1. Tạo và gửi thông báo

Hệ thống tự động tạo thông báo khi phát sinh các sự kiện liên quan đến hoạt động học tập.

Các sự kiện có thể bao gồm:

- Giảng viên tạo dự án mới và giao dự án cho lớp.
- Sinh viên được thêm vào nhóm hoặc được phân công nhiệm vụ.
- Nhiệm vụ mới được giao hoặc sắp đến hạn.
- Sinh viên nộp bài hoặc giảng viên cập nhật kết quả đánh giá.
- Có thông báo quan trọng từ giảng viên hoặc quản trị viên.

Hệ thống xác định người nhận phù hợp, tạo thông báo và lưu thông tin vào cơ sở dữ liệu.

### 2.2. Xem danh sách thông báo

Người dùng có thể truy cập mục Thông báo để xem các thông báo được gửi đến tài khoản của mình.

Danh sách thông báo hiển thị:

- Tiêu đề và nội dung thông báo.
- Thời gian tạo thông báo.
- Loại thông báo.
- Trạng thái đã đọc hoặc chưa đọc.

Khi người dùng mở một thông báo, hệ thống có thể cập nhật trạng thái thông báo thành đã đọc.

### 2.3. Lọc thông báo

Hệ thống cho phép người dùng lọc danh sách thông báo theo trạng thái:

- Tất cả thông báo.
- Thông báo chưa đọc.
- Thông báo đã đọc.

Danh sách hiển thị được cập nhật theo trạng thái lọc mà người dùng lựa chọn.

### 2.4. Đánh dấu thông báo đã đọc

Người dùng có thể đánh dấu từng thông báo hoặc toàn bộ thông báo là đã đọc.

Sau khi thực hiện, hệ thống cập nhật trạng thái tương ứng và tính lại số lượng thông báo chưa đọc.

### 2.5. Xóa thông báo

Người dùng có thể xóa thông báo không còn cần thiết khỏi danh sách cá nhân.

Hệ thống kiểm tra quyền truy cập, thực hiện thao tác xóa và cập nhật lại danh sách thông báo.

Việc xóa thông báo không làm ảnh hưởng đến dữ liệu nghiệp vụ liên quan như dự án, nhiệm vụ hoặc kết quả đánh giá.

## 3. Quy trình xử lý

### 3.1. Quy trình tạo và gửi thông báo

1. Người dùng thực hiện một hoạt động trên hệ thống.
2. Hệ thống ghi nhận sự kiện phát sinh.
3. Hệ thống xác định đối tượng nhận thông báo.
4. Hệ thống tạo thông báo với nội dung và thời gian tương ứng.
5. Thông báo được lưu vào cơ sở dữ liệu.
6. Hệ thống cập nhật danh sách thông báo của người nhận.

### 3.2. Quy trình quản lý thông báo

1. Người dùng truy cập chức năng Quản lý thông báo.
2. Hệ thống hiển thị danh sách thông báo của tài khoản.
3. Người dùng lựa chọn thao tác xem, lọc, đánh dấu đã đọc hoặc xóa thông báo.
4. Hệ thống kiểm tra quyền truy cập và tính hợp lệ của yêu cầu.
5. Hệ thống thực hiện thao tác và cập nhật dữ liệu.
6. Danh sách thông báo được hiển thị lại theo trạng thái mới.

## 4. Thiết kế dữ liệu

### 4.1. Bảng NOTIFICATIONS

| Tên trường | Kiểu dữ liệu | Mô tả |
|---|---|---|
| id | UUID | Mã thông báo, khóa chính |
| user_id | UUID | Mã người nhận, khóa ngoại |
| title | VARCHAR | Tiêu đề thông báo |
| content | TEXT | Nội dung thông báo |
| notification_type | VARCHAR | Loại thông báo |
| related_id | UUID | Mã đối tượng liên quan |
| related_type | VARCHAR | Loại đối tượng liên quan |
| is_read | BOOLEAN | Trạng thái đã đọc |
| created_at | TIMESTAMP | Thời gian tạo thông báo |

### 4.2. Mối quan hệ dữ liệu

- Một người dùng có thể nhận nhiều thông báo.
- Mỗi thông báo thuộc về một người dùng.
- Một thông báo có thể liên quan đến một dự án, nhiệm vụ, bài nộp hoặc hoạt động khác trong hệ thống.

Quan hệ chính:

USERS (1) —— (N) NOTIFICATIONS

## 5. Yêu cầu nghiệp vụ

- Mỗi thông báo phải xác định được người nhận.
- Người dùng chỉ được xem và thao tác trên thông báo thuộc tài khoản của mình.
- Thông báo mới phải có trạng thái chưa đọc.
- Hệ thống phải cập nhật chính xác số lượng thông báo chưa đọc.
- Nội dung thông báo phải phản ánh đúng sự kiện phát sinh.
- Việc xóa thông báo không được làm mất dữ liệu nghiệp vụ liên quan.
- Hệ thống phải kiểm tra quyền truy cập trước khi thực hiện các thao tác trên thông báo.

## 6. Kết quả đạt được

Chức năng Quản lý thông báo giúp giảng viên và sinh viên cập nhật kịp thời các hoạt động học tập, nâng cao khả năng tương tác và hỗ trợ theo dõi tiến độ thực hiện dự án.

Thông qua đó, CollabSphere góp phần xây dựng môi trường học tập chủ động, cộng tác và hiệu quả theo phương pháp Project-Based Learning (PBL).
