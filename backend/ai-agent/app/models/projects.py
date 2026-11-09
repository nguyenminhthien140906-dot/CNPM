
from sqlalchemy import (
    String,
    Text,
    Date,
    ForeignKey,
    UniqueConstraint,
    CheckConstraint,
    func,
)
from sqlalchemy.dialects.mysql import BIGINT, ENUM, TIMESTAMP

from app.models.base import db


# =========================
# PROJECT
# =========================
class Project(db.Model):
    __tablename__ = "projects"

    project_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    class_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("study_classes.class_id"),
        nullable=False,
    )

    syllabus_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("syllabi.syllabus_id"),
    )

    created_by = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id"),
        nullable=False,
    )

    approved_by = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id"),
    )

    project_name = db.Column(String(200), nullable=False)
    description = db.Column(Text)
    objectives = db.Column(Text)
    expected_outputs = db.Column(Text)
    requirements = db.Column(Text)

    start_date = db.Column(Date)
    deadline = db.Column(Date)

    status = db.Column(
        ENUM(
            "DRAFT",
            "PENDING",
            "APPROVED",
            "IN_PROGRESS",
            "COMPLETED",
            "CANCELLED",
        ),
        nullable=False,
        default="DRAFT",
        server_default="DRAFT",
    )

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

    __table_args__ = (
        CheckConstraint(
            "start_date IS NULL OR deadline IS NULL OR deadline >= start_date",
            name="chk_project_date_range",
        ),
    )

    study_class = db.relationship(
        "StudyClass",
        back_populates="projects",
    )

    syllabus = db.relationship(
        "Syllabus",
        back_populates="projects",
    )

    creator = db.relationship(
        "User",
        foreign_keys=[created_by],
        back_populates="created_projects",
    )

    approver = db.relationship(
        "User",
        foreign_keys=[approved_by],
        back_populates="approved_projects",
    )

    teams = db.relationship(
        "Team",
        back_populates="project",
    )

    milestones = db.relationship(
        "Milestone",
        back_populates="project",
    )


# =========================
# TEAM
# =========================
class Team(db.Model):
    __tablename__ = "teams"

    team_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    project_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("projects.project_id", ondelete="CASCADE"),
        nullable=False,
    )

    leader_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("students.student_id"),
    )

    team_name = db.Column(String(150), nullable=False)
    description = db.Column(Text)

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    __table_args__ = (
        UniqueConstraint(
            "project_id",
            "team_name",
            name="uq_team_project_name",
        ),
    )

    project = db.relationship(
        "Project",
        back_populates="teams",
    )

    leader = db.relationship(
        "Student",
        foreign_keys=[leader_id],
    )

    members = db.relationship(
        "TeamMember",
        back_populates="team",
    )

    tasks = db.relationship(
        "Task",
        back_populates="team",
    )


# =========================
# TEAM MEMBER
# =========================
class TeamMember(db.Model):
    __tablename__ = "team_members"

    team_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("teams.team_id", ondelete="CASCADE"),
        primary_key=True,
    )

    student_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("students.student_id", ondelete="CASCADE"),
        primary_key=True,
    )

    joined_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    member_role = db.Column(
        ENUM("LEADER", "MEMBER"),
        nullable=False,
        default="MEMBER",
        server_default="MEMBER",
    )

    team = db.relationship(
        "Team",
        back_populates="members",
    )

    student = db.relationship(
        "Student",
        back_populates="team_memberships",
    )
