from flask import Flask, jsonify
from flask_cors import CORS
from config.database import Config
from app.models.base import db

def create_app():
    app = Flask(__name__)

    app.config.from_object(Config)

    CORS(app)
    db.init_app(app)

    @app.route("/", methods=["GET"])
    def home():
        return jsonify({
            "success": True,
            "message": "CollabSphere AI Agent is running"
        })

    @app.route("/health", methods=["GET"])
    def health():
        try:
            from sqlalchemy import text

            with db.engine.connect() as connection:
                connection.execute(text("SELECT 1"))

            return jsonify({
                "success": True,
                "status": "healthy",
                "database": "connected"
            })
        except Exception:
            app.logger.exception("Database connection failed")
            return jsonify({
                "success": False,
                "status": "unhealthy",
                "database": "disconnected"
            }), 503

    return app
