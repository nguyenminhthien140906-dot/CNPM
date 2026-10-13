module.exports = function(io) {
  io.on('connection', (socket) => {
    console.log('Có người kết nối:', socket.id);

    socket.on('join_room', (roomId) => {
      socket.join(roomId);
    });

    socket.on('send_message', (data) => {
      io.to(data.roomId).emit('receive_message', data);
    });

    socket.on('webrtc_offer', (data) => {
      socket.to(data.targetSocketId).emit('webrtc_offer', {
        sdp: data.sdp,
        callerId: socket.id
      });
    });

    socket.on('webrtc_answer', (data) => {
      socket.to(data.targetSocketId).emit('webrtc_answer', {
        sdp: data.sdp
      });
    });

    socket.on('webrtc_ice_candidate', (data) => {
      socket.to(data.targetSocketId).emit('webrtc_ice_candidate', {
        candidate: data.candidate
      });
    });

    socket.on('disconnect', () => {
      console.log('Ngắt kết nối:', socket.id);
    });
  });
};