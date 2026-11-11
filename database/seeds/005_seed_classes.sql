
USE collabsphere;

-- ==========================================
-- 1. TAO LOP CONG NGHE PHAN MEM
-- ==========================================

INSERT INTO study_classes
(
    subject_id,
    class_code,
    class_name,
    semester,
    academic_year,
    capacity,
    status
)
SELECT
    subject_id,
    'CNPM-01',
    'Cong nghe phan mem - Lop 01',
    'HK1',
    '2026-2027',
    40,
    'OPEN'
FROM subjects
WHERE subject_code = 'CNPM'
AND NOT EXISTS (
    SELECT 1
    FROM study_classes
    WHERE class_code = 'CNPM-01'
);

-- ==========================================
-- 2. TAO LOP CAU TRUC DU LIEU
-- ==========================================

INSERT INTO study_classes
(
    subject_id,
    class_code,
    class_name,
    semester,
    academic_year,
    capacity,
    status
)
SELECT
    subject_id,
    'CTDL-01',
    'Cau truc du lieu - Lop 01',
    'HK1',
    '2026-2027',
    40,
    'OPEN'
FROM subjects
WHERE subject_code = 'CTDL'
AND NOT EXISTS (
    SELECT 1
    FROM study_classes
    WHERE class_code = 'CTDL-01'
);

-- ==========================================
-- 3. TAO LOP PROJECT BASED LEARNING
-- ==========================================

INSERT INTO study_classes
(
    subject_id,
    class_code,
    class_name,
    semester,
    academic_year,
    capacity,
    status
)
SELECT
    subject_id,
    'PBL-01',
    'Project Based Learning - Lop 01',
    'HK1',
    '2026-2027',
    35,
    'OPEN'
FROM subjects
WHERE subject_code = 'PBL'
AND NOT EXISTS (
    SELECT 1
    FROM study_classes
    WHERE class_code = 'PBL-01'
);

-- ==========================================
-- 4. PHAN CONG GIANG VIEN VAO CAC LOP
-- ==========================================

INSERT INTO class_lecturers
(
    class_id,
    lecturer_id,
    assignment_role
)
SELECT
    c.class_id,
    l.lecturer_id,
    'LECTURER'
FROM study_classes c
CROSS JOIN lecturers l
INNER JOIN users u
    ON u.user_id = l.user_id
WHERE u.username = 'lecturer01'
AND c.class_code IN ('CNPM-01', 'CTDL-01', 'PBL-01')
AND NOT EXISTS (
    SELECT 1
    FROM class_lecturers cl
    WHERE cl.class_id = c.class_id
    AND cl.lecturer_id = l.lecturer_id
);

-- ==========================================
-- 5. DANG KY SINH VIEN VAO CAC LOP
-- ==========================================

INSERT INTO class_students
(
    class_id,
    student_id
)
SELECT
    c.class_id,
    s.student_id
FROM study_classes c
CROSS JOIN students s
INNER JOIN users u
    ON u.user_id = s.user_id
WHERE u.username IN ('student01', 'student02')
AND c.class_code IN ('CNPM-01', 'CTDL-01', 'PBL-01')
AND NOT EXISTS (
    SELECT 1
    FROM class_students cs
    WHERE cs.class_id = c.class_id
    AND cs.student_id = s.student_id
);

-- ==========================================
-- 6. KIEM TRA LOP HOC
-- ==========================================

SELECT
    c.class_id,
    c.class_code,
    c.class_name,
    sub.subject_name,
    c.semester,
    c.academic_year,
    c.capacity,
    c.status
FROM study_classes c
INNER JOIN subjects sub
    ON sub.subject_id = c.subject_id
ORDER BY c.class_id;

-- ==========================================
-- 7. KIEM TRA GIANG VIEN
-- ==========================================

SELECT
    c.class_code,
    u.full_name AS lecturer_name,
    cl.assignment_role
FROM class_lecturers cl
INNER JOIN study_classes c
    ON c.class_id = cl.class_id
INNER JOIN lecturers l
    ON l.lecturer_id = cl.lecturer_id
INNER JOIN users u
    ON u.user_id = l.user_id;

-- ==========================================
-- 8. KIEM TRA SINH VIEN
-- ==========================================

SELECT
    c.class_code,
    u.full_name AS student_name,
    s.student_code
FROM class_students cs
INNER JOIN study_classes c
    ON c.class_id = cs.class_id
INNER JOIN students s
    ON s.student_id = cs.student_id
INNER JOIN users u
    ON u.user_id = s.user_id
ORDER BY c.class_code, s.student_code;

