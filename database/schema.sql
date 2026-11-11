
-- ============================================================
-- COLLABSPHERE DATABASE
-- MySQL 8.0.16+
-- ============================================================

CREATE DATABASE IF NOT EXISTS collabsphere
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE collabsphere;

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 1;

-- ============================================================
-- 001. IDENTITY AND ACADEMIC
-- ============================================================

CREATE TABLE roles (
    role_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    role_code VARCHAR(30) NOT NULL UNIQUE,
    role_name VARCHAR(100) NOT NULL,
    description TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE users (
    user_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    role_id BIGINT NOT NULL,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(255) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(150) NOT NULL,
    avatar_url VARCHAR(500),
    phone VARCHAR(20),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_users_role
        FOREIGN KEY (role_id)
        REFERENCES roles(role_id)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE students (
    student_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT NOT NULL UNIQUE,
    student_code VARCHAR(30) NOT NULL UNIQUE,

    CONSTRAINT fk_students_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE lecturers (
    lecturer_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT NOT NULL UNIQUE,
    lecturer_code VARCHAR(30) NOT NULL UNIQUE,

    CONSTRAINT fk_lecturers_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE subjects (
    subject_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    subject_code VARCHAR(30) NOT NULL UNIQUE,
    subject_name VARCHAR(150) NOT NULL,
    description TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE syllabi (
    syllabus_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    subject_id BIGINT NOT NULL,
    version VARCHAR(30) NOT NULL,
    title VARCHAR(200) NOT NULL,
    content TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT uq_syllabus_subject_version
        UNIQUE (subject_id, version),

    CONSTRAINT fk_syllabi_subject
        FOREIGN KEY (subject_id)
        REFERENCES subjects(subject_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE study_classes (
    class_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    subject_id BIGINT NOT NULL,
    class_code VARCHAR(30) NOT NULL UNIQUE,
    class_name VARCHAR(150) NOT NULL,
    semester VARCHAR(30),
    academic_year VARCHAR(20),
    capacity INTEGER,
    status VARCHAR(20) NOT NULL DEFAULT 'OPEN',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT ck_study_classes_capacity
        CHECK (capacity IS NULL OR capacity >= 0),

    CONSTRAINT ck_study_classes_status
        CHECK (status IN ('PLANNED', 'OPEN', 'CLOSED', 'ARCHIVED')),

    CONSTRAINT fk_classes_subject
        FOREIGN KEY (subject_id)
        REFERENCES subjects(subject_id)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE class_students (
    class_id BIGINT NOT NULL,
    student_id BIGINT NOT NULL,
    enrolled_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (class_id, student_id),

    FOREIGN KEY (class_id)
        REFERENCES study_classes(class_id)
        ON DELETE CASCADE,

    FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE class_lecturers (
    class_id BIGINT NOT NULL,
    lecturer_id BIGINT NOT NULL,
    assignment_role VARCHAR(50) NOT NULL DEFAULT 'LECTURER',
    assigned_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (class_id, lecturer_id),

    FOREIGN KEY (class_id)
        REFERENCES study_classes(class_id)
        ON DELETE CASCADE,

    FOREIGN KEY (lecturer_id)
        REFERENCES lecturers(lecturer_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================
-- 002. PROJECTS AND TEAMS
-- ============================================================

CREATE TABLE projects (
    project_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    class_id BIGINT NOT NULL,
    syllabus_id BIGINT,
    created_by BIGINT NOT NULL,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    goals TEXT,
    expected_outputs TEXT,
    requirements TEXT,
    start_date DATE,
    due_date DATE,
    min_group_size INT NOT NULL DEFAULT 1,
    max_group_size INT NOT NULL DEFAULT 5,
    status VARCHAR(30) NOT NULL DEFAULT 'DRAFT',
    approved_by BIGINT,
    approved_at TIMESTAMP NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT ck_projects_dates
        CHECK (
            start_date IS NULL
            OR due_date IS NULL
            OR start_date <= due_date
        ),

    CONSTRAINT ck_projects_group_size
        CHECK (
            min_group_size >= 1
            AND max_group_size >= min_group_size
        ),

    CONSTRAINT fk_projects_class
        FOREIGN KEY (class_id)
        REFERENCES study_classes(class_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_projects_syllabus
        FOREIGN KEY (syllabus_id)
        REFERENCES syllabi(syllabus_id)
        ON DELETE SET NULL,

    CONSTRAINT fk_projects_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
        ON DELETE RESTRICT,

    CONSTRAINT fk_projects_approver
        FOREIGN KEY (approved_by)
        REFERENCES users(user_id)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_projects_class_id ON projects(class_id);
CREATE INDEX idx_projects_created_by ON projects(created_by);

CREATE TABLE teams (
    team_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT NOT NULL,
    team_name VARCHAR(100) NOT NULL,
    leader_student_id BIGINT,
    description TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT uq_teams_project_name
        UNIQUE (project_id, team_name),

    CONSTRAINT fk_teams_project
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_teams_leader
        FOREIGN KEY (leader_student_id)
        REFERENCES students(student_id)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_teams_project_id ON teams(project_id);

CREATE TABLE team_members (
    team_id BIGINT NOT NULL,
    student_id BIGINT NOT NULL,
    joined_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT pk_team_members
        PRIMARY KEY (team_id, student_id),

    CONSTRAINT fk_team_members_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_team_members_student
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================
-- 003. MILESTONES, CHECKPOINTS, TASKS AND SUBMISSIONS
-- ============================================================

CREATE TABLE milestones (
    milestone_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT NOT NULL,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    due_date DATE,
    sort_order INT NOT NULL DEFAULT 0,
    status VARCHAR(30) NOT NULL DEFAULT 'PENDING',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT ck_milestones_sort_order
        CHECK (sort_order >= 0),

    CONSTRAINT fk_milestones_project
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_milestones_project_id
ON milestones(project_id);

CREATE TABLE checkpoints (
    checkpoint_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    milestone_id BIGINT NOT NULL,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    due_date TIMESTAMP NULL,
    max_score DECIMAL(5,2) NOT NULL DEFAULT 100,
    status VARCHAR(30) NOT NULL DEFAULT 'PENDING',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT ck_checkpoints_max_score
        CHECK (max_score >= 0),

    CONSTRAINT fk_checkpoints_milestone
        FOREIGN KEY (milestone_id)
        REFERENCES milestones(milestone_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_checkpoints_milestone_id
ON checkpoints(milestone_id);

CREATE TABLE tasks (
    task_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT NOT NULL,
    milestone_id BIGINT,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    deadline TIMESTAMP NULL,
    status VARCHAR(30) NOT NULL DEFAULT 'TODO',
    priority VARCHAR(30) NOT NULL DEFAULT 'MEDIUM',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_tasks_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_tasks_milestone
        FOREIGN KEY (milestone_id)
        REFERENCES milestones(milestone_id)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_tasks_team_id ON tasks(team_id);
CREATE INDEX idx_tasks_milestone_id ON tasks(milestone_id);

CREATE TABLE task_assignees (
    task_id BIGINT NOT NULL,
    student_id BIGINT NOT NULL,
    assigned_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT pk_task_assignees
        PRIMARY KEY (task_id, student_id),

    CONSTRAINT fk_task_assignees_task
        FOREIGN KEY (task_id)
        REFERENCES tasks(task_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_task_assignees_student
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE submissions (
    submission_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    task_id BIGINT,
    checkpoint_id BIGINT,
    team_id BIGINT NOT NULL,
    submitted_by BIGINT NOT NULL,
    content TEXT,
    file_url VARCHAR(1000),
    score DECIMAL(5,2),
    feedback TEXT,
    submitted_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT ck_submissions_single_target
        CHECK (
            (task_id IS NOT NULL AND checkpoint_id IS NULL)
            OR
            (task_id IS NULL AND checkpoint_id IS NOT NULL)
        ),

    CONSTRAINT ck_submissions_score
        CHECK (score IS NULL OR score >= 0),

    CONSTRAINT fk_submissions_task
        FOREIGN KEY (task_id)
        REFERENCES tasks(task_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_submissions_checkpoint
        FOREIGN KEY (checkpoint_id)
        REFERENCES checkpoints(checkpoint_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_submissions_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_submissions_student
        FOREIGN KEY (submitted_by)
        REFERENCES students(student_id)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_submissions_team_id ON submissions(team_id);
CREATE INDEX idx_submissions_submitted_by ON submissions(submitted_by);

-- ============================================================
-- 004. RESOURCES AND COMMUNICATION
-- ============================================================

CREATE TABLE resources (
    resource_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT,
    team_id BIGINT,
    uploaded_by BIGINT NOT NULL,
    title VARCHAR(200) NOT NULL,
    resource_url VARCHAR(1000) NOT NULL,
    resource_type VARCHAR(50),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT ck_resources_single_owner
        CHECK (
            (project_id IS NOT NULL AND team_id IS NULL)
            OR
            (project_id IS NULL AND team_id IS NOT NULL)
        ),

    CONSTRAINT fk_resources_project
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_resources_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_resources_uploader
        FOREIGN KEY (uploaded_by)
        REFERENCES users(user_id)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_resources_project_id ON resources(project_id);
CREATE INDEX idx_resources_team_id ON resources(team_id);

CREATE TABLE meetings (
    meeting_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT NOT NULL,
    created_by BIGINT NOT NULL,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    meeting_url VARCHAR(500),
    starts_at TIMESTAMP NOT NULL,
    ends_at TIMESTAMP NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT ck_meetings_time
        CHECK (ends_at IS NULL OR ends_at > starts_at),

    CONSTRAINT fk_meetings_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_meetings_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_meetings_team_id ON meetings(team_id);
CREATE INDEX idx_meetings_starts_at ON meetings(starts_at);

CREATE TABLE meeting_participants (
    meeting_id BIGINT NOT NULL,
    user_id BIGINT NOT NULL,
    joined_at TIMESTAMP NULL,
    attendance_status VARCHAR(30) NOT NULL DEFAULT 'INVITED',

    CONSTRAINT pk_meeting_participants
        PRIMARY KEY (meeting_id, user_id),

    CONSTRAINT fk_meeting_participants_meeting
        FOREIGN KEY (meeting_id)
        REFERENCES meetings(meeting_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_meeting_participants_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE messages (
    message_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT NOT NULL,
    sender_id BIGINT NOT NULL,
    content TEXT NOT NULL,
    message_type VARCHAR(30) NOT NULL DEFAULT 'TEXT',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_messages_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_messages_sender
        FOREIGN KEY (sender_id)
        REFERENCES users(user_id)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_messages_team_created
ON messages(team_id, created_at);

-- ============================================================
-- 005. WORKSPACES AND DOCUMENTS
-- ============================================================

CREATE TABLE workspaces (
    workspace_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT NOT NULL UNIQUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_workspaces_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE whiteboard_snapshots (
    snapshot_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    workspace_id BIGINT NOT NULL,
    created_by BIGINT NOT NULL,
    snapshot_data JSON NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_whiteboard_workspace
        FOREIGN KEY (workspace_id)
        REFERENCES workspaces(workspace_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_whiteboard_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_whiteboard_workspace_created
ON whiteboard_snapshots(workspace_id, created_at);

CREATE TABLE workspace_documents (
    document_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    workspace_id BIGINT NOT NULL,
    created_by BIGINT NOT NULL,
    title VARCHAR(200) NOT NULL,
    content TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_workspace_documents_workspace
        FOREIGN KEY (workspace_id)
        REFERENCES workspaces(workspace_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_workspace_documents_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_workspace_documents_workspace
ON workspace_documents(workspace_id);

CREATE TABLE document_versions (
    version_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    document_id BIGINT NOT NULL,
    edited_by BIGINT NOT NULL,
    version_number INT NOT NULL,
    content TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT ck_document_versions_number
        CHECK (version_number > 0),

    CONSTRAINT uq_document_versions_number
        UNIQUE (document_id, version_number),

    CONSTRAINT fk_document_versions_document
        FOREIGN KEY (document_id)
        REFERENCES workspace_documents(document_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_document_versions_editor
        FOREIGN KEY (edited_by)
        REFERENCES users(user_id)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_document_versions_document
ON document_versions(document_id);

-- ============================================================
-- 006. EVALUATIONS, NOTIFICATIONS AND AI
-- ============================================================

CREATE TABLE evaluations (
    evaluation_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    evaluator_user_id BIGINT NOT NULL,
    submission_id BIGINT,
    project_id BIGINT,
    team_id BIGINT,
    student_id BIGINT,
    evaluation_type VARCHAR(30) NOT NULL,
    score DECIMAL(5,2) NOT NULL,
    comment TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT ck_evaluations_score
        CHECK (score >= 0),

    CONSTRAINT ck_evaluations_single_target
        CHECK (
            (submission_id IS NOT NULL)
            + (project_id IS NOT NULL)
            + (team_id IS NOT NULL)
            + (student_id IS NOT NULL) = 1
        ),

    CONSTRAINT fk_evaluations_evaluator
        FOREIGN KEY (evaluator_user_id)
        REFERENCES users(user_id)
        ON DELETE RESTRICT,

    CONSTRAINT fk_evaluations_submission
        FOREIGN KEY (submission_id)
        REFERENCES submissions(submission_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_evaluations_project
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_evaluations_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_evaluations_student
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_evaluations_evaluator
ON evaluations(evaluator_user_id);

CREATE INDEX idx_evaluations_submission
ON evaluations(submission_id);

CREATE INDEX idx_evaluations_project
ON evaluations(project_id);

CREATE INDEX idx_evaluations_team
ON evaluations(team_id);

CREATE INDEX idx_evaluations_student
ON evaluations(student_id);

CREATE TABLE notifications (
    notification_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT NOT NULL,
    title VARCHAR(200) NOT NULL,
    message TEXT NOT NULL,
    is_read BOOLEAN NOT NULL DEFAULT FALSE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_notifications_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_notifications_user_read_created
ON notifications(user_id, is_read, created_at DESC);

CREATE TABLE ai_conversations (
    conversation_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT NOT NULL,
    title VARCHAR(200),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_ai_conversations_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_ai_conversations_user_created
ON ai_conversations(user_id, created_at DESC);

CREATE TABLE ai_messages (
    ai_message_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    conversation_id BIGINT NOT NULL,
    sender_type VARCHAR(20) NOT NULL,
    content TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_ai_messages_conversation
        FOREIGN KEY (conversation_id)
        REFERENCES ai_conversations(conversation_id)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_ai_messages_conversation_created
ON ai_messages(conversation_id, created_at);

-- ============================================================
-- 007. DEFAULT ROLES
-- ============================================================

INSERT INTO roles (role_code, role_name, description)
VALUES
    ('ADMIN', 'Administrator', 'Quản trị viên hệ thống'),
    ('HEAD', 'Head Department', 'Trưởng bộ phận'),
    ('STAFF', 'Staff', 'Nhân viên quản lý'),
    ('LECTURER', 'Lecturer', 'Giảng viên'),
    ('STUDENT', 'Student', 'Sinh viên')
ON DUPLICATE KEY UPDATE
    role_code = VALUES(role_code);

-- ============================================================
-- END OF COLLABSPHERE DATABASE
-- ============================================================
