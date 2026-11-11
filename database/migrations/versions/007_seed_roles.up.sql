
-- 007: Default role data

INSERT INTO roles (role_code, role_name, description)
VALUES
    ('ADMIN', 'Administrator', 'Quản trị viên hệ thống'),
    ('HEAD', 'Head Department', 'Trưởng bộ phận'),
    ('STAFF', 'Staff', 'Nhân viên quản lý'),
    ('LECTURER', 'Lecturer', 'Giảng viên'),
    ('STUDENT', 'Student', 'Sinh viên')
ON DUPLICATE KEY UPDATE
    role_code = VALUES(role_code);
