<!DOCTYPE html>
<html lang="vi">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>CollabSphere - FE07 Giao tiếp & Họp trực tuyến</title>
    <!-- Tailwind CSS & Lucide Icons -->
    <script src="https://cdn.tailwindcss.com"></script>
    <script src="https://unpkg.com/lucide@latest"></script>
    <style>
        .chat-scroll::-webkit-scrollbar { width: 6px; }
        .chat-scroll::-webkit-scrollbar-thumb { background: #cbd5e1; border-radius: 4px; }
        .animate-fade-in { animation: fadeIn 0.2s ease-in-out; }
        @keyframes fadeIn { from { opacity: 0; transform: translateY(4px); } to { opacity: 1; transform: translateY(0); } }
    </style>
</head>
<body class="bg-slate-100 font-sans text-slate-800 h-screen overflow-hidden flex flex-col">

    <!-- TOP HEADER -->
    <header class="bg-slate-900 text-white px-6 py-3 flex justify-between items-center shadow-md shrink-0">
        <div class="flex items-center gap-3">
            <div class="bg-indigo-600 p-2 rounded-lg"><i data-lucide="video" class="w-6 h-6 text-white"></i></div>
            <div>
                <h1 class="font-bold text-lg leading-tight">CollabSphere - PBL System</h1>
                <p class="text-xs text-slate-400">Chức năng FE07: Quản lý Giao tiếp & Họp trực tuyến</p>
            </div>
        </div>
        
        <!-- Role Swapper for Simulation -->
        <div class="flex items-center gap-4 bg-slate-800 px-3 py-1.5 rounded-lg border border-slate-700">
            <span class="text-xs text-slate-400">Giả lập người dùng:</span>
            <select id="userRoleSelect" onchange="switchUser(this.value)" class="bg-slate-900 text-slate-200 text-sm font-semibold rounded px-2 py-1 outline-none border border-slate-700 cursor-pointer">
                <option value="student1">Sinh viên: Nguyễn Văn A (Leader)</option>
                <option value="student2">Sinh viên: Trần Thị B (Thành viên)</option>
                <option value="lecturer">Giảng viên: TS. Lê Hoàng (GVHD)</option>
            </select>
        </div>
    </header>

    <!-- MAIN CONTAINER -->
    <div class="flex flex-1 overflow-hidden">
        
        <!-- LEFT SIDEBAR: CHANNELS & MEETINGS -->
        <aside class="w-80 bg-white border-r border-slate-200 flex flex-col shrink-0">
            <!-- Tabs -->
            <div class="flex border-b border-slate-200 bg-slate-50">
                <button onclick="switchTab('chat')" id="tabChatBtn" class="flex-1 py-3 text-sm font-semibold text-indigo-600 border-b-2 border-indigo-600 flex justify-center items-center gap-2">
                    <i data-lucide="message-square" class="w-4 h-4"></i> Trò chuyện
                </button>
                <button onclick="switchTab('meeting')" id="tabMeetingBtn" class="flex-1 py-3 text-sm font-semibold text-slate-500 hover:text-slate-700 flex justify-center items-center gap-2">
                    <i data-lucide="calendar" class="w-4 h-4"></i> Lịch họp (<span id="meetingCount">2</span>)
                </button>
            </div>

            <!-- Chat Channels List -->
            <div id="chatSidebar" class="flex-1 overflow-y-auto p-4 space-y-4">
                <div>
                    <h3 class="text-xs font-bold text-slate-400 uppercase tracking-wider mb-2">Kênh Dự Án</h3>
                    <div class="space-y-1">
                        <button onclick="selectChannel('proj-501')" id="chan-proj-501" class="w-full text-left px-3 py-2.5 rounded-lg bg-indigo-50 text-indigo-700 font-semibold flex items-center justify-between">
                            <span class="flex items-center gap-2 text-sm"><i data-lucide="hash" class="w-4 h-4"></i> Nhóm 1 - Project PBL</span>
                            <span class="bg-indigo-600 text-white text-[10px] px-1.5 py-0.5 rounded-full">Active</span>
                        </button>
                    </div>
                </div>

                <div>
                    <h3 class="text-xs font-bold text-slate-400 uppercase tracking-wider mb-2">Trò chuyện trực tiếp</h3>
                    <div class="space-y-1">
                        <button onclick="selectChannel('direct-lecturer')" id="chan-direct-lecturer" class="w-full text-left px-3 py-2 rounded-lg text-slate-600 hover:bg-slate-100 flex items-center gap-2 text-sm">
                            <div class="w-2 h-2 rounded-full bg-green-500"></div>
                            <span>TS. Lê Hoàng (GVHD)</span>
                        </button>
                    </div>
                </div>
            </div>

            <!-- Meetings List Sidebar -->
            <div id="meetingSidebar" class="flex-1 overflow-y-auto p-4 hidden space-y-3">
                <div class="flex justify-between items-center mb-2">
                    <h3 class="text-xs font-bold text-slate-400 uppercase tracking-wider">Danh sách buổi họp</h3>
                    <button onclick="openScheduleModal()" class="text-xs bg-indigo-600 text-white font-medium px-2 py-1 rounded hover:bg-indigo-700 flex items-center gap-1">
                        <i data-lucide="plus" class="w-3 h-3"></i> Đặt lịch
                    </button>
                </div>
                <div id="meetingListContainer" class="space-y-2">
                    <!-- Dynamic Meetings Rendered Here -->
                </div>
            </div>

            <!-- Current User Card -->
            <div class="p-3 bg-slate-50 border-t border-slate-200 flex items-center gap-3">
                <div id="userAvatar" class="w-9 h-9 rounded-full bg-indigo-600 text-white flex items-center justify-center font-bold text-sm">A</div>
                <div class="overflow-hidden">
                    <p id="userName" class="text-sm font-semibold text-slate-800 truncate">Nguyễn Văn A</p>
                    <p id="userRole" class="text-xs text-slate-500 truncate">Sinh viên (Leader)</p>
                </div>
            </div>
        </aside>

        <!-- CENTER DISPLAY: CHAT OR VIDEO CONFERENCING -->
        <main class="flex-1 flex flex-col bg-slate-50 relative overflow-hidden">
            
            <!-- CHAT VIEW -->
            <div id="chatView" class="flex-1 flex flex-col h-full">
                <!-- Chat Header -->
                <div class="bg-white px-6 py-3.5 border-b border-slate-200 flex justify-between items-center shadow-sm">
                    <div>
                        <h2 id="currentChannelTitle" class="font-bold text-slate-800 flex items-center gap-2">
                            <i data-lucide="hash" class="w-5 h-5 text-indigo-600"></i> Nhóm 1 - Project PBL
                        </h2>
                        <p class="text-xs text-slate-500">Kênh trao đổi dự án & báo cáo tiến độ hàng tuần với GVHD</p>
                    </div>
                    <button onclick="startInstantCall()" class="bg-emerald-600 hover:bg-emerald-700 text-white text-sm font-medium px-4 py-2 rounded-lg flex items-center gap-2 shadow-sm transition">
                        <i data-lucide="video" class="w-4 h-4"></i> Họp Ngay (Instant Call)
                    </button>
                </div>

                <!-- Messages Stream -->
                <div id="messageStream" class="flex-1 overflow-y-auto p-6 space-y-4 chat-scroll">
                    <!-- Messages will be injected here -->
                </div>

                <!-- Chat Input Area -->
                <div class="p-4 bg-white border-t border-slate-200">
                    <form id="chatForm" onsubmit="handleSendMessage(event)" class="flex items-center gap-2 bg-slate-100 rounded-xl px-4 py-2 border border-slate-200 focus-within:border-indigo-500 focus-within:bg-white transition">
                        <label for="fileAttachment" class="cursor-pointer text-slate-400 hover:text-slate-600">
                            <i data-lucide="paperclip" class="w-5 h-5"></i>
                            <input type="file" id="fileAttachment" class="hidden" onchange="handleFileSelect(this)">
                        </label>
                        <input type="text" id="chatInput" autocomplete="off" placeholder="Nhập tin nhắn thảo luận nhóm (Nhấn Enter để gửi)..." class="flex-1 bg-transparent border-none outline-none text-sm text-slate-800 placeholder-slate-400">
                        <span id="fileIndicator" class="hidden text-xs bg-indigo-100 text-indigo-700 px-2 py-0.5 rounded">Tệp đã chọn</span>
                        <button type="submit" class="bg-indigo-600 hover:bg-indigo-700 text-white p-2 rounded-lg transition">
                            <i data-lucide="send" class="w-4 h-4"></i>
                        </button>
                    </form>
                </div>
            </div>

            <!-- VIDEO CONFERENCE ROOM VIEW (Initially Hidden) -->
            <div id="videoRoomView" class="flex-1 bg-slate-950 text-white flex-col hidden relative">
                <!-- Video Header -->
                <div class="p-4 bg-slate-900/80 backdrop-blur border-b border-slate-800 flex justify-between items-center z-10">
                    <div>
                        <h2 id="videoRoomTitle" class="font-bold text-white flex items-center gap-2">
                            <span class="w-2.5 h-2.5 rounded-full bg-red-500 animate-pulse"></span>
                            Đang trong phòng họp: Báo cáo Tiến độ Sprint 2
                        </h2>
                        <p class="text-xs text-slate-400">Mã phòng: <span id="roomCodeDisplay" class="font-mono text-indigo-400">CS-PBL-9082</span></p>
                    </div>
                    <div class="flex items-center gap-2 text-xs text-slate-400">
                        <i data-lucide="users" class="w-4 h-4"></i> 3 người đang tham gia
                    </div>
                </div>

                <!-- Video Grid -->
                <div class="flex-1 p-4 grid grid-cols-2 gap-4 items-center justify-center overflow-hidden">
                    <!-- Local Stream Container -->
                    <div class="relative bg-slate-900 rounded-2xl border border-slate-800 overflow-hidden flex items-center justify-center h-full max-h-[380px]">
                        <video id="localVideo" autoplay playsinline muted class="w-full h-full object-cover"></video>
                        <div id="localAvatarPlaceholder" class="absolute flex flex-col items-center justify-center hidden">
                            <div class="w-20 h-20 rounded-full bg-indigo-600 text-white text-2xl font-bold flex items-center justify-center mb-2">You</div>
                            <span id="localCamName" class="text-sm font-medium text-slate-300">Bạn (Camera đang tắt)</span>
                        </div>
                        <div class="absolute bottom-3 left-3 bg-slate-900/80 backdrop-blur px-3 py-1 rounded-full text-xs text-white font-medium flex items-center gap-1.5">
                            <span id="localMicIcon" class="w-2 h-2 rounded-full bg-emerald-500"></span> Bạn (Chủ trì)
                        </div>
                    </div>

                    <!-- Screen Share / Remote Stream Container -->
                    <div class="relative bg-slate-900 rounded-2xl border border-slate-800 overflow-hidden flex items-center justify-center h-full max-h-[380px]">
                        <video id="screenVideo" autoplay playsinline class="w-full h-full object-cover hidden"></video>
                        <div id="remotePlaceholder" class="flex flex-col items-center justify-center text-center p-6">
                            <div class="w-20 h-20 rounded-full bg-emerald-700 text-white text-2xl font-bold flex items-center justify-center mb-3">LH</div>
                            <h4 class="font-semibold text-sm text-slate-200">TS. Lê Hoàng (Giảng viên)</h4>
                            <p class="text-xs text-slate-400 mt-1">Sẵn sàng lắng nghe & đặt câu hỏi phản biện</p>
                        </div>
                        <div class="absolute bottom-3 left-3 bg-slate-900/80 backdrop-blur px-3 py-1 rounded-full text-xs text-white font-medium">
                            TS. Lê Hoàng
                        </div>
                    </div>
                </div>

                <!-- Video Control Bar -->
                <div class="p-4 bg-slate-900 border-t border-slate-800 flex justify-center items-center gap-4 z-10">
                    <button id="toggleMicBtn" onclick="toggleMicrophone()" class="w-12 h-12 rounded-full bg-slate-800 hover:bg-slate-700 flex items-center justify-center text-white transition">
                        <i data-lucide="mic" id="micIcon" class="w-5 h-5"></i>
                    </button>
                    <button id="toggleCamBtn" onclick="toggleCamera()" class="w-12 h-12 rounded-full bg-slate-800 hover:bg-slate-700 flex items-center justify-center text-white transition">
                        <i data-lucide="video" id="camIcon" class="w-5 h-5"></i>
                    </button>
                    <button id="screenShareBtn" onclick="toggleScreenShare()" class="w-12 h-12 rounded-full bg-indigo-600 hover:bg-indigo-700 flex items-center justify-center text-white transition" title="Chia sẻ màn hình">
                        <i data-lucide="monitor" class="w-5 h-5"></i>
                    </button>
                    <button onclick="leaveMeeting()" class="px-6 h-12 rounded-full bg-red-600 hover:bg-red-700 font-semibold text-white flex items-center gap-2 transition">
                        <i data-lucide="phone-off" class="w-5 h-5"></i> Rời phòng
                    </button>
                </div>
            </div>

        </main>
    </div>

    <!-- SCHEDULE MEETING MODAL -->
    <div id="scheduleModal" class="fixed inset-0 bg-slate-900/60 backdrop-blur-sm flex items-center justify-center hidden z-50">
        <div class="bg-white rounded-2xl w-full max-w-md p-6 shadow-2xl animate-fade-in">
            <div class="flex justify-between items-center mb-4">
                <h3 class="font-bold text-slate-800 text-lg">Lên lịch cuộc họp mới</h3>
                <button onclick="closeScheduleModal()" class="text-slate-400 hover:text-slate-600"><i data-lucide="x" class="w-5 h-5"></i></button>
            </div>
            <form onsubmit="handleScheduleMeeting(event)" class="space-y-4">
                <div>
                    <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Tiêu đề cuộc họp</label>
                    <input type="text" id="meetTitle" required placeholder="Ví dụ: Báo cáo tiến độ Sprint 2" class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600">
                </div>
                <div class="grid grid-cols-2 gap-3">
                    <div>
                        <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Thời gian bắt đầu</label>
                        <input type="datetime-local" id="meetStart" required class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600">
                    </div>
                    <div>
                        <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Thời gian kết thúc</label>
                        <input type="datetime-local" id="meetEnd" required class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600">
                    </div>
                </div>
                <div>
                    <label class="block text-xs font-bold text-slate-600 uppercase mb-1">Ghi chú / Nội dung cuộc họp</label>
                    <textarea id="meetDesc" rows="2" placeholder="Nội dung thảo luận..." class="w-full border border-slate-300 rounded-lg px-3 py-2 text-sm outline-none focus:border-indigo-600"></textarea>
                </div>
                <div class="flex justify-end gap-2 pt-2">
                    <button type="button" onclick="closeScheduleModal()" class="px-4 py-2 text-sm font-medium text-slate-600 hover:bg-slate-100 rounded-lg">Hủy</button>
                    <button type="submit" class="px-4 py-2 text-sm font-medium bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 shadow-sm">Xác nhận tạo</button>
                </div>
            </form>
        </div>
    </div>

    <!-- APP ENGINE (JAVASCRIPT) -->
    <script>
        // State Management
        let currentUser = { id: 101, name: "Nguyễn Văn A", role: "Student (Leader)", initials: "A" };
        let activeChannel = "proj-501";
        let activeTab = "chat";
        let mediaStream = null;
        let screenStream = null;
        let isMicOn = true;
        let isCamOn = true;

        // Mock Data Database
        const mockUsers = {
            student1: { id: 101, name: "Nguyễn Văn A", role: "Student (Leader)", initials: "A" },
            student2: { id: 102, name: "Trần Thị B", role: "Student (Thành viên)", initials: "B" },
            lecturer: { id: 12, name: "TS. Lê Hoàng", role: "Lecturer (GVHD)", initials: "LH" }
        };

        let messagesStore = {
            'proj-501': [
                { senderId: 102, senderName: "Trần Thị B", text: "Chào mọi người, tuần này mình đã hoàn thành giao diện FE03 rồi nhé!", time: "09:15 AM", attachment: null },
                { senderId: 12, senderName: "TS. Lê Hoàng", text: "Tốt lắm nhóm 1. Nhớ kiểm tra lại Business Rule về việc thêm sinh viên trùng nhé.", time: "09:30 AM", attachment: null },
                { senderId: 101, senderName: "Nguyễn Văn A", text: "Vâng thưa thầy, bọn em sẽ tích hợp luôn cả mô-đun họp trực tuyến FE07 trong hôm nay ạ.", time: "09:42 AM", attachment: { name: "FE07_SRS_v1.pdf", type: "pdf" } }
            ],
            'direct-lecturer': [
                { senderId: 101, senderName: "Nguyễn Văn A", text: "Em chào thầy ạ, thầy cho nhóm em xin thêm 1 buổi họp online phản biện bài tập tuần tới với ạ.", time: "Hôm qua", attachment: null }
            ]
        };

        let meetingsStore = [
            { id: 1, title: "Báo cáo Tiến độ Sprint 2", start: "2026-10-28T09:00", end: "2026-10-28T10:30", host: "Nguyễn Văn A", roomCode: "CS-PBL-9082", status: "SCHEDULED" },
            { id: 2, title: "Duyệt Đồ án cuối kỳ với GVHD", start: "2026-11-02T14:00", end: "2026-11-02T15:30", host: "TS. Lê Hoàng", roomCode: "CS-PBL-1102", status: "SCHEDULED" }
        ];

        // Initialize App
        window.addEventListener('DOMContentLoaded', () => {
            lucide.createIcons();
            renderMessages();
            renderMeetings();
        });

        // Switch User Role Simulation
        function switchUser(roleKey) {
            currentUser = mockUsers[roleKey];
            document.getElementById('userName').textContent = currentUser.name;
            document.getElementById('userRole').textContent = currentUser.role;
            document.getElementById('userAvatar').textContent = currentUser.initials;
            renderMessages();
        }

        // Sidebar Tab Switching
        function switchTab(tab) {
            activeTab = tab;
            const chatBtn = document.getElementById('tabChatBtn');
            const meetBtn = document.getElementById('tabMeetingBtn');
            const chatSide = document.getElementById('chatSidebar');
            const meetSide = document.getElementById('meetingSidebar');

            if(tab === 'chat') {
                chatBtn.className = "flex-1 py-3 text-sm font-semibold text-indigo-600 border-b-2 border-indigo-600 flex justify-center items-center gap-2";
                meetBtn.className = "flex-1 py-3 text-sm font-semibold text-slate-500 hover:text-slate-700 flex justify-center items-center gap-2";
                chatSide.classList.remove('hidden');
                meetSide.classList.add('hidden');
            } else {
                meetBtn.className = "flex-1 py-3 text-sm font-semibold text-indigo-600 border-b-2 border-indigo-600 flex justify-center items-center gap-2";
                chatBtn.className = "flex-1 py-3 text-sm font-semibold text-slate-500 hover:text-slate-700 flex justify-center items-center gap-2";
                meetSide.classList.remove('hidden');
                chatSide.classList.add('hidden');
            }
        }

        // Channel Selection
        function selectChannel(chanId) {
            activeChannel = chanId;
            document.getElementById('currentChannelTitle').innerHTML = chanId === 'proj-501' 
                ? `<i data-lucide="hash" class="w-5 h-5 text-indigo-600"></i> Nhóm 1 - Project PBL`
                : `<i data-lucide="user" class="w-5 h-5 text-indigo-600"></i> TS. Lê Hoàng (GVHD)`;
            
            // Highlight active state
            document.getElementById('chan-proj-501').className = chanId === 'proj-501' ? "w-full text-left px-3 py-2.5 rounded-lg bg-indigo-50 text-indigo-700 font-semibold flex items-center justify-between" : "w-full text-left px-3 py-2 rounded-lg text-slate-600 hover:bg-slate-100 flex items-center gap-2 text-sm";
            document.getElementById('chan-direct-lecturer').className = chanId === 'direct-lecturer' ? "w-full text-left px-3 py-2.5 rounded-lg bg-indigo-50 text-indigo-700 font-semibold flex items-center justify-between" : "w-full text-left px-3 py-2 rounded-lg text-slate-600 hover:bg-slate-100 flex items-center gap-2 text-sm";
            
            lucide.createIcons();
            renderMessages();
        }

        // Render Chat Messages
        function renderMessages() {
            const container = document.getElementById('messageStream');
            container.innerHTML = '';
            const msgs = messagesStore[activeChannel] || [];

            msgs.forEach(m => {
                const isMe = m.senderId === currentUser.id;
                const msgEl = document.createElement('div');
                msgEl.className = `flex flex-col ${isMe ? 'items-end' : 'items-start'} animate-fade-in`;
                
                msgEl.innerHTML = `
                    <div class="flex items-center gap-2 mb-1">
                        <span class="text-xs font-bold ${isMe ? 'text-indigo-600' : 'text-slate-700'}">${m.senderName}</span>
                        <span class="text-[10px] text-slate-400">${m.time}</span>
                    </div>
                    <div class="max-w-md p-3 rounded-2xl text-sm ${isMe ? 'bg-indigo-600 text-white rounded-br-none' : 'bg-white border border-slate-200 text-slate-800 rounded-bl-none shadow-sm'}">
                        ${m.text}
                        ${m.attachment ? `
                            <div class="mt-2 p-2 rounded bg-slate-900/10 flex items-center gap-2 text-xs">
                                <i data-lucide="file-text" class="w-4 h-4"></i>
                                <span class="font-medium underline cursor-pointer">${m.attachment.name}</span>
                            </div>
                        ` : ''}
                    </div>
                `;
                container.appendChild(msgEl);
            });
            container.scrollTop = container.scrollHeight;
            lucide.createIcons();
        }

        // Send Message Handler
        function handleSendMessage(e) {
            e.preventDefault();
            const input = document.getElementById('chatInput');
            const fileInd = document.getElementById('fileIndicator');
            const fileInput = document.getElementById('fileAttachment');
            const text = input.value.trim();

            if(!text && fileInput.files.length === 0) return;

            let attachment = null;
            if(fileInput.files.length > 0) {
                attachment = { name: fileInput.files[0].name, type: 'file' };
            }

            const now = new Date();
            const timeStr = now.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

            messagesStore[activeChannel].push({
                senderId: currentUser.id,
                senderName: currentUser.name,
                text: text,
                time: timeStr,
                attachment: attachment
            });

            input.value = '';
            fileInput.value = '';
            fileInd.classList.add('hidden');
            renderMessages();
        }

        function handleFileSelect(input) {
            const ind = document.getElementById('fileIndicator');
            if(input.files.length > 0) ind.classList.remove('hidden');
        }

        // Render Meetings List
        function renderMeetings() {
            const container = document.getElementById('meetingListContainer');
            container.innerHTML = '';
            document.getElementById('meetingCount').textContent = meetingsStore.length;

            meetingsStore.forEach(m => {
                const el = document.createElement('div');
                el.className = "p-3 bg-white rounded-xl border border-slate-200 shadow-sm space-y-2";
                el.innerHTML = `
                    <div class="flex justify-between items-start">
                        <h4 class="font-bold text-sm text-slate-800 leading-tight">${m.title}</h4>
                        <span class="text-[10px] bg-indigo-50 text-indigo-600 font-semibold px-2 py-0.5 rounded">${m.roomCode}</span>
                    </div>
                    <div class="text-xs text-slate-500 space-y-1">
                        <p class="flex items-center gap-1"><i data-lucide="clock" class="w-3.5 h-3.5"></i> ${m.start.replace('T', ' ')}</p>
                        <p class="flex items-center gap-1"><i data-lucide="user" class="w-3.5 h-3.5"></i> Chủ trì: ${m.host}</p>
                    </div>
                    <button onclick="joinMeeting('${m.title}', '${m.roomCode}')" class="w-full mt-1 bg-emerald-600 hover:bg-emerald-700 text-white text-xs font-semibold py-1.5 rounded-lg flex justify-center items-center gap-1 transition">
                        <i data-lucide="video" class="w-3.5 h-3.5"></i> Tham gia ngay
                    </button>
                `;
                container.appendChild(el);
            });
            lucide.createIcons();
        }

        // Schedule Meeting Handlers
        function openScheduleModal() { document.getElementById('scheduleModal').classList.remove('hidden'); }
        function closeScheduleModal() { document.getElementById('scheduleModal').classList.add('hidden'); }

        function handleScheduleMeeting(e) {
            e.preventDefault();
            const title = document.getElementById('meetTitle').value;
            const start = document.getElementById('meetStart').value;
            const end = document.getElementById('meetEnd').value;

            const newMeet = {
                id: Date.now(),
                title: title,
                start: start,
                end: end,
                host: currentUser.name,
                roomCode: `CS-PBL-${Math.floor(1000 + Math.random() * 9000)}`,
                status: "SCHEDULED"
            };

            meetingsStore.push(newMeet);
            renderMeetings();
            closeScheduleModal();
            alert("Lên lịch cuộc họp thành công!");
        }

        // Real WebRTC Camera & Video Call Functionality
        async function startInstantCall() {
            joinMeeting("Cuộc họp nhanh (Instant Call)", `CS-PBL-${Math.floor(1000 + Math.random() * 9000)}`);
        }

        async function joinMeeting(title, code) {
            document.getElementById('chatView').classList.add('hidden');
            document.getElementById('videoRoomView').classList.remove('hidden');
            document.getElementById('videoRoomView').classList.add('flex');
            document.getElementById('videoRoomTitle').innerHTML = `<span class="w-2.5 h-2.5 rounded-full bg-red-500 animate-pulse"></span> Đang họp: ${title}`;
            document.getElementById('roomCodeDisplay').textContent = code;

            try {
                // Access Real WebRTC Media Stream
                mediaStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
                const localVideo = document.getElementById('localVideo');
                localVideo.srcObject = mediaStream;
                localVideo.classList.remove('hidden');
                document.getElementById('localAvatarPlaceholder').classList.add('hidden');
            } catch (err) {
                console.warn("Camera/Microphone permissions denied or not available:", err);
                document.getElementById('localVideo').classList.add('hidden');
                document.getElementById('localAvatarPlaceholder').classList.remove('hidden');
            }
        }

        function leaveMeeting() {
            if(mediaStream) {
                mediaStream.getTracks().forEach(track => track.stop());
            }
            if(screenStream) {
                screenStream.getTracks().forEach(track => track.stop());
            }

            document.getElementById('videoRoomView').classList.add('hidden');
            document.getElementById('videoRoomView').classList.remove('flex');
            document.getElementById('chatView').classList.remove('hidden');
        }

        function toggleMicrophone() {
            if (!mediaStream) return;
            isMicOn = !isMicOn;
            mediaStream.getAudioTracks().forEach(track => track.enabled = isMicOn);
            const btn = document.getElementById('toggleMicBtn');
            const icon = document.getElementById('localMicIcon');

            if (isMicOn) {
                btn.className = "w-12 h-12 rounded-full bg-slate-800 hover:bg-slate-700 flex items-center justify-center text-white transition";
                icon.className = "w-2 h-2 rounded-full bg-emerald-500";
            } else {
                btn.className = "w-12 h-12 rounded-full bg-red-600 hover:bg-red-700 flex items-center justify-center text-white transition";
                icon.className = "w-2 h-2 rounded-full bg-red-500";
            }
        }

        function toggleCamera() {
            if (!mediaStream) return;
            isCamOn = !isCamOn;
            mediaStream.getVideoTracks().forEach(track => track.enabled = isCamOn);
            const btn = document.getElementById('toggleCamBtn');
            const video = document.getElementById('localVideo');
            const placeholder = document.getElementById('localAvatarPlaceholder');

            if (isCamOn) {
                btn.className = "w-12 h-12 rounded-full bg-slate-800 hover:bg-slate-700 flex items-center justify-center text-white transition";
                video.classList.remove('hidden');
                placeholder.classList.add('hidden');
            } else {
                btn.className = "w-12 h-12 rounded-full bg-red-600 hover:bg-red-700 flex items-center justify-center text-white transition";
                video.classList.add('hidden');
                placeholder.classList.remove('hidden');
            }
        }

        // Real Screen Sharing using Display Media API
        async function toggleScreenShare() {
            try {
                if(!screenStream) {
                    screenStream = await navigator.mediaDevices.getDisplayMedia({ video: true });
                    const screenVideo = document.getElementById('screenVideo');
                    screenVideo.srcObject = screenStream;
                    screenVideo.classList.remove('hidden');
                    document.getElementById('remotePlaceholder').classList.add('hidden');
                    document.getElementById('screenShareBtn').className = "w-12 h-12 rounded-full bg-emerald-600 hover:bg-emerald-700 flex items-center justify-center text-white transition";

                    screenStream.getVideoTracks()[0].onended = () => {
                        stopScreenShare();
                    };
                } else {
                    stopScreenShare();
                }
            } catch(err) {
                console.warn("Screen share cancelled or failed:", err);
            }
        }

        function stopScreenShare() {
            if(screenStream) {
                screenStream.getTracks().forEach(track => track.stop());
                screenStream = null;
            }
            const screenVideo = document.getElementById('screenVideo');
            screenVideo.classList.add('hidden');
            document.getElementById('remotePlaceholder').classList.remove('hidden');
            document.getElementById('screenShareBtn').className = "w-12 h-12 rounded-full bg-indigo-600 hover:bg-indigo-700 flex items-center justify-center text-white transition";
        }
    </script>
</body>
</html>
