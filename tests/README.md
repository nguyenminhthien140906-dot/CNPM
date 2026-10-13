# Tests

Người phụ trách chính: Người 5.

Tạo test case theo:
- Authentication
- Authorization
- Project approval
- Team
- Milestone
- Task
- Checkpoint
- Evaluation
- Notification
- Realtime
- AI


##  Tiến độ & Bằng chứng triển khai (Evidence)
- **Chat gửi/nhận tin nhắn**: Đã làm tại `src/server.js` & `src/sockets/chat.js`
- **Tạo/tham gia Meeting**: Đã làm tại `src/server.js` (sự kiện `join_room`)
- **Kết nối WebRTC signaling**: Đã làm tại `src/sockets/chat.js`
- **Tích hợp AI Chatbot**: Đã làm tại `src/aiChat.js` (nhận lệnh `@ai`)
- **AI Phân tích tiến độ**: Đã làm tại `src/aiChat.js` (tự động phản hồi khi hỏi "tiến độ")

##  Chi tiết:
- `CollabSphere_TestCases1.xlsx`: File Test Cases.
- `src/server.js`: Server chính (Socket.IO + Express).
- `src/aiChat.js`: Xử lý AI & Báo cáo tiến độ.
- `src/sockets/chat.js`: Xử lý Chat & Signaling WebRTC.

## Cách chạy sever:
1. `npm install`
2. `node src/server.js`
