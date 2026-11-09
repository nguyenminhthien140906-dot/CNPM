#!/usr/bin/env bash
# =========================================================
# CollabSphere - MySQL database setup script
# Location: database/scripts/run_mysql.sh
# Requires: MySQL client tools (mysql), MySQL 8.0+
# =========================================================

set -Eeuo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
DATABASE_DIR="$(cd -- "${SCRIPT_DIR}/.." && pwd)"
SCHEMA_FILE="${DATABASE_DIR}/schema.sql"

DB_NAME="${MYSQL_DATABASE:-collabsphere}"
DB_USER="${MYSQL_USER:-root}"
DB_HOST="${MYSQL_HOST:-localhost}"
DB_PORT="${MYSQL_PORT:-3306}"

log() { printf '[CollabSphere DB] %s\n' "$*"; }
fail() { printf '[CollabSphere DB] ERROR: %s\n' "$*" >&2; exit 1; }

# Validate database name before using it as a command-line argument.
[[ "$DB_NAME" =~ ^[A-Za-z_][A-Za-z0-9_]*$ ]] || fail "Invalid database name: ${DB_NAME}"

command -v mysql >/dev/null 2>&1 || fail "mysql was not found. Install MySQL client tools and add them to PATH."
[[ -f "$SCHEMA_FILE" ]] || fail "MySQL schema not found: ${SCHEMA_FILE}"

MYSQL_ARGS=(-h "$DB_HOST" -P "$DB_PORT" -u "$DB_USER")

log "Project database folder: ${DATABASE_DIR}"
log "Host=${DB_HOST} Port=${DB_PORT} User=${DB_USER} Database=${DB_NAME}"
log "Checking MySQL connection..."

if ! mysql "${MYSQL_ARGS[@]}" -Nse 'SELECT 1' >/dev/null 2>&1; then
    fail "Cannot connect to MySQL. Check server status, MYSQL_HOST, MYSQL_PORT, MYSQL_USER and authentication."
fi

log "Checking whether database exists..."

DB_EXISTS="$(mysql "${MYSQL_ARGS[@]}" -Nse \
    "SELECT EXISTS (SELECT 1 FROM information_schema.schemata WHERE schema_name = '${DB_NAME}')")"

if [[ "$DB_EXISTS" != "1" ]]; then
    log "Creating database ${DB_NAME}..."
    mysql "${MYSQL_ARGS[@]}" -e \
        "CREATE DATABASE \`${DB_NAME}\` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"
else
    log "Database ${DB_NAME} already exists; it will not be dropped."
fi

log "Applying MySQL schema..."

mysql "${MYSQL_ARGS[@]}" "$DB_NAME" < "$SCHEMA_FILE"

TABLE_COUNT="$(mysql "${MYSQL_ARGS[@]}" -Nse \
    "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '${DB_NAME}' AND table_type = 'BASE TABLE'")"

log "Done. Database=${DB_NAME}; tables=${TABLE_COUNT}."
