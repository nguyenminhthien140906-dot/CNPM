
from flask import Blueprint, jsonify
from app.services.progress_service import analyze_team_progress

progress_bp = Blueprint("progress", __name__)


@progress_bp.route("/api/ai/teams/<int:team_id>/progress", methods=["GET"])
def get_team_progress(team_id):
    try:
        result = analyze_team_progress(team_id)

        return jsonify({
            "success": True,
            "data": result
        }), 200

    except Exception:
        from flask import current_app
        current_app.logger.exception("Progress analysis failed")

        return jsonify({
            "success": False,
            "message": "Không thể phân tích tiến độ nhóm."
        }), 500
