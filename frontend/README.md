# CollabSphere – Frontend

## 1. Giới thiệu

**CollabSphere** là hệ thống hỗ trợ học tập theo phương pháp **Project-Based Learning (PBL)**. Frontend là phần giao diện người dùng, cho phép giảng viên, sinh viên và các đối tượng quản lý tương tác với hệ thống thông qua trình duyệt web.

Frontend hướng đến việc cung cấp một môi trường học tập và làm việc nhóm tập trung, hỗ trợ các hoạt động quản lý môn học, lớp học, dự án, nhóm, nhiệm vụ, cột mốc, đánh giá và trao đổi thông tin.

Giao diện được tổ chức theo mô hình client-server:

- Frontend hiển thị giao diện, tiếp nhận thao tác và quản lý trạng thái phía người dùng.
- Backend cung cấp REST API để xử lý nghiệp vụ và truy xuất dữ liệu.
- SignalR được sử dụng cho các chức năng giao tiếp thời gian thực khi Backend cung cấp Hub tương ứng.

**Lưu ý về trạng thái mã nguồn:** Trong repository hiện tại, Frontend đang trong quá trình chuyển đổi/kết hợp. Cấu hình React và Vite đã có, đồng thời đã có một số component, context, hook và service viết bằng JSX/JavaScript. Tuy nhiên, nhiều trang nghiệp vụ vẫn đang ở định dạng `.razor`, một số tệp C# vẫn còn trong thư mục Frontend và `src/App.jsx` hiện chưa có nội dung. Vì vậy, không nên xem toàn bộ giao diện React là đã hoàn thiện chỉ dựa trên cấu hình Vite.

## 2. Công nghệ sử dụng

Các phiên bản dưới đây được lấy từ `package.json` của Frontend.

| Công nghệ | Phiên bản khai báo | Mục đích |
|---|---:|---|
| React | `^18.3.1` | Xây dựng giao diện người dùng theo component |
| React DOM | `^18.3.1` | Hiển thị ứng dụng React trên trình duyệt |
| React Router DOM | `^6.28.0` | Quản lý điều hướng và định tuyến phía client |
| Vite | `^5.4.11` | Công cụ phát triển và đóng gói ứng dụng |
| `@vitejs/plugin-react` | `^4.3.4` | Tích hợp React với Vite |
| JavaScript (JSX) | ES Modules | Xây dựng logic và thành phần giao diện |
| CSS | - | Thiết kế và định dạng giao diện |
| `@microsoft/signalr` | `^11.0.0` | Kết nối SignalR và giao tiếp thời gian thực |
| Node.js | `>=18` | Môi trường chạy JavaScript và npm |
| npm | Đi kèm Node.js | Cài đặt và quản lý thư viện |

## 3. Các chức năng chính

Frontend được tổ chức theo các nhóm chức năng nghiệp vụ của CollabSphere. Các nhóm dưới đây mô tả phạm vi giao diện hướng đến; mức độ hoạt động của từng chức năng phụ thuộc vào việc trang giao diện, service và API Backend đã được tích hợp đầy đủ hay chưa.

### 3.1. Xác thực và quản lý tài khoản

- Đăng nhập hệ thống.
- Quản lý trạng thái đăng nhập.
- Lưu trữ và sử dụng Access Token theo cơ chế xác thực của ứng dụng.
- Hiển thị thông tin người dùng.
- Kiểm soát truy cập giao diện theo trạng thái đăng nhập và vai trò.

**Lưu ý:** Trong source hiện có `Login.razor` và `Register.razor`. Không nên khẳng định chức năng đăng ký đã kết nối Backend nếu chưa có endpoint đăng ký tương ứng.

### 3.2. Quản lý học vụ

Các nhóm giao diện nghiệp vụ hiện có trong cấu trúc source gồm:

- Quản lý môn học.
- Quản lý đề cương môn học.
- Quản lý lớp học.
- Quản lý phân công/thành viên lớp.
- Các giao diện phục vụ nghiệp vụ quản lý và phê duyệt.

### 3.3. Quản lý dự án

- Hiển thị thông tin và danh sách dự án.
- Theo dõi mô tả, thời gian và trạng thái dự án.
- Hiển thị nhóm tham gia thực hiện.
- Theo dõi tiến độ dự án.

Trong source hiện có component `ProjectCard` và service `projectService.js`. Cần đối chiếu endpoint thực tế của Backend trước khi xác nhận các thao tác dữ liệu đã hoạt động.

### 3.4. Quản lý công việc

- Hiển thị và quản lý nhiệm vụ.
- Theo dõi trạng thái công việc.
- Quản lý cột mốc và điểm kiểm tra (checkpoint).
- Theo dõi thời hạn và tiến độ hoàn thành.

Source hiện có component `TaskCard`, service `taskService.js` và `checkpointService.js`. Việc tích hợp đầy đủ phụ thuộc vào API tương ứng ở Backend.

### 3.5. Không gian làm việc nhóm

- Hiển thị thông tin nhóm.
- Theo dõi thành viên.
- Hỗ trợ phối hợp thực hiện dự án.
- Kết nối các chức năng phục vụ làm việc nhóm.

Source hiện có component `TeamCard` và service `teamService.js`.

### 3.6. Đánh giá và báo cáo

- Hiển thị thông tin đánh giá.
- Theo dõi kết quả thực hiện.
- Hỗ trợ hiển thị nhận xét và phản hồi.
- Hiển thị thông tin báo cáo.

Source hiện có component `AssessmentCard` và service `assessmentService.js`. Cần kiểm tra API và dữ liệu Backend trước khi sử dụng trong quy trình đánh giá thực tế.

### 3.7. Thông báo và giao tiếp thời gian thực

- Quản lý trạng thái thông báo ở phía giao diện.
- Kết nối SignalR khi Backend Hub sẵn sàng.
- Nhận và hiển thị sự kiện theo thời gian thực.
- Hỗ trợ các hoạt động trao đổi giữa thành viên theo chức năng Backend cung cấp.

Backend hiện khai báo các Hub `/hubs/chat` và `/hubs/notifications`.

## 4. Cấu trúc thư mục

Cấu trúc dưới đây thể hiện các thành phần chính có trong ZIP hiện tại. Các thư mục được trình bày theo tên logic để dễ đọc. Một số tên thư mục thực tế trong ZIP có khoảng trắng ở đầu, được ghi chú ở cuối mục này.

```text
frontend/
│
├── public/
│   ├── images/
│   │   ├── default-avatar.svg
│   │   ├── empty-projects.svg
│   │   ├── empty-tasks.svg
│   │   └── logo.svg
│   │
│   └── FaviconGenerator.cs
│
├── src/
│   ├── assets/
│   │   ├── icons/
│   │   │   ├── AppIcon.razor
│   │   │   ├── IconNames.cs
│   │   │   └── collabsphere.svg
│   │   │
│   │   └── styles/
│   │       ├── components.css
│   │       ├── global.css
│   │       ├── responsive.css
│   │       └── variables.css
│   │
│   ├── components/
│   │   ├── Assessments/
│   │   ├── Common/
│   │   ├── Projects/
│   │   ├── Tasks/
│   │   ├── Teams/
│   │   ├── layout/
│   │   ├── DataTable.jsx
│   │   ├── DataTable.css
│   │   ├── Modal.jsx
│   │   └── Modal.css
│   │
│   ├── contexts/
│   │   ├── AuthContext.jsx
│   │   └── NotificationContext.jsx
│   │
│   ├── hooks/
│   │   ├── useAuthState.js
│   │   ├── useDataTable.js
│   │   └── useProjectState.js
│   │
│   ├── pages/
│   │   ├── admin/
│   │   ├── assessments/
│   │   ├── auth/
│   │   ├── checkpoints/
│   │   ├── classes/
│   │   ├── dashboard/
│   │   ├── head/
│   │   ├── projects/
│   │   ├── reports/
│   │   ├── settings/
│   │   ├── staff/
│   │   ├── subjects/
│   │   ├── syllabus/
│   │   ├── tasks/
│   │   ├── teams/
│   │   └── workspace/
│   │
│   ├── routes/
│   │   ├── AppRoutes.jsx
│   │   ├── ProtectedRoute.jsx
│   │   └── RoleBasedRoute.jsx
│   │
│   ├── services/
│   │   ├── ApiModels.js
│   │   ├── apiClient.js
│   │   ├── authService.js
│   │   ├── projectService.js
│   │   ├── teamService.js
│   │   ├── taskService.js
│   │   ├── checkpointService.js
│   │   ├── assessmentService.js
│   │   └── CollabSphereApiService.cs
│   │
│   ├── utils/
│   │   ├── constants.js
│   │   ├── formatDate.js
│   │   └── validators.js
│   │
│   ├── App.jsx
│   └── main.jsx
│
├── ImageGenerator/
│   ├── ImageGenerator.csproj
│   └── Program.cs
│
├── index.html
├── package.json
├── package-lock.json
├── vite.config.js
├── test-notification.mjs
├── test-realtime.mjs
└── README.md
```

### 4.1. Giải thích các thư mục

| Thư mục / tệp | Chức năng |
|---|---|
| `public/` | Chứa tài nguyên tĩnh được truy cập trực tiếp, ví dụ hình ảnh và logo. |
| `src/assets/` | Chứa icon, SVG và CSS dùng chung. |
| `src/components/` | Chứa các component giao diện có thể tái sử dụng. |
| `src/contexts/` | Chứa trạng thái dùng chung của ứng dụng như xác thực và thông báo. |
| `src/hooks/` | Chứa các React Hook phục vụ quản lý trạng thái và dữ liệu giao diện. |
| `src/pages/` | Chứa các trang theo nhóm nghiệp vụ. Trong source hiện tại nhiều trang vẫn là `.razor`. |
| `src/routes/` | Chứa các thành phần định tuyến và bảo vệ route React. |
| `src/services/` | Chứa logic gọi API và các service theo nghiệp vụ. |
| `src/utils/` | Chứa hằng số, hàm định dạng và kiểm tra dữ liệu. |
| `ImageGenerator/` | Project C# riêng liên quan đến chức năng tạo hình ảnh; không phải thư mục component React. |
| `test-realtime.mjs` | Script kiểm thử kết nối chat SignalR. |
| `test-notification.mjs` | Script kiểm thử liên quan đến thông báo. |
| `vite.config.js` | Cấu hình Vite và proxy đến Backend. |

### 4.2. Lưu ý về tên thư mục trong ZIP

Trong ZIP hiện tại, một số đường dẫn thực tế đang có dấu cách ở đầu tên, ví dụ:

```text
src/ contexts/
src/ routes/
```

Trong khi `main.jsx` import:

```js
import { AuthProvider } from "./contexts/AuthContext";
import { NotificationProvider } from "./contexts/NotificationContext";
```

Các đường dẫn trên không trùng nhau. Cần đổi tên thư mục thực tế thành `contexts` và `routes` (không có dấu cách ở đầu), sau đó kiểm tra và cập nhật toàn bộ import liên quan.

Ngoài ra, một số tên file trong `src/hooks/` có ký tự tab ở đầu tên trong ZIP. Cần chuẩn hóa thành:

```text
useDataTable.js
useProjectState.js
```

và kiểm tra các import đang sử dụng chúng.

## 5. Yêu cầu môi trường

Để cài đặt và chạy Frontend, cần chuẩn bị:

- Node.js phiên bản 18 trở lên.
- npm được cài đặt cùng Node.js.
- Visual Studio Code hoặc trình soạn thảo mã nguồn tương đương.
- Trình duyệt web hiện đại như Chrome, Edge hoặc Firefox.
- Backend `CollabSphere.API` đang hoạt động nếu cần sử dụng dữ liệu và chức năng từ API.
- MySQL đang hoạt động nếu Backend cần truy xuất cơ sở dữ liệu.

Kiểm tra phiên bản Node.js và npm:

```powershell
node -v
npm.cmd -v
```

Có thể sử dụng Visual Studio để mở thư mục dự án, nhưng các lệnh npm cần được chạy trong Terminal tại thư mục `frontend`.

## 6. Cài đặt và chạy dự án

### Bước 1: Mở thư mục Frontend

Mở Terminal trong Visual Studio Code hoặc PowerShell, sau đó di chuyển đến thư mục Frontend:

```powershell
cd CNPM-main/frontend
```

Nếu Terminal đang ở thư mục gốc repository, lệnh trên sẽ chuyển vào đúng thư mục.

### Bước 2: Cài đặt thư viện

Chạy lệnh:

```powershell
npm.cmd install
```

Lệnh này cài đặt các thư viện được khai báo trong `package.json`.

Nếu cần cài đặt đúng theo `package-lock.json` trong môi trường đã có lock file, có thể sử dụng:

```powershell
npm.cmd ci
```

### Bước 3: Cấu hình kết nối Backend

Tạo file `.env` trong thư mục `frontend`:

```env
VITE_API_URL=http://localhost:5000
```

Trong đó:

- `VITE_API_URL`: địa chỉ Backend API.
- `http://localhost:5000`: địa chỉ mặc định đang được cấu hình trong `vite.config.js`.

Nếu Backend chạy ở cổng khác, cần thay đổi URL cho phù hợp.

Không đưa mật khẩu, JWT secret hoặc thông tin bí mật vào biến `VITE_`, vì các biến có tiền tố này có thể được đưa vào mã Frontend phía trình duyệt.

### Bước 4: Khởi chạy Frontend

Chạy lệnh:

```powershell
npm.cmd run dev
```

Cấu hình hiện tại đặt Vite tại:

```text
http://localhost:5173
```

Mở địa chỉ trên trình duyệt để truy cập ứng dụng.

**Lưu ý:** Vite khởi động thành công chỉ xác nhận development server đã chạy. Do `src/App.jsx` hiện đang rỗng và các trang nghiệp vụ vẫn còn `.razor`, giao diện React có thể chưa hiển thị đầy đủ hoặc chưa điều hướng được đến các chức năng.

## 7. Kết nối Backend API

Frontend giao tiếp với Backend thông qua HTTP API. Logic gọi API được tổ chức trong `src/services/` để hạn chế việc viết request trực tiếp trong component giao diện.

### 7.1. Các Controller hiện có trong Backend

Theo source Backend trong repository hiện tại, các Controller gồm:

| Controller | Nhóm nghiệp vụ |
|---|---|
| `AuthController` | Xác thực và thông tin tài khoản |
| `AdminController` | Nghiệp vụ quản trị |
| `SubjectsController` | Quản lý môn học |
| `SyllabiController` | Quản lý đề cương |
| `StudyClassesController` | Quản lý lớp học |
| `ClassAssignmentsController` | Phân công và quản lý thành viên lớp |
| `MeetingsController` | Quản lý cuộc họp |
| `NotificationsController` | Quản lý thông báo |
| `AiController` | Các chức năng liên quan đến AI |

Các Controller có thể thay đổi khi Backend được phát triển tiếp. Cần kiểm tra route và HTTP method trong từng Controller trước khi tích hợp Frontend.

### 7.2. Các service phía Frontend

| Service | Mục đích |
|---|---|
| `apiClient.js` | Thành phần gọi HTTP API dùng chung |
| `authService.js` | Các thao tác xác thực |
| `projectService.js` | Các thao tác liên quan đến dự án |
| `teamService.js` | Các thao tác liên quan đến nhóm |
| `taskService.js` | Các thao tác liên quan đến nhiệm vụ |
| `checkpointService.js` | Các thao tác liên quan đến checkpoint |
| `assessmentService.js` | Các thao tác liên quan đến đánh giá |
| `ApiModels.js` | Các model/định nghĩa dữ liệu phía client |

Việc có service ở Frontend không đồng nghĩa Backend đã có endpoint tương ứng. Đặc biệt, trong danh sách Controller hiện tại chưa thấy các Controller riêng cho Projects, Teams, Tasks, Checkpoints và Assessments. Cần bổ sung hoặc xác nhận API ở Backend trước khi xem các luồng này là đã tích hợp hoàn chỉnh.

### 7.3. Cấu hình Proxy trong Vite

Tệp `vite.config.js` đọc biến môi trường `VITE_API_URL` và cấu hình proxy:

| Prefix | Mục đích |
|---|---|
| `/api` | Chuyển tiếp request REST API đến Backend |
| `/hubs` | Chuyển tiếp kết nối SignalR/WebSocket đến Backend |

Cấu hình proxy giúp Frontend sử dụng đường dẫn tương đối trong quá trình phát triển. Ví dụ, request đến `/api/Auth/login` sẽ được Vite chuyển tiếp đến Backend theo địa chỉ `VITE_API_URL`.

### 7.4. Xác thực bằng Access Token

Backend sử dụng JWT Bearer. Với request yêu cầu xác thực, Frontend cần gửi token trong header:

```http
Authorization: Bearer <access-token>
```

Cần thống nhất nơi lưu token và cách gửi token giữa `authService.js` và `apiClient.js`. Không nên tạo nhiều cơ chế lưu token khác nhau trong các component.

## 8. Giao tiếp thời gian thực với SignalR

Dự án sử dụng thư viện `@microsoft/signalr` để hỗ trợ giao tiếp thời gian thực giữa Frontend và Backend.

Backend hiện khai báo các Hub:

| Hub | Đường dẫn |
|---|---|
| Chat Hub | `/hubs/chat` |
| Notification Hub | `/hubs/notifications` |

Các hoạt động có thể được hỗ trợ tùy theo event và method được triển khai ở Backend:

- Gửi và nhận tin nhắn trong nhóm.
- Nhận thông báo theo thời gian thực.
- Cập nhật giao diện khi có sự kiện mới.
- Đồng bộ thông tin giữa các client đang kết nối.

### 8.1. Luồng kết nối cơ bản

1. Người dùng đăng nhập và nhận Access Token.
2. Frontend tạo SignalR connection đến Hub tương ứng.
3. Cung cấp token thông qua `accessTokenFactory` nếu Hub yêu cầu xác thực.
4. Bắt đầu kết nối.
5. Đăng ký các event do Backend gửi.
6. Gọi method của Hub khi người dùng thực hiện thao tác.
7. Cập nhật trạng thái giao diện khi nhận được event.

### 8.2. Kiểm thử Realtime

Trong Frontend có file:

```text
test-realtime.mjs
```

Script này thực hiện luồng kiểm thử chat:

- Đăng nhập hai tài khoản kiểm thử `student01` và `student02`.
- Lấy Access Token từ API đăng nhập.
- Tạo hai kết nối SignalR đến `/hubs/chat`.
- Cho hai tài khoản tham gia team có ID được cấu hình trong script (mặc định là `1`).
- Gửi tin nhắn từ tài khoản thứ nhất.
- Kiểm tra tài khoản thứ hai có nhận được tin nhắn hay không.

Script yêu cầu:

- Backend đang chạy tại `http://localhost:5000`.
- Hai tài khoản kiểm thử tồn tại và đăng nhập được.
- Hai tài khoản có quyền tham gia team ID được cấu hình.
- Biến môi trường `TEST_PASSWORD` đã được thiết lập.

Trên PowerShell:

```powershell
$env:TEST_PASSWORD = "MAT_KHAU_TEST"
npm.cmd run test:realtime
```

Không ghi mật khẩu kiểm thử vào repository.

## 9. Kiểm tra và Build dự án

### 9.1. Chạy môi trường phát triển

```powershell
npm.cmd run dev
```

Dùng để chạy Vite development server và kiểm tra giao diện trong quá trình phát triển.

### 9.2. Kiểm tra chức năng Realtime

```powershell
npm.cmd run test:realtime
```

Lệnh này yêu cầu Backend, tài khoản kiểm thử và biến môi trường `TEST_PASSWORD` được thiết lập như hướng dẫn ở mục 8.

### 9.3. Kiểm thử thông báo

Trong package hiện tại có file `test-notification.mjs`. Trước khi chạy trực tiếp bằng Node.js, cần đọc file để kiểm tra các biến môi trường, tài khoản và endpoint mà script yêu cầu.

### 9.4. Build dự án

```powershell
npm.cmd run build
```

Sau khi build thành công, Vite tạo các tệp triển khai trong thư mục:

```text
dist/
```

Build thành công là bước kiểm tra biên dịch, không thay thế việc kiểm thử các chức năng trên trình duyệt.

### 9.5. Xem thử bản Build

```powershell
npm.cmd run preview
```

Vite sẽ khởi chạy máy chủ preview. Địa chỉ truy cập được hiển thị trong Terminal.

## 10. Xử lý lỗi thường gặp

### 10.1. Lệnh `npm` không được nhận diện

Nguyên nhân thường gặp là Node.js chưa được cài đặt hoặc Terminal chưa nhận biến môi trường PATH.

Cách xử lý:

1. Cài Node.js phiên bản 18 trở lên.
2. Đóng và mở lại Terminal.
3. Chạy `node -v` và `npm.cmd -v`.
4. Kiểm tra PATH nếu vẫn không nhận lệnh.

### 10.2. Cổng 5173 đang được sử dụng

Vite hiện cấu hình `strictPort: true`. Nếu cổng 5173 đang được tiến trình khác sử dụng, Vite sẽ báo lỗi thay vì tự chuyển sang cổng khác.

Cách xử lý:

- Dừng tiến trình đang sử dụng cổng 5173; hoặc
- Thay đổi `server.port` trong `vite.config.js`, sau đó truy cập đúng cổng mới.

### 10.3. Frontend không kết nối được Backend

Kiểm tra:

- Backend đã chạy chưa.
- `VITE_API_URL` có đúng địa chỉ và cổng không.
- Backend có đang lắng nghe tại địa chỉ đã cấu hình không.
- Proxy `/api` và `/hubs` có đúng không.
- Endpoint đang gọi có thực sự được triển khai trong Backend không.

### 10.4. API trả về lỗi 401 hoặc 403

- `401 Unauthorized`: token thiếu, hết hạn hoặc không hợp lệ; người dùng có thể chưa đăng nhập.
- `403 Forbidden`: người dùng đã xác thực nhưng không có quyền thực hiện thao tác.

Kiểm tra Access Token, header `Authorization` và quyền của tài khoản. Không bỏ kiểm tra phân quyền ở Backend để xử lý lỗi giao diện.

### 10.5. SignalR không kết nối

Kiểm tra:

- Backend đã map đúng Hub chưa.
- Đường dẫn Hub có chính xác không.
- Access Token có hợp lệ không.
- Proxy `/hubs` đã bật `ws: true` chưa.
- Backend và Frontend có đang chạy đúng địa chỉ, cổng không.

### 10.6. Trang trắng hoặc lỗi import

Kiểm tra:

- `src/App.jsx` đã có component React chưa.
- Đường dẫn import có chính xác không.
- Tên file và thư mục có đúng chữ hoa/chữ thường không.
- Các thư mục có khoảng trắng ở đầu tên đã được chuẩn hóa chưa.
- Các trang `.razor` có bị import nhầm như module React không.

## 11. Quy trình phát triển

Quy trình làm việc với Frontend:

1. Lấy mã nguồn mới nhất từ GitHub.
2. Đọc tài liệu chức năng tương ứng trong thư mục `docs/`.
3. Kiểm tra API đã có trong Backend trước khi viết hoặc tích hợp service.
4. Cài đặt dependencies bằng `npm.cmd install`.
5. Cấu hình địa chỉ Backend trong `.env`.
6. Phát triển component giao diện theo từng nhóm chức năng.
7. Tách logic gọi API vào thư mục `services/`.
8. Sử dụng Context cho trạng thái dùng chung khi phù hợp.
9. Kiểm tra route và quyền truy cập.
10. Chạy `npm.cmd run build` để kiểm tra lỗi biên dịch.
11. Kiểm tra giao diện và API trên trình duyệt.
12. Commit và push mã nguồn lên GitHub.

### 11.1. Quy ước đặt tên tham khảo

| Thành phần | Quy ước | Ví dụ |
|---|---|---|
| React Component | PascalCase | `ProjectCard.jsx` |
| React Hook | Bắt đầu bằng `use` | `useProjectState.js` |
| Service | camelCase + `Service` | `projectService.js` |
| Utility | camelCase | `formatDate.js` |
| CSS component | Tên tương ứng component | `ProjectCard.css` |

### 11.2. Nguyên tắc tích hợp API

- Không tự suy đoán endpoint khi chưa kiểm tra Backend.
- Request và response phải khớp với DTO của Backend.
- Xử lý đầy đủ trạng thái loading, lỗi và không có dữ liệu.
- Không hard-code mật khẩu hoặc JWT secret.
- Không xem việc ẩn nút trên giao diện là biện pháp phân quyền.
- Khi Backend thay đổi API contract, cập nhật service và tài liệu liên quan.

## 12. Trạng thái hiện tại và công việc cần hoàn thiện

Dựa trên mã nguồn trong repository hiện tại:

### 12.1. Thành phần đã có

- Cấu hình React và Vite trong `package.json`, `vite.config.js`.
- Điểm khởi tạo React trong `src/main.jsx`.
- Context xác thực và thông báo.
- Một số route React và component bảo vệ route.
- Các component giao diện dùng chung như DataTable và Modal.
- Component theo nghiệp vụ như ProjectCard, TaskCard, TeamCard và AssessmentCard.
- Các service JavaScript theo nhóm nghiệp vụ.
- Các hook quản lý trạng thái.
- CSS, SVG và tài nguyên giao diện.
- Script kiểm thử chat realtime và thông báo.

### 12.2. Những điểm cần hoàn thiện

1. Hoàn thiện `src/App.jsx` và kết nối với hệ thống route.
2. Chuẩn hóa tên thư mục có khoảng trắng ở đầu, đặc biệt `src/ contexts/` và `src/ routes/`.
3. Chuẩn hóa tên file có ký tự tab ở đầu trong `src/hooks/`.
4. Kiểm tra và cập nhật toàn bộ đường dẫn import sau khi đổi tên.
5. Chuyển các trang `.razor` sang React nếu nhóm thống nhất React là công nghệ Frontend chính.
6. Xác định cách xử lý các tệp C# còn nằm trong thư mục Frontend.
7. Đối chiếu từng service với Controller, route và DTO thực tế của Backend.
8. Thống nhất cách lưu và gửi Access Token.
9. Hoàn thiện giao diện theo từng vai trò người dùng.
10. Chạy build và kiểm thử thực tế trước khi đánh dấu chức năng hoàn thành.

### 12.3. Tiêu chí hoàn thành một chức năng

Một chức năng chỉ nên được đánh dấu hoàn thành khi:

- Giao diện đã được triển khai.
- Route và quyền truy cập hoạt động đúng.
- Service gọi đúng API đã có trong Backend.
- Request/response khớp với DTO.
- Có xử lý loading, lỗi và trường hợp không có dữ liệu.
- Dữ liệu được hiển thị và cập nhật đúng.
- Build không có lỗi.
- Đã kiểm thử luồng chính và các trường hợp lỗi cơ bản.

## 13. Repository

**GitHub:** https://github.com/nguyenminhthien140906-dot/CNPM

Thư mục Frontend:

```text
CNPM/frontend/
```

Tài liệu này mô tả phần Frontend của dự án CollabSphere, phục vụ việc cài đặt môi trường, phát triển giao diện, kết nối Backend và kiểm tra ứng dụng.

---

**CollabSphere – Supporting Project-Based Learning Through Collaboration.**
