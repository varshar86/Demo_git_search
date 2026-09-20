# Searchify API - Visual Studio 2026

This is the modified version of the supplied SearchDemo project, implementing the attached **API Developer Technical Evaluation | Use Case - 001**.

The implementation covers search/query, filters, sorting, pagination, search history, database persistence, Swagger documentation, global error handling, error reporting, request logging, JWT security, rate limiting, maintainable layered architecture, and tests.

## Quick start

- Visual Studio 2026
- .NET 10 SDK
- Open `SearchDemo.sln`
- Set `SearchDemo.API` as startup project
- Run with F5
- Swagger: `https://localhost:7250/swagger`

Demo login:

```json
{
  "userName": "admin",
  "password": "Admin@123"
}
```

Authorize Swagger with `Bearer <accessToken>`.

The project uses SQLite and automatically creates/seeds `searchify.db`, so there is no SQL Server setup required for the supplied evaluation build.

See `Documentation/ProjectDocumentation.txt` for endpoint details and story-to-implementation mapping.
