
# FE03 – QUẢN LÝ LỚP HỌC

## 1. Giới thiệu

Chức năng Quản lý lớp học cho phép nhân viên quản lý (Staff) tổ chức và quản lý các lớp học trên hệ thống CollabSphere. Chức năng hỗ trợ tạo lớp từ tệp dữ liệu, quản lý thông tin lớp học, quản lý danh sách sinh viên và phân công giảng viên phụ trách. Đây là chức năng nền tảng giúp tổ chức lớp học và kết nối các hoạt động học tập theo phương pháp Project-Based Learning (PBL).

## 2. Các chức năng chính

### 2.1. Tạo lớp học

Hệ thống cung cấp chức năng tạo lớp học mới bằng cách nhập thông tin trực tiếp hoặc nhập dữ liệu từ tệp CSV. Khi tạo lớp, hệ thống ghi nhận các thông tin cơ bản như mã lớp, tên lớp, môn học, học kỳ, năm học, sĩ số tối đa, lịch học và phòng học.

Hệ thống kiểm tra tính hợp lệ của dữ liệu, bảo đảm mã lớp không bị trùng và môn học được liên kết đã tồn tại. Đối với dữ liệu nhập từ tệp, hệ thống hỗ trợ kiểm tra và hiển thị các thông tin không hợp lệ trước khi lưu.

### 2.2. Quản lý thông tin lớp học

Hệ thống cho phép Staff xem danh sách lớp học, tìm kiếm lớp theo mã lớp, tên lớp, môn học hoặc học kỳ, đồng thời xem thông tin chi tiết của từng lớp.

Staff có thể cập nhật thông tin lớp và thay đổi trạng thái hoạt động theo quyền hạn được cấp. Đối với lớp không còn hoạt động, hệ thống hỗ trợ chuyển sang trạng thái lưu trữ hoặc ngừng hoạt động nhằm bảo toàn dữ liệu liên quan.

Thông tin lớp học bao gồm:

| Thuộc tính | Mô tả |
|---|---|
| Mã lớp | Mã định danh duy nhất của lớp học |
| Tên lớp | Tên hiển thị của lớp |
| Môn học | Môn học được liên kết với lớp |
| Học kỳ | Học kỳ tổ chức lớp học |
| Năm học | Năm học tương ứng |
| Sĩ số tối đa | Số lượng sinh viên tối đa của lớp |
| Lịch học | Thời gian học của lớp |
| Phòng học | Địa điểm tổ chức lớp |
| Trạng thái | Trạng thái hoạt động của lớp |

### 2.3. Quản lý sinh viên trong lớp

Hệ thống cho phép Staff quản lý danh sách sinh viên thuộc từng lớp học. Các chức năng bao gồm thêm sinh viên, xem danh sách thành viên, cập nhật danh sách và gỡ sinh viên khỏi lớp theo quyền hạn được cấp.

Hệ thống kiểm tra tài khoản sinh viên trước khi thêm vào lớp và ngăn chặn việc một sinh viên bị thêm nhiều lần vào cùng một lớp. Nếu lớp có giới hạn sĩ số, hệ thống kiểm tra số lượng thành viên trước khi xác nhận việc thêm sinh viên.

### 2.4. Phân công giảng viên

Hệ thống hỗ trợ Staff phân công giảng viên phụ trách từng lớp học. Danh sách phân công được lưu trữ và hiển thị trong thông tin chi tiết của lớp.

Staff có thể xem danh sách giảng viên được phân công, thay đổi giảng viên phụ trách hoặc hủy phân công theo quyền hạn được cấp. Hệ thống kiểm tra tính hợp lệ của tài khoản giảng viên và bảo đảm việc phân công được ghi nhận chính xác.

### 2.5. Tìm kiếm và tra cứu lớp học

Hệ thống hỗ trợ tìm kiếm và lọc danh sách lớp học theo các tiêu chí như mã lớp, tên lớp, môn học, học kỳ, năm học và trạng thái hoạt động.

Kết quả tìm kiếm hiển thị những thông tin cần thiết để Staff nhanh chóng xác định lớp học cần quản lý. Việc tra cứu được thực hiện theo quyền truy cập của từng người dùng.

---

## 3. Quyền hạn theo vai trò

| Vai trò | Phạm vi chức năng |
|---|---|
| Staff | Tạo lớp, nhập dữ liệu, cập nhật thông tin, quản lý sinh viên và phân công giảng viên |
| Lecturer | Xem các lớp được phân công và truy cập thông tin lớp phục vụ hoạt động giảng dạy |
| Student | Xem thông tin lớp và danh sách hoạt động học tập thuộc phạm vi được cấp quyền |
| Admin | Quản lý quyền truy cập và hỗ trợ xử lý các vấn đề liên quan đến phân quyền |

Các thao tác tạo, chỉnh sửa, thêm hoặc gỡ thành viên và phân công giảng viên phải được kiểm soát theo quyền hạn của người dùng.

---

## 4. Quy tắc nghiệp vụ

- Mỗi lớp học phải có mã lớp duy nhất trong hệ thống.
- Lớp học phải được liên kết với một môn học hợp lệ.
- Chỉ người dùng có quyền phù hợp mới được quản lý thông tin lớp.
- Giảng viên và sinh viên được phân công hoặc thêm vào lớp phải có tài khoản hợp lệ.
- Một sinh viên không được xuất hiện nhiều lần trong danh sách thành viên của cùng một lớp.
- Số lượng sinh viên không được vượt quá sĩ số tối đa nếu lớp có quy định giới hạn.
- Dữ liệu nhập từ tệp phải được kiểm tra tính hợp lệ trước khi lưu.
- Các thay đổi về thông tin lớp, thành viên và giảng viên phải được lưu nhất quán.
- Lớp đã phát sinh dữ liệu liên quan nên được chuyển sang trạng thái ngừng hoạt động hoặc lưu trữ thay vì xóa trực tiếp.

---

## 5. Dữ liệu liên quan

Chức năng Quản lý lớp học có mối quan hệ với các dữ liệu và chức năng khác trong CollabSphere:

| Thành phần | Mối quan hệ |
|---|---|
| Tài khoản người dùng | Xác định Staff, giảng viên và sinh viên được phép truy cập |
| Môn học | Xác định môn học mà lớp được mở |
| Lớp học | Lưu thông tin tổ chức và quản lý lớp |
| Thành viên lớp | Liên kết sinh viên với lớp học |
| Phân công giảng viên | Liên kết giảng viên với lớp học |
| Quản lý dự án | Hỗ trợ liên kết hoạt động dự án với lớp khi nghiệp vụ yêu cầu |
| Quản lý nhóm | Hỗ trợ tổ chức nhóm sinh viên trong lớp theo nghiệp vụ PBL |

---

## 6. Yêu cầu xử lý và bảo đảm dữ liệu

Hệ thống phải kiểm tra dữ liệu đầu vào khi tạo lớp, nhập tệp, cập nhật thông tin và quản lý thành viên. Các trường hợp dữ liệu không hợp lệ phải được thông báo rõ ràng để người dùng có thể nhận biết và xử lý.

Khi nhập dữ liệu từ tệp, hệ thống cần hỗ trợ kiểm tra dữ liệu trước khi xác nhận lưu. Những bản ghi không hợp lệ phải được xác định cụ thể; quá trình lưu cần bảo đảm tính nhất quán của dữ liệu và tránh tạo ra các bản ghi trùng lặp.

Mọi chức năng quản lý lớp học phải tuân thủ cơ chế xác thực và phân quyền chung của CollabSphere.

---

## 7. Kết quả chức năng

Sau khi sử dụng chức năng Quản lý lớp học, hệ thống bảo đảm:

- Thông tin lớp học được quản lý tập trung.
- Dữ liệu lớp có thể được tạo trực tiếp hoặc nhập từ tệp.
- Danh sách sinh viên của từng lớp được quản lý chính xác.
- Việc phân công giảng viên được ghi nhận và tra cứu thuận tiện.
- Các thao tác quản lý được kiểm soát theo quyền hạn.
- Dữ liệu lớp học có thể được sử dụng làm cơ sở tổ chức hoạt động giảng dạy và học tập theo phương pháp PBL.
