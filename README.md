# Task Manager API

A RESTful API built with ASP.NET Core 8.0 for managing tasks and user authentication.

## Features

- JWT Authentication
- Task CRUD operations
- Role-based authorization
- Swagger/OpenAPI documentation
- In-memory database using Entity Framework Core

## Prerequisites

- .NET 8.0 SDK
- Visual Studio Code or Visual Studio 2022

## Getting Started

1. Clone the repository

```bash
git clone <repository-url>
cd TaskManagerAPI
```

2. Configure JWT in `appsettings.Development.json`:

```json
{
  "Jwt": {
    "Key": "your-secret-key-here-minimum-32-characters",
    "Issuer": "TaskManagerAPI",
    "Audience": "TaskManagerAPI.Client",
    "ExpirationHours": 24
  }
}
```

3. Run the application:

```bash
  dotnet run
```

The API will be available at:
- HTTP: http://localhost:5124
- HTTPS: https://localhost:7235
- Swagger UI: https://localhost:7235/swagger

## Project Structure

- `Controllers/`: API endpoints
- `Models/`: Data models
- `Services/`: Business logic
- `Repositories/`: Data access layer
- `Authorization/`: Authentication and authorization logic
- `Data/`: Database context and configurations

## Authentication

The API uses JWT Bearer authentication. To access protected endpoints:

1. Obtain a JWT token from the authentication endpoint
2. Include the token in the Authorization header: Authorization: Bearer <your-token>

## Development

The project uses:
- Entity Framework Core 9.0 for data access
- JWT Bearer for authentication
- Swagger/OpenAPI for API documentation

Configuration files:

csharp:Program.cs
startLine: 15
endLine: 35

## Environment Variables

The application uses the following environment variables:
- `ASPNETCORE_ENVIRONMENT`: Set to "Development" for development settings
- JWT configuration in appsettings.json/appsettings.Development.json

## Security Notes

- Never commit sensitive information like JWT keys to source control
- Use secure key storage in production (e.g., Azure Key Vault)
- The development environment uses an in-memory database

## License

[MIT License](LICENSE)

This README provides a comprehensive overview of your project, including setup instructions, project structure, and important security considerations. You may want to customize it further based on your specific requirements or additional features.