-- CollabSphere
CREATE SCHEMA IF NOT EXISTS collabsphere;

CREATE TABLE collabsphere.roles (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE collabsphere.users (
    id UUID PRIMARY KEY,
    role_id INTEGER NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    full_name VARCHAR(150) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,

    CONSTRAINT fk_users_role
        FOREIGN KEY (role_id)
        REFERENCES collabsphere.roles(id)
);
