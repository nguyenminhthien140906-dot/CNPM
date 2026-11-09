
from sqlalchemy import (
    String,
    Text,
    ForeignKey,
    UniqueConstraint,
    func,
)
from sqlalchemy.dialects.mysql import BIGINT, INTEGER, ENUM, LONGTEXT, TIMESTAMP

from app.models.base import db


# =========================
# SUBJECT
# =========================
class Subject(db.Model):
    __tablename__ = "subjects"

    subject_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    subject_code = db.Column(String(30), unique=True, nullable=False)
    subject_name = db.Column(String(150), nullable=False)
    description = db.Column(Text)

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    syllabi = db.relationship(
        "Syllabus",
        back_populates="subject",
    )

    study_classes = db.relationship(
        "StudyClass",
        back_populates="subject",
    )


# =========================
# SYLLABUS
# =========================
class Syllabus(db.Model):
    __tablename__ = "syllabi"

    syllabus_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    subject_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("subjects.subject_id"),
        nullable=False,
    )

    version = db.Column(String(30), nullable=False)
    title = db.Column(String(200), nullable=False)
    content = db.Column(LONGTEXT)

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    __table_args__ = (
        UniqueConstraint(
            "subject_id",
            "version",
            name="uq_syllabus_subject_version",
        ),
    )

    subject = db.relationship(
        "Subject",
        back_populates="syllabi",
    )

    projects = db.relationship(
        "Project",
        back_populates="syllabus",
    )


# =========================
# STUDY CLASS
# =========================
class StudyClass(db.Model):
    __tablename__ = "study_classes"

    class_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    subject_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("subjects.subject_id"),
        nullable=False,
    )

    class_code = db.Column(
        String(30),
        unique=True,
        nullable=False,
    )

    class_name = db.Column(String(150), nullable=False)
    semester = db.Column(String(30))
    academic_year = db.Column(String(20))

    capacity = db.Column(INTEGER(unsigned=True))

    status = db.Column(
        ENUM("PLANNED", "OPEN", "CLOSED", "ARCHIVED"),
        nullable=False,
        default="PLANNED",
        server_default="PLANNED",
    )

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    subject = db.relationship(
        "Subject",
        back_populates="study_classes",
    )

    students = db.relationship(
        "ClassStudent",
        back_populates="study_class",
    )

    lecturers = db.relationship(
        "ClassLecturer",
        back_populates="study_class",
    )

    projects = db.relationship(
        "Project",
        back_populates="study_class",
    )


# =========================
# CLASS STUDENT
# =========================
class ClassStudent(db.Model):
    __tablename__ = "class_students"

    class_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("study_classes.class_id", ondelete="CASCADE"),
        primary_key=True,
    )

    student_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("students.student_id", ondelete="CASCADE"),
        primary_key=True,
    )

    enrolled_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    study_class = db.relationship(
        "StudyClass",
        back_populates="students",
    )

    student = db.relationship(
        "Student",
        back_populates="class_enrollments",
    )


# =========================
# CLASS LECTURER
# =========================
class ClassLecturer(db.Model):
    __tablename__ = "class_lecturers"

    class_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("study_classes.class_id", ondelete="CASCADE"),
        primary_key=True,
    )

    lecturer_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("lecturers.lecturer_id", ondelete="CASCADE"),
        primary_key=True,
    )

    assignment_role = db.Column(String(50), nullable=False)

    assigned_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    study_class = db.relationship(
        "StudyClass",
        back_populates="lecturers",
    )

    lecturer = db.relationship(
        "Lecturer",
        back_populates="class_assignments",
    )
