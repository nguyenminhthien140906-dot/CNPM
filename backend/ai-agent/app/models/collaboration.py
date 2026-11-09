
from sqlalchemy import (
    String,
    Text,
    DateTime,
    ForeignKey,
    CheckConstraint,
)
from sqlalchemy.dialects.mysql import BIGINT, ENUM, LONGTEXT, TIMESTAMP
from sqlalchemy.sql import func

from app.models.base import db


# =========================
# SUBMISSION
# =========================
class Submission(db.Model):
    __tablename__ = "submissions"

    submission_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    task_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("tasks.task_id"),
        nullable=True,
    )

    checkpoint_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("checkpoints.checkpoint_id"),
        nullable=True,
    )

    student_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("students.student_id"),
        nullable=False,
    )

    submission_url = db.Column(String(1000))
    content = db.Column(LONGTEXT)

    submitted_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    status = db.Column(
        ENUM(
            "SUBMITTED",
            "LATE",
            "ACCEPTED",
            "REVISION_REQUIRED",
            "REJECTED",
        ),
        nullable=False,
        default="SUBMITTED",
        server_default="SUBMITTED",
    )

    __table_args__ = (
        CheckConstraint(
            """
            (task_id IS NOT NULL AND checkpoint_id IS NULL)
            OR
            (task_id IS NULL AND checkpoint_id IS NOT NULL)
            """,
            name="chk_submission_target",
        ),
    )

    task = db.relationship("Task")
    checkpoint = db.relationship("Checkpoint")
    student = db.relationship("Student")


# =========================
# RESOURCE
# =========================
class Resource(db.Model):
    __tablename__ = "resources"

    resource_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    project_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("projects.project_id"),
        nullable=True,
    )

    team_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("teams.team_id"),
        nullable=True,
    )

    uploaded_by = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id"),
        nullable=False,
    )

    resource_name = db.Column(String(200), nullable=False)
    resource_url = db.Column(String(1000), nullable=False)
    resource_type = db.Column(String(50))

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    __table_args__ = (
        CheckConstraint(
            """
            (project_id IS NOT NULL AND team_id IS NULL)
            OR
            (project_id IS NULL AND team_id IS NOT NULL)
            """,
            name="chk_resource_owner",
        ),
    )

    project = db.relationship("Project")
    team = db.relationship("Team")
    uploader = db.relationship("User")


# =========================
# MEETING
# =========================
class Meeting(db.Model):
    __tablename__ = "meetings"

    __table_args__ = (
        db.CheckConstraint(
            "end_time IS NULL OR end_time >= start_time",
            name="chk_meeting_time_range",
        ),
    )

    meeting_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    team_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("teams.team_id"),
        nullable=False,
    )

    created_by = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id"),
        nullable=False,
    )

    meeting_title = db.Column(String(200), nullable=False)
    description = db.Column(Text)
    meeting_url = db.Column(String(1000))

    # Keep DATETIME according to ERD
    start_time = db.Column(DateTime, nullable=False)
    end_time = db.Column(DateTime)

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    team = db.relationship("Team")
    creator = db.relationship("User")


# =========================
# MEETING PARTICIPANT
# =========================
class MeetingParticipant(db.Model):
    __tablename__ = "meeting_participants"

    meeting_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("meetings.meeting_id"),
        primary_key=True,
    )

    user_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id"),
        primary_key=True,
    )

    joined_at = db.Column(TIMESTAMP)

    attendance_status = db.Column(
        ENUM(
            "INVITED",
            "ATTENDED",
            "ABSENT",
            "EXCUSED",
        ),
        nullable=False,
        default="INVITED",
        server_default="INVITED",
    )

    meeting = db.relationship("Meeting")
    user = db.relationship("User")


# =========================
# MESSAGE
# =========================
class Message(db.Model):
    __tablename__ = "messages"

    message_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    team_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("teams.team_id"),
        nullable=False,
    )

    sender_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id"),
        nullable=False,
    )

    content = db.Column(Text, nullable=False)

    sent_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    edited_at = db.Column(TIMESTAMP)

    team = db.relationship("Team")
    sender = db.relationship("User")
