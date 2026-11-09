
from sqlalchemy import (
    String,
    Text,
    Boolean,
    ForeignKey,
    func,
    true,
)
from sqlalchemy.dialects.mysql import BIGINT, TIMESTAMP

from app.models.base import db


# =========================
# ROLE
# =========================
class Role(db.Model):
    __tablename__ = "roles"

    role_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    role_code = db.Column(
        String(30),
        unique=True,
        nullable=False,
    )

    role_name = db.Column(
        String(100),
        nullable=False,
    )

    description = db.Column(Text)

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    users = db.relationship(
        "User",
        back_populates="role",
    )


# =========================
# USER
# =========================
class User(db.Model):
    __tablename__ = "users"

    user_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    role_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("roles.role_id"),
        nullable=False,
    )

    username = db.Column(
        String(50),
        unique=True,
        nullable=False,
    )

    email = db.Column(
        String(255),
        unique=True,
        nullable=False,
    )

    password_hash = db.Column(
        String(255),
        nullable=False,
    )

    full_name = db.Column(
        String(150),
        nullable=False,
    )

    avatar_url = db.Column(String(500))
    phone = db.Column(String(20))

    is_active = db.Column(
        Boolean,
        nullable=False,
        default=True,
        server_default=true(),
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

    role = db.relationship(
        "Role",
        back_populates="users",
    )

    student = db.relationship(
        "Student",
        back_populates="user",
        uselist=False,
    )

    lecturer = db.relationship(
        "Lecturer",
        back_populates="user",
        uselist=False,
    )

    created_projects = db.relationship(
        "Project",
        foreign_keys="Project.created_by",
        back_populates="creator",
    )

    approved_projects = db.relationship(
        "Project",
        foreign_keys="Project.approved_by",
        back_populates="approver",
    )


# =========================
# STUDENT
# =========================
class Student(db.Model):
    __tablename__ = "students"

    student_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    user_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id", ondelete="CASCADE"),
        unique=True,
        nullable=False,
    )

    student_code = db.Column(
        String(30),
        unique=True,
        nullable=False,
    )

    user = db.relationship(
        "User",
        back_populates="student",
    )

    class_enrollments = db.relationship(
        "ClassStudent",
        back_populates="student",
    )

    team_memberships = db.relationship(
        "TeamMember",
        back_populates="student",
    )


# =========================
# LECTURER
# =========================
class Lecturer(db.Model):
    __tablename__ = "lecturers"

    lecturer_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    user_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id", ondelete="CASCADE"),
        unique=True,
        nullable=False,
    )

    lecturer_code = db.Column(
        String(30),
        unique=True,
        nullable=False,
    )

    user = db.relationship(
        "User",
        back_populates="lecturer",
    )

    class_assignments = db.relationship(
        "ClassLecturer",
        back_populates="lecturer",
    )
