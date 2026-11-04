# COLLABSPHERE - ENTITY RELATIONSHIP DIAGRAM (ERD)

## 1. Giới thiệu

CollabSphere là hệ thống hỗ trợ học tập theo phương pháp Project-Based Learning (PBL). Hệ thống cung cấp các chức năng quản lý môn học, lớp học, dự án, nhóm, công việc, tiến độ, trao đổi thông tin và đánh giá thành viên.

Cơ sở dữ liệu được thiết kế bằng **MySQL 8.0**, sử dụng InnoDB để đảm bảo tính toàn vẹn dữ liệu và hỗ trợ các mối quan hệ giữa các bảng.

## 2. Quy ước thiết kế

| Ký hiệu | Ý nghĩa |
|---|---|
| PK | Primary Key - Khóa chính |
| FK | Foreign Key - Khóa ngoại |
| UK | Unique Key - Khóa duy nhất |
| NN | Not Null - Không được để trống |
| AI | Auto Increment - Tự động tăng |

Các bảng sử dụng:
- Character Set: `utf8mb4`
- Collation: `utf8mb4_unicode_ci`
- Storage Engine: `InnoDB`
- Database: `collabsphere`

## 3. Danh sách thực thể và thuộc tính

### 3.1. ROLES - Quản lý vai trò

Lưu thông tin các vai trò trong hệ thống.

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| role_id | BIGINT UNSIGNED | PK, AI |
| role_code | VARCHAR(30) | UK, NN |
| role_name | VARCHAR(100) | NN |
| description | VARCHAR(255) | |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

### 3.2. USERS - Quản lý tài khoản

Lưu thông tin tài khoản của người dùng.

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| user_id | BIGINT UNSIGNED | PK, AI |
| role_id | BIGINT UNSIGNED | FK, NN |
| username | VARCHAR(50) | UK, NN |
| email | VARCHAR(150) | UK, NN |
| password_hash | VARCHAR(255) | NN |
| full_name | VARCHAR(150) | NN |
| phone | VARCHAR(20) | |
| avatar_url | VARCHAR(500) | |
| is_active | BOOLEAN | DEFAULT TRUE |
| last_login_at | DATETIME | |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |
| updated_at | TIMESTAMP | ON UPDATE CURRENT_TIMESTAMP |

FK: `role_id` tham chiếu `roles(role_id)`.

### 3.3. STUDENTS - Quản lý sinh viên

Lưu thông tin bổ sung của sinh viên.

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| student_id | BIGINT UNSIGNED | PK, AI |
| user_id | BIGINT UNSIGNED | FK, UK, NN |
| student_code | VARCHAR(30) | UK, NN |
| department | VARCHAR(120) | |
| cohort | VARCHAR(30) | |

FK: `user_id` tham chiếu `users(user_id)`.

### 3.4. LECTURERS - Quản lý giảng viên

Lưu thông tin bổ sung của giảng viên.

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| lecturer_id | BIGINT UNSIGNED | PK, AI |
| user_id | BIGINT UNSIGNED | FK, UK, NN |
| lecturer_code | VARCHAR(30) | UK, NN |
| department | VARCHAR(120) | |
| academic_title | VARCHAR(80) | |

FK: `user_id` tham chiếu `users(user_id)`.

### 3.5. SUBJECTS - Quản lý môn học

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| subject_id | BIGINT UNSIGNED | PK, AI |
| subject_code | VARCHAR(30) | UK, NN |
| subject_name | VARCHAR(180) | NN |
| description | TEXT | |
| credit_count | TINYINT UNSIGNED | DEFAULT 0 |
| is_active | BOOLEAN | DEFAULT TRUE |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

### 3.6. SYLLABI - Quản lý đề cương môn học

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| syllabus_id | BIGINT UNSIGNED | PK, AI |
| subject_id | BIGINT UNSIGNED | FK, NN |
| version | VARCHAR(30) | NN |
| objectives | TEXT | |
| content | LONGTEXT | |
| learning_outcomes | TEXT | |
| reference_materials | TEXT | |
| file_url | VARCHAR(500) | |
| created_by | BIGINT UNSIGNED | FK |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

FK:
- `subject_id` tham chiếu `subjects(subject_id)`.
- `created_by` tham chiếu `users(user_id)`.

UK: `(subject_id, version)`.

### 3.7. STUDY_CLASSES - Quản lý lớp học

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| class_id | BIGINT UNSIGNED | PK, AI |
| subject_id | BIGINT UNSIGNED | FK, NN |
| class_code | VARCHAR(40) | UK, NN |
| class_name | VARCHAR(150) | NN |
| semester | VARCHAR(30) | |
| academic_year | VARCHAR(20) | |
| capacity | SMALLINT UNSIGNED | |
| schedule | VARCHAR(255) | |
| room | VARCHAR(80) | |
| status | ENUM | DEFAULT 'planned' |
| created_by | BIGINT UNSIGNED | FK |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |
| updated_at | TIMESTAMP | ON UPDATE CURRENT_TIMESTAMP |

FK:
- `subject_id` tham chiếu `subjects(subject_id)`.
- `created_by` tham chiếu `users(user_id)`.

### 3.8. CLASS_STUDENTS - Sinh viên thuộc lớp học

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| class_id | BIGINT UNSIGNED | PK, FK |
| student_id | BIGINT UNSIGNED | PK, FK |
| joined_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |
| status | ENUM | DEFAULT 'enrolled' |

FK:
- `class_id` tham chiếu `study_classes(class_id)`.
- `student_id` tham chiếu `students(student_id)`.

PK kép: `(class_id, student_id)`.

### 3.9. CLASS_LECTURERS - Giảng viên phụ trách lớp

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| class_id | BIGINT UNSIGNED | PK, FK |
| lecturer_id | BIGINT UNSIGNED | PK, FK |
| is_primary | BOOLEAN | DEFAULT FALSE |
| assigned_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

FK:
- `class_id` tham chiếu `study_classes(class_id)`.
- `lecturer_id` tham chiếu `lecturers(lecturer_id)`.

### 3.10. PROJECTS - Quản lý dự án

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| project_id | BIGINT UNSIGNED | PK, AI |
| class_id | BIGINT UNSIGNED | FK, NN |
| created_by | BIGINT UNSIGNED | FK, NN |
| title | VARCHAR(200) | NN |
| description | TEXT | NN |
| objectives | TEXT | NN |
| requirements | TEXT | |
| acceptance_criteria | TEXT | |
| start_date | DATE | NN |
| due_date | DATE | NN |
| min_group_size | TINYINT UNSIGNED | DEFAULT 1 |
| max_group_size | TINYINT UNSIGNED | DEFAULT 6 |
| status | ENUM | DEFAULT 'draft' |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |
| updated_at | TIMESTAMP | ON UPDATE CURRENT_TIMESTAMP |

FK:
- `class_id` tham chiếu `study_classes(class_id)`.
- `created_by` tham chiếu `users(user_id)`.

### 3.11. TEAMS - Quản lý nhóm

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| team_id | BIGINT UNSIGNED | PK, AI |
| project_id | BIGINT UNSIGNED | FK, NN |
| team_name | VARCHAR(120) | NN |
| team_code | VARCHAR(30) | |
| leader_student_id | BIGINT UNSIGNED | FK |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

FK:
- `project_id` tham chiếu `projects(project_id)`.
- `leader_student_id` tham chiếu `students(student_id)`.

UK: `(project_id, team_name)`.

### 3.12. TEAM_MEMBERS - Thành viên nhóm

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| team_id | BIGINT UNSIGNED | PK, FK |
| student_id | BIGINT UNSIGNED | PK, FK |
| member_role | ENUM | DEFAULT 'member' |
| joined_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

FK:
- `team_id` tham chiếu `teams(team_id)`.
- `student_id` tham chiếu `students(student_id)`.

### 3.13. MILESTONES - Các giai đoạn dự án

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| milestone_id | BIGINT UNSIGNED | PK, AI |
| project_id | BIGINT UNSIGNED | FK, NN |
| title | VARCHAR(180) | NN |
| description | TEXT | |
| start_date | DATE | |
| due_date | DATE | NN |
| sequence_no | SMALLINT UNSIGNED | DEFAULT 1 |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

FK: `project_id` tham chiếu `projects(project_id)`.

UK: `(project_id, sequence_no)`.

### 3.14. CHECKPOINTS - Các mốc kiểm tra

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| checkpoint_id | BIGINT UNSIGNED | PK, AI |
| milestone_id | BIGINT UNSIGNED | FK, NN |
| title | VARCHAR(180) | NN |
| instructions | TEXT | |
| due_date | DATETIME | |
| max_score | DECIMAL(5,2) | DEFAULT 10.00 |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

FK: `milestone_id` tham chiếu `milestones(milestone_id)`.

### 3.15. TASKS - Quản lý công việc

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| task_id | BIGINT UNSIGNED | PK, AI |
| team_id | BIGINT UNSIGNED | FK, NN |
| checkpoint_id | BIGINT UNSIGNED | FK |
| created_by | BIGINT UNSIGNED | FK |
| title | VARCHAR(180) | NN |
| description | TEXT | |
| status | ENUM | DEFAULT 'to_do' |
| priority | ENUM | DEFAULT 'medium' |
| start_date | DATETIME | |
| deadline | DATETIME | |
| completed_at | DATETIME | |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |
| updated_at | TIMESTAMP | ON UPDATE CURRENT_TIMESTAMP |

FK:
- `team_id` tham chiếu `teams(team_id)`.
- `checkpoint_id` tham chiếu `checkpoints(checkpoint_id)`.
- `created_by` tham chiếu `users(user_id)`.

### 3.16. TASK_ASSIGNEES - Phân công công việc

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| task_id | BIGINT UNSIGNED | PK, FK |
| student_id | BIGINT UNSIGNED | PK, FK |
| assigned_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

FK:
- `task_id` tham chiếu `tasks(task_id)`.
- `student_id` tham chiếu `students(student_id)`.

### 3.17. SUBMISSIONS - Bài nộp

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| submission_id | BIGINT UNSIGNED | PK, AI |
| checkpoint_id | BIGINT UNSIGNED | FK, NN |
| team_id | BIGINT UNSIGNED | FK, NN |
| submitted_by | BIGINT UNSIGNED | FK, NN |
| content | TEXT | |
| file_url | VARCHAR(500) | |
| submitted_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |
| status | ENUM | DEFAULT 'submitted' |
| feedback | TEXT | |
| score | DECIMAL(5,2) | |

FK:
- `checkpoint_id` tham chiếu `checkpoints(checkpoint_id)`.
- `team_id` tham chiếu `teams(team_id)`.
- `submitted_by` tham chiếu `users(user_id)`.

### 3.18. RESOURCES - Tài nguyên dự án

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| resource_id | BIGINT UNSIGNED | PK, AI |
| team_id | BIGINT UNSIGNED | FK |
| project_id | BIGINT UNSIGNED | FK |
| uploaded_by | BIGINT UNSIGNED | FK, NN |
| resource_name | VARCHAR(180) | NN |
| file_url | VARCHAR(500) | NN |
| file_type | VARCHAR(80) | |
| file_size | BIGINT UNSIGNED | |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

### 3.19. MEETINGS - Quản lý cuộc họp

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| meeting_id | BIGINT UNSIGNED | PK, AI |
| team_id | BIGINT UNSIGNED | FK, NN |
| created_by | BIGINT UNSIGNED | FK, NN |
| title | VARCHAR(180) | NN |
| meeting_url | VARCHAR(500) | |
| start_at | DATETIME | NN |
| end_at | DATETIME | |
| status | ENUM | DEFAULT 'scheduled' |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

### 3.20. MEETING_PARTICIPANTS - Người tham gia cuộc họp

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| meeting_id | BIGINT UNSIGNED | PK, FK |
| user_id | BIGINT UNSIGNED | PK, FK |
| joined_at | DATETIME | |
| left_at | DATETIME | |

### 3.21. MESSAGES - Tin nhắn nhóm

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| message_id | BIGINT UNSIGNED | PK, AI |
| team_id | BIGINT UNSIGNED | FK, NN |
| sender_id | BIGINT UNSIGNED | FK, NN |
| message_content | TEXT | NN |
| sent_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |
| edited_at | DATETIME | |
| is_deleted | BOOLEAN | DEFAULT FALSE |

### 3.22. WORKSPACES - Không gian làm việc

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| workspace_id | BIGINT UNSIGNED | PK, AI |
| team_id | BIGINT UNSIGNED | FK, UK, NN |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |
| updated_at | TIMESTAMP | ON UPDATE CURRENT_TIMESTAMP |

### 3.23. WHITEBOARD_SNAPSHOTS - Dữ liệu bảng trắng

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| snapshot_id | BIGINT UNSIGNED | PK, AI |
| workspace_id | BIGINT UNSIGNED | FK, NN |
| created_by | BIGINT UNSIGNED | FK |
| snapshot_data | JSON | NN |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

### 3.24. WORKSPACE_DOCUMENTS - Tài liệu làm việc chung

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| document_id | BIGINT UNSIGNED | PK, AI |
| workspace_id | BIGINT UNSIGNED | FK, NN |
| created_by | BIGINT UNSIGNED | FK |
| title | VARCHAR(180) | NN |
| content | LONGTEXT | |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |
| updated_at | TIMESTAMP | ON UPDATE CURRENT_TIMESTAMP |

### 3.25. DOCUMENT_VERSIONS - Lịch sử tài liệu

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| version_id | BIGINT UNSIGNED | PK, AI |
| document_id | BIGINT UNSIGNED | FK, NN |
| edited_by | BIGINT UNSIGNED | FK |
| content | LONGTEXT | NN |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

### 3.26. EVALUATIONS - Đánh giá

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| evaluation_id | BIGINT UNSIGNED | PK, AI |
| evaluator_id | BIGINT UNSIGNED | FK, NN |
| target_student_id | BIGINT UNSIGNED | FK |
| team_id | BIGINT UNSIGNED | FK |
| project_id | BIGINT UNSIGNED | FK |
| evaluation_type | ENUM | NN |
| score | DECIMAL(4,2) | NN |
| comment | TEXT | |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

Điểm đánh giá phải nằm trong khoảng từ 0 đến 10.

### 3.27. NOTIFICATIONS - Thông báo

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| notification_id | BIGINT UNSIGNED | PK, AI |
| user_id | BIGINT UNSIGNED | FK, NN |
| title | VARCHAR(180) | NN |
| content | TEXT | NN |
| notification_type | VARCHAR(50) | NN |
| related_type | VARCHAR(50) | |
| related_id | BIGINT UNSIGNED | |
| is_read | BOOLEAN | DEFAULT FALSE |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

### 3.28. AI_CONVERSATIONS - Cuộc trò chuyện AI

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| conversation_id | BIGINT UNSIGNED | PK, AI |
| user_id | BIGINT UNSIGNED | FK, NN |
| title | VARCHAR(180) | |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

### 3.29. AI_MESSAGES - Tin nhắn AI

| Thuộc tính | Kiểu dữ liệu | Ràng buộc |
|---|---|---|
| ai_message_id | BIGINT UNSIGNED | PK, AI |
| conversation_id | BIGINT UNSIGNED | FK, NN |
| sender_type | ENUM | NN |
| content | LONGTEXT | NN |
| created_at | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP |

## 4. Mối quan hệ giữa các thực thể

| STT | Thực thể thứ nhất | Quan hệ | Thực thể thứ hai |
|---|---|---|---|
| 1 | roles | 1-N | users |
| 2 | users | 1-0..1 | students |
| 3 | users | 1-0..1 | lecturers |
| 4 | subjects | 1-N | syllabi |
| 5 | subjects | 1-N | study_classes |
| 6 | study_classes | N-N | students |
| 7 | study_classes | N-N | lecturers |
| 8 | study_classes | 1-N | projects |
| 9 | projects | 1-N | teams |
| 10 | projects | 1-N | milestones |
| 11 | teams | N-N | students |
| 12 | milestones | 1-N | checkpoints |
| 13 | teams | 1-N | tasks |
| 14 | tasks | N-N | students |
| 15 | checkpoints | 1-N | submissions |
| 16 | teams | 1-N | submissions |
| 17 | teams | 1-N | resources |
| 18 | teams | 1-N | meetings |
| 19 | meetings | N-N | users |
| 20 | teams | 1-N | messages |
| 21 | teams | 1-1 | workspaces |
| 22 | workspaces | 1-N | whiteboard_snapshots |
| 23 | workspaces | 1-N | workspace_documents |
| 24 | workspace_documents | 1-N | document_versions |
| 25 | users | 1-N | notifications |
| 26 | users | 1-N | ai_conversations |
| 27 | ai_conversations | 1-N | ai_messages |

## 5. Các ràng buộc dữ liệu

### 5.1. Primary Key
Mỗi bảng có khóa chính để xác định duy nhất từng bản ghi.

### 5.2. Foreign Key
Các khóa ngoại đảm bảo dữ liệu giữa những bảng liên quan luôn nhất quán.

### 5.3. Unique
Các trường cần duy nhất gồm:
- users.username
- users.email
- students.student_code
- lecturers.lecturer_code
- subjects.subject_code
- study_classes.class_code
- teams (project_id, team_name)
- syllabi (subject_id, version)

### 5.4. Not Null
Các thuộc tính quan trọng như tên người dùng, email, mật khẩu, tên dự án, tên nhóm và nội dung công việc không được để trống.

### 5.5. Check Constraint
- Ngày kết thúc dự án phải lớn hơn hoặc bằng ngày bắt đầu.
- Điểm đánh giá nằm trong khoảng 0 đến 10.
- Điểm bài nộp không được âm.

## 6. Quy tắc xóa dữ liệu

- Khi xóa tài khoản, hồ sơ sinh viên hoặc giảng viên tương ứng được xóa theo.
- Khi xóa lớp học, các bản ghi sinh viên và giảng viên tham gia lớp được xóa theo.
- Khi xóa dự án, nhóm và các giai đoạn của dự án được xóa theo.
- Khi xóa nhóm, thành viên, công việc và workspace liên quan được xóa theo.
- Khi xóa tài liệu, lịch sử phiên bản liên quan được xóa theo.
- Các bản ghi có người tạo bị xóa sẽ được giữ lại với trường người tạo bằng NULL nếu có thiết kế ON DELETE SET NULL.

## 7. Kết luận

Cơ sở dữ liệu CollabSphere gồm 29 thực thể, đáp ứng các nghiệp vụ chính của hệ thống học tập theo Project-Based Learning.

Thiết kế hỗ trợ quản lý tài khoản, phân quyền, môn học, lớp học, dự án, nhóm, công việc, checkpoint, bài nộp, họp nhóm, giao tiếp, workspace, đánh giá và AI.

Cấu trúc có thể được mở rộng khi hệ thống phát triển thêm các chức năng mới.
