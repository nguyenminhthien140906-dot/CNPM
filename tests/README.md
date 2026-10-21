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
### 1. Nhiệm vụ nền tảng & Kết nối Realtime (Đã hoàn thành)
- **Thiết lập Server Node.js & Socket.IO:** Đã dựng server hoàn chỉnh tại `src/server.js`, cấu hình Express và Socket.IO phục vụ truyền nhận dữ liệu thời gian thực giữa nhiều client cùng lúc.
- **Tính năng Chat Realtime:** Đã xử lý các sự kiện `send_message` và `receive_message` tại `src/server.js` cho phép gửi/nhận tin nhắn tức thì.

### 2. Bổ sung WebRTC & Chức năng Meeting (Đã hoàn thành)
- **Tạo/Tham gia phòng Meeting:** Đã xử lý sự kiện `join_room` tại `src/server.js`.
- **Cơ chế WebRTC Signaling:** Đã bổ sung đầy đủ các sự kiện `sending_signal` và `returning_signal` tại `src/server.js` để trao đổi SDP Offer/Answer giữa các peer, phục vụ kết nối cuộc gọi video/meeting.

### 3. Tích hợp AI AWS Bedrock (Đã hoàn thành)
- **Tích hợp SDK chính thức:** Đã kết nối thư viện `@aws-sdk/client-bedrock-runtime` (Sử dụng model Anthropic Claude 3 Haiku) tại `src/ai/aiChat.js`.
- **Xử lý lệnh `@ai` & Phân tích tiến độ:** Tự động lắng nghe câu hỏi có tiền tố `@ai` hoặc truy vấn tiến độ để trả về phản hồi từ AI Assistant.
- **Cơ chế Try/Catch Fallback:** Thiết lập log xác thực gọi SDK kèm cơ chế xử lý lỗi/fallback linh hoạt, đảm bảo hệ thống phản hồi ổn định trong môi trường thử nghiệm local.

### 4. Giao diện Test & Tài liệu Bằng chứng (Đã hoàn thành)
- **Giao diện Client (`index.html`):** Hoàn thiện giao diện test tập trung gồm khung Chat Realtime, ô hỏi `@ai` và nút "Vào phòng Meeting".
- **Thư mục Bằng chứng (`Evidence/`):** Lưu trữ đầy đủ 3 ảnh chụp màn hình thực nghiệm:
  - `Evidence_Chat_AI.png`: Demo nhắn tin realtime và tương tác với AI.
  - `Evidence_WebRTC_Meeting.png`: Dòng log Terminal xác thực kết nối Room & WebRTC Signaling.
  - `Evidence_AWS_Bedrock_SDK.png`: Dòng log Terminal xác thực khởi tạo và gửi request qua AWS Bedrock Runtime SDK.

---
##  Chi tiết:
- **`Evidence/`** : Thư mục chứa bằng chứng hình ảnh chạy thực tế.
- **`src/server.js`** : Server chính (Node.js, Express, Socket.IO, WebRTC Signaling).
- **`src/ai/aiChat.js`** : Module tích hợp AWS Bedrock Runtime SDK & Phân tích báo cáo tiến độ.
- **`index.html`** : Giao diện test client cho Chat, AI Assistant và Meeting.
- 
## Cách chạy sever:
1. `npm install`
2. `node src/server.js`
