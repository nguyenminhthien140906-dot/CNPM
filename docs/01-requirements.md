# CollabSphere - Requirements Baseline

## 1. Actors

### Admin
- Xem tất cả tài khoản
- Vô hiệu hóa tài khoản
- Xem system reports qua email
- Dashboard

### Staff
- Import/manage subjects
- Import/manage syllabus
- Import/manage classes
- Import/manage lecturer/student accounts
- Xem subject/syllabus
- Xem class
- Xem lecturer/student
- Gán lecturer và student vào class

### Head Department
- Xem classes
- Xem subject/syllabus
- Xem projects theo trạng thái pending/approved/denied
- Approve/deny project
- Cập nhật approved project
- Assign project cho classes

### Lecturer
- Tạo/quản lý project theo syllabus
- Submit project để duyệt
- Assign project cho class
- Xem/manage classes
- Tạo/manage teams
- Manage team members
- Theo dõi progress và contribution
- Manage milestones/questions
- Xem checkpoint submissions
- Xem workspace
- Manage resources
- Evaluate/feedback
- Chat, meeting, schedule
- Realtime collaboration
- Chatbot
- AI phân tích progress/contribution là optional theo đề

### Student
- Xem classes/team/subject/syllabus
- Xem milestone/checkpoint
- Theo dõi progress/contribution
- Mark milestone/checkpoint done
- Trả lời milestone questions
- Tạo/submit checkpoint
- Workspace: cards/tasks/subtasks
- Manage resources
- Peer evaluation/feedback
- Chat, meeting, schedule
- Realtime collaboration
- Chatbot
- AI phân tích progress/contribution là optional theo đề

## 2. Core business flow

Staff -> tạo class/account/subject/syllabus
Lecturer -> tạo Project -> submit
Head Department -> approve/deny
Head Department -> assign Project cho Class
Lecturer -> chọn Project cho Team
Team Leader -> quản lý milestone/checkpoint/tasks
Team Members -> thực hiện và submit
Lecturer -> theo dõi/evaluate
Students -> peer evaluate
System -> notification/realtime collaboration

## 3. MVP ưu tiên

P0:
- Login + role
- User/account
- Subject/Syllabus
- Class
- Project approval
- Team/Team member
- Milestone
- Task/Subtask
- Checkpoint
- Evaluation/Feedback

P1:
- Resource management
- Notification
- Chat

P2:
- Video/audio meeting
- Screen sharing
- Whiteboard
- Realtime text editor
- AI assistance

Lưu ý: P0/P1/P2 là cách nhóm sắp xếp để triển khai; đề gốc không quy định mức ưu tiên này.
