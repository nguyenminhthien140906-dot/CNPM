# Git Workflow

## Main branch
`main` chỉ nhận code đã kiểm tra.

## Branch
Mỗi thành viên tạo branch riêng:

- `feature/backend-auth`
- `feature/frontend-admin`
- `feature/frontend-lecturer-student`
- `feature/realtime-ai`
- `docs/system-design`

## Quy trình

```bash
git checkout main
git pull origin main
git checkout -b feature/ten-chuc-nang
```

Làm code:

```bash
git add .
git commit -m "feat: ten chuc nang"
git push -u origin feature/ten-chuc-nang
```

Sau đó tạo Pull Request vào `main`.

## Commit convention

- `feat:` chức năng mới
- `fix:` sửa lỗi
- `docs:` tài liệu
- `refactor:` chỉnh cấu trúc code
- `test:` test
- `chore:` cấu hình

## Không push
- `.env`
- password
- API key
- connection string thật
- `node_modules`
- `bin/`
- `obj/`
