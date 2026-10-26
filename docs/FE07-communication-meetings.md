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

  //
