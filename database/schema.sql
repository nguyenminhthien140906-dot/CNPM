-- ========================================================
-- COLLABSPHERE DATABASE SCHEMA
-- Database Management System: MySQL 8.0+
-- Project: Project-Based Learning (PBL) System
-- ========================================================

CREATE DATABASE IF NOT EXISTS collabsphere
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE collabsphere;

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- --------------------------------------------------------
-- 1. ROLES
-- --------------------------------------------------------
DROP TABLE IF EXISTS roles;
CREATE TABLE roles (
    role_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    role_code VARCHAR(30) NOT NULL UNIQUE,
    role_name VARCHAR(100) NOT NULL,
    description VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- --------------------------------------------------------
-- 2. USERS
-- --------------------------------------------------------
DROP TABLE IF EXISTS users;
CREATE TABLE users (
    user_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    role_id BIGINT UNSIGNED NOT NULL,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    avatar_url VARCHAR(255),
    phone VARCHAR(20),
    status ENUM('ACTIVE', 'INACTIVE', 'BLOCKED') DEFAULT 'ACTIVE',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_users_roles FOREIGN KEY (role_id) REFERENCES roles(role_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- --------------------------------------------------------
-- 3. SUBJECTS (Môn học / Học phần)
-- --------------------------------------------------------
DROP TABLE IF EXISTS subjects;
CREATE TABLE subjects (
    subject_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    subject_code VARCHAR(20) NOT NULL UNIQUE,
    subject_name VARCHAR(150) NOT NULL,
    description TEXT,
    credits INT NOT NULL DEFAULT 3,
    status ENUM('ACTIVE', 'INACTIVE') DEFAULT 'ACTIVE',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- --------------------------------------------------------
-- 4. CLASSES (Lớp học phần)
-- --------------------------------------------------------
DROP TABLE IF EXISTS classes;
CREATE TABLE classes (
    class_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    subject_id BIGINT UNSIGNED NOT NULL,
    lecturer_id BIGINT UNSIGNED NOT NULL,
    class_code VARCHAR(30) NOT NULL UNIQUE,
    semester VARCHAR(20) NOT NULL,
    academic_year VARCHAR(20) NOT NULL,
    status ENUM('OPEN', 'CLOSED', 'ARCHIVED') DEFAULT 'OPEN',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_classes_subjects FOREIGN KEY (subject_id) REFERENCES subjects(subject_id) ON DELETE CASCADE,
    CONSTRAINT fk_classes_lecturer FOREIGN KEY (lecturer_id) REFERENCES users(user_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- --------------------------------------------------------
-- 5. CLASS_MEMBERS (Sinh viên trong lớp)
-- --------------------------------------------------------
DROP TABLE IF EXISTS class_members;
CREATE TABLE class_members (
    class_member_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    class_id BIGINT UNSIGNED NOT NULL,
    student_id BIGINT UNSIGNED NOT NULL,
    joined_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE KEY unique_class_student (class_id, student_id),
    CONSTRAINT fk_cm_classes FOREIGN KEY (class_id) REFERENCES classes(class_id) ON DELETE CASCADE,
    CONSTRAINT fk_cm_users FOREIGN KEY (student_id) REFERENCES users(user_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- --------------------------------------------------------
-- 6. PROJECTS (Đề tài / Dự án)
-- --------------------------------------------------------
DROP TABLE IF EXISTS projects;
CREATE TABLE projects (
    project_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    class_id BIGINT UNSIGNED NOT NULL,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    max_team_size INT DEFAULT 5,
    start_date DATE,
    end_date DATE,
    status ENUM('DRAFT', 'PUBLISHED', 'COMPLETED') DEFAULT 'DRAFT',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_projects_classes FOREIGN KEY (class_id) REFERENCES classes(class_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- --------------------------------------------------------
-- 7. TEAMS (Nhóm làm bài)
-- --------------------------------------------------------
DROP TABLE IF EXISTS teams;
CREATE TABLE teams (
    team_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT UNSIGNED NOT NULL,
    team_name VARCHAR(100) NOT NULL,
    leader_id BIGINT UNSIGNED,
    status ENUM('FORMING', 'APPROVED', 'LOCKED') DEFAULT 'FORMING',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_teams_projects FOREIGN KEY (project_id) REFERENCES projects(project_id) ON DELETE CASCADE,
    CONSTRAINT fk_teams_leader FOREIGN KEY (leader_id) REFERENCES users(user_id) ON DELETE SET NULL
) ENGINE=InnoDB;

-- --------------------------------------------------------
-- 8. TEAM_MEMBERS (Thành viên nhóm)
-- --------------------------------------------------------
DROP TABLE IF EXISTS team_members;
CREATE TABLE team_members (
    team_member_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED NOT NULL,
    student_id BIGINT UNSIGNED NOT NULL,
    role_in_team VARCHAR(50) DEFAULT 'Member',
    joined_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE KEY unique_team_student (team_id, student_id),
    CONSTRAINT fk_tm_teams FOREIGN KEY (team_id) REFERENCES teams(team_id) ON DELETE CASCADE,
    CONSTRAINT fk_tm_users FOREIGN KEY (student_id) REFERENCES users(user_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- --------------------------------------------------------
-- 9. MILESTONES & CHECKPOINTS (Cột mốc & Điểm kiểm tra)
-- --------------------------------------------------------
DROP TABLE IF EXISTS milestones;
CREATE TABLE milestones (
    milestone_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT UNSIGNED NOT NULL,
    title VARCHAR(150) NOT NULL,
    description TEXT,
    due_date DATETIME NOT NULL,
    weight_percent DECIMAL(5,2) DEFAULT 0.00,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_milestones_projects FOREIGN KEY (project_id) REFERENCES projects(project_id) ON DELETE CASCADE
) ENGINE=InnoDB;

DROP TABLE IF EXISTS checkpoints;
CREATE TABLE checkpoints (
    checkpoint_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    milestone_id BIGINT UNSIGNED NOT NULL,
    title VARCHAR(150) NOT NULL,
    instructions TEXT,
    due_date DATETIME NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_checkpoints_milestones FOREIGN KEY (milestone_id) REFERENCES milestones(milestone_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- --------------------------------------------------------
-- 10. TASKS (Nhiệm vụ của nhóm)
-- --------------------------------------------------------
DROP TABLE IF EXISTS tasks;
CREATE TABLE tasks (
    task_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED NOT NULL,
    milestone_id BIGINT UNSIGNED,
    assignee_id BIGINT UNSIGNED,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    priority ENUM('LOW', 'MEDIUM', 'HIGH', 'URGENT') DEFAULT 'MEDIUM',
    status ENUM('TODO', 'IN_PROGRESS', 'IN_REVIEW', 'DONE') DEFAULT 'TODO',
    due_date DATETIME,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_tasks_teams FOREIGN KEY (team_id) REFERENCES teams(team_id) ON DELETE CASCADE,
    CONSTRAINT fk_tasks_milestones FOREIGN KEY (milestone_id) REFERENCES milestones(milestone_id) ON DELETE SET NULL,
    CONSTRAINT fk_tasks_assignee FOREIGN KEY (assignee_id) REFERENCES users(user_id) ON DELETE SET NULL
) ENGINE=InnoDB;

-- --------------------------------------------------------
-- 11. SUBMISSIONS & ASSESSMENTS (Nộp bài & Đánh giá)
-- --------------------------------------------------------
DROP TABLE IF EXISTS submissions;
CREATE TABLE submissions (
    submission_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    checkpoint_id BIGINT UNSIGNED NOT NULL,
    team_id BIGINT UNSIGNED NOT NULL,
    submitted_by BIGINT UNSIGNED NOT NULL,
    file_url VARCHAR(255),
    notes TEXT,
    submitted_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_sub_checkpoints FOREIGN KEY (checkpoint_id) REFERENCES checkpoints(checkpoint_id) ON DELETE CASCADE,
    CONSTRAINT fk_sub_teams FOREIGN KEY (team_id) REFERENCES teams(team_id) ON DELETE CASCADE,
    CONSTRAINT fk_sub_users FOREIGN KEY (submitted_by) REFERENCES users(user_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

DROP TABLE IF EXISTS assessments;
CREATE TABLE assessments (
    assessment_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    submission_id BIGINT UNSIGNED NOT NULL,
    evaluator_id BIGINT UNSIGNED NOT NULL,
    score DECIMAL(4,2),
    feedback TEXT,
    assessed_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_assess_submissions FOREIGN KEY (submission_id) REFERENCES submissions(submission_id) ON DELETE CASCADE,
    CONSTRAINT fk_assess_evaluator FOREIGN KEY (evaluator_id) REFERENCES users(user_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- --------------------------------------------------------
-- 12. MEETINGS & NOTIFICATIONS
-- --------------------------------------------------------
DROP TABLE IF EXISTS meetings;
CREATE TABLE meetings (
    meeting_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT UNSIGNED NOT NULL,
    created_by BIGINT UNSIGNED NOT NULL,
    title VARCHAR(150) NOT NULL,
    meeting_link VARCHAR(255),
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_meetings_teams FOREIGN KEY (team_id) REFERENCES teams(team_id) ON DELETE CASCADE,
    CONSTRAINT fk_meetings_users FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

DROP TABLE IF EXISTS notifications;
CREATE TABLE notifications (
    notification_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    user_id BIGINT UNSIGNED NOT NULL,
    title VARCHAR(150) NOT NULL,
    message TEXT NOT NULL,
    is_read BOOLEAN DEFAULT FALSE,
    type VARCHAR(50) DEFAULT 'GENERAL',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_notif_users FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
) ENGINE=InnoDB;

SET FOREIGN_KEY_CHECKS = 1;

-- ========================================================
-- SEED DATA (Dữ liệu mẫu khởi tạo)
-- ========================================================

INSERT INTO roles (role_code, role_name, description) VALUES
('ADMIN', 'Administrator', 'Quản trị viên hệ thống'),
('LECTURER', 'Lecturer', 'Giảng viên hướng dẫn'),
('STUDENT', 'Student', 'Sinh viên');

INSERT INTO users (role_id, username, email, password_hash, full_name, status) VALUES
(1, 'admin', 'admin@collabsphere.edu.vn', '$2a$12$eImiTXuWVxfM37uY4JANjO5E.y4mC7aQYx2eO', 'System Admin', 'ACTIVE'),
(2, 'lecturer_nam', 'nam.nguyen@collabsphere.edu.vn', '$2a$12$eImiTXuWVxfM37uY4JANjO5E.y4mC7aQYx2eO', 'Nguyễn Văn Nam', 'ACTIVE'),
(3, 'student_thien', 'thien.nm@collabsphere.edu.vn', '$2a$12$eImiTXuWVxfM37uY4JANjO5E.y4mC7aQYx2eO', 'Nguyễn Minh Thiên', 'ACTIVE');
