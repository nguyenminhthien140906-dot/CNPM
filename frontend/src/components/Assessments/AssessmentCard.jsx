
import React from "react";

const AssessmentCard = ({
    assessment,
    onView,
    onEvaluate
}) => {
    if (!assessment) return null;

    const title =
        assessment.title ??
        assessment.Title ??
        "Đánh giá";

    const description =
        assessment.description ??
        assessment.Description ??
        "Chưa có mô tả";

    const score =
        assessment.score ??
        assessment.Score;

    const status =
        assessment.status ??
        assessment.Status ??
        "Chưa đánh giá";

    return (
        <div className="assessment-card">
            <div className="assessment-card-header">
                <h3>{title}</h3>
                <span>{status}</span>
            </div>

            <p>{description}</p>

            <div className="assessment-score">
                <strong>Điểm đánh giá</strong>

                <span>
                    {score !== undefined && score !== null
                        ? `${score}`
                        : "Chưa có điểm"}
                </span>
            </div>

            <div className="assessment-actions">
                <button onClick={() => onView?.(assessment)}>
                    Chi tiết
                </button>

                <button onClick={() => onEvaluate?.(assessment)}>
                    Đánh giá
                </button>
            </div>
        </div>
    );
};

export default AssessmentCard;
