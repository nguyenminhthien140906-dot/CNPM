
-- 006: Evaluations, notifications and AI conversations

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

    CONSTRAINT ck_evaluations_score CHECK (score >= 0),

    CONSTRAINT ck_evaluations_single_target CHECK (
        (submission_id IS NOT NULL)
        + (project_id IS NOT NULL)
        + (team_id IS NOT NULL)
        + (student_id IS NOT NULL) = 1
    ),

    CONSTRAINT fk_evaluations_evaluator FOREIGN KEY (evaluator_user_id)
        REFERENCES users(user_id) ON DELETE RESTRICT,
    CONSTRAINT fk_evaluations_submission FOREIGN KEY (submission_id)
        REFERENCES submissions(submission_id) ON DELETE CASCADE,
    CONSTRAINT fk_evaluations_project FOREIGN KEY (project_id)
        REFERENCES projects(project_id) ON DELETE CASCADE,
    CONSTRAINT fk_evaluations_team FOREIGN KEY (team_id)
        REFERENCES teams(team_id) ON DELETE CASCADE,
    CONSTRAINT fk_evaluations_student FOREIGN KEY (student_id)
        REFERENCES students(student_id) ON DELETE CASCADE
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

    CONSTRAINT fk_notifications_user FOREIGN KEY (user_id)
        REFERENCES users(user_id) ON DELETE CASCADE
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

    CONSTRAINT fk_ai_conversations_user FOREIGN KEY (user_id)
        REFERENCES users(user_id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_ai_conversations_user_created
    ON ai_conversations(user_id, created_at DESC);

CREATE TABLE ai_messages (
    ai_message_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    conversation_id BIGINT NOT NULL,
    sender_type VARCHAR(20) NOT NULL,
    content TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_ai_messages_conversation FOREIGN KEY (conversation_id)
        REFERENCES ai_conversations(conversation_id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_ai_messages_conversation_created
    ON ai_messages(conversation_id, created_at);
