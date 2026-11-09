
from sqlalchemy import String, ForeignKey, UniqueConstraint
from sqlalchemy.dialects.mysql import BIGINT, INTEGER, LONGTEXT, JSON, TIMESTAMP
from sqlalchemy.orm import relationship
from sqlalchemy.sql import func

from app.models.base import db


class Workspace(db.Model):
    __tablename__ = "workspaces"

    workspace_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    team_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("teams.team_id"),
        unique=True,
        nullable=False,
    )

    workspace_name = db.Column(String(200), nullable=False)

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    team = relationship("Team")
    snapshots = relationship(
        "WhiteboardSnapshot",
        back_populates="workspace",
        cascade="all, delete-orphan",
    )
    documents = relationship(
        "WorkspaceDocument",
        back_populates="workspace",
        cascade="all, delete-orphan",
    )


class WhiteboardSnapshot(db.Model):
    __tablename__ = "whiteboard_snapshots"

    snapshot_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    workspace_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("workspaces.workspace_id"),
        nullable=False,
    )

    snapshot_name = db.Column(String(200))
    snapshot_data = db.Column(JSON, nullable=False)

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    workspace = relationship(
        "Workspace",
        back_populates="snapshots",
    )


class WorkspaceDocument(db.Model):
    __tablename__ = "workspace_documents"

    document_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    workspace_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("workspaces.workspace_id"),
        nullable=False,
    )

    document_name = db.Column(String(200), nullable=False)
    document_type = db.Column(String(50))

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    updated_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
        onupdate=func.current_timestamp(),
    )

    workspace = relationship(
        "Workspace",
        back_populates="documents",
    )

    versions = relationship(
        "DocumentVersion",
        back_populates="document",
        cascade="all, delete-orphan",
    )


class DocumentVersion(db.Model):
    __tablename__ = "document_versions"

    version_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    document_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("workspace_documents.document_id"),
        nullable=False,
    )

    edited_by = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id"),
        nullable=False,
    )

    version_number = db.Column(
        INTEGER(unsigned=True),
        nullable=False,
    )

    file_url = db.Column(String(1000))
    content = db.Column(LONGTEXT)

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    __table_args__ = (
        UniqueConstraint(
            "document_id",
            "version_number",
            name="uq_document_version",
        ),
    )

    document = relationship(
        "WorkspaceDocument",
        back_populates="versions",
    )

    editor = relationship("User")
