
from sqlalchemy import BigInteger, String, Text, ForeignKey
from sqlalchemy.dialects.mysql import TIMESTAMP
from sqlalchemy.orm import relationship
from sqlalchemy.sql import func

from app.models.base import db


class AIConversation(db.Model):
    __tablename__ = "ai_conversations"

    conversation_id = db.Column(
        BigInteger,
        primary_key=True,
        autoincrement=True
    )

    user_id = db.Column(
        BigInteger,
        ForeignKey("users.user_id"),
        nullable=False
    )

    title = db.Column(String(200), nullable=True)

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp()
    )

    updated_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp(),
        server_onupdate=func.current_timestamp()
    )

    user = relationship("User")

    messages = relationship(
        "AIMessage",
        back_populates="conversation",
        cascade="all, delete-orphan"
    )


class AIMessage(db.Model):
    __tablename__ = "ai_messages"

    ai_message_id = db.Column(
        BigInteger,
        primary_key=True,
        autoincrement=True
    )

    conversation_id = db.Column(
        BigInteger,
        ForeignKey("ai_conversations.conversation_id"),
        nullable=False
    )

    sender_type = db.Column(
        String(20),
        nullable=False
    )

    content = db.Column(
        Text,
        nullable=False
    )

    created_at = db.Column(
        TIMESTAMP,
        nullable=False,
        server_default=func.current_timestamp()
    )

    conversation = relationship(
        "AIConversation",
        back_populates="messages"
    )
