# FE12 – Quản lý thông báo

## 1. Mô tả chức năng

Chức năng quản lý thông báo giúp giảng viên và sinh viên cập nhật kịp thời các hoạt động liên quan đến lớp học, dự án, nhiệm vụ và kết quả đánh giá trên hệ thống CollabSphere.

Hệ thống tự động gửi thông báo khi có sự kiện mới, giúp người dùng theo dõi tiến độ học tập và không bỏ lỡ các thông tin quan trọng.

## 2. Đối tượng sử dụng

- Sinh viên: Nhận thông báo về dự án, nhiệm vụ, thời hạn nộp bài, kết quả đánh giá và hoạt động nhóm.
- Giảng viên: Nhận thông báo về bài nộp, tiến độ dự án, hoạt động của nhóm và các vấn đề cần xử lý.
- Quản trị viên: Quản lý các thông báo hệ thống và thông báo chung.

## 3. Các chức năng chính

### 3.1. Xem danh sách thông báo

Người dùng có thể xem danh sách thông báo được gửi đến tài khoản của mình.

Mỗi thông báo hiển thị các thông tin:
- Tiêu đề thông báo.
- Nội dung thông báo.
- Thời gian gửi.
- Trạng thái đã đọc hoặc chưa đọc.

### 3.2. Nhận thông báo tự động

Hệ thống tự động tạo thông báo khi xảy ra các sự kiện như:
- Giảng viên giao dự án mới cho lớp.
- Sinh viên được phân công vào nhóm.
- Có nhiệm vụ mới hoặc nhiệm vụ sắp đến hạn.
- Thành viên nhóm nộp bài.
- Giảng viên cập nhật kết quả đánh giá.
- Có thông báo hoặc thay đổi quan trọng từ hệ thống.

### 3.3. Đánh dấu thông báo đã đọc

Người dùng có thể đánh dấu từng thông báo là đã đọc hoặc đánh dấu tất cả thông báo là đã đọc.

Hệ thống cập nhật trạng thái thông báo và số lượng thông báo chưa đọc.

### 3.4. Xóa thông báo

Người dùng có thể xóa các thông báo không còn cần thiết khỏi danh sách thông báo cá nhân.

Việc xóa thông báo không làm ảnh hưởng đến dữ liệu dự án, nhiệm vụ hoặc kết quả đánh giá liên quan.

### 3.5. Lọc thông báo

Hệ thống cho phép người dùng lọc thông báo theo trạng thái:
- Tất cả thông báo.
- Thông báo chưa đọc.
- Thông báo đã đọc.

## 4. Quy trình xử lý

1. Hệ thống phát sinh một sự kiện liên quan đến người dùng.
2. Hệ thống xác định người nhận thông báo.
3. Hệ thống tạo và lưu thông báo vào cơ sở dữ liệu.
4. Người dùng truy cập mục Thông báo.
5. Hệ thống hiển thị danh sách thông báo của người dùng.
6. Người dùng có thể xem, lọc, đánh dấu đã đọc hoặc xóa thông báo.
7. Hệ thống cập nhật trạng thái và hiển thị kết quả tương ứng.

## 5. Dữ liệu thông báo

| Trường dữ liệu | Mô tả |
|---|---|
| notification_id | Mã thông báo |
| user_id | Mã người nhận thông báo |
| title | Tiêu đề thông báo |
| content | Nội dung thông báo |
| notification_type | Loại thông báo |
| created_at | Thời gian tạo thông báo |
| is_read | Trạng thái đã đọc |
| related_id | Mã đối tượng liên quan |
| related_type | Loại đối tượng liên quan |

## 6. Yêu cầu nghiệp vụ

- Mỗi thông báo phải được gắn với một người nhận cụ thể.
- Người dùng chỉ được xem và thao tác trên thông báo thuộc tài khoản của mình.
- Thông báo mới phải có trạng thái chưa đọc.
- Hệ thống phải cập nhật số lượng thông báo chưa đọc khi người dùng thay đổi trạng thái.
- Thông báo phải hiển thị đúng thời gian và nội dung sự kiện.
- Việc xóa thông báo không được làm mất dữ liệu nghiệp vụ liên quan.

## 7. Kết quả đầu ra

Người dùng có thể theo dõi các hoạt động học tập, cập nhật thông tin kịp thời và chủ động thực hiện các nhiệm vụ được giao.

Chức năng giúp tăng khả năng tương tác giữa giảng viên và sinh viên, đồng thời hỗ trợ quản lý tiến độ học tập theo phương pháp Project-Based Learning (PBL).
