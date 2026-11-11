
-- 003: Milestones, checkpoints, tasks, assignees and submissions

CREATE TABLE milestones (
    milestone_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    project_id BIGINT NOT NULL,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    due_date DATE,
    sort_order INT NOT NULL DEFAULT 0,
    status VARCHAR(30) NOT NULL DEFAULT 'PENDING',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT ck_milestones_sort_order CHECK (sort_order >= 0),
    CONSTRAINT fk_milestones_project FOREIGN KEY (project_id)
        REFERENCES projects(project_id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_milestones_project_id ON milestones(project_id);

CREATE TABLE checkpoints (
    checkpoint_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    milestone_id BIGINT NOT NULL,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    due_date TIMESTAMP NULL,
    max_score DECIMAL(5,2) NOT NULL DEFAULT 100,
    status VARCHAR(30) NOT NULL DEFAULT 'PENDING',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT ck_checkpoints_max_score CHECK (max_score >= 0),
    CONSTRAINT fk_checkpoints_milestone FOREIGN KEY (milestone_id)
        REFERENCES milestones(milestone_id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_checkpoints_milestone_id ON checkpoints(milestone_id);

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

    CONSTRAINT fk_tasks_team FOREIGN KEY (team_id)
        REFERENCES teams(team_id) ON DELETE CASCADE,
    CONSTRAINT fk_tasks_milestone FOREIGN KEY (milestone_id)
        REFERENCES milestones(milestone_id) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_tasks_team_id ON tasks(team_id);
CREATE INDEX idx_tasks_milestone_id ON tasks(milestone_id);

CREATE TABLE task_assignees (
    task_id BIGINT NOT NULL,
    student_id BIGINT NOT NULL,
    assigned_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT pk_task_assignees PRIMARY KEY (task_id, student_id),
    CONSTRAINT fk_task_assignees_task FOREIGN KEY (task_id)
        REFERENCES tasks(task_id) ON DELETE CASCADE,
    CONSTRAINT fk_task_assignees_student FOREIGN KEY (student_id)
        REFERENCES students(student_id) ON DELETE CASCADE
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

    CONSTRAINT ck_submissions_single_target CHECK (
        (task_id IS NOT NULL AND checkpoint_id IS NULL)
        OR
        (task_id IS NULL AND checkpoint_id IS NOT NULL)
    ),
    CONSTRAINT ck_submissions_score CHECK (score IS NULL OR score >= 0),

    CONSTRAINT fk_submissions_task FOREIGN KEY (task_id)
        REFERENCES tasks(task_id) ON DELETE CASCADE,
    CONSTRAINT fk_submissions_checkpoint FOREIGN KEY (checkpoint_id)
        REFERENCES checkpoints(checkpoint_id) ON DELETE CASCADE,
    CONSTRAINT fk_submissions_team FOREIGN KEY (team_id)
        REFERENCES teams(team_id) ON DELETE CASCADE,
    CONSTRAINT fk_submissions_student FOREIGN KEY (submitted_by)
        REFERENCES students(student_id) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_submissions_team_id ON submissions(team_id);
CREATE INDEX idx_submissions_submitted_by ON submissions(submitted_by);
