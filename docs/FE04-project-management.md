# FE04 – Quản lý đề bài dự án

## 1. Quản lý dự án

### 1.1. Tạo dự án

| STT | Chức năng | Mô tả chi tiết |
|---:|---|---|
| 1 | Tạo dự án mới | Cho phép giảng viên tạo dự án PBL mới gắn với lớp học phần mà giảng viên được phân công phụ trách. |
| 2 | Nhập thông tin dự án | Nhập tên dự án, mô tả vấn đề thực tế cần giải quyết và nội dung tổng quan của dự án. |
| 3 | Thiết lập mục tiêu học tập | Xác định kiến thức, kỹ năng và kết quả học tập mà sinh viên cần đạt được sau khi hoàn thành dự án. |
| 4 | Xác định yêu cầu sản phẩm | Mô tả sản phẩm cần xây dựng, các yêu cầu bắt buộc và kết quả đầu ra dự kiến. |
| 5 | Thiết lập tiêu chí nghiệm thu | Xác định các điều kiện để đánh giá sản phẩm có đáp ứng yêu cầu của dự án hay không. |
| 6 | Thiết lập thời gian | Khai báo ngày bắt đầu và hạn cuối hoàn thành dự án. Hạn cuối không được trước ngày bắt đầu. |
| 7 | Thiết lập quy mô nhóm | Xác định số lượng sinh viên tối thiểu hoặc tối đa trong một nhóm theo yêu cầu của dự án. |
| 8 | Thiết lập rubric | Khai báo các tiêu chí đánh giá, mô tả mức độ đạt được và trọng số hoặc điểm số tương ứng để phục vụ đánh giá sản phẩm. |
| 9 | Lưu bản nháp | Lưu dự án ở trạng thái `Draft` để giảng viên tiếp tục chỉnh sửa trước khi công bố. |
| 10 | Kiểm tra dữ liệu | Kiểm tra các trường bắt buộc và tính hợp lệ của thông tin trước khi lưu dự án. |

### 1.2. Xem danh sách và chi tiết dự án

| STT | Chức năng | Mô tả chi tiết |
|---:|---|---|
| 1 | Xem danh sách dự án | Hiển thị các dự án do giảng viên tạo hoặc được phân quyền quản lý. |
| 2 | Hiển thị thông tin tổng quan | Hiển thị tên dự án, lớp học phần liên quan, thời gian thực hiện và trạng thái dự án. |
| 3 | Tìm kiếm dự án | Cho phép tìm kiếm dự án theo tên hoặc từ khóa liên quan. |
| 4 | Lọc dự án | Hỗ trợ lọc dự án theo lớp học phần và trạng thái dự án. |
| 5 | Xem chi tiết dự án | Hiển thị mô tả vấn đề, mục tiêu học tập, yêu cầu sản phẩm, tiêu chí nghiệm thu, thời hạn, quy mô nhóm và rubric. |
| 6 | Xem dự án dành cho sinh viên | Cho phép sinh viên đã ghi danh vào lớp học phần xem các dự án đã được công bố cho lớp mình. |
| 7 | Xem thời hạn và yêu cầu | Hiển thị thời hạn hoàn thành, yêu cầu sản phẩm và các tiêu chí đánh giá để sinh viên nắm rõ nhiệm vụ. |

### 1.3. Cập nhật thông tin dự án

| STT | Chức năng | Mô tả chi tiết |
|---:|---|---|
| 1 | Chỉnh sửa bản nháp | Cho phép giảng viên phụ trách cập nhật thông tin dự án khi dự án còn ở trạng thái `Draft`. |
| 2 | Cập nhật mô tả và mục tiêu | Chỉnh sửa mô tả vấn đề, mục tiêu học tập, phạm vi và nội dung dự án. |
| 3 | Cập nhật yêu cầu sản phẩm | Điều chỉnh yêu cầu, kết quả đầu ra và tiêu chí nghiệm thu của dự án. |
| 4 | Cập nhật thời gian | Điều chỉnh ngày bắt đầu và hạn cuối hoàn thành theo quy định của hệ thống. |
| 5 | Cập nhật quy mô nhóm | Điều chỉnh số lượng sinh viên tối thiểu hoặc tối đa trong nhóm nếu trạng thái dự án cho phép. |
| 6 | Cập nhật rubric | Chỉnh sửa tiêu chí đánh giá trước khi rubric được khóa theo quy trình của hệ thống. |
| 7 | Kiểm tra dữ liệu | Kiểm tra tính đầy đủ và hợp lệ của thông tin sau khi chỉnh sửa. |
| 8 | Lưu thay đổi | Lưu thông tin mới sau khi kiểm tra hợp lệ. Việc chỉnh sửa dự án đã công bố phải tuân theo quyền hạn và quy định của hệ thống. |

### 1.4. Công bố dự án

| STT | Chức năng | Mô tả chi tiết |
|---:|---|---|
| 1 | Kiểm tra trước khi công bố | Kiểm tra các thông tin bắt buộc gồm mô tả dự án, mục tiêu, yêu cầu sản phẩm, thời gian, quy mô nhóm và rubric theo cấu hình của hệ thống. |
| 2 | Công bố dự án | Cho phép giảng viên phụ trách công bố dự án cho lớp học phần đã được liên kết. |
| 3 | Cập nhật trạng thái | Chuyển trạng thái dự án từ `Draft` sang `Published` khi công bố thành công. |
| 4 | Hiển thị dự án cho sinh viên | Cho phép sinh viên đã ghi danh vào lớp học phần xem đề bài, yêu cầu và thời hạn của dự án. |
| 5 | Kiểm soát quyền công bố | Chỉ giảng viên phụ trách hoặc người được cấp quyền phù hợp mới được công bố dự án. |
| 6 | Kiểm tra kết quả | Thông báo kết quả công bố và hiển thị trạng thái mới của dự án. |

### 1.5. Đánh dấu hoàn thành dự án

| STT | Chức năng | Mô tả chi tiết |
|---:|---|---|
| 1 | Kiểm tra tình trạng dự án | Cho phép giảng viên xem thông tin và tình trạng hiện tại trước khi đánh dấu hoàn thành. |
| 2 | Đánh dấu hoàn thành | Cho phép giảng viên phụ trách chuyển dự án sang trạng thái `Completed` khi dự án đã hoàn tất theo quy trình. |
| 3 | Cập nhật trạng thái | Lưu trạng thái hoàn thành để phân biệt với dự án đang được triển khai. |
| 4 | Kiểm soát thao tác | Chỉ người dùng có quyền phù hợp mới được đánh dấu hoàn thành dự án. |

### 1.6. Lưu trữ dự án

| STT | Chức năng | Mô tả chi tiết |
|---:|---|---|
| 1 | Lưu trữ dự án | Cho phép giảng viên phụ trách lưu trữ dự án khi không còn cần sử dụng trong hoạt động thường xuyên. |
| 2 | Cập nhật trạng thái | Chuyển dự án sang trạng thái `Archived` sau khi lưu trữ thành công. |
| 3 | Xem dự án đã lưu trữ | Cho phép người dùng có quyền xem lại thông tin dự án đã lưu trữ. |
| 4 | Kiểm soát quyền lưu trữ | Chỉ giảng viên phụ trách hoặc người được cấp quyền phù hợp mới được lưu trữ dự án. |

## 2. Quy tắc nghiệp vụ

| Mã quy tắc | Nội dung |
|---|---|
| BR04.01 | Dự án phải được gắn với một lớp học phần phù hợp. |
| BR04.02 | Chỉ giảng viên phụ trách hoặc người được cấp quyền mới được tạo, chỉnh sửa, công bố, đánh dấu hoàn thành hoặc lưu trữ dự án. |
| BR04.03 | Hạn cuối hoàn thành dự án không được trước ngày bắt đầu dự án. |
| BR04.04 | Dự án phải có đầy đủ các thông tin bắt buộc trước khi công bố. |
| BR04.05 | Sinh viên chỉ được xem các dự án đã công bố thuộc lớp học phần mà mình đã ghi danh. |
| BR04.06 | Sinh viên không được chỉnh sửa đề bài chính thức hoặc rubric đã bị khóa. |
| BR04.07 | Quy mô nhóm được thiết lập trong dự án phải được sử dụng làm căn cứ kiểm tra số lượng thành viên khi sinh viên lập hoặc tham gia nhóm. |
| BR04.08 | Dự án ở trạng thái `Draft` chỉ được giảng viên phụ trách hoặc người có quyền phù hợp xem và chỉnh sửa. |
| BR04.09 | Dự án ở trạng thái `Completed` không tiếp nhận sản phẩm mới, trừ khi giảng viên mở lại theo quy định của hệ thống. |
| BR04.10 | Dự án đã lưu trữ chỉ được truy cập theo quyền được cấp. |
| BR04.11 | Quyền truy cập và thao tác phải được kiểm tra ở phía máy chủ, không chỉ dựa vào việc ẩn hoặc hiện chức năng trên giao diện. |

## 3. Trạng thái dự án

| Trạng thái | NỘI DUNG |
|---|---|
| `Draft` | Dự án đang ở dạng bản nháp; giảng viên tiếp tục hoàn thiện trước khi công bố. |
| `Published` | Dự án đã được công bố; sinh viên thuộc lớp học phần có thể xem và tham gia theo cấu hình. |
| `InProgress` | Dự án đang trong quá trình thực hiện. |
| `Completed` | Dự án đã hoàn thành; không tiếp nhận sản phẩm mới trừ khi được mở lại theo quy định. |
| `Archived` | Dự án đã được lưu trữ và chỉ được xem theo quyền truy cập. |

## 4. Quy trình nghiệp vụ

```mermaid
flowchart TD
    A([Bắt đầu]) --> B[Giảng viên tạo dự án]
    B --> C[Nhập mô tả và mục tiêu học tập]
    C --> D[Thiết lập yêu cầu sản phẩm và tiêu chí nghiệm thu]
    D --> E[Thiết lập thời hạn, quy mô nhóm và rubric]
    E --> F{Thông tin hợp lệ?}
    F -->|Không| C
    F -->|Có| G[Lưu dự án ở trạng thái Draft]
    G --> H{Đã sẵn sàng công bố?}
    H -->|Chưa| C
    H -->|Rồi| I[Công bố dự án cho lớp học phần]
    I --> J[Chuyển trạng thái Published]
    J --> K[Sinh viên xem đề bài và thời hạn]
    K --> L[Triển khai dự án theo quy trình PBL]
    L --> M[Giảng viên đánh dấu hoàn thành]
    M --> N[Lưu trữ dự án khi cần]
    N --> O([Kết thúc])
```

## 5. Quy tắc nghiệp vụ

- Dự án phải liên kết với đề cương môn học phù hợp.
- Thông tin dự án phải đầy đủ trước khi gửi phê duyệt.
- Mỗi milestone phải có tên và thời hạn hoàn thành.
- Thời hạn milestone phải nằm trong khoảng thời gian thực hiện dự án.
- Dự án chỉ được phân công cho lớp hoặc nhóm sau khi được phê duyệt.
- Nhóm được phân công phải thuộc lớp học phần được giao dự án.
- Dự án cần chỉnh sửa phải được cập nhật trước khi gửi lại phê duyệt.
- Dự án bị từ chối không được phân công; giảng viên chỉ được tạo hoặc gửi lại bản chỉnh sửa nếu quy trình cho phép.
- Chỉ người dùng có quyền phù hợp mới được tạo, chỉnh sửa, phê duyệt hoặc phân công dự án.
- Hệ thống cần lưu kết quả phê duyệt và cập nhật trạng thái dự án.
- Việc chỉnh sửa dự án sau phê duyệt phải tuân theo quyền hạn và quy định của hệ thống.

