
import React from "react";

const EmptyState = ({
    title = "Không có dữ liệu",
    description = "Hiện tại chưa có dữ liệu để hiển thị.",
    action,
    actionLabel = "Thử lại"
}) => {
    return (
        <div className="empty-state">
            <div className="empty-state-icon">📂</div>

            <h3>{title}</h3>

            <p>{description}</p>

            {action && (
                <button onClick={action}>
                    {actionLabel}
                </button>
            )}
        </div>
    );
};

export default EmptyState;
