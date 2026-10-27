import React, { useState, useEffect, useRef } from 'react';
import { io } from 'socket.io-client';
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';
import { Excalidraw } from '@excalidraw/excalidraw';

// Kết nối tới Backend Socket (Đổi URL này thành URL server của bạn sau này)
const socket = io('http://localhost:5000', { autoConnect: false });

const CollaborationWorkspace = ({ currentUser }) => {
  const [activeTab, setActiveTab] = useState('whiteboard'); // 'whiteboard' | 'editor'
  const [editorContent, setEditorContent] = useState('');
  const [onlineUsers, setOnlineUsers] = useState([]);
  const excalidrawRef = useRef(null);

  useEffect(() => {
    // 1. Khởi tạo kết nối Socket khi vào phòng làm việc
    socket.connect();
    
    // Gửi thông tin user tham gia phòng
    socket.emit('join_room', { roomId: 'project-123', user: currentUser?.name || 'Nguyễn Duy' });

    // 2. Lắng nghe danh sách người dùng online (Presence)
    socket.on('update_users', (users) => {
      setOnlineUsers(users);
    });

    // 3. Lắng nghe thay đổi văn bản từ người khác
    socket.on('receive_text_update', (content) => {
      setEditorContent(content);
    });

    // 4. Lắng nghe thay đổi trên bảng trắng (Whiteboard)
    socket.on('receive_whiteboard_update', (elements) => {
      if (excalidrawRef.current) {
        excalidrawRef.current.updateScene({ elements });
      }
    });

    // Cleanup khi rời khỏi component
    return () => {
      socket.disconnect();
    };
  }, [currentUser]);

  // Hàm xử lý khi gõ văn bản
  const handleEditorChange = (content) => {
    setEditorContent(content);
    // Gửi thay đổi lên server
    socket.emit('send_text_update', { roomId: 'project-123', content });
  };

  // Hàm xử lý khi vẽ trên bảng trắng
  const handleWhiteboardChange = (elements, state) => {
    // Chỉ gửi dữ liệu lên server nếu có thay đổi thực sự để tránh lag
    socket.emit('send_whiteboard_update', { roomId: 'project-123', elements });
  };

  return (
    <div style={styles.container}>
      {/* Thanh công cụ và Danh sách người dùng online */}
      <div style={styles.header}>
        <div style={styles.tabs}>
          <button 
            style={activeTab === 'whiteboard' ? styles.activeTabBtn : styles.tabBtn}
            onClick={() => setActiveTab('whiteboard')}
          >
            Bảng trắng & Sơ đồ
          </button>
          <button 
            style={activeTab === 'editor' ? styles.activeTabBtn : styles.tabBtn}
            onClick={() => setActiveTab('editor')}
          >
            Soạn thảo Văn bản
          </button>
        </div>
        
        {/* Hiển thị Presence (Ai đang online) */}
        <div style={styles.onlineUsers}>
          <span style={{ fontSize: '14px', marginRight: '10px' }}>Đang online:</span>
          {onlineUsers.length > 0 ? onlineUsers.map((user, index) => (
            <div key={index} style={styles.avatar} title={user}>
              {user.charAt(0).toUpperCase()}
            </div>
          )) : (
            <div style={styles.avatar} title="Nguyễn Duy">D</div> // Mock UI
          )}
        </div>
      </div>

      {/* Khu vực làm việc chính */}
      <div style={styles.workspace}>
        {activeTab === 'whiteboard' ? (
          <div style={{ height: '100%', width: '100%' }}>
            <Excalidraw 
              ref={excalidrawRef}
              onChange={handleWhiteboardChange}
              theme="light"
            />
          </div>
        ) : (
          <div style={styles.editorContainer}>
            <ReactQuill 
              theme="snow" 
              value={editorContent} 
              onChange={handleEditorChange} 
              style={{ height: 'calc(100% - 42px)' }}
            />
          </div>
        )}
      </div>
    </div>
  );
};

// CSS in JS
const styles = {
  container: { display: 'flex', flexDirection: 'column', height: '100vh', backgroundColor: '#f8fafc' },
  header: { 
    display: 'flex', justifyContent: 'space-between', alignItems: 'center', 
    padding: '10px 20px', backgroundColor: '#fff', borderBottom: '1px solid #e2e8f0',
    boxShadow: '0 1px 2px rgba(0,0,0,0.05)'
  },
  tabs: { display: 'flex', gap: '10px' },
  tabBtn: { 
    padding: '8px 16px', border: '1px solid #cbd5e1', backgroundColor: '#f1f5f9', 
    borderRadius: '6px', cursor: 'pointer', color: '#475569', fontWeight: '500' 
  },
  activeTabBtn: { 
    padding: '8px 16px', border: 'none', backgroundColor: '#3b82f6', 
    borderRadius: '6px', cursor: 'pointer', color: '#fff', fontWeight: 'bold',
    boxShadow: '0 2px 4px rgba(59, 130, 246, 0.3)'
  },
  onlineUsers: { display: 'flex', alignItems: 'center', gap: '5px' },
  avatar: { 
    width: '32px', height: '32px', borderRadius: '50%', backgroundColor: '#10b981', 
    color: '#fff', display: 'flex', alignItems: 'center', justifyContent: 'center', 
    fontWeight: 'bold', border: '2px solid #fff', boxShadow: '0 0 0 1px #cbd5e1', cursor: 'help'
  },
  workspace: { flex: 1, padding: '20px', overflow: 'hidden' },
  editorContainer: { height: '100%', backgroundColor: '#fff', borderRadius: '8px', overflow: 'hidden', boxShadow: '0 1px 3px rgba(0,0,0,0.1)' }
};

export default CollaborationWorkspace;
