
from sqlalchemy import text
from app.models.base import db


def analyze_team_contributions(team_id):
    """Thống kê đóng góp thành viên dựa trên task được giao."""

    sql = text("""
        SELECT
            s.student_id,
            u.full_name,
            COUNT(DISTINCT t.task_id) AS total_tasks,
            COUNT(DISTINCT CASE
                WHEN t.status = 'COMPLETED' THEN t.task_id
            END) AS completed_tasks,
            COUNT(DISTINCT CASE
                WHEN t.status = 'IN_PROGRESS' THEN t.task_id
            END) AS in_progress_tasks,
            COUNT(DISTINCT CASE
                WHEN t.status = 'TODO' THEN t.task_id
            END) AS pending_tasks
        FROM task_assignees ta
        JOIN tasks t ON t.task_id = ta.task_id
        JOIN students s ON s.student_id = ta.student_id
        JOIN users u ON u.user_id = s.user_id
        WHERE t.team_id = :team_id
        GROUP BY s.student_id, u.full_name
        ORDER BY s.student_id
    """)

    rows = db.session.execute(
        sql, {"team_id": team_id}
    ).mappings().all()

    members = []

    for row in rows:
        total = int(row["total_tasks"])
        completed = int(row["completed_tasks"])
        rate = round(completed / total * 100, 2) if total else 0

        members.append({
            "student_id": row["student_id"],
            "full_name": row["full_name"],
            "total_tasks": total,
            "completed_tasks": completed,
            "in_progress_tasks": int(row["in_progress_tasks"]),
            "pending_tasks": int(row["pending_tasks"]),
            "completion_rate": rate,
            "assessment": (
                "Đã hoàn thành tất cả công việc được giao."
                if rate == 100
                else "Đã hoàn thành một phần công việc được giao."
                if completed > 0
                else "Chưa có công việc được ghi nhận hoàn thành."
            )
        })

    return {
        "team_id": team_id,
        "total_members_with_tasks": len(members),
        "members": members,
        "note": "Thống kê chỉ mang tính tham khảo, không phải điểm đánh giá chính thức."
    }