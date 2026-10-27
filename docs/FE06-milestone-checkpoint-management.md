# FE6. Quản lý Milestone và Checkpoint

## 1. Mục tiêu
Hỗ trợ giảng viên và trưởng nhóm khởi tạo, quản lý các mốc tiến độ (Milestone) cho đồ án/môn học; thiết lập câu hỏi Milestone; phân công cụ thể thành viên phụ trách từng điểm kiểm tra (Checkpoint). Hệ thống cho phép sinh viên tải lên bài nộp (file/link), ghi nhận thời gian nộp và tự động cập nhật trạng thái hoàn thành công việc[cite: 6].

## 2. Vai trò liên quan
Lecturer (Giảng viên), Team Leader (Trưởng nhóm), Member (Thành viên nhóm).

## 3. Luồng nghiệp vụ
1. Người dùng truy cập giao diện quản lý mốc tiến độ của đồ án/môn học.
2. Hệ thống kiểm tra vai trò để phân quyền thao tác (Giảng viên/Trưởng nhóm thiết lập; Thành viên thực hiện bài nộp)[cite: 1, 6].
3. Hệ thống trả về danh sách các Milestone, danh sách câu hỏi và danh sách Checkpoint tương ứng kèm thời hạn (deadline)[cite: 1, 6].
4. Khi người dùng thực hiện các thao tác (tạo Milestone, gán Checkpoint cho thành viên, tải lên bài nộp), hệ thống xác thực dữ liệu, lưu thông tin bài nộp và cập nhật trạng thái hoàn thành[cite: 1, 6].

## 4. Quy tắc nghiệp vụ
- Giảng viên/Trưởng nhóm được quyền khởi tạo, chỉnh sửa các mốc Milestone, thời hạn nộp bài và tạo bộ câu hỏi yêu cầu giải trình cho từng mốc[cite: 6].
- Trưởng nhóm hoặc Giảng viên phân công cụ thể từng thành viên phụ trách hoàn thành các Checkpoint[cite: 6].
- Thành viên được phân công phải đính kèm tệp tin hoặc liên kết bài nộp và trả lời các câu hỏi Milestone bắt buộc trước khi bấm xác nhận nộp[cite: 6].
- Hệ thống tự động ghi nhận thời gian nộp bài; nếu quá hạn (Overdue), bài nộp sẽ bị đánh dấu nộp trễ nhưng vẫn cho phép lưu để phục vụ đánh giá.
- Không cho phép thành viên không được phân công tự ý chỉnh sửa thông tin bài nộp của người khác[cite: 1].

## 5. API dự kiến
> Đây là hợp đồng API đề xuất để nhóm thống nhất trước khi code, chưa xác nhận là đã được triển khai[cite: 2].

| Phương thức | Endpoint | Mục đích |
| :--- | :--- | :--- |
| **GET** | `/api/milestones` | Lấy danh sách Milestone và Checkpoint theo nhóm/đồ án |
| **POST** | `/api/milestones` | Giảng viên/Trưởng nhóm tạo mốc Milestone và câu hỏi |
| **POST** | `/api/checkpoints/{id}/assign` | Phân công thành viên thực hiện Checkpoint |
| **POST** | `/api/checkpoints/{id}/submit` | Thành viên đính kèm bài nộp và trả lời câu hỏi Milestone |
| **PATCH** | `/api/checkpoints/{id}/status` | Cập nhật trạng thái hoàn thành Checkpoint |

### Ví dụ dữ liệu JSON (Payload nộp bài Checkpoint)
```json
{
  "checkpointId": "CP_1002",
  "submittedBy": "ST12345",
  "submissionUrl": "[https://github.com/project/docs/phase1.pdf](https://github.com/project/docs/phase1.pdf)",
  "answers": [
    {
      "questionId": "Q_01",
      "answerText": "Đã hoàn thiện thiết kế Database và API hợp đồng"
    }
  ],
  "isCompleted": true
}
