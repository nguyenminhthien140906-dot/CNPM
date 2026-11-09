
-- 005: Workspaces, whiteboards and shared documents

CREATE TABLE workspaces (
    workspace_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    team_id BIGINT NOT NULL UNIQUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT fk_workspaces_team FOREIGN KEY (team_id)
        REFERENCES teams(team_id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE whiteboard_snapshots (
    snapshot_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    workspace_id BIGINT NOT NULL,
    created_by BIGINT NOT NULL,
    snapshot_data JSON NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_whiteboard_workspace FOREIGN KEY (workspace_id)
        REFERENCES workspaces(workspace_id) ON DELETE CASCADE,
    CONSTRAINT fk_whiteboard_creator FOREIGN KEY (created_by)
        REFERENCES users(user_id) ON DELETE RESTRICT
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

    CONSTRAINT fk_workspace_documents_workspace FOREIGN KEY (workspace_id)
        REFERENCES workspaces(workspace_id) ON DELETE CASCADE,
    CONSTRAINT fk_workspace_documents_creator FOREIGN KEY (created_by)
        REFERENCES users(user_id) ON DELETE RESTRICT
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

    CONSTRAINT ck_document_versions_number CHECK (version_number > 0),
    CONSTRAINT uq_document_versions_number
        UNIQUE (document_id, version_number),
    CONSTRAINT fk_document_versions_document FOREIGN KEY (document_id)
        REFERENCES workspace_documents(document_id) ON DELETE CASCADE,
    CONSTRAINT fk_document_versions_editor FOREIGN KEY (edited_by)
        REFERENCES users(user_id) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX idx_document_versions_document
    ON document_versions(document_id);
