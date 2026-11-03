from enum import Enum

class UserRole(Enum):
    ADMIN = "Admin"
    HEAD = "Head"
    STAFF = "Staff"
    LECTURER = "Lecturer"
    STUDENT = "Student"

class TaskStatus(Enum):
    TO_DO = "To Do"
    IN_PROGRESS = "In Progress"
    REVIEW = "Review"
    DONE = "Done"

class EvaluationType(Enum):
    LECTURER_EVAL = "Lecturer_To_Group"
    PEER_REVIEW = "Peer_Review"
