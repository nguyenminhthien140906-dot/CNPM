
from app.models.base import db

from app.models.identity import Role, User, Student, Lecturer
from app.models.academic import (
    Subject, Syllabus, StudyClass,
    ClassStudent, ClassLecturer
)
from app.models.projects import Project, Team, TeamMember
from app.models.tasks import Milestone, Checkpoint, Task, TaskAssignee
from app.models.collaboration import (
    Submission, Resource, Meeting,
    MeetingParticipant, Message
)
from app.models.workspace import (
    Workspace, WhiteboardSnapshot,
    WorkspaceDocument, DocumentVersion
)
from app.models.evaluation import Evaluation
from app.models.notification import Notification
from app.models.ai import AIConversation, AIMessage

__all__ = [
    "db",
    "Role", "User", "Student", "Lecturer",
    "Subject", "Syllabus", "StudyClass",
    "ClassStudent", "ClassLecturer",
    "Project", "Team", "TeamMember",
    "Milestone", "Checkpoint", "Task", "TaskAssignee",
    "Submission", "Resource", "Meeting",
    "MeetingParticipant", "Message",
    "Workspace", "WhiteboardSnapshot",
    "WorkspaceDocument", "DocumentVersion",
    "Evaluation", "Notification",
    "AIConversation", "AIMessage",
]
