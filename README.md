## Match Listing and Tracking

Sample .NET 5 reference application, based on a simplified CQRS architecture and Docker containers.

### Getting Started

```sh
docker-compose up --build
```

### Docker Containers

| Image | Port | Host |
| ------ | ------ | ------  |
| matchlistclient:latest | 4000 | localhost |
| matchlistapi:latest | 5200 | localhost |
| postgres:latest | 5432 | localhost |
| datalust/seq:latest | 5340 | localhost |

### Tech Stack

 - .Net 5
 - Serilog & Seq
 - CQRS
 - MediaTR
 - ReactJS
 - Docker
 - PostgreSQL
 - Entity Framework Core
 - Ant Design
 - FluentValidation