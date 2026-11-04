erDiagram~~
-- =====================================================
-- COLLABSPHERE DATABASE
-- Database Management System: MySQL 8.0+
-- Project: Project-Based Learning (PBL)
-- =====================================================

CREATE DATABASE IF NOT EXISTS collabsphere
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE collabsphere;

SET NAMES utf8mb4;

-- =====================================================
-- 1. ROLES
-- =====================================================

CREATE TABLE IF NOT EXISTS roles (
    role_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    role_code VARCHAR(30) NOT NULL UNIQUE,
    role_name VARCHAR(100) NOT NULL,
    description VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- =====================================================
-- 2. USERS
-- =====================================================

CREATE TABLE IF NOT EXISTS users (
    user_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    role_id BIGINT UNSIGNED NOT NULL,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(150) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(150) NOT NULL,
    phone VARCHAR(20),
    avatar_url VARCHAR(500),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    last_login_at DATETIME,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_users_role
        FOREIGN KEY (role_id)
        REFERENCES roles(role_id)
) ENGINE=InnoDB;

-- =====================================================
-- 3. STUDENTS
-- =====================================================

CREATE TABLE IF NOT EXISTS students (
    student_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT UNSIGNED NOT NULL UNIQUE,
    student_code VARCHAR(30) NOT NULL UNIQUE,
    department VARCHAR(120),
    cohort VARCHAR(30),

    CONSTRAINT fk_students_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 4. LECTURERS
-- =====================================================

CREATE TABLE IF NOT EXISTS lecturers (
    lecturer_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT UNSIGNED NOT NULL UNIQUE,
    lecturer_code VARCHAR(30) NOT NULL UNIQUE,
    department VARCHAR(120),
    academic_title VARCHAR(80),

    CONSTRAINT fk_lecturers_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 5. SUBJECTS
-- =====================================================

CREATE TABLE IF NOT EXISTS subjects (
    subject_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    subject_code VARCHAR(30) NOT NULL UNIQUE,
    subject_name VARCHAR(180) NOT NULL,
    description TEXT,
    credit_count TINYINT UNSIGNED DEFAULT 0,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- =====================================================
-- 6. SYLLABI
-- =====================================================

CREATE TABLE IF NOT EXISTS syllabi (
    syllabus_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    subject_id BIGINT UNSIGNED NOT NULL,
    version VARCHAR(30) NOT NULL DEFAULT '1.0',
    objectives TEXT,
    content LONGTEXT,
    learning_outcomes TEXT,
    reference_materials TEXT,
    file_url VARCHAR(500),
    created_by BIGINT UNSIGNED,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    UNIQUE KEY uq_syllabus_subject_version
        (subject_id, version),

    CONSTRAINT fk_syllabi_subject
        FOREIGN KEY (subject_id)
        REFERENCES subjects(subject_id),

    CONSTRAINT fk_syllabi_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
        ON DELETE SET NULL
) ENGINE=InnoDB;

-- =====================================================
-- 7. STUDY_CLASSES
-- =====================================================

CREATE TABLE IF NOT EXISTS study_classes (
    class_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    subject_id BIGINT UNSIGNED NOT NULL,
    class_code VARCHAR(40) NOT NULL UNIQUE,
    class_name VARCHAR(150) NOT NULL,
    semester VARCHAR(30),
    academic_year VARCHAR(20),
    capacity SMALLINT UNSIGNED,
    schedule VARCHAR(255),
    room VARCHAR(80),

    status ENUM(
        'planned',
        'active',
        'completed',
        'cancelled'
    ) DEFAULT 'planned',

    created_by BIGINT UNSIGNED,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_study_classes_subject
        FOREIGN KEY (subject_id)
        REFERENCES subjects(subject_id),

    CONSTRAINT fk_study_classes_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
        ON DELETE SET NULL
) ENGINE=InnoDB;

-- =====================================================
-- 8. CLASS_STUDENTS
-- =====================================================

CREATE TABLE IF NOT EXISTS class_students (
    class_id BIGINT UNSIGNED NOT NULL,
    student_id BIGINT UNSIGNED NOT NULL,
    joined_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    status ENUM(
        'enrolled',
        'withdrawn',
        'completed'
    ) DEFAULT 'enrolled',

    PRIMARY KEY (class_id, student_id),

    CONSTRAINT fk_class_students_class
        FOREIGN KEY (class_id)
        REFERENCES study_classes(class_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_class_students_student
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 9. CLASS_LECTURERS
-- =====================================================

CREATE TABLE IF NOT EXISTS class_lecturers (
    class_id BIGINT UNSIGNED NOT NULL,
    lecturer_id BIGINT UNSIGNED NOT NULL,
    is_primary BOOLEAN DEFAULT FALSE,
    assigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (class_id, lecturer_id),

    CONSTRAINT fk_class_lecturers_class
        FOREIGN KEY (class_id)
        REFERENCES study_classes(class_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_class_lecturers_lecturer
        FOREIGN KEY (lecturer_id)
        REFERENCES lecturers(lecturer_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 10. PROJECTS
-- =====================================================

CREATE TABLE IF NOT EXISTS projects (
    project_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    class_id BIGINT UNSIGNED NOT NULL,
    created_by BIGINT UNSIGNED NOT NULL,
    title VARCHAR(200) NOT NULL,
    description TEXT NOT NULL,
    objectives TEXT NOT NULL,
    requirements TEXT,
    acceptance_criteria TEXT,
    start_date DATE NOT NULL,
    due_date DATE NOT NULL,
    min_group_size TINYINT UNSIGNED DEFAULT 1,
    max_group_size TINYINT UNSIGNED DEFAULT 6,

    status ENUM(
        'draft',
        'published',
        'in_progress',
        'completed',
        'archived'
    ) DEFAULT 'draft',

    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    INDEX idx_projects_class (class_id),

    CONSTRAINT chk_project_dates
        CHECK (due_date >= start_date),

    CONSTRAINT chk_project_group_size
        CHECK (max_group_size >= min_group_size),

    CONSTRAINT fk_projects_class
        FOREIGN KEY (class_id)
        REFERENCES study_classes(class_id),

    CONSTRAINT fk_projects_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
) ENGINE=InnoDB;

-- =====================================================
-- 11. TEAMS
-- =====================================================

CREATE TABLE IF NOT EXISTS teams (
    team_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT UNSIGNED NOT NULL,
    team_name VARCHAR(120) NOT NULL,
    team_code VARCHAR(30),
    leader_student_id BIGINT UNSIGNED,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    UNIQUE KEY uq_team_project_name
        (project_id, team_name),

    CONSTRAINT fk_teams_project
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_teams_leader
        FOREIGN KEY (leader_student_id)
        REFERENCES students(student_id)
        ON DELETE SET NULL
) ENGINE=InnoDB;

-- =====================================================
-- 12. TEAM_MEMBERS
-- =====================================================

CREATE TABLE IF NOT EXISTS team_members (
    team_id BIGINT UNSIGNED NOT NULL,
    student_id BIGINT UNSIGNED NOT NULL,

    member_role ENUM(
        'leader',
        'member'
    ) DEFAULT 'member',

    joined_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (team_id, student_id),

    CONSTRAINT fk_team_members_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_team_members_student
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 13. MILESTONES
-- =====================================================

CREATE TABLE IF NOT EXISTS milestones (
    milestone_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT UNSIGNED NOT NULL,
    title VARCHAR(180) NOT NULL,
    description TEXT,
    start_date DATE,
    due_date DATE NOT NULL,
    sequence_no SMALLINT UNSIGNED DEFAULT 1,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    UNIQUE KEY uq_milestone_sequence
        (project_id, sequence_no),

    CONSTRAINT fk_milestones_project
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 14. CHECKPOINTS
-- =====================================================

CREATE TABLE IF NOT EXISTS checkpoints (
    checkpoint_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    milestone_id BIGINT UNSIGNED NOT NULL,
    title VARCHAR(180) NOT NULL,
    instructions TEXT,
    due_date DATETIME,
    max_score DECIMAL(5,2) DEFAULT 10.00,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT chk_checkpoint_score
        CHECK (max_score >= 0),

    CONSTRAINT fk_checkpoints_milestone
        FOREIGN KEY (milestone_id)
        REFERENCES milestones(milestone_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 15. TASKS
-- =====================================================

CREATE TABLE IF NOT EXISTS tasks (
    task_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED NOT NULL,
    checkpoint_id BIGINT UNSIGNED,
    created_by BIGINT UNSIGNED,
    title VARCHAR(180) NOT NULL,
    description TEXT,

    status ENUM(
        'to_do',
        'in_progress',
        'review',
        'done'
    ) DEFAULT 'to_do',

    priority ENUM(
        'low',
        'medium',
        'high',
        'urgent'
    ) DEFAULT 'medium',

    start_date DATETIME,
    deadline DATETIME,
    completed_at DATETIME,

    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    INDEX idx_tasks_team_status (team_id, status),

    CONSTRAINT fk_tasks_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_tasks_checkpoint
        FOREIGN KEY (checkpoint_id)
        REFERENCES checkpoints(checkpoint_id)
        ON DELETE SET NULL,

    CONSTRAINT fk_tasks_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
        ON DELETE SET NULL
) ENGINE=InnoDB;

-- =====================================================
-- 16. TASK_ASSIGNEES
-- =====================================================

CREATE TABLE IF NOT EXISTS task_assignees (
    task_id BIGINT UNSIGNED NOT NULL,
    student_id BIGINT UNSIGNED NOT NULL,
    assigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (task_id, student_id),

    CONSTRAINT fk_task_assignees_task
        FOREIGN KEY (task_id)
        REFERENCES tasks(task_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_task_assignees_student
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 17. SUBMISSIONS
-- =====================================================

CREATE TABLE IF NOT EXISTS submissions (
    submission_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    checkpoint_id BIGINT UNSIGNED NOT NULL,
    team_id BIGINT UNSIGNED NOT NULL,
    submitted_by BIGINT UNSIGNED NOT NULL,
    content TEXT,
    file_url VARCHAR(500),
    submitted_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    status ENUM(
        'submitted',
        'late',
        'reviewed',
        'resubmission_required'
    ) DEFAULT 'submitted',

    feedback TEXT,
    score DECIMAL(5,2),

    CONSTRAINT chk_submission_score
        CHECK (score IS NULL OR score >= 0),

    CONSTRAINT fk_submissions_checkpoint
        FOREIGN KEY (checkpoint_id)
        REFERENCES checkpoints(checkpoint_id),

    CONSTRAINT fk_submissions_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id),

    CONSTRAINT fk_submissions_user
        FOREIGN KEY (submitted_by)
        REFERENCES users(user_id)
) ENGINE=InnoDB;

-- =====================================================
-- 18. RESOURCES
-- =====================================================

CREATE TABLE IF NOT EXISTS resources (
    resource_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED,
    project_id BIGINT UNSIGNED,
    uploaded_by BIGINT UNSIGNED NOT NULL,
    resource_name VARCHAR(180) NOT NULL,
    file_url VARCHAR(500) NOT NULL,
    file_type VARCHAR(80),
    file_size BIGINT UNSIGNED,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_resources_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_resources_project
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_resources_uploader
        FOREIGN KEY (uploaded_by)
        REFERENCES users(user_id)
) ENGINE=InnoDB;

-- =====================================================
-- 19. MEETINGS
-- =====================================================

CREATE TABLE IF NOT EXISTS meetings (
    meeting_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED NOT NULL,
    created_by BIGINT UNSIGNED NOT NULL,
    title VARCHAR(180) NOT NULL,
    meeting_url VARCHAR(500),
    start_at DATETIME NOT NULL,
    end_at DATETIME,

    status ENUM(
        'scheduled',
        'ongoing',
        'completed',
        'cancelled'
    ) DEFAULT 'scheduled',

    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT chk_meeting_dates
        CHECK (end_at IS NULL OR end_at >= start_at),

    CONSTRAINT fk_meetings_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_meetings_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
) ENGINE=InnoDB;

-- =====================================================
-- 20. MEETING_PARTICIPANTS
-- =====================================================

CREATE TABLE IF NOT EXISTS meeting_participants (
    meeting_id BIGINT UNSIGNED NOT NULL,
    user_id BIGINT UNSIGNED NOT NULL,
    joined_at DATETIME,
    left_at DATETIME,

    PRIMARY KEY (meeting_id, user_id),

    CONSTRAINT fk_meeting_participants_meeting
        FOREIGN KEY (meeting_id)
        REFERENCES meetings(meeting_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_meeting_participants_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 21. MESSAGES
-- =====================================================

CREATE TABLE IF NOT EXISTS messages (
    message_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED NOT NULL,
    sender_id BIGINT UNSIGNED NOT NULL,
    message_content TEXT NOT NULL,
    sent_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    edited_at DATETIME,
    is_deleted BOOLEAN DEFAULT FALSE,

    INDEX idx_messages_team_time (team_id, sent_at),

    CONSTRAINT fk_messages_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_messages_sender
        FOREIGN KEY (sender_id)
        REFERENCES users(user_id)
) ENGINE=InnoDB;

-- =====================================================
-- 22. WORKSPACES
-- =====================================================

CREATE TABLE IF NOT EXISTS workspaces (
    workspace_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED NOT NULL UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_workspaces_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 23. WHITEBOARD_SNAPSHOTS
-- =====================================================

CREATE TABLE IF NOT EXISTS whiteboard_snapshots (
    snapshot_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    workspace_id BIGINT UNSIGNED NOT NULL,
    created_by BIGINT UNSIGNED,
    snapshot_data JSON NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_whiteboard_workspace
        FOREIGN KEY (workspace_id)
        REFERENCES workspaces(workspace_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_whiteboard_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
        ON DELETE SET NULL
) ENGINE=InnoDB;

-- =====================================================
-- 24. WORKSPACE_DOCUMENTS
-- =====================================================

CREATE TABLE IF NOT EXISTS workspace_documents (
    document_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    workspace_id BIGINT UNSIGNED NOT NULL,
    created_by BIGINT UNSIGNED,
    title VARCHAR(180) NOT NULL,
    content LONGTEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_documents_workspace
        FOREIGN KEY (workspace_id)
        REFERENCES workspaces(workspace_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_documents_creator
        FOREIGN KEY (created_by)
        REFERENCES users(user_id)
        ON DELETE SET NULL
) ENGINE=InnoDB;

-- =====================================================
-- 25. DOCUMENT_VERSIONS
-- =====================================================

CREATE TABLE IF NOT EXISTS document_versions (
    version_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    document_id BIGINT UNSIGNED NOT NULL,
    edited_by BIGINT UNSIGNED,
    content LONGTEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_document_versions_document
        FOREIGN KEY (document_id)
        REFERENCES workspace_documents(document_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_document_versions_editor
        FOREIGN KEY (edited_by)
        REFERENCES users(user_id)
        ON DELETE SET NULL
) ENGINE=InnoDB;

-- =====================================================
-- 26. EVALUATIONS
-- =====================================================

CREATE TABLE IF NOT EXISTS evaluations (
    evaluation_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    evaluator_id BIGINT UNSIGNED NOT NULL,
    target_student_id BIGINT UNSIGNED,
    team_id BIGINT UNSIGNED,
    project_id BIGINT UNSIGNED,

    evaluation_type ENUM(
        'lecturer_to_team',
        'peer_review',
        'lecturer_to_student'
    ) NOT NULL,

    score DECIMAL(4,2) NOT NULL,
    comment TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT chk_evaluation_score
        CHECK (score BETWEEN 0 AND 10),

    CONSTRAINT fk_evaluations_evaluator
        FOREIGN KEY (evaluator_id)
        REFERENCES users(user_id),

    CONSTRAINT fk_evaluations_student
        FOREIGN KEY (target_student_id)
        REFERENCES students(student_id)
        ON DELETE SET NULL,

    CONSTRAINT fk_evaluations_team
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE SET NULL,

    CONSTRAINT fk_evaluations_project
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE SET NULL
) ENGINE=InnoDB;

-- =====================================================
-- 27. NOTIFICATIONS
-- =====================================================

CREATE TABLE IF NOT EXISTS notifications (
    notification_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT UNSIGNED NOT NULL,
    title VARCHAR(180) NOT NULL,
    content TEXT NOT NULL,
    notification_type VARCHAR(50) NOT NULL,
    related_type VARCHAR(50),
    related_id BIGINT UNSIGNED,
    is_read BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_notifications_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 28. AI_CONVERSATIONS
-- =====================================================

CREATE TABLE IF NOT EXISTS ai_conversations (
    conversation_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT UNSIGNED NOT NULL,
    title VARCHAR(180),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_ai_conversations_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- 29. AI_MESSAGES
-- =====================================================

CREATE TABLE IF NOT EXISTS ai_messages (
    ai_message_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    conversation_id BIGINT UNSIGNED NOT NULL,

    sender_type ENUM(
        'user',
        'assistant',
        'system'
    ) NOT NULL,

    content LONGTEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_ai_messages_conversation
        FOREIGN KEY (conversation_id)
        REFERENCES ai_conversations(conversation_id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

-- =====================================================
-- DEFAULT ROLES
-- =====================================================

INSERT IGNORE INTO roles
(role_code, role_name, description)
VALUES
('ADMIN', 'Admin', 'Quan tri he thong'),
('HEAD', 'Head Department', 'Truong bo mon va khoa'),
('STAFF', 'Staff', 'Nhan vien quan ly'),
('LECTURER', 'Lecturer', 'Giang vien'),
('STUDENT', 'Student', 'Sinh vien');

-- =====================================================
-- VERIFY DATABASE
-- =====================================================

SELECT 'CollabSphere database created successfully' AS message;

SELECT COUNT(*) AS total_tables
FROM information_schema.tables
WHERE table_schema = 'collabsphere';

SHOW TABLES;

-- =====================================================
-- END OF SCHEMA
-- =====================================================

