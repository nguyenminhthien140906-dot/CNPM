
USE collabsphere;

-- ADMIN
INSERT INTO users
    (role_id, username, email, password_hash, full_name, phone, is_active)
SELECT
    role_id,
    'admin',
    'admin@collabsphere.local',
    'REPLACE_WITH_VALID_PASSWORD_HASH',
    'System Administrator',
    '0900000001',
    TRUE
FROM roles
WHERE role_code = 'ADMIN'
AND NOT EXISTS (
    SELECT 1 FROM users WHERE username = 'admin'
);

-- HEAD
INSERT INTO users
    (role_id, username, email, password_hash, full_name, phone, is_active)
SELECT
    role_id,
    'head01',
    'head01@collabsphere.local',
    'REPLACE_WITH_VALID_PASSWORD_HASH',
    'Department Head',
    '0900000002',
    TRUE
FROM roles
WHERE role_code = 'HEAD'
AND NOT EXISTS (
    SELECT 1 FROM users WHERE username = 'head01'
);

-- STAFF
INSERT INTO users
    (role_id, username, email, password_hash, full_name, phone, is_active)
SELECT
    role_id,
    'staff01',
    'staff01@collabsphere.local',
    'REPLACE_WITH_VALID_PASSWORD_HASH',
    'Academic Staff',
    '0900000003',
    TRUE
FROM roles
WHERE role_code = 'STAFF'
AND NOT EXISTS (
    SELECT 1 FROM users WHERE username = 'staff01'
);

-- LECTURER
INSERT INTO users
    (role_id, username, email, password_hash, full_name, phone, is_active)
SELECT
    role_id,
    'lecturer01',
    'lecturer01@collabsphere.local',
    'REPLACE_WITH_VALID_PASSWORD_HASH',
    'Nguyen Van Giang',
    '0900000004',
    TRUE
FROM roles
WHERE role_code = 'LECTURER'
AND NOT EXISTS (
    SELECT 1 FROM users WHERE username = 'lecturer01'
);

-- STUDENT 1
INSERT INTO users
    (role_id, username, email, password_hash, full_name, phone, is_active)
SELECT
    role_id,
    'student01',
    'student01@collabsphere.local',
    'REPLACE_WITH_VALID_PASSWORD_HASH',
    'Tran Van An',
    '0900000005',
    TRUE
FROM roles
WHERE role_code = 'STUDENT'
AND NOT EXISTS (
    SELECT 1 FROM users WHERE username = 'student01'
);

-- STUDENT 2
INSERT INTO users
    (role_id, username, email, password_hash, full_name, phone, is_active)
SELECT
    role_id,
    'student02',
    'student02@collabsphere.local',
    'REPLACE_WITH_VALID_PASSWORD_HASH',
    'Le Thi Binh',
    '0900000006',
    TRUE
FROM roles
WHERE role_code = 'STUDENT'
AND NOT EXISTS (
    SELECT 1 FROM users WHERE username = 'student02'
);

-- TAO HO SO GIANG VIEN
INSERT INTO lecturers (user_id, lecturer_code)
SELECT u.user_id, 'GV001'
FROM users u
WHERE u.username = 'lecturer01'
AND NOT EXISTS (
    SELECT 1 FROM lecturers l WHERE l.user_id = u.user_id
);

-- TAO HO SO SINH VIEN 1
INSERT INTO students (user_id, student_code)
SELECT u.user_id, 'SV001'
FROM users u
WHERE u.username = 'student01'
AND NOT EXISTS (
    SELECT 1 FROM students s WHERE s.user_id = u.user_id
);

-- TAO HO SO SINH VIEN 2
INSERT INTO students (user_id, student_code)
SELECT u.user_id, 'SV002'
FROM users u
WHERE u.username = 'student02'
AND NOT EXISTS (
    SELECT 1 FROM students s WHERE s.user_id = u.user_id
);

-- KIEM TRA
SELECT
    u.user_id,
    u.username,
    u.full_name,
    r.role_code
FROM users u
INNER JOIN roles r ON u.role_id = r.role_id
ORDER BY u.user_id;

SELECT * FROM lecturers;
SELECT * FROM students;

