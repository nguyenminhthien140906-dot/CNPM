
def generate_reply(message):
    """
    Chatbot hỗ trợ sử dụng CollabSphere.
    Phiên bản trả lời theo quy tắc, chưa dùng mô hình AI.
    """
    message = message.strip()

    if not message:
        return "Vui lòng nhập câu hỏi."

    message_lower = message.lower()

    responses = {
        "xin chào": "Xin chào! Tôi là trợ lý CollabSphere. Bạn cần hỗ trợ gì?",
        "chào": "Xin chào! Tôi có thể hướng dẫn bạn sử dụng CollabSphere.",
        "tiến độ": "Bạn có thể xem tiến độ dự án thông qua các milestone và task.",
        "dự án": "Bạn có thể tạo, quản lý và theo dõi dự án trong CollabSphere.",
        "nhiệm vụ": "Bạn có thể xem và cập nhật nhiệm vụ trong mục Task.",
        "task": "Bạn có thể theo dõi trạng thái và thời hạn của task.",
        "milestone": "Milestone là cột mốc quan trọng trong quá trình thực hiện dự án.",
        "nhóm": "Bạn có thể trao đổi và phân chia công việc với các thành viên trong nhóm.",
        "đóng góp": "CollabSphere hỗ trợ thống kê mức độ đóng góp của thành viên.",
        "họp": "Bạn có thể quản lý lịch họp nhóm thông qua chức năng Meeting.",
        "thông báo": "Bạn có thể xem các cập nhật mới trong mục thông báo.",
    }

    for keyword, reply in responses.items():
        if keyword in message_lower:
            return reply

    return (
        "Tôi chưa có thông tin để trả lời câu hỏi này. "
        "Bạn có thể hỏi về dự án, nhiệm vụ, tiến độ, nhóm hoặc milestone."
    )