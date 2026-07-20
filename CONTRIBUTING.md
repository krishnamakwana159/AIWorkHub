# Contributing to AIWorkHub

Thank you for contributing to AIWorkHub.

## Development Principles

- Keep changes focused and easy to review.
- Prefer clear architecture boundaries between backend, frontend, infrastructure, and documentation.
- Add or update documentation when a decision affects setup, architecture, deployment, or team workflow.
- Record significant architectural decisions in `docs/ADRs.md`.

## Repository Status

The repository is currently scaffold-only. Do not add a .NET solution, React application, package manifests, or CI workflows until the corresponding milestone is started.

## Branching

Use short, descriptive branch names such as:

```text
feature/backend-bootstrap
docs/update-architecture
chore/repository-hygiene
```

## Pull Request Checklist

- The change is scoped to one concern.
- Documentation has been updated where needed.
- No generated build artifacts, local secrets, or dependency folders are committed.
- The repository remains free of application code until the planned implementation milestone.


## Technology Stack

Backend

- .NET 9
- Clean Architecture
- CQRS
- MediatR
- Entity Framework Core
- SQL Server
- SignalR
- Hangfire

Frontend

- React
- TypeScript
- Vite
- Material UI
- React Query
- Axios

---

## Branch Naming

feature/<feature-name>

Example

feature/dashboard

bugfix/<issue>

hotfix/<issue>

---

## Commit Format

feat:

fix:

refactor:

docs:

test:

Example

feat: add project analytics endpoint

---

## Coding Guidelines

- Follow Clean Architecture
- Keep CQRS separation
- Do not access DbContext directly
