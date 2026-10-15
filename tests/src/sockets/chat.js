```javascript
module.exports = function (io) {
  io.on('connection', (socket) => {
    console.log('Có người dùng kết nối:', socket.id);

    socket.on('chat_message', (message) => {
      if (!message) return;

      io.emit('chat_message', {
        userId: socket.id,
        message: message,
        timestamp: new Date().toISOString()
      });
    });

    socket.on('disconnect', () => {
      console.log('Người dùng đã ngắt kết nối:', socket.id);
    });
  });
};
```