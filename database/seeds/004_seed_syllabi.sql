
USE collabsphere;

-- DE CUONG CONG NGHE PHAN MEM
INSERT INTO syllabi
    (subject_id, version, title, content)
SELECT
    s.subject_id,
    '1.0',
    'De cuong Cong nghe phan mem',
    'Phan tich yeu cau, thiet ke he thong, lap trinh, kiem thu va bao tri phan mem.'
FROM subjects s
WHERE s.subject_code = 'CNPM'
AND NOT EXISTS (
    SELECT 1
    FROM syllabi sy
    WHERE sy.subject_id = s.subject_id
    AND sy.version = '1.0'
);

-- DE CUONG CAU TRUC DU LIEU
INSERT INTO syllabi
    (subject_id, version, title, content)
SELECT
    s.subject_id,
    '1.0',
    'De cuong Cau truc du lieu va giai thuat',
    'Mang, danh sach lien ket, ngan xep, hang doi, cay, do thi va cac thuat toan co ban.'
FROM subjects s
WHERE s.subject_code = 'CTDL'
AND NOT EXISTS (
    SELECT 1
    FROM syllabi sy
    WHERE sy.subject_id = s.subject_id
    AND sy.version = '1.0'
);

-- DE CUONG PROJECT BASED LEARNING
INSERT INTO syllabi
    (subject_id, version, title, content)
SELECT
    s.subject_id,
    '1.0',
    'De cuong Project Based Learning',
    'Lap ke hoach du an, phan chia cong viec, lam viec nhom, theo doi tien do va danh gia ket qua.'
FROM subjects s
WHERE s.subject_code = 'PBL'
AND NOT EXISTS (
    SELECT 1
    FROM syllabi sy
    WHERE sy.subject_id = s.subject_id
    AND sy.version = '1.0'
);

-- KIEM TRA
SELECT
    sy.syllabus_id,
    s.subject_code,
    s.subject_name,
    sy.version,
    sy.title
FROM syllabi sy
INNER JOIN subjects s
    ON sy.subject_id = s.subject_id
ORDER BY sy.syllabus_id;

