# AutomatedTesting

A small .NET 10 Task management API built as a **learning lab for automated testing**. The
application itself (a CRUD Task service) is intentionally simple — the real focus of this repo is
practicing unit testing with xUnit, Moq, and FluentValidation.

## Tech stack

- **.NET 10** (`net10.0`)
- **ASP.NET Core Web API** + Swagger / OpenAPI
- **MediatR** — CQRS command/query dispatch
- **FluentValidation** — request validation (wired in as a MediatR pipeline behavior)
- **Entity Framework Core** + SQL Server
- **xUnit**, **Moq**, **coverlet** — testing

## Architecture

The solution follows a layered, feature-sliced (CQRS) structure. Each project has one job:

| Project | Responsibility |
|---|---|
| `AutomatedTesting.Api` | HTTP entry point — controllers, DI setup, Swagger (`Program.cs`) |
| `AutomatedTesting.Application` | Business logic: MediatR commands/queries, handlers, validators, pipeline behaviors |
| `AutomatedTesting.Repositories` | Data access abstraction (`ITaskRepository` / `TaskRepository`) |
| `AutomatedTesting.Db` | EF Core `AppDbContext` and entities (`TaskItem`) |
| `AutomatedTesting.Shared` | Cross-cutting types: `Result<T>`, request/response models, mapper |
| `AutomatedTesting.Tests` | Automated tests |

**Request flow:**

```
HTTP request → TaskController → MediatR → ValidationBehavior → Handler → ITaskRepository → EF Core → SQL Server
```

Handlers return a `Result<T>` (in `AutomatedTesting.Shared`) that carries success/error/validation/
not-found state, which the controller maps to the appropriate HTTP status code.

## API endpoints

Base route: `/api/Task`

| Method | Route | Description |
|---|---|---|
| `GET` | `/api/Task` | Get all tasks |
| `GET` | `/api/Task/{id}` | Get a task by id |
| `POST` | `/api/Task` | Create a task |
| `PATCH` | `/api/Task/{id}` | Update a task |
| `DELETE` | `/api/Task/{id}` | Delete a task |

`AutomatedTesting.Api/AutomatedTesting.Api.http` contains ready-to-run sample requests.

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or SQL Server Express / LocalDB)

### Configure the database

Set the `DefaultConnection` connection string in
`AutomatedTesting.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AutomatedTesting;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Run the API

```bash
dotnet run --project AutomatedTesting.Api
```

Swagger UI is available in development at `/swagger`.

## Running the tests

```bash
# run all tests
dotnet test

# run with code coverage (coverlet is already referenced)
dotnet test --collect:"XPlat Code Coverage"
```

## Testing notes

This project doubles as a testing playground. Current coverage lives in `AutomatedTesting.Tests`:

- **`CreateTaskHandlerTests`** — unit-tests the handler with a mocked `ITaskRepository` (Moq),
  covering the success and failure paths.
- **`CreateTaskCommandValidatorTests`** — validates the FluentValidation rules using
  `TestValidate` helpers.

Techniques being practiced here: mocking dependencies, verifying interactions, argument capture,
data-driven tests (`[Theory]` / `[InlineData]` / `[MemberData]`), boundary testing, and
exception-path testing.

> Next testing topics (not yet covered): **integration tests** (real MediatR pipeline + in-memory /
> SQLite database) and **coverage analysis**.
