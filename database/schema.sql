-- =============================================================
-- COLLABSPHERE (COSRE) - MySQL 8.0+
-- Complete schema based on database/01-erd.md (29 tables); evaluations also supports submission assessment
-- Run this file in MySQL Workbench or phpMyAdmin.
-- This script does NOT drop existing tables or data.
-- =============================================================

CREATE DATABASE IF NOT EXISTS collabsphere
  CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE collabsphere;

-- 1. Roles and accounts
CREATE TABLE IF NOT EXISTS roles (
  role_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  role_code VARCHAR(30) NOT NULL UNIQUE,
  role_name VARCHAR(100) NOT NULL,
  description TEXT,
  created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS users (
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
  updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT fk_users_role FOREIGN KEY (role_id) REFERENCES roles(role_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS students (
  student_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  user_id BIGINT UNSIGNED NOT NULL UNIQUE,
  student_code VARCHAR(30) NOT NULL UNIQUE,
  CONSTRAINT fk_students_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS lecturers (
  lecturer_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  user_id BIGINT UNSIGNED NOT NULL UNIQUE,
  lecturer_code VARCHAR(30) NOT NULL UNIQUE,
  CONSTRAINT fk_lecturers_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- 2. Subjects, syllabi and classes
CREATE TABLE IF NOT EXISTS subjects (
  subject_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  subject_code VARCHAR(30) NOT NULL UNIQUE,
  subject_name VARCHAR(150) NOT NULL,
  description TEXT,
  created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS syllabi (
  syllabus_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  subject_id BIGINT UNSIGNED NOT NULL,
  version VARCHAR(30) NOT NULL,
  title VARCHAR(200) NOT NULL,
  content LONGTEXT,
  created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  UNIQUE KEY uq_syllabus_subject_version (subject_id, version),
  CONSTRAINT fk_syllabi_subject FOREIGN KEY (subject_id) REFERENCES subjects(subject_id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS study_classes (
  class_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  subject_id BIGINT UNSIGNED NOT NULL,
  class_code VARCHAR(30) NOT NULL UNIQUE,
  class_name VARCHAR(150) NOT NULL,
semester VARCHAR(30),
  academic_year VARCHAR(20),
  capacity INT UNSIGNED,
  status ENUM('PLANNED','OPEN','CLOSED','ARCHIVED') NOT NULL DEFAULT 'OPEN',
  created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT fk_classes_subject FOREIGN KEY (subject_id) REFERENCES subjects(subject_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS class_students (
  class_id BIGINT UNSIGNED NOT NULL,
  student_id BIGINT UNSIGNED NOT NULL,
  enrolled_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (class_id, student_id),
  CONSTRAINT fk_class_students_class FOREIGN KEY (class_id) REFERENCES study_classes(class_id) ON DELETE CASCADE,
  CONSTRAINT fk_class_students_student FOREIGN KEY (student_id) REFERENCES students(student_id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS class_lecturers (
  class_id BIGINT UNSIGNED NOT NULL,
  lecturer_id BIGINT UNSIGNED NOT NULL,
  assignment_role VARCHAR(50) NOT NULL DEFAULT 'LECTURER',
  assigned_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
