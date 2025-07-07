# PayTrack

This project was developed as part of a code challenge. The project is named **PayTrack**.

**PayTrack** is a simple transaction management API designed to store user and transaction data in a relational database and provide efficient in-memory summarization of transaction records.

[![.NET](https://github.com/VasiErdeiEdeka/PayTrackApi/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/VasiErdeiEdeka/PayTrackApi/actions/workflows/build.yml)

---

## Objective

The objective was to demonstrate:

- SQL database integration  
- Clean and layered architecture  
- Efficient in-memory data processing  
- AutoMapper and DTO usage  
- EF Core and repository patterns  
- SOLID principles  
- Unit test samples  
- Swagger-based API documentation

---

## Architecture

The project follows a Clean Architecture-inspired layered structure:


PayTrack

PayTrack.API             --> API controllers and configuration

PayTrack.Application     --> DTOs, interfaces, AutoMapper profiles

PayTrack.Domain          --> Entity models, enums, interfaces

PayTrack.Infrastructure  --> EF DbContext and repository implementations

PayTrack.Tests           --> Unit tests using xUnit and Moq

## Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- AutoMapper
- Swagger / Swashbuckle
- xUnit & Moq


## Configuration Notes

Before applying migrations and running the project:

1. Update the connection string in:
   - "PayTrack.Infrastructure/PayTrackDbContextFactory.cs"
   - "PayTrack.API/appsettings.json"

**Example Connection String**
```csharp
"Server=localhost\\SQLEXPRESS;Database=PayTrackDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

2. Run migration and apply to database using command below
```shell
dotnet ef database update --project PayTrack.Infrastructure --startup-project PayTrack.API
```


## Challenge Notes

- Although polymorphism was considered for transaction types (e.g., `DebitTransaction`, `CreditTransaction`), a simpler enum-based model was chosen due to EF Core discriminator limitations. This decision was intentional for maintainability and simplicity.


## Repository Structure

All logic is separated into clean layers. The solution is ready to scale with additional transaction types or services.
