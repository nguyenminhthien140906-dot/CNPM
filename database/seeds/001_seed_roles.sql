
USE collabsphere;

INSERT INTO roles (role_code, role_name, description)
VALUES
    ('ADMIN', 'Administrator', 'Quan tri vien he thong'),
    ('HEAD', 'Head Department', 'Truong bo phan'),
    ('STAFF', 'Staff', 'Nhan vien quan ly'),
    ('LECTURER', 'Lecturer', 'Giang vien'),
    ('STUDENT', 'Student', 'Sinh vien')
ON DUPLICATE KEY UPDATE
    role_name = VALUES(role_name),
    description = VALUES(description);

-- Kiem tra
SELECT role_id, role_code, role_name
FROM roles
ORDER BY role_id;

