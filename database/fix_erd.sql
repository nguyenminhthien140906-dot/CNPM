SET search_path TO collabsphere, public;

-- Bổ sung bảng SUBTASK theo ERD
CREATE TABLE IF NOT EXISTS subtasks (
    subtask_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    task_id UUID NOT NULL,
    assigned_to UUID,
    subtask_name VARCHAR(200) NOT NULL,
    status VARCHAR(30) NOT NULL DEFAULT 'todo',

    CONSTRAINT fk_subtasks_tasks
        FOREIGN KEY (task_id)
        REFERENCES tasks(task_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_subtasks_students
        FOREIGN KEY (assigned_to)
        REFERENCES students(user_id)
        ON DELETE SET NULL
);

CREATE INDEX IF NOT EXISTS idx_subtasks_task_id
    ON subtasks(task_id);

CREATE INDEX IF NOT EXISTS idx_subtasks_assigned_to
    ON subtasks(assigned_to);
-- Bổ sung điểm tối đa cho checkpoint
ALTER TABLE checkpoints
ADD COLUMN IF NOT EXISTS max_score NUMERIC(5,2)
DEFAULT 100;

ALTER TABLE checkpoints
ADD CONSTRAINT chk_checkpoints_max_score
CHECK (max_score BETWEEN 0 AND 100);
-- Mỗi nhóm chỉ được có tối đa một trưởng nhóm
CREATE UNIQUE INDEX IF NOT EXISTS
    uq_team_members_one_leader
ON team_members(team_id)
WHERE is_leader = TRUE;
-- 1. Ty le dong gop cua thanh vien: 0-100
ALTER TABLE collabsphere.team_members
ADD CONSTRAINT chk_team_members_contribution
CHECK (contribution_percent BETWEEN 0 AND 100);

-- 2. Diem danh gia: 0-100
ALTER TABLE collabsphere.evaluations
ADD CONSTRAINT chk_evaluations_score
CHECK (score BETWEEN 0 AND 100);