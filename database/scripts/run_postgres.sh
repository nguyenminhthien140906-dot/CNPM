# Sơ đồ CSDL CollabSphere (ERD)

```mermaid
erDiagram
    ROLES ||--o{ USERS : "has"
    SUBJECTS ||--o{ CLASSES : "contains"
    USERS ||--o{ CLASSES : "teaches"
    CLASSES ||--o{ PROJECTS : "has"
    PROJECTS ||--o{ TEAMS : "forms"
    USERS ||--o{ TEAMS : "leads"

    ROLES {
        bigint role_id PK
        string role_code
        string role_name
    }

    USERS {
        bigint user_id PK
        bigint role_id FK
        string username
        string email
        string full_name
    }

    CLASSES {
        bigint class_id PK
        bigint subject_id FK
        bigint lecturer_id FK
        string class_code
        string semester
    }

    PROJECTS {
        bigint project_id PK
        bigint class_id FK
        string title
        int max_team_size
    }

    TEAMS {
        bigint team_id PK
        bigint project_id FK
        bigint leader_id FK
        string team_name
    }
