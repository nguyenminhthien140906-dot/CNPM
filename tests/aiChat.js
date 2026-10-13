const processUserMessage = async (message) => {
  const lowerMsg = message.toLowerCase();

  if (lowerMsg.includes('tiến độ') || lowerMsg.includes('progress')) {
    return {
      type: 'ai_response',
      text: "🤖 [AI Bedrock Analysis]: Tiến độ hiện tại của module Realtime & Chat đã hoàn thành phần kết nối Socket.IO cơ bản. Cần tiếp tục tối ưu hóa kết nối WebRTC và hoàn thiện giao diện."
    };
  }


  return {
    type: 'ai_response',
    text: `🤖 [AI Assistant]: Tôi đã tiếp nhận yêu cầu: "${message}". Hệ thống đang phân tích và hỗ trợ bạn tối ưu hóa mã nguồn.`
  };
};

module.exports = { processUserMessage };