```mermaid
erDiagram
    ROLE ||--o{ USER : assigns
    USER ||--o| STUDENT : profile
    USER ||--o| LECTURER : profile
    USER ||--o| STAFF : profile
    USER ||--o| HEAD_DEPARTMENT : profile

    SUBJECT ||--o| SYLLABUS : has
    SUBJECT ||--o{ PROJECT : supports
    SYLLABUS ||--o{ PROJECT : guides
    USER ||--o{ PROJECT : creates

    LECTURER ||--o{ STUDY_CLASS : teaches
    SUBJECT o|--o{ STUDY_CLASS : offered_in
    STUDY_CLASS ||--o{ CLASS_MEMBER : contains
    STUDENT ||--o{ CLASS_MEMBER : joins

    PROJECT ||--o{ PROJECT_MILESTONE : has
    PROJECT ||--o{ PROJECT_CLASS : assigned_to
    STUDY_CLASS ||--o{ PROJECT_CLASS : receives

    STUDY_CLASS ||--o{ TEAM : has
    TEAM ||--o{ TEAM_MEMBER : contains
    STUDENT ||--o{ TEAM_MEMBER : joins

    TEAM ||--o{ TASK : has
    TASK ||--o{ SUBTASK : contains
    STUDENT o|--o{ TASK : assigned
    STUDENT o|--o{ SUBTASK : assigned

    TEAM ||--o{ CHECKPOINT : has
    CHECKPOINT ||--o{ CHECKPOINT_SUBMISSION : receives
    STUDENT ||--o{ CHECKPOINT_SUBMISSION : submits

    TEAM ||--o{ EVALUATION : receives
    USER ||--o{ EVALUATION : evaluates
    USER ||--o{ EVALUATION : is_evaluated

    STUDY_CLASS o|--o{ RESOURCE : owns
    TEAM o|--o{ RESOURCE : owns
    USER ||--o{ RESOURCE : uploads
    USER ||--o{ NOTIFICATION : receives

    ROLE {
      int role_id PK
      string role_name UK
      string description
    }
   USER {
  uuid user_id PK
  int role_id FK
  string email UK
  string password_hash
  string full_name
  bool is_active
  string phone
  string avatar_url
  datetime created_at
  datetime updated_at
}
    STUDENT {
      uuid user_id PK,FK
      string student_code UK
      string major
      int academic_year
    }
    LECTURER {
      uuid user_id PK,FK
      string lecturer_code UK
      string department
      string academic_title
    }
    STAFF {
      uuid user_id PK,FK
    }
    HEAD_DEPARTMENT {
      uuid user_id PK,FK
      string department_name
    }
    SUBJECT {
      uuid subject_id PK
      string subject_code UK
      string subject_name
      int credits
    }
    SYLLABUS {
      uuid syllabus_id PK
      uuid subject_id FK,UK
      string title
      string content
      int version
    }
    STUDY_CLASS {
  uuid class_id PK
  uuid lecturer_id FK
  uuid subject_id FK
  string class_code UK
  string class_name
  string semester
  string academic_year
  datetime created_at
}
    CLASS_MEMBER {
      uuid class_id PK,FK
      uuid student_id PK,FK
    }
   PROJECT {
  uuid project_id PK
  uuid subject_id FK
  uuid syllabus_id FK
  uuid created_by FK
  string project_code UK
  string project_name
  string description
  string status
  date start_date
  date end_date
  datetime created_at
}
    PROJECT_CLASS {
      uuid project_id PK,FK
      uuid class_id PK,FK
    }
    PROJECT_MILESTONE {
      uuid milestone_id PK
      uuid project_id FK
      string milestone_name
      date due_date
    }
    TEAM {
      uuid team_id PK
      uuid class_id FK
      string team_name
    }
    TEAM_MEMBER {
      uuid team_id PK,FK
      uuid student_id PK,FK
      bool is_leader
      decimal contribution_percent
    }
    TASK {
  uuid task_id PK
  uuid team_id FK
  uuid assigned_to FK
  string task_name
  string description
  string status
  string priority
  datetime due_date
  datetime created_at
}
    SUBTASK {
      uuid subtask_id PK
      uuid task_id FK
      uuid assigned_to FK
      string subtask_name
      string status
    }
  CHECKPOINT {
  uuid checkpoint_id PK
  uuid team_id FK
  string checkpoint_name
  string description
  datetime due_date
  decimal max_score
  string status
}
    CHECKPOINT_SUBMISSION {
      uuid submission_id PK
      uuid checkpoint_id FK
      uuid student_id FK
      string content
    }
    EVALUATION {
      uuid evaluation_id PK
      uuid team_id FK
      uuid evaluator_id FK
      uuid target_user_id FK
      decimal score
      string feedback
    }
    RESOURCE {
  uuid resource_id PK
  uuid class_id FK
  uuid team_id FK
  uuid uploaded_by FK
  string resource_name
  string resource_url
  string description
  datetime created_at
}
    NOTIFICATION {
      uuid notification_id PK
      uuid user_id FK
      string notification_type
      string content
      bool is_read
    }
```

## Ràng buộc nghiệp vụ chính

- `students`, `lecturers`, `staff`, `head_departments`: `user_id` vừa là PK vừa là FK đến `users.user_id`.
- Mỗi môn học có tối đa một đề cương (`syllabus.subject_id` là `UNIQUE`); môn học có thể chưa có đề cương.
- Một sinh viên không thể được thêm hai lần vào cùng lớp hoặc cùng nhóm.
- Mỗi nhóm có tối đa một trưởng nhóm (`team_members.is_leader = TRUE`).
- `contribution_percent` nằm trong khoảng 0–100; điểm đánh giá nằm trong khoảng 0–100.
- Tài liệu (`resources`) phải thuộc **một trong hai** đối tượng: lớp học hoặc nhóm; không được để cả hai cùng rỗng hoặc cùng có giá trị.
- Ngày kết thúc dự án không được trước ngày bắt đầu.
