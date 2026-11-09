
from sqlalchemy import String, Text, ForeignKey, Boolean, text
from sqlalchemy.dialects.mysql import BIGINT, TIMESTAMP
from sqlalchemy.orm import relationship
from sqlalchemy.sql import func

from app.models.base import db


class Notification(db.Model):
    __tablename__ = "notifications"

    notification_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    user_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id"),
        nullable=False,
    )

    title = db.Column(String(200), nullable=False)
    content = db.Column(Text)
    notification_type = db.Column(String(50))

    is_read = db.Column(
        Boolean,
        nullable=False,
        default=False,
        server_default=text("0"),
    )

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    user = relationship("User")
