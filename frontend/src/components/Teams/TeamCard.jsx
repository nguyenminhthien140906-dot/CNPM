
import React from "react";

const TeamCard = ({
    team,
    onClick,
    onEdit
}) => {
    if (!team) return null;

    const name = team.teamName ?? team.TeamName ?? "Nhóm";
    const members = team.members ?? team.Members ?? [];

    return (
        <div className="team-card">
            <div className="team-icon">👥</div>

            <h3>{name}</h3>

            <p>
                Số thành viên: {members.length}
            </p>

            <div className="team-members">
                {members.slice(0, 4).map((member, index) => (
                    <span key={member.userId ?? member.UserId ?? index}>
                        {(member.fullName ?? member.FullName ?? member.username ?? "?")
                            .charAt(0).toUpperCase()}
                    </span>
                ))}
            </div>

            <div className="team-actions">
                <button onClick={() => onClick?.(team)}>
                    Xem nhóm
                </button>

                <button onClick={() => onEdit?.(team)}>
                    Chỉnh sửa
                </button>
            </div>
        </div>
    );
};

export default TeamCard;
