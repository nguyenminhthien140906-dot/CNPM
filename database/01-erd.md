-- =========================================================
-- CollabSphere - Database Schema
-- PostgreSQL
-- =========================================================

CREATE SCHEMA IF NOT EXISTS collabsphere;

SET search_path TO collabsphere, public;

-- =========================================================
-- 1. ROLES - Vai trò người dùng
-- =========================================================

CREATE TABLE IF NOT EXISTS roles (
    role_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    role_name VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

-- =========================================================
-- 2. USERS - Tài khoản người dùng
-- =========================================================

CREATE TABLE IF NOT EXISTS users (
    user_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    role_id UUID NOT NULL,
    full_name VARCHAR(150) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    phone VARCHAR(20),
    avatar_url TEXT,
    status VARCHAR(20) NOT NULL DEFAULT 'active',
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_users_roles
        FOREIGN KEY (role_id)
        REFERENCES roles(role_id),

    CONSTRAINT chk_users_status
        CHECK (status IN ('active', 'inactive', 'locked'))
);

-- =========================================================
-- 3. STUDENTS - Thông tin sinh viên
-- =========================================================

CREATE TABLE IF NOT EXISTS students (
    student_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL UNIQUE,
    student_code VARCHAR(30) NOT NULL UNIQUE,
    major VARCHAR(100),
    academic_year INT,

    CONSTRAINT fk_students_users
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE,

    CONSTRAINT chk_students_academic_year
        CHECK (academic_year IS NULL OR academic_year > 0)
);

-- =========================================================
-- 4. LECTURERS - Thông tin giảng viên
-- =========================================================

CREATE TABLE IF NOT EXISTS lecturers (
    lecturer_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL UNIQUE,
    lecturer_code VARCHAR(30) NOT NULL UNIQUE,
    department VARCHAR(150),
    academic_title VARCHAR(100),

    CONSTRAINT fk_lecturers_users
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
);

-- =========================================================
-- 5. STAFF - Nhân viên
-- =========================================================

CREATE TABLE IF NOT EXISTS staff (
    staff_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL UNIQUE,
    staff_code VARCHAR(30) NOT NULL UNIQUE,
    department VARCHAR(150),

    CONSTRAINT fk_staff_users
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
);

-- =========================================================
-- 6. HEAD_DEPARTMENTS - Trưởng bộ môn
-- =========================================================

CREATE TABLE IF NOT EXISTS head_departments (
    head_department_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    lecturer_id UUID NOT NULL UNIQUE,
    department_name VARCHAR(150) NOT NULL,

    CONSTRAINT fk_head_departments_lecturers
        FOREIGN KEY (lecturer_id)
        REFERENCES lecturers(lecturer_id)
        ON DELETE CASCADE
);

-- =========================================================
-- 7. SUBJECTS - Môn học
-- =========================================================

CREATE TABLE IF NOT EXISTS subjects (
    subject_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    subject_code VARCHAR(30) NOT NULL UNIQUE,
    subject_name VARCHAR(200) NOT NULL,
    description TEXT,
    credits INT NOT NULL DEFAULT 3,

    CONSTRAINT chk_subjects_credits
        CHECK (credits > 0)
);

-- =========================================================
-- 8. SYLLABUS - Đề cương môn học
-- =========================================================

CREATE TABLE IF NOT EXISTS syllabus (
    syllabus_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    subject_id UUID NOT NULL,
    lecturer_id UUID,
    title VARCHAR(200) NOT NULL,
    content TEXT,
    version INT NOT NULL DEFAULT 1,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_syllabus_subjects
        FOREIGN KEY (subject_id)
        REFERENCES subjects(subject_id),

    CONSTRAINT fk_syllabus_lecturers
        FOREIGN KEY (lecturer_id)
        REFERENCES lecturers(lecturer_id)
        ON DELETE SET NULL,

    CONSTRAINT chk_syllabus_version
        CHECK (version > 0)
);

-- =========================================================
-- 9. STUDY_CLASSES - Lớp học
-- =========================================================

CREATE TABLE IF NOT EXISTS study_classes (
    class_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    subject_id UUID NOT NULL,
    lecturer_id UUID,
    class_code VARCHAR(30) NOT NULL UNIQUE,
    class_name VARCHAR(150) NOT NULL,
    semester VARCHAR(30),
    academic_year VARCHAR(20),
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_study_classes_subjects
        FOREIGN KEY (subject_id)
        REFERENCES subjects(subject_id),

    CONSTRAINT fk_study_classes_lecturers
        FOREIGN KEY (lecturer_id)
        REFERENCES lecturers(lecturer_id)
        ON DELETE SET NULL
);

-- =========================================================
-- 10. CLASS_MEMBERS - Sinh viên trong lớp
-- =========================================================

CREATE TABLE IF NOT EXISTS class_members (
    class_id UUID NOT NULL,
    student_id UUID NOT NULL,
    joined_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (class_id, student_id),

    CONSTRAINT fk_class_members_classes
        FOREIGN KEY (class_id)
        REFERENCES study_classes(class_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_class_members_students
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE
);

-- =========================================================
-- 11. PROJECTS - Dự án
-- =========================================================

CREATE TABLE IF NOT EXISTS projects (
    project_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    project_code VARCHAR(30) UNIQUE,
    project_name VARCHAR(200) NOT NULL,
    description TEXT,
    created_by UUID NOT NULL,
    start_date DATE,
    end_date DATE,
    status VARCHAR(30) NOT NULL DEFAULT 'planning',
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_projects_users
        FOREIGN KEY (created_by)
        REFERENCES users(user_id),

    CONSTRAINT chk_projects_dates
        CHECK (
            end_date IS NULL
            OR start_date IS NULL
            OR end_date >= start_date
        ),

    CONSTRAINT chk_projects_status
        CHECK (
            status IN (
                'planning',
                'in_progress',
                'completed',
                'cancelled'
            )
        )
);

-- =========================================================
-- 12. PROJECT_CLASSES - Dự án thuộc lớp học
-- =========================================================

CREATE TABLE IF NOT EXISTS project_classes (
    project_id UUID NOT NULL,
    class_id UUID NOT NULL,

    PRIMARY KEY (project_id, class_id),

    CONSTRAINT fk_project_classes_projects
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_project_classes_classes
        FOREIGN KEY (class_id)
        REFERENCES study_classes(class_id)
        ON DELETE CASCADE
);

-- =========================================================
-- 13. PROJECT_MILESTONES - Các cột mốc dự án
-- =========================================================

CREATE TABLE IF NOT EXISTS project_milestones (
    milestone_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    project_id UUID NOT NULL,
    milestone_name VARCHAR(200) NOT NULL,
    description TEXT,
    due_date DATE,
    status VARCHAR(30) NOT NULL DEFAULT 'pending',

    CONSTRAINT fk_milestones_projects
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE,

    CONSTRAINT chk_milestones_status
        CHECK (
            status IN ('pending', 'in_progress', 'completed')
        )
);

-- =========================================================
-- 14. TEAMS - Nhóm dự án
-- =========================================================

CREATE TABLE IF NOT EXISTS teams (
    team_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    project_id UUID NOT NULL,
    team_name VARCHAR(150) NOT NULL,
    description TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_teams_projects
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE,

    CONSTRAINT uq_teams_project_name
        UNIQUE (project_id, team_name)
);

-- =========================================================
-- 15. TEAM_MEMBERS - Thành viên nhóm
-- =========================================================

CREATE TABLE IF NOT EXISTS team_members (
    team_id UUID NOT NULL,
    student_id UUID NOT NULL,
    joined_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    team_role VARCHAR(30) NOT NULL DEFAULT 'member',

    PRIMARY KEY (team_id, student_id),

    CONSTRAINT fk_team_members_teams
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_team_members_students
        FOREIGN KEY (student_id)
        REFERENCES students(student_id)
        ON DELETE CASCADE,

    CONSTRAINT chk_team_members_role
        CHECK (team_role IN ('leader', 'member'))
);

-- =========================================================
-- 16. TASKS - Công việc
-- =========================================================

CREATE TABLE IF NOT EXISTS tasks (
    task_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    team_id UUID NOT NULL,
    milestone_id UUID,
    assigned_to UUID,
    task_name VARCHAR(200) NOT NULL,
    description TEXT,
    priority VARCHAR(20) NOT NULL DEFAULT 'medium',
    status VARCHAR(30) NOT NULL DEFAULT 'todo',
    due_date TIMESTAMPTZ,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_tasks_teams
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_tasks_milestones
        FOREIGN KEY (milestone_id)
        REFERENCES project_milestones(milestone_id)
        ON DELETE SET NULL,

    CONSTRAINT fk_tasks_students
        FOREIGN KEY (assigned_to)
        REFERENCES students(student_id)
        ON DELETE SET NULL,

    CONSTRAINT chk_tasks_priority
        CHECK (priority IN ('low', 'medium', 'high')),

    CONSTRAINT chk_tasks_status
        CHECK (
            status IN (
                'todo',
                'in_progress',
                'review',
                'completed',
                'cancelled'
            )
        )
);

-- =========================================================
-- 17. SUBTASKS - Công việc con
-- =========================================================

CREATE TABLE IF NOT EXISTS subtasks (
    subtask_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    task_id UUID NOT NULL,
    subtask_name VARCHAR(200) NOT NULL,
    description TEXT,
    status VARCHAR(30) NOT NULL DEFAULT 'todo',
    assigned_to UUID,
    due_date TIMESTAMPTZ,

    CONSTRAINT fk_subtasks_tasks
        FOREIGN KEY (task_id)
        REFERENCES tasks(task_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_subtasks_students
        FOREIGN KEY (assigned_to)
        REFERENCES students(student_id)
        ON DELETE SET NULL,

    CONSTRAINT chk_subtasks_status
        CHECK (status IN ('todo', 'in_progress', 'completed'))
);

-- =========================================================
-- 18. CHECKPOINTS - Các đợt kiểm tra tiến độ
-- =========================================================

CREATE TABLE IF NOT EXISTS checkpoints (
    checkpoint_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    project_id UUID NOT NULL,
    checkpoint_name VARCHAR(200) NOT NULL,
    description TEXT,
    due_date TIMESTAMPTZ,
    max_score NUMERIC(5,2) NOT NULL DEFAULT 10,

    CONSTRAINT fk_checkpoints_projects
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE,

    CONSTRAINT chk_checkpoints_max_score
        CHECK (max_score > 0)
);

-- =========================================================
-- 19. CHECKPOINT_SUBMISSIONS - Bài nộp checkpoint
-- =========================================================

CREATE TABLE IF NOT EXISTS checkpoint_submissions (
    submission_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    checkpoint_id UUID NOT NULL,
    team_id UUID NOT NULL,
    submitted_by UUID,
    submission_url TEXT,
    submission_note TEXT,
    submitted_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_submissions_checkpoints
        FOREIGN KEY (checkpoint_id)
        REFERENCES checkpoints(checkpoint_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_submissions_teams
        FOREIGN KEY (team_id)
        REFERENCES teams(team_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_submissions_students
        FOREIGN KEY (submitted_by)
        REFERENCES students(student_id)
        ON DELETE SET NULL,

    CONSTRAINT uq_checkpoint_team
        UNIQUE (checkpoint_id, team_id)
);

-- =========================================================
-- 20. EVALUATIONS - Đánh giá
-- =========================================================

CREATE TABLE IF NOT EXISTS evaluations (
    evaluation_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    submission_id UUID NOT NULL,
    evaluator_id UUID NOT NULL,
    score NUMERIC(5,2) NOT NULL,
    feedback TEXT,
    evaluated_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_evaluations_submissions
        FOREIGN KEY (submission_id)
        REFERENCES checkpoint_submissions(submission_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_evaluations_users
        FOREIGN KEY (evaluator_id)
        REFERENCES users(user_id),

    CONSTRAINT chk_evaluations_score
        CHECK (score >= 0)
);

-- =========================================================
-- 21. RESOURCES - Tài liệu học tập
-- =========================================================

CREATE TABLE IF NOT EXISTS resources (
    resource_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    project_id UUID,
    uploaded_by UUID NOT NULL,
    resource_name VARCHAR(200) NOT NULL,
    resource_url TEXT NOT NULL,
    description TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_resources_projects
        FOREIGN KEY (project_id)
        REFERENCES projects(project_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_resources_users
        FOREIGN KEY (uploaded_by)
        REFERENCES users(user_id)
);

-- =========================================================
-- 22. NOTIFICATIONS - Thông báo
-- =========================================================

CREATE TABLE IF NOT EXISTS notifications (
    notification_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL,
    title VARCHAR(200) NOT NULL,
    content TEXT NOT NULL,
    is_read BOOLEAN NOT NULL DEFAULT FALSE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_notifications_users
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON DELETE CASCADE
);

-- =========================================================
-- INDEXES - Tối ưu truy vấn khóa ngoại
-- =========================================================

CREATE INDEX IF NOT EXISTS idx_users_role_id
    ON users(role_id);

CREATE INDEX IF NOT EXISTS idx_classes_subject_id
    ON study_classes(subject_id);

CREATE INDEX IF NOT EXISTS idx_classes_lecturer_id
    ON study_classes(lecturer_id);

CREATE INDEX IF NOT EXISTS idx_projects_created_by
    ON projects(created_by);

CREATE INDEX IF NOT EXISTS idx_teams_project_id
    ON teams(project_id);

CREATE INDEX IF NOT EXISTS idx_tasks_team_id
    ON tasks(team_id);

CREATE INDEX IF NOT EXISTS idx_tasks_assigned_to
    ON tasks(assigned_to);

CREATE INDEX IF NOT EXISTS idx_notifications_user_id
    ON notifications(user_id);

CREATE INDEX IF NOT EXISTS idx_notifications_is_read
    ON notifications(is_read);

-- =========================================================
-- INITIAL DATA - Vai trò mặc định
-- =========================================================

INSERT INTO roles (role_name, description)
VALUES
    ('student', 'Sinh viên'),
    ('lecturer', 'Giảng viên'),
    ('staff', 'Nhân viên'),
    ('head_department', 'Trưởng bộ môn'),
    ('admin', 'Quản trị viên')
ON CONFLICT (role_name) DO NOTHING;
