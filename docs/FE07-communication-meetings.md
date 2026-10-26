{
  "projectId": 101,
  "title": "Họp Review Tiến độ Sprint 2",
  "startTime": "2026-10-28T09:00:00Z",
  "durationMinutes": 45
}
{
  "id": "meet_001",
  "projectId": 101,
  "title": "Họp Review Tiến độ Sprint 2",
  "hostId": 12,
  "startTime": "2026-10-28T09:00:00Z",
  "durationMinutes": 45,
  "roomId": "room_abc123",
  "status": "SCHEDULED"
}
flowchart TD
    A[Người dùng bắt đầu] --> B{Có thuộc dự án?}
    B -- Không --> C[Trả về lỗi quyền truy cập]
    B -- Có --> D[Chọn thao tác: Chat / Lên lịch / Vào họp]
    D --> E[Kiểm tra dữ liệu đầu vào]
    E -- Không hợp lệ --> F[Thông báo lỗi và yêu cầu nhập lại]
    E -- Hợp lệ --> G[Thực hiện nghiệp vụ FE7]
    G --> H[Phát tín hiệu Realtime / Lưu CSDL & Cập nhật giao diện]
