
import React from "react";

const TaskCard = ({
    task,
    onComplete,
    onEdit,
    onDelete
}) => {
    if (!task) return null;

    const title = task.taskName ?? task.TaskName ?? "Nhiệm vụ";
    const description =
        task.description ?? task.Description ?? "";

    const status = task.status ?? task.Status ?? "Pending";

    return (
        <div className="task-card">
            <div className="task-card-top">
                <h4>{title}</h4>

                <span className={`task-status ${String(status).toLowerCase()}`}>
                    {status}
                </span>
            </div>

            <p>{description}</p>

            <div className="task-card-actions">
                <button onClick={() => onComplete?.(task)}>
                    Hoàn thành
                </button>

                <button onClick={() => onEdit?.(task)}>
                    Chỉnh sửa
                </button>

                <button onClick={() => onDelete?.(task)}>
                    Xóa
                </button>
            </div>
        </div>
    );
};

export default TaskCard;
