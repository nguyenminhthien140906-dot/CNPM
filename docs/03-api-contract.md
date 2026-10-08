# API Contract - Initial

Base URL:
`/api`

## Auth
POST `/auth/login`
POST `/auth/logout`
GET `/auth/me`

## Users
GET `/users`
GET `/users/{id}`
PATCH `/users/{id}/deactivate`

## Subjects / Syllabus
GET `/subjects`
POST `/subjects`
GET `/subjects/{id}`
GET `/subjects/{id}/syllabus`
POST `/subjects/{id}/syllabus`

## Classes
GET `/classes`
POST `/classes`
GET `/classes/{id}`
POST `/classes/{id}/students`
POST `/classes/{id}/lecturer`

## Projects
GET `/projects`
POST `/projects`
GET `/projects/{id}`
PUT `/projects/{id}`
POST `/projects/{id}/submit`
POST `/projects/{id}/approve`
POST `/projects/{id}/deny`
POST `/projects/{id}/assign-class`

## Teams
GET `/teams`
POST `/teams`
GET `/teams/{id}`
POST `/teams/{id}/members`
DELETE `/teams/{id}/members/{studentId}`

## Milestones
GET `/projects/{projectId}/milestones`
POST `/projects/{projectId}/milestones`
PUT `/milestones/{id}`
PATCH `/milestones/{id}/complete`

## Tasks
GET `/teams/{teamId}/tasks`
POST `/teams/{teamId}/tasks`
PUT `/tasks/{id}`
DELETE `/tasks/{id}`

## Checkpoints
GET `/teams/{teamId}/checkpoints`
POST `/teams/{teamId}/checkpoints`
POST `/checkpoints/{id}/submit`
PATCH `/checkpoints/{id}/complete`

## Evaluation
POST `/teams/{teamId}/evaluations`
POST `/members/{memberId}/evaluations`
POST `/checkpoints/{id}/feedback`

## Realtime / AI
Các endpoint cụ thể sẽ được chốt sau khi Người 5 thiết kế interface cho realtime/AI.

### Quy tắc
- Frontend không truy cập PostgreSQL trực tiếp.
- Frontend gọi Backend API.
- Không đổi route đã chốt nếu chưa thống nhất.
