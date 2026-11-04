DROP TABLE IF EXISTS document_versions;
DROP TRIGGER IF EXISTS trg_workspace_documents_updated_at ON workspace_documents;
DROP TABLE IF EXISTS workspace_documents;
DROP TABLE IF EXISTS whiteboard_snapshots;
DROP TRIGGER IF EXISTS trg_workspaces_updated_at ON workspaces;
DROP TABLE IF EXISTS workspaces;

