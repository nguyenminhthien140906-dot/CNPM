

USE collabsphere;

INSERT INTO subjects
    (subject_code, subject_name, description)
SELECT
    'CNPM',
    'Cong nghe phan mem',
    'Nghien cuu quy trinh phat trien phan mem'
WHERE NOT EXISTS (
    SELECT 1 FROM subjects WHERE subject_code = 'CNPM'
);

INSERT INTO subjects
    (subject_code, subject_name, description)
SELECT
    'CTDL',
    'Cau truc du lieu va giai thuat',
    'Nghien cuu cau truc du lieu va thuat toan'
WHERE NOT EXISTS (
    SELECT 1 FROM subjects WHERE subject_code = 'CTDL'
);

INSERT INTO subjects
    (subject_code, subject_name, description)
SELECT
    'PBL',
    'Project Based Learning',
    'Hoc tap va thuc hanh theo phuong phap hoc qua du an'
WHERE NOT EXISTS (
    SELECT 1 FROM subjects WHERE subject_code = 'PBL'
);

-- KIEM TRA
SELECT
    subject_id,
    subject_code,
    subject_name,
    description
FROM subjects
ORDER BY subject_id;



