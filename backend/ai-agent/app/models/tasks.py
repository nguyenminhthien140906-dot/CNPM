
from sqlalchemy import (
    ForeignKey,
    String,
    Text,
    Date,
    func,
)
from sqlalchemy.dialects.mysql import BIGINT, ENUM, TIMESTAMP
from sqlalchemy.orm import relationship

from app.models.base import db


# =========================
# MILESTONE
# =========================
class Milestone(db.Model):
    __tablename__ = "milestones"

    milestone_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    project_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("projects.project_id"),
        nullable=False,
    )

    milestone_name = db.Column(String(200), nullable=False)
    description = db.Column(Text)
    due_date = db.Column(Date)

    status = db.Column(
        ENUM(
            "PENDING",
            "IN_PROGRESS",
            "COMPLETED",
            "OVERDUE",
        ),
        nullable=False,
        default="PENDING",
        server_default="PENDING",
    )

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    project = relationship(
        "Project",
        back_populates="milestones",
    )

    checkpoints = relationship(
        "Checkpoint",
        back_populates="milestone",
        cascade="all, delete-orphan",
    )

    tasks = relationship(
        "Task",
        back_populates="milestone",
    )


# =========================
# CHECKPOINT
# =========================
class Checkpoint(db.Model):
    __tablename__ = "checkpoints"

    checkpoint_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    milestone_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("milestones.milestone_id"),
        nullable=False,
    )

    checkpoint_name = db.Column(String(200), nullable=False)
    description = db.Column(Text)
    due_date = db.Column(Date)

    status = db.Column(
        ENUM(
            "PENDING",
            "IN_PROGRESS",
            "COMPLETED",
            "OVERDUE",
        ),
        nullable=False,
        default="PENDING",
        server_default="PENDING",
    )

    milestone = relationship(
        "Milestone",
        back_populates="checkpoints",
    )


# =========================
# TASK
# =========================
class Task(db.Model):
    __tablename__ = "tasks"

    task_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    team_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("teams.team_id"),
        nullable=False,
    )

    milestone_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("milestones.milestone_id"),
    )

    task_name = db.Column(String(200), nullable=False)
    description = db.Column(Text)
    start_date = db.Column(Date)
    due_date = db.Column(Date)

    priority = db.Column(
        ENUM(
            "LOW",
            "MEDIUM",
            "HIGH",
            "URGENT",
        ),
        nullable=False,
        default="MEDIUM",
        server_default="MEDIUM",
    )

    status = db.Column(
        ENUM(
            "TODO",
            "IN_PROGRESS",
            "REVIEW",
            "COMPLETED",
            "CANCELLED",
        ),
        nullable=False,
        default="TODO",
        server_default="TODO",
    )

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    team = relationship(
        "Team",
        back_populates="tasks",
    )

    milestone = relationship(
        "Milestone",
        back_populates="tasks",
    )

    assignees = relationship(
        "TaskAssignee",
        back_populates="task",
        cascade="all, delete-orphan",
    )


# =========================
# TASK ASSIGNEE
# =========================
class TaskAssignee(db.Model):
    __tablename__ = "task_assignees"

    task_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("tasks.task_id"),
        primary_key=True,
    )

    student_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("students.student_id"),
        primary_key=True,
    )

    assigned_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    task = relationship(
        "Task",
        back_populates="assignees",
    )

    student = relationship("Student")
