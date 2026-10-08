# System Architecture

```mermaid
flowchart LR
    UI[ReactJS Web Client]
    API[ASP.NET Core Web API]
    DB[(PostgreSQL)]
    AI[AWS Bedrock]
    FILE[Cloudinary]
    REDIS[Upstash Redis]
    RTC[WebRTC]
    CHAT[Signal]
    WHITE[Socket.IO]

    UI --> API
    API --> DB
    API --> AI
    API --> FILE
    API --> REDIS
    UI <--> RTC
    UI <--> CHAT
    UI <--> WHITE
```

## Layering Backend

```text
API / Controllers
        ↓
Application / Services
        ↓
Domain / Entities
        ↓
Infrastructure / EF Core
        ↓
PostgreSQL
```

## Frontend

```text
src/
  app/
  components/
  layouts/
  pages/
  features/
  services/
  hooks/
  types/
```

## Backend

```text
backend/
  CollabSphere.Api/
  CollabSphere.Application/
  CollabSphere.Domain/
  CollabSphere.Infrastructure/
```

Tên project có thể điều chỉnh khi nhóm tạo solution, nhưng phải thống nhất trước khi merge.
