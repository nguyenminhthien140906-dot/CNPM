
from datetime import datetime, timezone

from sqlalchemy import text
from app.models.base import db


def analyze_team_progress(team_id):
    rows = db.session.execute(
        text("""
            SELECT status, deadline
            FROM tasks
            WHERE team_id = :team_id
        """),
        {"team_id": team_id}
    ).mappings().all()

    total = len(rows)
    completed = 0
    in_progress = 0
    pending = 0
    overdue = 0

    now = datetime.now(timezone.utc)

    for task in rows:
        status = task["status"]
        deadline = task["deadline"]

        if status == "COMPLETED":
            completed += 1
        elif status == "IN_PROGRESS":
            in_progress += 1
        else:
            pending += 1

        if (
            deadline is not None
            and status != "COMPLETED"
            and deadline.replace(tzinfo=timezone.utc) < now
        ):
            overdue += 1

    completion_rate = round(
        completed / total * 100, 2
    ) if total else 0

    if total == 0:
        assessment = "Nhóm chưa có công việc."
    elif overdue > 0:
        assessment = "Nhóm có công việc quá hạn, cần ưu tiên xử lý."
    elif completion_rate == 100:
        assessment = "Nhóm đã hoàn thành tất cả công việc."
    elif completion_rate >= 70:
        assessment = "Nhóm đang có tiến độ tốt."
    else:
        assessment = "Nhóm cần tiếp tục đẩy nhanh tiến độ."

    return {
        "team_id": team_id,
        "total_tasks": total,
        "completed_tasks": completed,
        "in_progress_tasks": in_progress,
        "pending_tasks": pending,
        "overdue_tasks": overdue,
        "completion_rate": completion_rate,
        "assessment": assessment
    }
