# ERD - Initial Design

Đây là ERD thiết kế ban đầu từ requirements. Nhóm cần review trước khi code database.

```mermaid
erDiagram
    USER ||--|| ROLE : has
    USER ||--o| STUDENT : is
    USER ||--o| LECTURER : is
    USER ||--o| STAFF : is
    USER ||--o| HEAD_DEPARTMENT : is

    SUBJECT ||--|| SYLLABUS : has
    SUBJECT ||--o{ PROJECT : supports
    SYLLABUS ||--o{ PROJECT : guides

    CLASS ||--o{ CLASS_MEMBER : contains
    USER ||--o{ CLASS_MEMBER : joins
    LECTURER ||--o{ CLASS : teaches

    PROJECT ||--o{ PROJECT_MILESTONE : has
    PROJECT ||--o{ PROJECT_CLASS : assigned_to
    CLASS ||--o{ PROJECT_CLASS : receives

    CLASS ||--o{ TEAM : has
    TEAM ||--o{ TEAM_MEMBER : contains
    STUDENT ||--o{ TEAM_MEMBER : joins

    TEAM ||--o{ TASK : has
    TASK ||--o{ SUBTASK : contains
    TEAM ||--o{ CHECKPOINT : has

    CHECKPOINT ||--o{ CHECKPOINT_SUBMISSION : receives
    USER ||--o{ CHECKPOINT_SUBMISSION : submits

    TEAM ||--o{ EVALUATION : receives
    USER ||--o{ EVALUATION : gives

    TEAM ||--o{ RESOURCE : owns
    CLASS ||--o{ RESOURCE : owns

    USER ||--o{ NOTIFICATION : receives

    ROLE {
      int id PK
      string name
    }
    USER {
      uuid id PK
      int role_id FK
      string email
      string password_hash
      string full_name
      bool is_active
    }
    STUDENT {
      uuid user_id PK,FK
      string student_code
    }
    LECTURER {
      uuid user_id PK,FK
      string lecturer_code
    }
    STAFF {
      uuid user_id PK,FK
    }
    HEAD_DEPARTMENT {
      uuid user_id PK,FK
    }
    SUBJECT {
      uuid id PK
      string code
      string name
    }
    SYLLABUS {
      uuid id PK
      uuid subject_id FK
      string content
    }
    CLASS {
      uuid id PK
      uuid lecturer_id FK
      string code
      string name
    }
    CLASS_MEMBER {
      uuid class_id FK
      uuid student_id FK
    }
    PROJECT {
      uuid id PK
      uuid subject_id FK
      uuid syllabus_id FK
      uuid created_by FK
      string title
      string description
      string status
    }
    PROJECT_MILESTONE {
      uuid id PK
      uuid project_id FK
      string title
      string description
      date due_date
    }
    TEAM {
      uuid id PK
      uuid class_id FK
      string name
    }
    TEAM_MEMBER {
      uuid team_id FK
      uuid student_id FK
      bool is_leader
      decimal contribution_percent
    }
    TASK {
      uuid id PK
      uuid team_id FK
      string title
      string status
    }
    SUBTASK {
      uuid id PK
      uuid task_id FK
      string title
      string status
    }
    CHECKPOINT {
      uuid id PK
      uuid team_id FK
      string title
      string status
    }
    CHECKPOINT_SUBMISSION {
      uuid id PK
      uuid checkpoint_id FK
      uuid student_id FK
      string content
    }
    EVALUATION {
      uuid id PK
      uuid team_id FK
      uuid evaluator_id FK
      uuid target_user_id FK
      decimal score
      string feedback
    }
    RESOURCE {
      uuid id PK
      uuid class_id FK
      uuid team_id FK
      string name
      string url
    }
    NOTIFICATION {
      uuid id PK
      uuid user_id FK
      string type
      string content
      bool is_read
    }
```

## Các điểm cần review trước khi migration

- Có cần tách Department thành bảng riêng hay không.
- Syllabus có một hay nhiều version.
- Contribution percent được tính tự động hay Lecturer/Team Leader nhập.
- Resource thuộc Class/Team/Milestone/Checkpoint như thế nào.
- Meeting/Chat/Message/Whiteboard sẽ dùng storage riêng hay lưu metadata trong PostgreSQL.

Các điểm trên chưa được đặc tả chi tiết trong đề, nên nhóm phải thống nhất trước khi chốt schema.
