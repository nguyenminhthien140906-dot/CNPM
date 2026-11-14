
from flask import Blueprint, jsonify, current_app
from app.services.contribution_service import analyze_team_contributions

contribution_bp = Blueprint("contribution", __name__)


@contribution_bp.route(
    "/api/ai/teams/<int:team_id>/contributions",
    methods=["GET"]
)
def get_team_contributions(team_id):
    try:
        result = analyze_team_contributions(team_id)

        return jsonify({
            "success": True,
            "data": result
        }), 200

    except Exception:
        current_app.logger.exception(
            "Failed to analyze team contributions"
        )

        return jsonify({
            "success": False,
            "message": "Không thể phân tích đóng góp thành viên."
        }), 500
