@echo off
"C:\Program Files\MySQL\MySQL Server 26.10\bin\mysql.exe" -h 127.0.0.1 -P 3306 -u root -p --default-character-set=utf8mb4 -D collabsphere < "D:\DoanhThuNike\CNPM-main\database\schema.mysql.preview.sql"
