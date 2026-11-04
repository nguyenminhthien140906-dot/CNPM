#!/usr/bin/env bash
# =========================================================
# CollabSphere - PostgreSQL database setup script
# Location: database/scripts/run_postgres.sh
# Requires: PostgreSQL client tools (psql, createdb), PostgreSQL 14+
# =========================================================

set -Eeuo pipefail

SCRIPT_DIR="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
DATABASE_DIR="$(cd -- "${SCRIPT_DIR}/.." && pwd)"
SCHEMA_FILE="${DATABASE_DIR}/schema_postgres.sql"

DB_NAME="${PGDATABASE:-collabsphere}"
DB_USER="${PGUSER:-postgres}"
DB_HOST="${PGHOST:-localhost}"
DB_PORT="${PGPORT:-5432}"
MAINTENANCE_DB="${PGMAINTENANCE_DB:-postgres}"

log() { printf '[CollabSphere DB] %s\n' "$*"; }
fail() { printf '[CollabSphere DB] ERROR: %s\n' "$*" >&2; exit 1; }

# Validate database name before using it as a command-line argument.
[[ "$DB_NAME" =~ ^[A-Za-z_][A-Za-z0-9_]*$ ]] || fail "Invalid database name: ${DB_NAME}"

command -v psql >/dev/null 2>&1 || fail "psql was not found. Install PostgreSQL client tools and add them to PATH."
command -v createdb >/dev/null 2>&1 || fail "createdb was not found. Install PostgreSQL client tools and add them to PATH."
[[ -f "$SCHEMA_FILE" ]] || fail "PostgreSQL schema not found: ${SCHEMA_FILE}"

PG_ARGS=(-h "$DB_HOST" -p "$DB_PORT" -U "$DB_USER")

log "Project database folder: ${DATABASE_DIR}"
log "Host=${DB_HOST} Port=${DB_PORT} User=${DB_USER} Database=${DB_NAME}"
log "Checking PostgreSQL connection..."

if ! psql "${PG_ARGS[@]}" -d "$MAINTENANCE_DB" -v ON_ERROR_STOP=1 -Atqc 'SELECT 1' >/dev/null; then
    fail "Cannot connect to PostgreSQL. Check server status, PGHOST, PGPORT, PGUSER and authentication."
fi

log "Checking whether database exists..."
DB_EXISTS="$(psql "${PG_ARGS[@]}" -d "$MAINTENANCE_DB" -v ON_ERROR_STOP=1 \
    -v db_name="$DB_NAME" \
    -Atqc "SELECT EXISTS (SELECT 1 FROM pg_database WHERE datname = :'db_name')")"

if [[ "$DB_EXISTS" != "t" ]]; then
    log "Creating database ${DB_NAME}..."
    createdb "${PG_ARGS[@]}" "$DB_NAME"
else
    log "Database ${DB_NAME} already exists; it will not be dropped."
fi

log "Applying PostgreSQL schema..."
psql "${PG_ARGS[@]}" -d "$DB_NAME" -v ON_ERROR_STOP=1 -f "$SCHEMA_FILE"

TABLE_COUNT="$(psql "${PG_ARGS[@]}" -d "$DB_NAME" -v ON_ERROR_STOP=1 -Atqc \
    "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE'")"

log "Done. Database=${DB_NAME}; public tables=${TABLE_COUNT}."
