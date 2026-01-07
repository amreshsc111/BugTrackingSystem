# BugTrackingSystem

A simple bug tracking backend built with .NET 10, MediatR and ASP.NET Core Web API. Implements authenticated endpoints for creating, assigning and managing bugs with support for file attachments.

Frontend: https://github.com/amreshsc111/BugTrackingSystem-FE

## Highlights
- .NET 10 Web API
- MediatR-based CQRS commands/queries
- File attachments support (multipart/form-data)
- Role-based authorization (Developer role required for some actions)
- Clean separation: `API`, `Application`, `Infrastructure` projects

## Requirements
- .NET 10 SDK
- Visual Studio 2026 or later (or VS Code + C# extensions)
- (Optional) SQL Server or other database configured in `ApplicationDbContext`

## Getting started

1. Clone
   - git clone https://github.com/amreshsc111/BugTrackingSystem

2. Restore & build
   - dotnet restore
   - dotnet build

3. Configure
   - Set your connection string in `appsettings.json` or user secrets under the API project.
   - Provide JWT and identity configuration (see `Program.cs` / `AuthController`).

4. Database
   - Create and apply EF Core migrations (project names may vary):
     - dotnet ef migrations add InitialCreate --project BugTrackingSystem.Infrastructure --startup-project BugTrackingSystem.API
     - dotnet ef database update --project BugTrackingSystem.Infrastructure --startup-project BugTrackingSystem.API

5. Run
   - dotnet run --project BugTrackingSystem.API
   - Or use Visual Studio: open the solution and run the API project.

## API (selected endpoints)
- POST `/api/bug/create-bug`  
  - Creates a bug. Accepts `multipart/form-data` for attachments. Authenticated users required.
- PUT `/api/bug/{id}/assign`  
  - Assign bug to the calling developer. Requires role: `Developer`.
- PUT `/api/bug/{id}/status`  
  - Update bug status. Requires role: `Developer`.
- GET `/api/bug/search`  
  - Search bugs by term, status or assignee. Requires role: `Developer`.
- GET `/api/bug/list-bugs`  
  - List bugs for the current user.

All endpoints are under JWT-based auth. Ensure Authorization header: `Bearer <token>`.

## Development tips
- Use the `CreateBugFormRequest` model for multipart requests (attachments are `IFormFile`).
- Controller logic uses `TryGetUserId` on `BaseApiController` to determine the calling user.
- Commands and queries live in the `Application` project and are dispatched via MediatR.

## To Do - Testing
- Run unit/integration tests with:
  - dotnet test

