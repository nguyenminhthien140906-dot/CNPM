
import React from "react";

const ProjectCard = ({
    project,
    onClick,
    onEdit,
    onDelete
}) => {
    if (!project) return null;

    const name = project.projectName ?? project.ProjectName ?? "Dự án";
    const description =
        project.description ?? project.Description ?? "Chưa có mô tả";

    const status = project.status ?? project.Status ?? "Chưa xác định";

    return (
        <div className="project-card">
            <div className="project-card-header">
                <h3>{name}</h3>
                <span className="project-status">{status}</span>
            </div>

            <p>{description}</p>

            <div className="project-card-actions">
                <button onClick={() => onClick?.(project)}>
                    Chi tiết
                </button>

                <button onClick={() => onEdit?.(project)}>
                    Sửa
                </button>

                <button onClick={() => onDelete?.(project)}>
                    Xóa
                </button>
            </div>
        </div>
    );
};

export default ProjectCard;
