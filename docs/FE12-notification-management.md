## 3. Quy trình nghiệp vụ

### 3.1. Quy trình tạo và gửi thông báo

**Đối tượng thực hiện:** Hệ thống, giảng viên, sinh viên.

**Mục đích:** Tự động tạo và gửi thông báo khi có sự kiện liên quan đến hoạt động học tập theo dự án.

**Quy trình:**

1. Người dùng thực hiện một hoạt động trên hệ thống, chẳng hạn tạo dự án, giao nhiệm vụ, nộp bài hoặc cập nhật kết quả đánh giá.
2. Hệ thống ghi nhận sự kiện phát sinh từ chức năng tương ứng.
3. Hệ thống xác định những người dùng cần nhận thông báo dựa trên sự kiện.
4. Hệ thống tạo thông báo với tiêu đề, nội dung, loại thông báo và thời gian tạo.
5. Hệ thống lưu thông báo vào cơ sở dữ liệu.
6. Thông báo được hiển thị trong danh sách thông báo của người nhận.
7. Người dùng có thể truy cập thông báo để xem nội dung chi tiết.

### 3.2. Quy trình xem và quản lý thông báo

**Đối tượng thực hiện:** Giảng viên, sinh viên.

**Mục đích:** Cho phép người dùng theo dõi, cập nhật trạng thái và quản lý các thông báo cá nhân.

**Quy trình:**

1. Người dùng đăng nhập vào hệ thống CollabSphere.
2. Người dùng truy cập chức năng Quản lý thông báo.
3. Hệ thống truy xuất danh sách thông báo thuộc tài khoản người dùng.
4. Hệ thống hiển thị danh sách thông báo, số lượng thông báo chưa đọc và trạng thái tương ứng.
5. Người dùng lựa chọn một trong các thao tác:
   - Xem chi tiết thông báo.
   - Lọc thông báo theo trạng thái đã đọc hoặc chưa đọc.
   - Đánh dấu một thông báo là đã đọc.
   - Đánh dấu tất cả thông báo là đã đọc.
   - Xóa thông báo không còn cần thiết.
6. Hệ thống kiểm tra quyền truy cập và tính hợp lệ của thao tác.
7. Hệ thống cập nhật dữ liệu thông báo theo yêu cầu.
8. Hệ thống hiển thị lại danh sách thông báo sau khi cập nhật.

### 3.3. Quy trình xử lý ngoại lệ

Trong quá trình sử dụng, hệ thống cần xử lý các trường hợp phát sinh:

- Nếu người dùng không có thông báo, hệ thống hiển thị thông báo danh sách trống.
- Nếu thông báo không tồn tại hoặc không thuộc tài khoản người dùng, hệ thống từ chối thao tác.
- Nếu xảy ra lỗi khi lưu hoặc cập nhật thông báo, hệ thống thông báo lỗi và không ghi nhận thay đổi chưa hoàn tất.
- Nếu thông báo đã được đánh dấu là đã đọc, hệ thống không thực hiện cập nhật trùng lặp không cần thiết.

## 4. Sơ đồ quy trình nghiệp vụ

### 4.1. Quy trình tạo thông báo

```mermaid
flowchart TD
    A([Bắt đầu]) --> B[Phát sinh sự kiện học tập]
    B --> C[Xác định người nhận]
    C --> D[Tạo nội dung thông báo]
    D --> E[Lưu thông báo vào cơ sở dữ liệu]
    E --> F{Lưu thành công?}
    F -- Có --> G[Hiển thị thông báo cho người nhận]
    F -- Không --> H[Thông báo lỗi và ghi nhận sự cố]
    G --> I([Kết thúc])
    H --> I
