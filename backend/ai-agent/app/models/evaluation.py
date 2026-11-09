
from sqlalchemy import (
    ForeignKey,
    Text,
    DECIMAL,
    CheckConstraint,
)
from sqlalchemy.dialects.mysql import BIGINT, TIMESTAMP
from sqlalchemy.orm import relationship
from sqlalchemy.sql import func

from app.models.base import db


class Evaluation(db.Model):
    __tablename__ = "evaluations"

    evaluation_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    evaluator_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id"),
        nullable=False,
    )

    submission_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("submissions.submission_id"),
    )

    project_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("projects.project_id"),
    )

    team_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("teams.team_id"),
    )

    student_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("students.student_id"),
    )

    score = db.Column(DECIMAL(5, 2))
    comments = db.Column(Text)

    evaluated_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    __table_args__ = (
        CheckConstraint(
            """
            (submission_id IS NOT NULL) +
            (project_id IS NOT NULL) +
            (team_id IS NOT NULL) +
            (student_id IS NOT NULL) = 1
            """,
            name="chk_evaluation_exactly_one_target",
        ),
        CheckConstraint(
            "score IS NULL OR (score >= 0 AND score <= 100)",
            name="chk_evaluation_score",
        ),
    )

    evaluator = relationship("User")
    submission = relationship("Submission")
    project = relationship("Project")
    team = relationship("Team")
    student = relationship("Student")
