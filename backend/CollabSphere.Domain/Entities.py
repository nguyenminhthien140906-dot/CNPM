from datetime import datetime
from typing import List, Optional

# 1. Thực thể Người dùng (Tài khoản) - FE1
class User:
    def __init__(self, user_id: str, full_name: str, email: str, role: str):
        self.user_id = user_id
        self.full_name = full_name
        self.email = email
        self.role = role  # Các role: Admin, Head, Staff, Lecturer, Student
        self.is_active = True
        self.created_at = datetime.now()

    def deactivate(self):
        self.is_active = False

    def activate(self):
        self.is_active = True


# 2. Thực thể Nhóm - FE5
class Group:
    def __init__(self, group_id: str, name: str, course_id: str, leader_id: str):
        self.group_id = group_id
        self.name = name
        self.course_id = course_id
        self.leader_id = leader_id
        self.member_ids: List[str] = [leader_id] # Trưởng nhóm mặc định là thành viên
        self.created_at = datetime.now()

    def add_member(self, student_id: str):
        if student_id not in self.member_ids:
            self.member_ids.append(student_id)
            
    def remove_member(self, student_id: str):
        if student_id in self.member_ids and student_id != self.leader_id:
            self.member_ids.remove(student_id)


# 3. Thực thể Nhiệm vụ (Task) - FE5
class Task:
    def __init__(self, task_id: str, group_id: str, title: str, deadline: datetime):
        self.task_id = task_id
        self.group_id = group_id
        self.title = title
        self.deadline = deadline
        self.status = "To Do"  # Trạng thái: To Do, In Progress, Review, Done
        self.assignee_id: Optional[str] = None
        self.created_at = datetime.now()

    def assign_to(self, student_id: str):
        self.assignee_id = student_id
        self.status = "In Progress"

    def update_status(self, new_status: str):
        valid_statuses = ["To Do", "In Progress", "Review", "Done"]
        if new_status in valid_statuses:
            self.status = new_status


# 4. Thực thể Không gian làm việc (Workspace) - FE9
class Workspace:
    def __init__(self, workspace_id: str, group_id: str):
        self.workspace_id = workspace_id
        self.group_id = group_id
        self.whiteboard_data = {}
        self.document_data = {}
        self.last_updated = datetime.now()

    def save_snapshot(self, whiteboard_payload: dict, document_payload: dict):
        self.whiteboard_data = whiteboard_payload
        self.document_data = document_payload
        self.last_updated = datetime.now()


# 5. Thực thể Đánh giá (Evaluation) - FE10
class Evaluation:
    def __init__(self, eval_id: str, evaluator_id: str, target_id: str, eval_type: str, score: float, comment: str):
        self.eval_id = eval_id
        self.evaluator_id = evaluator_id
        self.target_id = target_id 
        self.eval_type = eval_type # 'Lecturer_To_Group' hoặc 'Peer_Review' (Sinh viên chấm chéo)
        self.score = score
        self.comment = comment
        self.created_at = datetime.now()
