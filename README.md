# CollabSphere (COSRE)

**Hệ thống hỗ trợ việc học theo phương pháp Project-Based Learning (PBL)**

CollabSphere là hệ thống hỗ trợ tổ chức và quản lý hoạt động học tập theo dự án. Hệ thống giúp giảng viên xây dựng dự án học tập, giao nhiệm vụ, theo dõi tiến độ và đánh giá kết quả. Sinh viên có thể làm việc nhóm, trao đổi thông tin, thực hiện nhiệm vụ và nộp sản phẩm trực tiếp trên hệ thống.

Dự án được xây dựng nhằm hỗ trợ quá trình học tập theo phương pháp Project-Based Learning (PBL), trong đó sinh viên chủ động tiếp thu kiến thức thông qua việc thực hiện các dự án thực tế.

## 1. Mục tiêu dự án

- Hỗ trợ giảng viên tổ chức và quản lý hoạt động học tập theo dự án.
- Hỗ trợ sinh viên làm việc nhóm, phân công nhiệm vụ và theo dõi tiến độ.
- Tạo môi trường trao đổi, cộng tác và chia sẻ tài nguyên học tập.
- Hỗ trợ đánh giá quá trình thực hiện và kết quả dự án.
- Ứng dụng công nghệ để nâng cao hiệu quả quản lý và trải nghiệm học tập.

## 2. Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Frontend | ReactJS |
| Backend | ASP.NET Core Web API |
| Cơ sở dữ liệu | PostgreSQL |
| Giao tiếp thời gian thực | WebRTC, Signal, Socket.IO |
| Hỗ trợ AI | AWS Bedrock |
| Cloud và lưu trữ | Azure, AWS, Cloudinary, Upstash |

Các công nghệ trên là định hướng công nghệ của dự án. Việc triển khai thực tế có thể thay đổi tùy theo từng giai đoạn phát triển.

## 3. Các chức năng chính

Hệ thống được chia thành 12 chức năng chính:

| Mã | Chức năng |
|---|---|
| FE01 | Quản lý tài khoản người dùng |
| FE02 | Quản lý môn học và đề cương |
| FE03 | Quản lý lớp học |
| FE04 | Quản lý dự án |
| FE05 | Quản lý nhóm và không gian làm việc |
| FE06 | Quản lý cột mốc và điểm kiểm tra |
| FE07 | Giao tiếp và tổ chức cuộc họp |
| FE08 | Quản lý tài nguyên học tập |
| FE09 | Công cụ cộng tác thời gian thực |
| FE10 | Đánh giá và phản hồi |
| FE11 | Hỗ trợ học tập bằng AI |
| FE12 | Quản lý thông báo |

## 4. Cấu trúc thư mục dự án

```text
CollabSphere/
├── docs/
│   ├── FE01-user-account-management.md
│   ├── FE02-subject-syllabus-management.md
│   ├── FE03-class-management.md
│   ├── FE04-project-management.md
│   ├── FE05-team-workspace-management.md
│   ├── FE06-milestone-checkpoint-management.md
│   ├── FE07-communication-meetings.md
│   ├── FE08-resource-management.md
│   ├── FE09-realtime-collaboration-tools.md
│   ├── FE10-assessment-feedback.md
│   ├── FE11-ai-assistance.md
│   ├── FE12-notification-management.md
│   ├── API-CONTRACT-GUIDE.md
│   └── README.md
│
├── database/
│   ├── 01-erd.md
│   ├── schema.sql
│   ├── migrations/
│   └── scripts/
│       └── run_postgres.sh
│
├── backend/
│   ├── CollabSphere.API/
│   │   ├── Controllers/
│   │   ├── Contracts/
│   │   ├── Middleware/
│   │   └── Program.cs
│   │
│   ├── CollabSphere.Application/
│   │   ├── Features/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Services/
│   │   ├── UseCases/
│   │   └── Validators/
│   │
│   ├── CollabSphere.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Exceptions/
│   │   ├── Interfaces/
│   │   └── ValueObjects/
│   │
│   ├── CollabSphere.Infrastructure/
│   │   ├── Configurations/
│   │   ├── Migrations/
│   │   ├── Persistence/
│   │   ├── Repositories/
│   │   └── Services/
│   │
│   ├── tests/
│   ├── CollabSphere.sln
│   └── README.md
│
├── frontend/
│   ├── public/
│   ├── src/
│   ├── package.json
│   └── README.md
│
├── figma/
│   ├── assets/
│   ├── design-system/
│   └── prototypes/
│
└── README.md
```

### Ý nghĩa các thư mục

- `docs/`: Lưu tài liệu yêu cầu, phân tích và thiết kế 12 chức năng của hệ thống.
- `database/`: Lưu thiết kế cơ sở dữ liệu, sơ đồ ERD, SQL và các phiên bản thay đổi cấu trúc dữ liệu.
- `backend/`: Chứa mã nguồn API và các lớp xử lý nghiệp vụ theo Clean Architecture.
- `frontend/`: Chứa giao diện người dùng được xây dựng bằng ReactJS.
- `figma/`: Lưu tài nguyên thiết kế, hệ thống giao diện và prototype của các luồng chức năng.
- `tests/`: Chứa các thành phần kiểm thử của hệ thống.

## 5. Kiến trúc hệ thống

Backend được tổ chức theo mô hình Clean Architecture, phân chia trách nhiệm thành các lớp:

- **API:** Tiếp nhận yêu cầu từ Frontend, xử lý giao tiếp HTTP và trả kết quả.
- **Application:** Điều phối các ca sử dụng, xử lý nghiệp vụ ứng dụng và kiểm tra dữ liệu đầu vào.
- **Domain:** Chứa các thực thể, quy tắc nghiệp vụ và thành phần cốt lõi của hệ thống.
- **Infrastructure:** Triển khai truy cập cơ sở dữ liệu, repository và các dịch vụ bên ngoài.

Frontend giao tiếp với Backend thông qua API. Backend sử dụng PostgreSQL để lưu trữ dữ liệu và có thể kết nối các dịch vụ hỗ trợ như AI, lưu trữ tệp và giao tiếp thời gian thực.

## 6. Cơ sở dữ liệu

Cơ sở dữ liệu PostgreSQL phục vụ việc lưu trữ thông tin người dùng, môn học, lớp học, dự án, nhóm, nhiệm vụ, bài nộp và kết quả đánh giá.

Các tài liệu liên quan:

- `database/01-erd.md`: Sơ đồ thực thể và mối quan hệ dữ liệu.
- `database/schema.sql`: Định nghĩa cấu trúc cơ sở dữ liệu.
- `database/migrations/`: Lưu các phiên bản thay đổi cấu trúc cơ sở dữ liệu.
- `database/scripts/run_postgres.sh`: Script hỗ trợ chạy các thao tác với PostgreSQL.

## 7. Thiết kế giao diện

Thư mục `figma/` lưu trữ các tài nguyên thiết kế giao diện và prototype của CollabSphere.

```text
figma/
├── assets/
│   ├── fonts/
│   ├── icons/
│   └── images/
├── design-system/
│   ├── buttons.fig
│   ├── colors.fig
│   ├── components.fig
│   └── typography.fig
└── prototypes/
    ├── authentication-flow.fig
    ├── lecturer-flow.fig
    ├── project-management-flow.fig
    └── student-flow.fig
```

## 8. Hướng dẫn phát triển

### Yêu cầu môi trường

- Node.js và npm để chạy Frontend.
- .NET SDK tương thích với các project trong Backend.
- PostgreSQL để chạy cơ sở dữ liệu.

### Chạy Backend

Mở terminal tại thư mục Backend:

```bash
cd backend
dotnet restore CollabSphere.sln
dotnet build CollabSphere.sln
```

### Chạy Frontend

Mở terminal tại thư mục Frontend:

```bash
cd frontend
npm install
npm run dev
```

### Cấu hình cơ sở dữ liệu

1. Cài đặt và khởi động PostgreSQL.
2. Cấu hình chuỗi kết nối cơ sở dữ liệu trong Backend.
3. Khởi tạo cơ sở dữ liệu theo các file SQL và migration tương ứng.

Lưu ý: Các lệnh trên là hướng dẫn cơ bản. Cần đối chiếu cấu hình thực tế của từng project trước khi chạy.

## 9. Định hướng phát triển

CollabSphere hướng đến việc xây dựng một môi trường học tập theo dự án có khả năng mở rộng, hỗ trợ tốt hơn cho giảng viên và sinh viên trong quá trình tổ chức, thực hiện và đánh giá dự án.

Các chức năng sẽ được phát triển theo từng giai đoạn, bảo đảm sự thống nhất giữa tài liệu yêu cầu, thiết kế cơ sở dữ liệu, giao diện và mã nguồn.

---

**CollabSphere — Học tập thông qua dự án, cộng tác để phát triển.**
