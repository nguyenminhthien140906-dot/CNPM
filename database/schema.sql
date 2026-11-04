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
  PRIMARY KEY (class_id, lecturer_id),
  CONSTRAINT fk_class_lecturers_class FOREIGN KEY (class_id) REFERENCES study_classes(class_id) ON DELETE CASCADE,
CONSTRAINT fk_class_lecturers_lecturer FOREIGN KEY (lecturer_id) REFERENCES lecturers(lecturer_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- 3. PBL projects and teams
CREATE TABLE IF NOT EXISTS projects (
  project_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  class_id BIGINT UNSIGNED NOT NULL,
  syllabus_id BIGINT UNSIGNED NULL,
  created_by BIGINT UNSIGNED NOT NULL,
  title VARCHAR(200) NOT NULL,
  description TEXT,
  goals TEXT,
  expected_outputs TEXT,
  requirements TEXT,
  start_date DATE,
  due_date DATE,
  min_group_size INT UNSIGNED NOT NULL DEFAULT 1,
  max_group_size INT UNSIGNED NOT NULL DEFAULT 5,
  status ENUM('DRAFT','PENDING','APPROVED','PUBLISHED','IN_PROGRESS','COMPLETED','CANCELLED') NOT NULL DEFAULT 'DRAFT',
  approved_by BIGINT UNSIGNED NULL,
  approved_at DATETIME NULL,
  created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  INDEX idx_projects_class_status (class_id, status),
  CONSTRAINT chk_project_group_size CHECK (max_group_size >= min_group_size AND min_group_size >= 1),
  CONSTRAINT fk_projects_class FOREIGN KEY (class_id) REFERENCES study_classes(class_id) ON DELETE CASCADE,
  CONSTRAINT fk_projects_syllabus FOREIGN KEY (syllabus_id) REFERENCES syllabi(syllabus_id) ON DELETE SET NULL,
  CONSTRAINT fk_projects_creator FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE RESTRICT,
  CONSTRAINT fk_projects_approver FOREIGN KEY (approved_by) REFERENCES users(user_id) ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS teams (
  team_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  project_id BIGINT UNSIGNED NOT NULL,
  team_name VARCHAR(100) NOT NULL,
  leader_student_id BIGINT UNSIGNED NULL,
  description TEXT,
  created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  UNIQUE KEY uq_team_name_per_project (project_id, team_name),
  CONSTRAINT fk_teams_project FOREIGN KEY (project_id) REFERENCES projects(project_id) ON DELETE CASCADE,
  CONSTRAINT fk_teams_leader FOREIGN KEY (leader_student_id) REFERENCES students(student_id) ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS team_members (
  team_id BIGINT UNSIGNED NOT NULL,
  student_id BIGINT UNSIGNED NOT NULL,
  joined_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (team_id, student_id),
  CONSTRAINT fk_team_members_team FOREIGN KEY (team_id) REFERENCES teams(team_id) ON DELETE CASCADE,
  CONSTRAINT fk_team_members_student FOREIGN KEY (student_id) REFERENCES students(student_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- 4. Progress, tasks and submissions
CREATE TABLE IF NOT EXISTS milestones (
  milestone_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  project_id BIGINT UNSIGNED NOT NULL,
  title VARCHAR(200) NOT NULL,
  description TEXT,
  due_date DATE,
  sort_order INT UNSIGNED NOT NULL DEFAULT 1,
  status ENUM('PENDING','IN_PROGRESS','COMPLETED','OVERDUE') NOT NULL DEFAULT 'PENDING',
created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  INDEX idx_milestones_project_order (project_id, sort_order),
  CONSTRAINT fk_milestones_project FOREIGN KEY (project_id) REFERENCES projects(project_id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS checkpoints (
  checkpoint_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  milestone_id BIGINT UNSIGNED NOT NULL,
  title VARCHAR(200) NOT NULL,
  description TEXT,
  due_date DATETIME,
  max_score DECIMAL(5,2) NOT NULL DEFAULT 100.00,
  status ENUM('PENDING','OPEN','CLOSED','COMPLETED') NOT NULL DEFAULT 'PENDING',
  created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT chk_checkpoint_max_score CHECK (max_score >= 0),
  CONSTRAINT fk_checkpoints_milestone FOREIGN KEY (milestone_id) REFERENCES milestones(milestone_id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tasks (
  task_id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  team_id BIGINT UNSIGNED NOT NULL,
  milestone_id BIGINT UNSIGNED NULL,
  title VARCHAR(200) NOT NULL,
  description TEXT,
  deadline DATETIME,
  status ENUM('TODO','IN_PROGRESS','IN_REVIEW','DONE','CANCELLED') NOT NULL DEFAULT 'TODO',
  priority ENUM('LOW','MEDIUM','HIGH','URGENT') NOT NULL DEFAULT 'MEDIUM',
  created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  INDEX idx_tasks_team_status (team_id, status),
  CONSTRAINT fk_tasks_team FOREIGN KEY (team_id) REFERENCES teams(team_id) ON DELETE CASCADE,
  CONSTRAINT fk_tasks_milestone FOREIGN KEY (milestone_id) REFERENCES milestones(milestone_id) ON DELETE SET NULL
) ENGINE=InnoDB;
