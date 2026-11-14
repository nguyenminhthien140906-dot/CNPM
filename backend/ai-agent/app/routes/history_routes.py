
from flask import Blueprint, request, jsonify
from sqlalchemy.exc import SQLAlchemyError

from app.models.base import db
from app.models.ai import AIConversation, AIMessage

history_bp = Blueprint("history", __name__)


@history_bp.route("/api/conversations", methods=["POST"])
def create_conversation():
    data = request.get_json(silent=True)

    if not isinstance(data, dict):
        return jsonify({
            "success": False,
            "message": "Du lieu JSON khong hop le"
        }), 400

    user_id = data.get("user_id")
    title = data.get("title", "Cuoc hoi thoai moi")

    if type(user_id) is not int or user_id <= 0:
        return jsonify({
            "success": False,
            "message": "user_id khong hop le"
        }), 400

    if not isinstance(title, str) or len(title) > 200:
        return jsonify({
            "success": False,
            "message": "title khong hop le"
        }), 400

    try:
        conversation = AIConversation(
            user_id=user_id,
            title=title
        )

        db.session.add(conversation)
        db.session.commit()

        return jsonify({
            "success": True,
            "conversation_id": conversation.conversation_id,
            "title": conversation.title
        }), 201

    except SQLAlchemyError:
        db.session.rollback()
        return jsonify({
            "success": False,
            "message": "Khong the tao cuoc hoi thoai"
        }), 500


@history_bp.route(
    "/api/conversations/<int:conversation_id>/messages",
    methods=["POST"]
)
def save_message(conversation_id):
    data = request.get_json(silent=True)

    if not isinstance(data, dict):
        return jsonify({
            "success": False,
            "message": "JSON khong hop le"
        }), 400

    sender_type = data.get("sender_type")
    content = data.get("content")

    if sender_type not in ("USER", "ASSISTANT", "SYSTEM"):
        return jsonify({
            "success": False,
            "message": "sender_type khong hop le"
        }), 400

    if not isinstance(content, str) or not content.strip():
        return jsonify({
            "success": False,
            "message": "Noi dung tin nhan khong hop le"
        }), 400

    try:
        conversation = db.session.get(
            AIConversation, conversation_id
        )

        if conversation is None:
            return jsonify({
                "success": False,
                "message": "Khong tim thay cuoc hoi thoai"
            }), 404

        message = AIMessage(
            conversation_id=conversation_id,
            sender_type=sender_type,
            content=content.strip()
        )

        db.session.add(message)
        db.session.commit()

        return jsonify({
            "success": True,
            "ai_message_id": message.ai_message_id,
            "conversation_id": conversation_id
        }), 201

    except SQLAlchemyError:
        db.session.rollback()
        return jsonify({
            "success": False,
            "message": "Khong the luu tin nhan"
        }), 500


@history_bp.route(
    "/api/conversations/<int:conversation_id>/messages",
    methods=["GET"]
)
def get_messages(conversation_id):
    try:
        conversation = db.session.get(
            AIConversation, conversation_id
        )

        if conversation is None:
            return jsonify({
                "success": False,
                "message": "Khong tim thay cuoc hoi thoai"
            }), 404

        messages = (
            db.session.query(AIMessage)
            .filter_by(conversation_id=conversation_id)
            .order_by(AIMessage.ai_message_id.asc())
            .all()
        )

        return jsonify({
            "success": True,
            "conversation_id": conversation_id,
            "messages": [
                {
                    "ai_message_id": msg.ai_message_id,
                    "sender_type": msg.sender_type,
                    "content": msg.content
                }
                for msg in messages
            ]
        }), 200

    except SQLAlchemyError:
        db.session.rollback()
        return jsonify({
            "success": False,
            "message": "Khong the doc lich su"
        }), 500
