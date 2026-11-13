
from flask import Blueprint, request, jsonify
from app.agents.chatbot_agent import generate_reply

chat_bp = Blueprint("chat", __name__)


@chat_bp.route("/api/chat", methods=["POST"])
def chat():
    data = request.get_json(silent=True)

    if not isinstance(data, dict):
        return jsonify({
            "success": False,
            "message": "Vui lòng gửi dữ liệu JSON hợp lệ."
        }), 400

    message = data.get("message")

    if not isinstance(message, str) or not message.strip():
        return jsonify({
            "success": False,
            "message": "Nội dung tin nhắn không hợp lệ."
        }), 400

    reply = generate_reply(message)

    return jsonify({
        "success": True,
        "reply": reply
    }), 200
