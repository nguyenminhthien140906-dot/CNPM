-- ============================================================
-- COLLABSPHERE - DATABASE SCHEMA
-- MySQL 8.0+
-- ============================================================

CREATE DATABASE IF NOT EXISTS collabsphere
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE collabsphere;

-- ============================================================
-- 1. ROLES - USERS - STUDENTS - LECTURERS
-- ============================================================

CREATE TABLE roles (
    role_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    role_code VARCHAR(30) NOT NULL UNIQUE,
    role_name VARCHAR(100) NOT NULL,
    description TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE users (
    user_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    role_id BIGINT UNSIGNED NOT NULL,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(255) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(150) NOT NULL,
    avatar_url VARCHAR(500),
    phone VARCHAR(20),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL
        DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_users_role
        FOREIGN KEY (role_id)
        REFERENCES roles(role_id)
        ON DELETE RESTRICT
);

CREATE TABLE students (
    student_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT UNSIGNED NOT NULL UNIQUE,
    student_code VARCHAR(30) NOT NULL UNIQUE,

    CONSTRAINT fk_students_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
);

CREATE TABLE lecturers (
    lecturer_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT UNSIGNED NOT NULL UNIQUE,
    lecturer_code VARCHAR(30) NOT NULL UNIQUE,

    CONSTRAINT fk_lecturers_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
);

-- ============================================================
-- 2. SUBJECTS - SYLLABI - STUDY CLASSES
-- ============================================================

CREATE TABLE subjects (
    subject_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    subject_code VARCHAR(30) NOT NULL UNIQUE,
    subject_name VARCHAR(150) NOT NULL,
    description TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE syllabi (
    syllabus_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    subject_id BIGINT UNSIGNED NOT NULL,
    version VARCHAR(30) NOT NULL,
    title VARCHAR(200) NOT NULL,
    content LONGTEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    UNIQUE KEY uq_syllabus_subject_version
        (subject_id, version),

    CONSTRAINT fk_syllabi_subject
        FOREIGN KEY (subject_id)
        REFERENCES subjects(subject_id)
        ON DELETE CASCADE
);

CREATE TABLE study_classes (
    class_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    subject_id BIGINT UNSIGNED NOT NULL,
    class_code VARCHAR(30) NOT NULL UNIQUE,
    class_name VARCHAR(150) NOT NULL,
    semester VARCHAR(30),
    academic_year VARCHAR(20),
    capacity INT UNSIGNED,
    status ENUM(
        'PLANNED',
        'OPEN',
        'CLOSED',
        'ARCHIVED'
    ) NOT NULL DEFAULT 'OPEN',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_classes_subject
        FOREIGN KEY (subject_id)
        REFERENCES subjects(subject_id)
        ON DELETE RESTRICT,

    CONSTRAINT chk_class_capacity
        CHECK (capacity IS NULL OR capacity > 0)
);

CREATE TABLE class_students (
    class_id BIGINT UNSIGNED NOT NULL,
    student_id BIGINT UNSIGNED NOT NULL,
    enrolled_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (class_id, student_id),

    CONSTRAINT fk_class_students_class
        FOREIGN KEY (class_id)
        REFERENCES study_classes(class_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_class_students_student
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE
);

CREATE TABLE class_lecturers (
    class_id BIGINT UNSIGNED NOT NULL,
    lecturer_id BIGINT UNSIGNED NOT NULL,
    assignment_role VARCHAR(50) NOT NULL DEFAULT 'LECTURER',
    assigned_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (class_id, lecturer_id),

    CONSTRAINT fk_class_lecturers_class
        FOREIGN KEY (class_id)
        REFERENCES study_classes(class_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_class_lecturers_lecturer
        FOREIGN KEY (lecturer_id)
        REFERENCES lecturers(lecturer_id)
        ON DELETE CASCADE
);

-- ============================================================
-- 3. PROJECTS - TEAMS - TEAM MEMBERS
-- ============================================================

CREATE TABLE projects (
    project_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    class_id BIGINT UNSIGNED NOT NULL,
    syllabus_id BIGINT UNSIGNED,
    created_by BIGINT UNSIGNED NOT NULL,
    approved_by BIGINT UNSIGNED,
    project_name VARCHAR(200) NOT NULL,
    description TEXT,
    objectives TEXT,
    expected_outputs TEXT,
    requirements TEXT,
    start_date DATE,
    deadline DATE,
    status ENUM(
        'DRAFT',
        'PENDING',
        'APPROVED',
        'IN_PROGRESS',
        'COMPLETED',
        'CANCELLED'
    ) NOT NULL DEFAULT 'DRAFT',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL
        DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_projects_class
        FOREIGN KEY (class_id)
        REFERENCES study_classes(class_id)
        ON DELETE RESTRICT,

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
        ON DELETE SET NULL,

    CONSTRAINT chk_project_dates
        CHECK (
            deadline IS NULL
            OR start_date IS NULL
            OR deadline >= start_date
        )
);

CREATE TABLE teams (
    team_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT UNSIGNED NOT NULL,
    leader_id BIGINT UNSIGNED,
    team_name VARCHAR(150) NOT NULL,
    description TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    UNIQUE KEY uq_team_project_name
        (project_id, team_name),

    CONSTRAINT fk_teams_project
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_teams_leader
        FOREIGN KEY (leader_id)
        REFERENCES students(student_id)
        ON DELETE SET NULL
);

CREATE TABLE team_members (
    team_id BIGINT UNSIGNED NOT NULL,
    student_id BIGINT UNSIGNED NOT NULL,
    joined_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    member_role ENUM(
        'LEADER',
        'MEMBER'
    ) NOT NULL DEFAULT 'MEMBER',

    PRIMARY KEY (team_id, student_id),

    CONSTRAINT fk_team_members_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_team_members_student
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE
);

-- ============================================================
-- 4. MILESTONES - CHECKPOINTS - TASKS
-- ============================================================

CREATE TABLE milestones (
    milestone_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT UNSIGNED NOT NULL,
    milestone_name VARCHAR(200) NOT NULL,
    description TEXT,
    due_date DATE,
    status ENUM(
        'PENDING',
        'IN_PROGRESS',
        'COMPLETED'
    ) NOT NULL DEFAULT 'PENDING',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_milestones_project
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE
);

CREATE TABLE checkpoints (
    checkpoint_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    milestone_id BIGINT UNSIGNED NOT NULL,
    checkpoint_name VARCHAR(200) NOT NULL,
    description TEXT,
    due_date DATE,
    status ENUM(
        'PENDING',
        'IN_PROGRESS',
        'COMPLETED'
    ) NOT NULL DEFAULT 'PENDING',

    CONSTRAINT fk_checkpoints_milestone
        FOREIGN KEY (milestone_id)
        REFERENCES milestones(milestone_id)
        ON DELETE CASCADE
);

CREATE TABLE tasks (
    task_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED NOT NULL,
    milestone_id BIGINT UNSIGNED,
    task_name VARCHAR(200) NOT NULL,
    description TEXT,
    priority ENUM(
        'LOW',
        'MEDIUM',
        'HIGH',
        'URGENT'
    ) NOT NULL DEFAULT 'MEDIUM',
    status ENUM(
        'TODO',
        'IN_PROGRESS',
        'REVIEW',
        'COMPLETED'
    ) NOT NULL DEFAULT 'TODO',
    start_date DATE,
    due_date DATE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_tasks_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_tasks_milestone
        FOREIGN KEY (milestone_id)
        REFERENCES milestones(milestone_id)
        ON DELETE SET NULL,

    CONSTRAINT chk_task_dates
        CHECK (
            due_date IS NULL
            OR start_date IS NULL
            OR due_date >= start_date
        )
);

CREATE TABLE task_assignees (
    task_id BIGINT UNSIGNED NOT NULL,
    student_id BIGINT UNSIGNED NOT NULL,
    assigned_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (task_id, student_id),

    CONSTRAINT fk_task_assignees_task
        FOREIGN KEY (task_id)
        REFERENCES tasks(task_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_task_assignees_student
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE
);

-- ============================================================
-- 5. SUBMISSIONS - RESOURCES
-- ============================================================

CREATE TABLE submissions (
    submission_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    task_id BIGINT UNSIGNED,
    checkpoint_id BIGINT UNSIGNED,
    student_id BIGINT UNSIGNED NOT NULL,
    submission_url VARCHAR(1000),
    content LONGTEXT,
    submitted_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    status ENUM(
        'SUBMITTED',
        'LATE',
        'RESUBMITTED'
    ) NOT NULL DEFAULT 'SUBMITTED',

    CONSTRAINT fk_submissions_task
        FOREIGN KEY (task_id)
        REFERENCES tasks(task_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_submissions_checkpoint
        FOREIGN KEY (checkpoint_id)
        REFERENCES checkpoints(checkpoint_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_submissions_student
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE,

    CONSTRAINT chk_submission_target
        CHECK (
            task_id IS NOT NULL
            OR checkpoint_id IS NOT NULL
        )
);

CREATE TABLE resources (
    resource_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT UNSIGNED,
    team_id BIGINT UNSIGNED,
    uploaded_by BIGINT UNSIGNED NOT NULL,
    resource_name VARCHAR(200) NOT NULL,
    resource_url VARCHAR(1000) NOT NULL,
    resource_type VARCHAR(50),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

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
        ON DELETE RESTRICT,

    CONSTRAINT chk_resource_owner
        CHECK (
            project_id IS NOT NULL
            OR team_id IS NOT NULL
        )
);

-- ============================================================
-- 6. MEETINGS - PARTICIPANTS - MESSAGES
-- ============================================================

CREATE TABLE meetings (
    meeting_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED NOT NULL,
    created_by BIGINT UNSIGNED NOT NULL,
    meeting_title VARCHAR(200) NOT NULL,
    description TEXT,
    meeting_url VARCHAR(1000),
    start_time DATETIME NOT NULL,
    end_time DATETIME,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_meetings_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_meetings_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
        ON DELETE RESTRICT,

    CONSTRAINT chk_meeting_time
        CHECK (
            end_time IS NULL
            OR end_time > start_time
        )
);

CREATE TABLE meeting_participants (
    meeting_id BIGINT UNSIGNED NOT NULL,
    user_id BIGINT UNSIGNED NOT NULL,
    joined_at TIMESTAMP NULL,
    attendance_status ENUM(
        'INVITED',
        'ACCEPTED',
        'DECLINED',
        'ATTENDED',
        'ABSENT'
    ) NOT NULL DEFAULT 'INVITED',

    PRIMARY KEY (meeting_id, user_id),

    CONSTRAINT fk_meeting_participants_meeting
        FOREIGN KEY (meeting_id)
        REFERENCES meetings(meeting_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_meeting_participants_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
);

CREATE TABLE messages (
    message_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED NOT NULL,
    sender_id BIGINT UNSIGNED NOT NULL,
    content TEXT NOT NULL,
    sent_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    edited_at TIMESTAMP NULL,

    CONSTRAINT fk_messages_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_messages_sender
        FOREIGN KEY (sender_id)
        REFERENCES users(user_id)
        ON DELETE RESTRICT
);

-- ============================================================
-- 7. WORKSPACES - WHITEBOARD - DOCUMENTS
-- ============================================================

CREATE TABLE workspaces (
    workspace_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED NOT NULL UNIQUE,
    workspace_name VARCHAR(200) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_workspaces_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE
);

CREATE TABLE whiteboard_snapshots (
    snapshot_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    workspace_id BIGINT UNSIGNED NOT NULL,
    snapshot_name VARCHAR(200),
    snapshot_data JSON NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_whiteboard_workspace
        FOREIGN KEY (workspace_id)
        REFERENCES workspaces(workspace_id)
        ON DELETE CASCADE
);

CREATE TABLE workspace_documents (
    document_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    workspace_id BIGINT UNSIGNED NOT NULL,
    document_name VARCHAR(200) NOT NULL,
    document_type VARCHAR(50),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL
        DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_workspace_documents_workspace
        FOREIGN KEY (workspace_id)
        REFERENCES workspaces(workspace_id)
        ON DELETE CASCADE
);

CREATE TABLE document_versions (
    version_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    document_id BIGINT UNSIGNED NOT NULL,
    edited_by BIGINT UNSIGNED NOT NULL,
    version_number INT UNSIGNED NOT NULL,
    file_url VARCHAR(1000),
    content LONGTEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    UNIQUE KEY uq_document_version
        (document_id, version_number),

    CONSTRAINT fk_document_versions_document
        FOREIGN KEY (document_id)
        REFERENCES workspace_documents(document_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_document_versions_editor
        FOREIGN KEY (edited_by)
        REFERENCES users(user_id)
        ON DELETE RESTRICT,

    CONSTRAINT chk_document_version
        CHECK (version_number > 0)
);

-- ============================================================
-- 8. EVALUATIONS - NOTIFICATIONS
-- ============================================================

CREATE TABLE evaluations (
    evaluation_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    evaluator_id BIGINT UNSIGNED NOT NULL,
    submission_id BIGINT UNSIGNED,
    project_id BIGINT UNSIGNED,
    team_id BIGINT UNSIGNED,
    student_id BIGINT UNSIGNED,
    score DECIMAL(5,2),
    comments TEXT,
    evaluated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_evaluations_evaluator
        FOREIGN KEY (evaluator_id)
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
        ON DELETE CASCADE,

    CONSTRAINT chk_evaluation_score
        CHECK (
            score IS NULL
            OR (score >= 0 AND score <= 100)
        ),

    CONSTRAINT chk_evaluation_target
        CHECK (
            (submission_id IS NOT NULL)
            + (project_id IS NOT NULL)
            + (team_id IS NOT NULL)
            + (student_id IS NOT NULL) = 1
        )
);

CREATE TABLE notifications (
    notification_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT UNSIGNED NOT NULL,
    title VARCHAR(200) NOT NULL,
    content TEXT,
    notification_type VARCHAR(50),
    is_read BOOLEAN NOT NULL DEFAULT FALSE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_notifications_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
);

-- ============================================================
-- 9. AI CONVERSATIONS - AI MESSAGES
-- ============================================================

CREATE TABLE ai_conversations (
    conversation_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT UNSIGNED NOT NULL,
    conversation_title VARCHAR(200),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_ai_conversations_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
);

CREATE TABLE ai_messages (
    ai_message_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    conversation_id BIGINT UNSIGNED NOT NULL,
    sender_type ENUM(
        'USER',
        'ASSISTANT',
        'SYSTEM'
    ) NOT NULL,
    content LONGTEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_ai_messages_conversation
        FOREIGN KEY (conversation_id)
        REFERENCES ai_conversations(conversation_id)
        ON DELETE CASCADE
);

-- ============================================================
-- END OF SCHEMA
-- ============================================================
