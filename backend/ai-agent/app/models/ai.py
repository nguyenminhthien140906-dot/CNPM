
from sqlalchemy import String, ForeignKey
from sqlalchemy.dialects.mysql import BIGINT, LONGTEXT, ENUM, TIMESTAMP
from sqlalchemy.orm import relationship
from sqlalchemy.sql import func

from app.models.base import db


# =========================
# AI CONVERSATION
# =========================
class AIConversation(db.Model):
    __tablename__ = "ai_conversations"

    conversation_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    user_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("users.user_id"),
        nullable=False,
    )

    conversation_title = db.Column(String(200))

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    user = relationship("User")

    messages = relationship(
        "AIMessage",
        back_populates="conversation",
        cascade="all, delete-orphan",
    )


# =========================
# AI MESSAGE
# =========================
class AIMessage(db.Model):
    __tablename__ = "ai_messages"

    ai_message_id = db.Column(
        BIGINT(unsigned=True),
        primary_key=True,
        autoincrement=True,
    )

    conversation_id = db.Column(
        BIGINT(unsigned=True),
        ForeignKey("ai_conversations.conversation_id"),
        nullable=False,
    )

    sender_type = db.Column(
        ENUM("USER", "ASSISTANT", "SYSTEM"),
        nullable=False,
    )

    content = db.Column(LONGTEXT, nullable=False)

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
    )

    conversation = relationship(
        "AIConversation",
        back_populates="messages",
    )
