
def generate_reply(message):
    """
    Xử lý tin nhắn gửi đến chatbot CollabSphere.
    Đây là phiên bản thử nghiệm, chưa kết nối mô hình AI.
    """
    message = message.strip()

    if not message:
        return "Vui lòng nhập câu hỏi."

    message_lower = message.lower()

    if "xin chào" in message_lower:
        return "Xin chào! Tôi là trợ lý CollabSphere."

    if "dự án" in message_lower:
        return "Bạn có thể theo dõi tiến độ dự án trong CollabSphere."

    if "nhiệm vụ" in message_lower:
        return "Bạn có thể xem các nhiệm vụ được giao trong mục Task."

    return "Tôi đã nhận được câu hỏi của bạn."
