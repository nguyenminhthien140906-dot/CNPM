const express = require('express');
const http = require('http');
const { Server } = require('socket.io');
const path = require('path');

const app = express();
const server = http.createServer(app);
const io = new Server(server, { cors: { origin: "*" } });

app.use(express.json());

// ==========================================
// 1. CƠ SỞ DỮ LIỆU TẠM THỜI (IN-MEMORY DB)
// ==========================================
const meetingsDB = [];
const messagesDB = [];

// ==========================================
// 2. API ENDPOINTS (LÊN LỊCH & LẤY LỊCH HỌP)
// ==========================================

// API Lên lịch cuộc họp
app.post('/api/meetings/schedule', (req, res) => {
  const { projectId, title, hostId, startTime, durationMinutes } = req.body;

  if (!projectId || !title || !startTime) {
    return res.status(400).json({ error: "Thiếu thông tin bắt buộc!" });
  }

  const newMeeting = {
    id: `meet_${Date.now()}`,
    projectId,
    title,
    hostId: hostId || "Anonymous",
    startTime,
    durationMinutes: durationMinutes || 45,
    roomId: `room_${Math.random().toString(36).substring(2, 9)}`,
    createdAt: new Date()
  };

  meetingsDB.push(newMeeting);

  // Thông báo tới tất cả người dùng trong phòng dự án về lịch họp mới
  io.to(projectId).emit('new_meeting_scheduled', newMeeting);

  return res.status(201).json({
    message: "Lên lịch cuộc họp thành công!",
    meeting: newMeeting
  });
});

// API Lấy danh sách cuộc họp theo dự án
app.get('/api/meetings/project/:projectId', (req, res) => {
  const projectMeetings = meetingsDB.filter(m => m.projectId === req.params.projectId);
  res.json(projectMeetings);
});

// Serve file HTML giao diện trực tiếp
app.get('/', (req, res) => {
  res.send(getFrontendHTML());
});

// ==========================================
// 3. REAL-TIME SOCKET.IO (CHAT & WEBRTC SIGNALING)
// ==========================================
io.on('connection', (socket) => {
  console.log('Người dùng kết nối Socket ID:', socket.id);

  // Tham gia phòng dự án
  socket.on('join_project', (projectId) => {
    socket.join(projectId);
    console.log(`Socket ${socket.id} đã vào dự án: ${projectId}`);
    
    // Gửi lại danh sách tin nhắn cũ của dự án
    const oldMessages = messagesDB.filter(m => m.projectId === projectId);
    socket.emit('load_old_messages', oldMessages);
  });

  // Xử lý gửi/nhận tin nhắn Chat
  socket.on('send_message', (data) => {
    // data: { projectId, senderId, senderName, content, time }
    messagesDB.push(data);
    io.to(data.projectId).emit('receive_message', data);
  });

  // Signaling cho WebRTC (Video Call)
  socket.on('join_video_room', ({ roomId, userId }) => {
    socket.join(roomId);
    socket.to(roomId).emit('user_connected', userId);

    socket.on('disconnect', () => {
      socket.to(roomId).emit('user_disconnected', userId);
    });
  });
});

// Chạy server
const PORT = 4000;
server.listen(PORT, () => {
  console.log(`Server FE7 đang chạy tại: http://localhost:${PORT}`);
});

// ==========================================
// 4. FRONTEND HTML/JS INTERFACE (EMBEDDED)
// ==========================================
function getFrontendHTML() {
  return `
<!DOCTYPE html>
<html lang="vi">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>FE7 - Giao tiếp và Họp trực tuyến</title>
  <script src="/socket.io/socket.io.js"></script>
  <style>
    * { box-sizing: border-box; font-family: Arial, sans-serif; }
    body { margin: 20px; background-color: #f5f5f5; }
    .container { display: flex; gap: 20px; }
    .card { background: white; padding: 20px; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
    .left-panel { flex: 1; }
    .right-panel { flex: 1.5; }
    
    /* Chat UI */
    .chat-box { height: 250px; overflow-y: auto; border: 1px solid #ddd; padding: 10px; margin-bottom: 10px; border-radius: 4px; }
    .message { margin-bottom: 8px; }
    .message strong { color: #007bff; }
    .chat-input { display: flex; gap: 10px; }
    .chat-input input { flex: 1; padding: 8px; }

    /* Video UI */
    .video-grid { display: flex; gap: 10px; margin-top: 10px; }
    video { width: 48%; background: #222; border-radius: 6px; height: 180px; object-fit: cover; }
    button { padding: 8px 15px; cursor: pointer; background: #007bff; color: white; border: none; border-radius: 4px; }
    button:hover { background: #0056b3; }
    
    /* Schedule Form */
    .form-group { margin-bottom: 10px; }
    .form-group label { display: block; font-size: 12px; font-weight: bold; }
    .form-group input { width: 100%; padding: 6px; }
  </style>
</head>
<body>

  <h2>Hệ Thống Giao Tiếp & Họp Trực Tuyến Dự Án (FE7)</h2>

  <div class="container">
    <!-- LEFT PANEL: CHAT & LÊN LỊCH -->
    <div class="left-panel">
      <!-- TẠO LỊCH HỌP -->
      <div class="card" style="margin-bottom: 20px;">
        <h3>1. Lên Lịch Cuộc Họp</h3>
        <form id="scheduleForm">
          <div class="form-group">
            <label>Tiêu đề cuộc họp:</label>
            <input type="text" id="meetTitle" value="Họp Review Tiến Độ" required />
          </div>
          <div class="form-group">
            <label>Thời gian bắt đầu:</label>
            <input type="datetime-local" id="meetTime" required />
          </div>
          <button type="submit">Lên Lịch Họp</button>
        </form>
        <h4>Danh sách cuộc họp:</h4>
        <ul id="meetingList"></ul>
      </div>

      <!-- CHAT THỜI GIAN THỰC -->
      <div class="card">
        <h3>2. Chat Nhóm Dự Án</h3>
        <div class="chat-box" id="chatBox"></div>
        <form id="chatForm" class="chat-input">
          <input type="text" id="msgInput" placeholder="Nhập tin nhắn..." required />
          <button type="submit">Gửi</button>
        </form>
      </div>
    </div>

    <!-- RIGHT PANEL: WEBRTC VIDEO & SCREEN SHARE -->
    <div class="right-panel">
      <div class="card">
        <h3>3. Phòng Họp Trực Tuyến & Chia Sẻ Màn Hình</h3>
        <div class="video-grid">
          <video id="localVideo" autoplay playsinline muted></video>
          <video id="remoteVideo" autoplay playsinline></video>
        </div>
        <br/>
        <div style="display: flex; gap: 10px;">
          <button id="btnStartCam">Bật Camera & Mic</button>
          <button id="btnShareScreen" style="background: #28a745;">Chia Sẻ Màn Hình</button>
        </div>
      </div>
    </div>
  </div>

  <script>
    const socket = io();
    const projectId = "PROJ_101"; // Mã dự án cố định để test
    const currentUser = { id: "USER_" + Math.floor(Math.random() * 1000), name: "SinhVien_" + Math.floor(Math.random() * 100) };

    // 1. KẾT NỐI DỰ ÁN
    socket.emit('join_project', projectId);

    // 2. XỬ LÝ CHAT
    const chatBox = document.getElementById('chatBox');
    const chatForm = document.getElementById('chatForm');
    const msgInput = document.getElementById('msgInput');

    socket.on('load_old_messages', (messages) => {
      chatBox.innerHTML = '';
      messages.forEach(appendMessage);
    });

    socket.on('receive_message', (data) => {
      appendMessage(data);
    });

    function appendMessage(data) {
      const msgDiv = document.createElement('div');
      msgDiv.className = 'message';
      msgDiv.innerHTML = \`<strong>\${data.senderName}:</strong> \${data.content} <small>(\${data.time})</small>\`;
      chatBox.appendChild(msgDiv);
      chatBox.scrollTop = chatBox.scrollHeight;
    }

    chatForm.addEventListener('submit', (e) => {
      e.preventDefault();
      const content = msgInput.value.trim();
      if (!content) return;

      const msgData = {
        projectId,
        senderId: currentUser.id,
        senderName: currentUser.name,
        content,
        time: new Date().toLocaleTimeString()
      };

      socket.emit('send_message', msgData);
      msgInput.value = '';
    });

    // 3. XỬ LÝ LÊN LỊCH HỌP
    const scheduleForm = document.getElementById('scheduleForm');
    const meetingList = document.getElementById('meetingList');

    scheduleForm.addEventListener('submit', async (e) => {
      e.preventDefault();
      const title = document.getElementById('meetTitle').value;
      const startTime = document.getElementById('meetTime').value;

      const res = await fetch('/api/meetings/schedule', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ projectId, title, hostId: currentUser.name, startTime })
      });

      if (res.ok) {
        alert("Lên lịch thành công!");
      }
    });

    socket.on('new_meeting_scheduled', (meeting) => {
      const li = document.createElement('li');
      li.innerHTML = \`<b>\${meeting.title}</b> - Bắt đầu: \${new Date(meeting.startTime).toLocaleString()} (Phòng: \${meeting.roomId})\`;
      meetingList.appendChild(li);
    });

    // 4. XỬ LÝ WEBRTC (CAMERA & SCREEN SHARE)
    let localStream;
    const localVideo = document.getElementById('localVideo');

    document.getElementById('btnStartCam').onclick = async () => {
      try {
        localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
        localVideo.srcObject = localStream;
      } catch (err) {
        alert("Không thể mở Camera/Microphone: " + err.message);
      }
    };

    document.getElementById('btnShareScreen').onclick = async () => {
      try {
        const screenStream = await navigator.mediaDevices.getDisplayMedia({ video: true });
        localVideo.srcObject = screenStream;

        // Trở lại Camera khi tắt chia sẻ màn hình
        screenStream.getVideoTracks()[0].onended = () => {
          localVideo.srcObject = localStream;
        };
      } catch (err) {
        console.error("Lỗi chia sẻ màn hình:", err);
      }
    };
  </script>
</body>
</html>
  `;
}
