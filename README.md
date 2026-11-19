CollabSphere (COSRE)
Hệ thống hỗ trợ học tập phương pháp Project-Based Learning (PBL)
CollabSphere là hệ thống hỗ trợ tổ chức, quản lý và thực hiện hoạt động học tập theo dự án. Nền tảng hướng dẫn kết nối quản trị viên, bộ phận quản lý học vụ, học viên và sinh viên trong toàn bộ quá trình học tập: từ quản lý môn học, đề kim cương và lớp học đến tổ chức dự án, làm việc nhóm, thực hiện nhiệm vụ, sản phẩm và đánh giá kết quả.

Dự án được xây dựng theo hướng Học tập dựa trên dự án (PBL) định hướng . Thầy chỉ tiếp tục nhận thức kiến ​​thức một chiều, sinh viên tham gia giải quyết vấn đề, phân phối hợp lý các thành viên trong nhóm và tạo ra các sản phẩm học tập cụ thể. Giảng viên theo dõi tiến trình, hướng dẫn và đánh giá quá trình thực hiện.
Dự án tên: CollabSphere 
Tên viết tắt: COSRE 
Định hướng: Nền tảng hỗ trợ học tập và cộng tác theo dự án (PBL)

Mục lục
- 1. Giới thiệu dự án
- 2. Mục tiêu
- 3. Đối tượng sử dụng
- 4. Phạm vi chức năng
- 5. Công nghệ và nền tảng
- 6. Kiến trúc hệ thống
- 7. Cấu trúc thư mục
- 8. Cơ sở dữ liệu thiết kế
- 9. Phần phụ trợ và API
- 10. Giao diện người dùng
- 11. Cài đặt môi trường
- 12. Khởi tạo và chạy Backend
- 13. Kiểm tra API
- 14. vật liệu mẫu
- 15. Kiểm thử
- 16. Quy trình phát triển và đóng góp ý kiến
- 17. Tài liệu liên quan
- 18. Phạm vi phát triển khai hiện tại
- 19. Phát triển định hướng
1. Giới thiệu dự án
Trong quá trình học tập theo dự án, học viên và sinh viên cần phối hợp nhiều hoạt động khác nhau như quản lý lớp, chia nhóm, giao nhiệm vụ, theo dõi tiến độ, trao đổi tài liệu và đánh giá sản phẩm. Nếu hoạt động này được thực hiện trên nhiều công cụ phân tách, việc theo dõi tiến trình và tổng hợp thông tin có thể trở nên khó khăn.

CollabSphere được định hướng xây dựng thành một nền tảng tập tin để hỗ trợ các hoạt động trên. Hệ thống cung cấp môi trường quản lý dịch vụ học tập và không có cộng tác, giúp các bên tham gia theo dõi thông tin liên quan đến dự án trong cùng một hệ thống.
1.1. Vấn đề cần giải quyết
- Thông tin môn học, đề cương và lớp học cần được tổ chức thống nhất.
- Hoạt động học tập theo dự án cần có mục tiêu, phạm vi và sản phẩm rõ ràng.
- Nhóm thành viên cần biết nhiệm vụ, người phụ trách và thời gian thực hiện.
- Giảng viên cần theo dõi tiến trình và đánh giá kết quả của cá nhân hoặc nhóm.
- Quá trình trao đổi, chia sẻ tài nguyên và thông báo cần được tập trung.
- Người học cần có thêm công cụ hỗ trợ hỗ trợ và giải đáp trong quá trình thực hiện dự án.
1.2. Giải pháp định hướng
CollabSphere hướng dẫn kết nối các hoạt động quản lý học tập, tổ chức dự án, cộng tác nhóm và đánh giá học tập. Các chức năng được chia thành từng nhóm dịch vụ để thuận tiện cho việc phân tích, phát triển, kiểm tra và mở rộng.
2. Mục tiêu
Dự án hướng đến các mục tiêu sau:
1. Tập trung hóa thông tin học tập: quản lý tài khoản, môn học, đề cương và lớp học trên một nền tảng.
2. Hỗ trợ tổ chức dự án: tạo dự án, xác định mục tiêu, phạm vi, thời gian và sản phẩm đầu ra.
3. Tăng cường nhóm làm việc: hỗ trợ nhóm tổ chức, quản lý thành viên và phân công công việc.
4. Theo dõi tiến trình: quản lý nhiệm vụ, cột đèn, kiểm tra và trạng thái thực hiện.
5. Hỗ trợ trao đổi: tạo môi trường giao tiếp tiếp theo, trình bày và chia sẻ dự án tài nguyên tài nguyên.
6. Hỗ trợ đánh giá: lưu trữ bài viết, kết quả đánh giá và phản hồi.
7. Hỗ trợ người học: công cụ AI định hướng cụ thể để hỗ trợ tìm kiếm thông tin và giải đáp thắc mắc.
8. Bảo đảm khả năng mở rộng: phân chia hệ thống thành các lớp và nhóm chức năng có trách nhiệm rõ ràng.
3. Đối tượng sử dụng
Đối tượng	Sử dụng trò chơi và phạm vi
Quản trị viên (ADMIN)	Quản lý tài khoản, vai trò và các quản trị thông tin của hệ thống.
Trưởng bộ phận (HEAD)	Tham gia các nghiệp vụ quản lý và phê duyệt theo phân quyền.
Nhân viên học vụ (STAFF)	Quản lý thông tin môn học, đề cương, lớp học và phân công học vụ.
Giảng viên (LECTURER)	Theo dõi lớp học, tổ chức học tập, hướng dẫn dự án và đánh giá kết quả.
Sinh viên (STUDENT)	Tham gia lớp, làm việc nhóm, thực hiện nhiệm vụ, trao đổi và phụ sản phẩm.


Quyền truy cập được kiểm soát Kiểm soát trò chơi. Phạm vi quyền cụ thể của từng chức năng cần thiết vào tài liệu nghiệp vụ và chính sách phân quyền được phát triển trong API.
4. Phạm vi chức năng
Hệ thống được phân chia thành 12 nhóm chức năng (FE01–FE12). Đây là cách phân nhóm nghiệp vụ của dự án; không có định nghĩa toàn bộ API và quy trình của tất cả các nhóm.
Mã	Nhóm chức năng	Nội dung
FE01	Quản lý người dùng tài khoản	Tài khoản, thông tin cá nhân, trò chơi và xác thực.
FE02	Quản lý môn học và đề cương	Thông tin môn học, phiên bản và nội dung đề cương.
FE03	quản lý lớp học	Tạo và cập nhật lớp, phân công học viên, bổ sung hoặc xóa sinh viên khỏi lớp.
FE04	quản lý dự án	Xây dựng dự án, mục tiêu, phạm vi, thời gian và sản phẩm đầu ra.
FE05	Quản lý nhóm và không làm việc	Tổ chức nhóm, thành viên và khu vực làm việc của nhóm.
FE06	Quản lý cột cột và điểm kiểm tra	Thiết lập tiến độ và các điểm kiểm tra trong dự án.
FE07	Giao tiếp và tổ chức cuộc họp	Trao đổi thông tin và quản lý hoạt động.
FE08	Quản lý tài nguyên học tập	Chia sẻ và quản lý tài liệu, tài nguyên phục vụ học tập.
FE09	Thực thi công cụ cộng thời gian	Hỗ trợ các hoạt động cộng đồng trong quá trình làm việc của nhóm.
FE10	Đánh giá và phản hồi	Quản lý bài viết, đánh giá và nhận xét.
FE11	Hỗ trợ AI tập học	Hỗ trợ đào tạo, giải đáp và một số tác vụ học tập.
FE12	bảo lý thông báo	Thông báo về nhiệm vụ, tiến trình và hoạt động liên quan.


4.1. Luồng thông thường của chủ sở hữu
Quản lý tài khoản và phân quyền
              |
              v
Quản lý môn học và đề cương
              |
              v
Quản lý lớp học và thành viên lớp
              |
              v
Tổ chức dự án học tập
              |
              v
Thành lập nhóm và phân công nhiệm vụ
              |
              v
Thực hiện nhiệm vụ - theo dõi tiến độ
              |
              v
Trao đổi - họp - chia sẻ tài nguyên
              |
              v
Nộp sản phẩm và đánh giá
              |
              v
Phản hồi và tổng hợp kết quả
Luồng trên mô tả tổng thể dịch vụ định hướng. Trình tự thực thi có thể thay đổi quy trình tùy chỉnh và quyền của từng trò chơi.
5. Công nghệ và nền tảng
Công nghệ dưới đây đã được ghi nhận từ cấu hình và nguồn mã hiện có trong kho lưu trữ. Các dịch vụ chưa có cấu hình hoặc hợp nhất dịch vụ được đánh dấu là định hướng, không được xem là hành động đã được vận hành của các thành phần.
Thành phần	Công nghệ / công cụ	Vai trò
API Backend	API Web ASP.NET Core	Tiếp nhận và xử lý yêu cầu HTTP.
Ngôn ngữ Backend	C#	Triển khai API và dịch vụ phía máy chủ.
Nền tảng .NET	.NET 10	Thời gian chạy và khung của Backend của dự án.
ORM	Entity Framework Core	Thực thể ánh sáng và dữ liệu hoạt động.
Nhà cung cấp cơ sở dữ liệu	MySql.EntityFrameworkCore	Kết nối EF Core với MySQL.
Trình điều khiển cơ sở dữ liệu	MySqlConnector	Thực hiện các lệnh SQL và Migration.
Sở hữu dữ liệu	MySQL 8.0+	Lưu trữ hệ thống dữ liệu.
Xé thực	JWT Bearer	Xác thực yêu cầu bằng mã thông báo truy cập.
Mã bảo hiểm	ASP.NET Identity PasswordHasher	Băm và kiểm tra mật khẩu.
Giao tiếp thời gian tiếp theo	Gói SignalR có trong nguồn mã hóa	Hỗ trợ thực hiện kết nối thời gian ở phía máy khách/máy chủ.
Tác nhân AI	Python	Mô-đun AI được tổ chức riêng trong backend/ai-agent/ .
Giao diện người dùng	JSX và các thành phần .razor /C#	Hiện tại giao diện mã đang ở trạng thái kết hợp/chuyển đổi.
bảo lý mã nguồn	Git, GitHub	Quản lý phiên bản và cộng đồng phát triển.


Lưu ý: Repository hiện có mã nguồn và cấu hình cho một số công nghệ, nhưng điều đó không đồng nghĩa với mọi dịch vụ đã được cấu hình đầy đủ hoặc đang hoạt động. Các nền tảng như AWS Bedrock, Azure, Cloudinary hay Upstash chỉ nên được xem là lựa chọn hợp lý khi có cấu hình và phát triển mã tương ứng.
6. Kiến trúc hệ thống
6.1. Kiến trúc tổng quan
Phần cuối được chia thành bốn dự án theo phân lớp hướng dẫn, gần với Clean Architecture. Mỗi dự án đảm nhận một nhóm trách nhiệm riêng.
                    Người dùng
                        |
                        v
                 Frontend Client
                        |
                    HTTP / JSON
                        |
                        v
                CollabSphere.API
                 /             \
                v               v
      Application Layer    Infrastructure
                \               |
                 v              v
                 Domain      Entity Framework
                                |
                                v
                              MySQL

       AI Agent (Python) - module riêng trong backend/ai-agent
Sơ đồ có thể hiển thị chính các thành phần. Hệ thống phụ thuộc có thể giữa các Backend của dự án được mô tả như sau:
CollabSphere.API
   |                 \
   v                  v
Application       Infrastructure
   |                  |
   v                  v
Domain <---------- Domain
6.2. Các lớp Backend
Dự án	Trách	Thành phần tiêu biểu
CollabSphere.API	Giao tiếp HTTP, bộ điều khiển, xác thực, phân quyền và khởi động ứng dụng.	Bộ điều khiển, phần mềm trung gian, dịch vụ JWT, Program.cs.
CollabSphere.Application	DTO, giao diện và các thành phần phân phối ứng dụng nghiệp vụ.	Các DTO xác thực, giao diện dịch vụ, các trường hợp sử dụng.
CollabSphere.Domain	Mô hình nghiệp vụ cốt lõi, thực thể, enum, ngoại lệ và đối tượng giá trị.	Entities.cs, Enums.cs, Exceptions.cs
CollabSphere.Infrastructure	Kết nối dữ liệu, ánh xạ, kho lưu trữ, dịch vụ và khởi chạy di chuyển/hạt giống.	Persistence.cs, Configurations.cs, Repositories.cs, MigrationEnvironment.cs.


6.3. Luồng xử lý một yêu cầu
1. Người dùng thao tác trên giao diện.
2. Frontend gửi yêu cầu đến endpoint của CollabSphere.API .
3. API kiểm tra đầu vào dữ liệu và xác thực/phân quyền nếu điểm cuối yêu cầu.
4. Thành phần ứng dụng hoặc điều phối nghiệp vụ.
5. Cơ sở hạ tầng truy cập MySQL thông qua các thành phần truy cập dữ liệu.
6. API trả kết quả theo định dạng JSON cho Frontend.
7. Cấu trúc thư mục
Cấu trúc dưới đây được rút gọn từ kho lưu trữ để có thể thực hiện các lĩnh vực chính:
CNPM/
│
├── README.md
│
├── docs/
│   ├── README.md
│   ├── PROJECT-TREE.md
│   ├── API-CONTRACT-GUIDE.md
│   ├── api-hoc-vu.md
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
│   └── FE12-notification-management.md
│
├── database/
│   ├── 01-erd.md
│   ├── schema.sql
│   ├── migrations/
│   │   ├── MigrationEnvironment.cs
│   │   └── versions/
│   │       ├── 001_identity_academic.up.sql
│   │       ├── 001_identity_academic.down.sql
│   │       ├── 002_projects_teams.up.sql
│   │       ├── 002_projects_teams.down.sql
│   │       ├── 003_progress_tasks_submissions.up.sql
│   │       ├── 003_progress_tasks_submissions.down.sql
│   │       ├── 004_resources_communication.up.sql
│   │       ├── 004_resources_communication.down.sql
│   │       ├── 005_workspaces_documents.up.sql
│   │       ├── 005_workspaces_documents.down.sql
│   │       ├── 006_evaluations_notifications_ai.up.sql
│   │       ├── 006_evaluations_notifications_ai.down.sql
│   │       └── 007_seed_roles.up.sql
│   ├── scripts/
│   │   └── run_mysql.sh
│   └── seeds/
│       ├── 001_seed_roles.sql
│       ├── 002_seed_users.sql
│       ├── 003_seed_subjects.sql
│       ├── 004_seed_syllabi.sql
│       └── 005_seed_classes.sql
│
├── backend/
│   ├── CollabSphere.API/
│   ├── CollabSphere.Application/
│   ├── CollabSphere.Domain/
│   ├── CollabSphere.Infrastructure/
│   ├── tests/
│   ├── ai-agent/
│   ├── CollabSphere.Backend.sln
│   ├── CollabSphere.sln
│   └── README.md
│
├── frontend/
│   ├── public/
│   ├── src/
│   │   ├── assets/
│   │   ├── components/
│   │   ├── contexts/
│   │   ├── hooks/
│   │   ├── pages/
│   │   ├── routes/
│   │   ├── services/
│   │   └── utils/
│   ├── package.json
│   └── README.md
│
└── figma/
    ├── assets/
    ├── design-system/
    ├── prototypes/
    └── wireframes/
7.1. Vai trò từng thư mục
Thư mục	Nội dung
docs/	Yêu cầu tài liệu, mô tả chức năng, hợp đồng API và dự án cấu trúc.
cơ sở dữ liệu/	ERD, tổng hợp lược đồ, di chuyển, cơ sở dữ liệu hỗ trợ hạt giống và tập lệnh.
backend/CollabSphere.API/	API Web, bộ điều khiển, phần mềm trung gian, xác thực và cấu hình ứng dụng.
backend/CollabSphere.Application/	DTO, giao diện, trường hợp sử dụng và thành phần ứng dụng.
backend/CollabSphere.Domain/	Entity và quy tắc nghiệp vụ cốt lõi.
backend/CollabSphere.Infrastructure/	Tính bền vững, ánh xạ, kho lưu trữ, dịch vụ, di chuyển và dữ liệu gốc.
backend/ai-agent/	Mô-đun AI viết bằng Python, được tách riêng khỏi .NET dự án.
backend/tests/	Đã có API kiểm tra tập lệnh.
giao diện người dùng/	Mã giao diện, thành phần, trang, tuyến đường, dịch vụ và tài nguyên tĩnh.
figma/	Tài nguyên thiết kế, hệ thống thiết kế, nguyên mẫu và wireframe.


8. Cơ sở dữ liệu thiết kế
8.1. Hệ thống quản trị và quy ước
Cơ sở dữ liệu sử dụng MySQL 8.0 trở lên , với bộ ký tự utf8mb4 và collation utf8mb4_unicode_ci . Bảng sử dụng InnoDB để hỗ trợ khóa và giao dịch.

Cơ sở dữ liệu có tên mặc định:
CREATE DATABASE collabsphere
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;
8.2. Dữ liệu nhóm
Các bảng được chia thành các nhóm dịch vụ:
Nhóm	vật liệu tiêu biểu
Định danh và học vụ	vai trò , người dùng , sinh viên , giảng viên , môn học , giáo trình, lớp học , sinh viên lớp học , giảng viên lớp học
Dự án và nhóm	dự án , nhóm , thành viên nhóm
Tiến độ và sự thay đổi	các mốc quan trọng , các điểm kiểm tra , nhiệm vụ , người được giao nhiệm vụ , bài nộp
Tài nguyên và giao tiếp	tài nguyên , cuộc họp , người tham dự cuộc họp , tin nhắn
Không gian làm	không gian làm việc , ảnh chụp bảng trắng , tài liệu không gian làm việc , phiên bản tài liệu
Đánh giá, thông báo và AI	đánh giá , thông báo , cuộc hội thoại AI , tin nhắn AI


8.3. Quản lý cấu trúc bằng SQL Migration
Kho lưu trữ hiện có các lần di chuyển được đánh số từ 001 đến 007 :
Phiên bản	Di chuyển tên	Phạm vi
001	identity_academic	User, vai trò trò chơi và dữ liệu học tập.
002	projects_teams	Dự án, nhóm và nhóm thành viên.
003	progress_tasks_submissions	Cột cột, điểm kiểm tra, nhiệm vụ và bài học.
004	tài nguyên_truyền thông	Tài nguyên, cuộc họp và tin nhắn.
005	workspaces_documents	Không có công việc, bảng cộng và tài liệu.
006	evaluations_notifications_ai	Đánh giá, thông báo và hội thoại AI.
007	seed_roles	Thêm mặc định các vai trò.


Phần cuối sử dụng bộ riêng di chuyển SQL chạy trong CollabSphere.Infrastructure/Database/MigrationEnvironment.cs . Tệp *.up.sql được sắp xếp theo tên và chỉ được áp dụng nếu chưa có phiên bản trong bảng lược đồ_migrations .

Quy tắc quan trọng khi cập nhật cơ sở dữ liệu:
- Không thể chỉnh sửa quá trình di chuyển đã được áp dụng trên môi trường chung.
- Khi cần thay đổi cấu hình, tạo mới di chuyển theo thứ tự tiếp theo.
- File .down.sql đã được lưu để tham khảo hoặc chạy thủ công; Phần cuối không tự động chạy rollback.
- Không sử dụng đồng thời EnsureCreated() hoặc Database.Migrate() cho lược đồ đang được quản lý bằng cách riêng biệt di chuyển SQL.
- Nên sao lưu dữ liệu trước khi chạy thay đổi lược đồ trên môi trường có quan trọng về dữ liệu.
8.4. Cơ sở dữ liệu Các bước
/ thư tín	Mục đích
database/01-erd.md	Mô tả thực tế, thuộc tính và quan hệ dữ liệu.
database/schema.sql	Schema MySQL tổng hợp để tham khảo hoặc khởi tạo theo hướng dẫn phù hợp.
cơ sở dữ liệu/di chuyển/phiên bản/	Việc di chuyển tệp tăng/giảm cấu hình theo phiên bản.
database/migrations/MigrationEnvironment.cs	Quá trình di chuyển được sử dụng bởi Backend.
cơ sở dữ liệu/hạt giống/	Phát triển và kiểm tra mẫu dữ liệu.
database/scripts/run_mysql.sh	Hỗ trợ tập lệnh hoạt động với MySQL.


Trong quá trình chạy Hiện tại cuối cùng, quá trình di chuyển là lược đồ tạo cơ sở khởi động chính. Không thể chạy đồng thời lược đồ.sql rồi chạy lại toàn bộ quá trình di chuyển trên cùng một cơ sở dữ liệu trống, vì có thể dẫn đến tồn tại lỗi bảng.
9. Phần phụ trợ và API
9.1. Tổng quan
Phần cuối được phát triển bằng ASP.NET Core Web API trên .NET 10. Dữ liệu được truy cập thông qua nhà cung cấp Entity Framework Core và MySQL. Một số hoạt động khởi động cơ sở dữ liệu sử dụng MySqlConnector để chạy quá trình di chuyển tệp SQL.
9.2. Các thành phần chính
- Bộ điều khiển: tiếp tục nhận yêu cầu và trả lời phản hồi.
- Hợp đồng / DTO: xác định yêu cầu và phản hồi dữ liệu.
- Dịch vụ: xử lý các ứng dụng tác vụ như xác thực và tạo mã thông báo.
- Middleware: xử lý lỗi chưa được xử lý ở tầng yêu cầu.
- Persistence: định nghĩa DbContext và DbSet.
- Cấu hình: thực thể ánh sáng với cơ sở dữ liệu bảng, cột và cưỡng bức.
- Repositories: cung cấp lớp truy cập chung cho dữ liệu.
- MigrationEnvironment: kiểm tra và áp dụng Migration.
- DatabaseSeder: chuẩn hóa mẫu dữ liệu trong môi trường phát triển.
9.3. API hiện có
Theo phần cuối nguồn mã hóa trong kho lưu trữ, các bộ điều khiển hiện có:
Bộ điều khiển	Nhóm endpoint / trách nhiệm
AuthController	Đăng nhập và thông tin người dùng hiện tại.
AdminController	Quản trị điểm cuối và kiểm tra quyền quản trị.
SubjectsController	Quản lý môn học.
SyllabiController	Quản lý đề cương môn học.
StudyClassesController	Quản lý lớp học.
ClassAssignmentsController	Phân chia công viên và sinh viên vào lớp.
NotificationsController	Điểm cuối thông báo


Ngoài ra, API có các Hub server kết nối thời gian thực và hệ thống điểm cuối để kiểm tra trạng thái ứng dụng/cơ sở dữ liệu. Chi tiết lộ trình, yêu cầu, phản hồi và quyền truy cập cần xem trong backend/README.md và docs/API-CONTRACT-GUIDE.md .
9.4. Xác thực và phân quyền
Phần cuối sử dụng JWT Bearer. Sau khi đăng nhập thành công, khách hàng nhận mã thông báo truy cập và gửi mã thông báo trong tiêu đề của các yêu cầu cần xác thực:
Authorization: Bearer <access-token>
Vai trò mã hóa đang được sử dụng trong Phần cuối:
Mã vai trò	Ý nghĩa
QUẢN TRỊ VIÊN	Quản trị viên
ĐẦU TRANG	Trưởng bộ
NHÂN VIÊN	nhân viên học vụ
GIẢNG VIÊN	Giảng viên
HỌC SINH	Sinh viên


Mật khẩu được xử lý bằng ASP.NET Identity PassHasher; không lưu mật khẩu dạng văn bản tĩnh trong cơ sở dữ liệu.
10. Giao diện người dùng
Frontend thư mục / chứa giao diện và các thành phần dịch vụ tương thích với Backend.
10.1. Thành phần của nhóm
Thư mục	Trách
tài sản/	Icon, hình ảnh và CSS.
các thành phần/	Giao diện thành phần được sử dụng lại như bảng, modal, nút, biểu mẫu và nội dung thẻ.
ngữ cảnh/	Trạng thái chung như xác thực và thông báo.
hooks/	Các dịch vụ được quản lý trạng thái và giao diện dữ liệu.
trang/	Các trang có chức năng như đăng nhập, lớp học, môn học, dự án, nhóm và nhiệm vụ.
routes/	Khai báo điều hướng và kiểm soát lộ trình theo trạng thái/quyền.
dịch vụ/	Client call API và các dịch vụ theo nghiệp vụ.
tiện ích/	Hằng số, dữ liệu định dạng và kiểm tra đầu vào.
công khai/	Tài nguyên được phục vụ trực tiếp.


10.2. Source code trạng thái
Frontend hiện chứa các tệp .jsx , .razor và .cs . Vì vậy, không nên mô tả giao diện người dùng là một ứng dụng React mực hoặc một ứng dụng Blazor đã hoàn thiện. Đây là khu vực đang có bộ kết hợp/chuyển đổi nguồn mã hóa.

frontend/package.json hiện chưa khai báo đầy đủ các tập lệnh chạy giao diện ứng dụng. Do đó, npm run dev chỉ có thể sử dụng sau khi gói và cấu hình giao diện người dùng tương thích hoàn thiện. Không thể xem đủ gói cài đặt npm để khởi chạy toàn bộ hiện tại giao diện.
11. Cài đặt môi trường
11.1. Yêu cầu
Công cụ	Công bản / yêu cầu
Git	Phiên bản phù hợp với hệ điều hành.
SDK .NET	.NET 10 SDK
Máy chủ MySQL	MySQL 8.0 trở lên.
Máy khách MySQL	MySQL Workbench hoặc MySQL CLI để quản lý cơ sở dữ liệu.
IDE	Visual Studio hoặc Visual Studio Code.
PowerShell	Khuyến nghị nếu chạy thử tập lệnh .ps1 .
Python	Chỉ cần phát triển hoặc chạy module backend/ai-agent/ .


Kiểm tra cài đặt:
git --version
dotnet --version
mysql --version
11.2. Tải mã nguồn
git clone https://github.com/nguyenminhthien140906-dot/CNPM.git
cd CNPM
Nếu đã tải ZIP từ GitHub, hãy giải nén mã nguồn và mở terminal tại thư mục gốc CNPM .
12. Khởi tạo và chạy Backend
Các bước sau được căn cứ theo hướng dẫn trong backend/README.md .
Bước 1. Tạo cơ sở dữ liệu
Mở MySQL Workbench hoặc MySQL CLI và chạy:
CREATE DATABASE collabsphere
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;
Chỉ tạo cơ sở dữ liệu trống. Phần cuối sẽ tự động áp dụng việc di chuyển tệp để tạo bảng.
Bước 2. Move to project API
cd backend/CollabSphere.API
Bước 3. Cấu hình User Secrets
Bí mật người dùng được sử dụng để lưu thông tin nhạy cảm trong môi trường phát triển, tránh ghi mật khẩu cơ sở dữ liệu và khóa JWT trực tiếp vào mã nguồn.

Windows PowerShell:
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=collabsphere;User=root;Password=MAT_KHAU_MYSQL;"
dotnet user-secrets set "Jwt:Key" ([Convert]::ToBase64String((1..48 | ForEach-Object { Get-Random -Maximum 256 })))
dotnet user-secrets set "Seed:DefaultPassword" "Collab@12345"
Thay đổi MAT_KHAU_MYSQL bằng MySQL mật khẩu đang được sử dụng trên máy chủ. Mẫu mật khẩu ở trên chỉ dành riêng cho môi trường phát triển; khi phát triển khai thực, hãy sử dụng mật khẩu riêng và bảo mật.

Nếu sử dụng PowerShell, hãy chạy từng lệnh tại backend/CollabSphere.API thư mục .
Bước 4. Chọn môi trường Development
Trong PowerShell:
$env:ASPNETCORE_ENVIRONMENT = "Development"
Seed mẫu dữ liệu được chạy chỉ trong môi trường Development và khi cấu hình Seed:DefaultPassword .
Bước 5. Khôi phục và xây dựng
Đang ở backend/CollabSphere.API thư mục thư mục , chạy:
dotnet restore
dotnet build
Bước 6. Chạy API
dotnet run --urls "http://localhost:5000"
Nếu khởi động thành công, terminal sẽ hiển thị địa chỉ ứng dụng đang lắng nghe. Giữ thiết bị đầu cuối này mở trong quá trình kiểm tra thời gian.
Bước 7. Kiểm tra trạng thái
Mở trình duyệt hoặc gửi yêu cầu:
GET http://localhost:5000/api/system/status
Kiểm tra ánh xạ tra cứu và cơ sở dữ liệu kết nối:
GET http://localhost:5000/api/system/database-check
Điểm cuối này được mô tả trong Phần cuối tài liệu. Nếu sử dụng một cổng khác, thay thế 5000 cổng sẽ được hiển thị trong thiết bị đầu cuối.
Lưu ý khi bắt đầu
- Tài khoản MySQL phải có quyền tạo bảng và ghi dữ liệu trong cơ sở dữ liệu cộng tác .
- Jwt:Key phải là hợp lệ Base64 chuỗi và giải mã tối thiểu 32 byte.
- Không cam kết Bí mật người dùng hoặc thông tin xác thực lên GitHub.
- Nếu quá trình di chuyển bị lỗi ở giữa, cần đọc di chuyển tên và thông báo lỗi trước khi xử lý cơ sở dữ liệu.
- Trên cơ sở dữ liệu đã có dữ liệu, không tự động xóa cơ sở dữ liệu để sửa lỗi. Hãy sao lưu và xác định nhân vật trước đó.
13. Kiểm tra API
Có thể sử dụng Postman, Curl hoặc công cụ HTTP bất kỳ để kiểm tra API.
13.1. Kiểm tra hệ thống trạng thái
curl http://localhost:5000/api/system/status
13.2. Đăng nhập
POST http://localhost:5000/api/auth/login
Content-Type: application/json
Minh họa thân bài:
{
  "usernameOrEmail": "admin",
  "password": "Collab@12345"
}
Tài khoản và mật khẩu hợp lệ nếu mẫu dữ liệu đã được gieo thành công và trùng lặp mật khẩu với Seed:DefaultPassword được cấu hình trên máy chủ.
13.3. Gửi yêu cầu xác thực
Sau khi nhận access token, add header:
Authorization: Bearer <access-token>
13.4. Một số điểm cuối được mô tả trong Phần cuối
Phương thức	Điểm cuối	Mục đích
GET	/api/system/status	Kiểm tra trạng thái ứng dụng và cơ sở dữ liệu.
GET	/api/system/database-check	Kiểm tra cơ sở dữ liệu kết nối và ánh xạ.
BÀI ĐĂNG	/api/auth/login	Đăng nhập
GET	/api/auth/me	Lấy thông tin người dùng từ token.
GET	/api/subjects	Nhận danh sách môn học.
GET	/api/syllabi	Truy xuất đề cương theo API được phát triển khai.
GET	/api/classes	Xuất bản danh sách lớp theo API được phát triển khai.


Đây là danh sách tham khảo từ Backend hiện có; xem backend/README.md để biết đầy đủ thông số, nội dung yêu cầu, phản hồi và quyền của từng điểm cuối.
14. vật liệu mẫu
Các tập tin hạt giống nằm ở cơ sở dữ liệu/hạt giống/ :
Tệp	Nội dung
001_seed_roles.sql	Các vai trò mặc định.
002_seed_users.sql	Tài khoản mẫu theo các trò chơi.
003_seed_subjects.sql	vật liệu môn học.
004_seed_syllabi.sql	vật liệu đề cập.
005_seed_classes.sql	Lớp học dữ liệu và công cụ phân tích.


Seed được gọi backend trong môi trường Development khi có cấu hình Seed:DefaultPassword .

Một số mẫu tài khoản được mô tả trong backend/README.md :
Tên người dùng	Email	Vai trò
quản trị viên	admin@collabsphere.local	QUẢN TRỊ VIÊN
head01	head01@collabsphere.local	ĐẦU TRANG
staff01	staff01@collabsphere.local	NHÂN VIÊN
giảng viên01	lecture01@collabsphere.local	GIẢNG VIÊN
student01	student01@collabsphere.local	HỌC SINH
sinh viên02	student02@collabsphere.local	HỌC SINH


Mật khẩu được sử dụng để đăng nhập là Seed có giá trị:DefaultPassword được cấu hình trên máy phát triển. Không sử dụng mẫu tài khoản này trong môi trường thật.

Lưu ý: File user có nội dung giữ chỗ cho mật khẩu băm. Không chạy tệp riêng trực tiếp trong MySQL Workbench để tạo thông tin đăng nhập tài khoản; hãy để DatabaseSeeder xử lý quy trình của Backend.
15. Kiểm thử
Kho lưu trữ hiện có API kiểm tra tập lệnh:
backend/tests/api-smoke-test.ps1
Có thể chạy PowerShell sau khi khởi động Backend:
.\backend\tests\api-smoke-test.ps1
Nếu tập lệnh yêu cầu tham số hoặc URL cơ sở riêng, hãy kiểm tra tệp nội dung trước khi chạy.
15.1. Phạm vi cần kiểm tra
- Đăng nhập với thông tin hợp lệ và không hợp lệ.
- Kiểm tra mã thông báo còn hạn, hết hạn và mã thông báo không hợp lệ.
- Kiểm tra quyền truy cập theo từng vai trò.
- Tạo, xem, cập nhật và xóa dữ liệu học tập theo quyền.
- Kiểm tra mã hóa trùng lặp dữ liệu và dữ liệu không hợp lệ.
- Kiểm tra ngoại khóa và kích hoạt dữ liệu.
- Kiểm tra kết nối giữa Frontend, API và MySQL.
- Kiểm tra quá trình xử lý lỗi khi cơ sở dữ liệu không khả dụng.
15.2. Đã hoàn thành quy tắc xác thực
Một chức năng sẽ được đánh dấu hoàn thành khi:
1. Có mô tả yêu cầu và quy tắc nghiệp vụ.
2. Có phần khai triển tương ứng.
3. Có kiểm tra quyền truy cập nếu có chức năng yêu cầu quyền.
4. Kiểm tra các trường hợp lệ thành công và thất bại.
5. Kết quả được đối chiếu với yêu cầu ban đầu.
16. Quy trình phát triển và đóng góp ý kiến
Để hạn chế xung đột khi nhiều thành viên cùng làm việc, nên quy trình thống nhất sau:
16.1. Quy trình thực thi chức năng
1. Đọc các chức năng tài liệu tương ứng trong docs/ .
2. Xác định vai trò sử dụng, đầu vào dữ liệu, kết quả và quy tắc nghiệp vụ.
3. Kiểm tra ERD và các liên kết bảng trong cơ sở dữ liệu/ .
4. Nếu cần thay đổi cơ sở dữ liệu, hãy tạo mới di chuyển.
5. Triển khai Domain/Application/Infrastructure/API theo tinh thần trách nhiệm từng lớp.
6. Kết nối giao diện với API theo hợp đồng API.
7. Kiểm tra và cập nhật tài liệu.
8. Tạo yêu cầu kéo để kiểm tra trước khi hợp nhất vào nhánh chính.
16.2. Quy tắc làm việc với cơ sở dữ liệu
- Bảng tên, cột tên và kiểu dữ liệu thấp nhất trước khi khai báo.
- Mọi thay đổi lược đồ cần có sự di chuyển tương ứng.
- Quá trình di chuyển không tự động sửa đổi trên môi trường dùng chung.
- Không xác nhận mật khẩu, khóa JWT hoặc kết nối thông tin thật.
- Cập nhật ERD và tài liệu khi thay đổi hệ thống dữ liệu.
16.3. Quy tắc làm việc với API
- Sử dụng điểm cuối và dữ liệu cấu trúc mới nhất trong hợp đồng API.
- Kiểm tra tính xác thực và phân quyền ở các điểm cuối cần bảo vệ.
- Trả lại trạng thái HTTP phù hợp.
- Không trả lời cảm biến thông tin trong phản hồi hoặc nhật ký.
- Cập nhật tài liệu khi thay đổi lộ trình hoặc yêu cầu/phản hồi.
16.4. Git cơ bản
git status
git pull
git checkout -b feature/ten-chuc-nang
git add .
git commit -m "feat: mo ta thay doi"
git push -u origin feature/ten-chuc-nang
Tên nhánh và nội dung cam kết cần phải đánh giá tính chính xác của công việc được thực hiện. Trước khi tạo Yêu cầu kéo, cần kiểm tra thay đổi và chạy xây dựng/kiểm tra mức độ phù hợp.
17. Tài liệu liên quan
Tài liệu	Nội dung
[`docs/README.md`](docs/README.md)	Hướng dẫn đọc và sử dụng dự án tài liệu.
[`docs/PROJECT-TREE.md`](docs/PROJECT-TREE.md)	Chi tiết thư mục cấu trúc.
[`docs/API-CONTRACT-GUIDE.md`](docs/API-CONTRACT-GUIDE.md)	Quy định giao tiếp giữa Frontend và Backend.
[`docs/api-hoc-vu.md`](docs/api-hoc-vu.md)	Tài liệu API học vụ.
[`docs/FE01-user-account-management.md`](docs/FE01-user-account-management.md)	Quản lý người dùng tài khoản.
[`docs/FE02-subject-syllabus-management.md`](docs/FE02-subject-syllabus-management.md)	Quản lý môn học và đề cương.
[`docs/FE03-class-management.md`](docs/FE03-class-management.md)	Quản lý lớp học.
[`docs/FE04-project-management.md`](docs/FE04-project-management.md)	Quản lý dự án.
[`docs/FE05-team-workspace-management.md`](docs/FE05-team-workspace-management.md)	Quản lý nhóm và không làm việc.
[`docs/FE06-milestone-checkpoint-management.md`](docs/FE06-milestone-checkpoint-management.md)	Quản lý cột cột và điểm kiểm tra.
[`docs/FE07-communication-meetings.md`](docs/FE07-communication-meetings.md)	Giao tiếp và cuộc họp.
[`docs/FE08-resource-management.md`](docs/FE08-resource-management.md)	Quản lý tài nguyên.
[`docs/FE09-realtime-collaboration-tools.md`](docs/FE09-realtime-collaboration-tools.md)	Công cụ cộng tác thời gian thực hiện.
[`docs/FE10-assessment-feedback.md`](docs/FE10-assessment-feedback.md)	Đánh giá và phản hồi.
[`docs/FE11-ai-assistance.md`](docs/FE11-ai-assistance.md)	Hỗ trợ AI học tập.
[`docs/FE12-notification-management.md`](docs/FE12-notification-management.md)	Quản lý thông báo.
[`database/01-erd.md`](database/01-erd.md)	Mô hình thực thi và quan hệ dữ liệu.
[`database/schema.sql`](database/schema.sql)	Schema MySQL tổng hợp.
[`backend/README.md`](backend/README.md)	Hướng dẫn kỹ thuật chi tiết cho Backend.
[`frontend/README.md`](frontend/README.md)	Tài liệu Frontend


18. Phạm vi phát triển khai hiện tại
Phần này giúp phân biệt phạm vi chức năng của toàn bộ dự án và những thành phần đã có trong kho lưu trữ tại bản cập nhật README thời điểm hiện tại.
18.1. Phần phụ trợ
Theo tài liệu Backend và nguồn cấu trúc hiện tại:
- Đã có API dự án, Ứng dụng, Miền và Cơ sở hạ tầng trên .NET 10.
- Đã cấu hình kết nối MySQL, JWT Bearer và chạy SQL Migration.
- Đã có Migration 001 đến 007 và file Seed.
- Các API học vụ hiện có tập trung vào xác thực, môn học, đề cương, lớp học và phân công thành viên lớp.
- Cơ sở dữ liệu đã có cấu trúc cho nhiều nhóm dịch vụ khác; API của các nhóm đã được hoàn thiện không đồng nghĩa.
18.2. Giao diện người dùng
- Có cấu trúc trang, thành phần, dịch vụ, tuyến đường, bối cảnh và tiện ích.
- Nguồn đang chứa .jsx , .razor và .cs .
- Hiện tại gói cấu hình chưa đủ để xác nhận toàn bộ giao diện có thể chạy bằng tiêu chuẩn npm lệnh.
- Cần thống nhất hướng dẫn phát triển giao diện người dùng và hoàn thiện cấu hình xây dựng/chạy trước khi xem đây là bản phát hành hoàn chỉnh.
18.3. Tác nhân AI
- Có mô-đun Python dành riêng trong backend/ai-agent/ .
- Có các nhóm mô hình, tuyến đường, dịch vụ và đại lý.
- Cần kiểm tra cấu hình môi trường và thực thi hợp lệ luồng trước khi xác nhận AI Agent đã kết nối hoàn chỉnh với chính API.
18.4. Trạng thái cập nhật nguyên tắc
README này không đánh dấu toàn bộ bộ FE01–FE12 đã hoàn thành. Khi một chức năng được phát triển, cần phải cập nhật trạng thái dựa trên mã, API, giao diện và kết quả kiểm tra thực tế.
19. Phát triển định hướng
Các hướng phát triển tiếp theo của CollabSphere:
1. Frontend kiến ​​trúc hoàn thiện và hệ thống nhất.
2. API mở rộng cho các dự án nghiệp vụ, nhóm, nhiệm vụ, bài phụ và đánh giá.
3. Đồng bộ thực thể, ánh xạ EF Core và lược đồ MySQL.
4. Hoàn thiện kiểm soát quyền theo vai trò trò chơi và theo phạm vi dữ liệu.
5. Hoàn thiện các chức năng cộng đồng, thông báo và trao đổi thời gian.
6. Làm rõ cơ chế AI Agent hợp lý với Backend chính.
7. Thử nghiệm tự động và tài liệu API bổ sung.
8. Chuẩn bị hóa cấu hình môi trường phát triển, kiểm tra và phát triển khai báo.
quyền và đóng góp
CollabSphere là một dự án phát triển phần mềm dịch vụ đào tạo mục tiêu và thực hành xây dựng hệ thống. Các thành viên tham gia cần bổ sung quy ước mã nguồn, tài liệu và quy trình cộng tác của kho lưu trữ.
CollabSphere — Học tập thông qua dự án, cộng tác để phát triển.
