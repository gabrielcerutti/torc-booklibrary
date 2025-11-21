# .NET 10 Upgrade Report

## Project target framework modifications

| Project name                                   | Old Target Framework | New Target Framework | Commits   |
|:-----------------------------------------------|:--------------------:|:--------------------:|-----------|
| Torc.BookLibrary.API\Torc.BookLibrary.API.csproj   | net9.0               | net10.0              | 70bd8142, 86d8fb44 |
| Torc.BookLibrary.Tests\Torc.BookLibrary.Tests.csproj | net9.0               | net10.0              | b27a4723 |

## NuGet Packages

| Package Name                                   | Old Version | New Version | Commit Id |
|:-----------------------------------------------|:-----------:|:-----------:|:---------:|
| Microsoft.AspNetCore.OpenApi                   | 9.0.4       | 10.0.0      | 04a58f55  |
| Microsoft.EntityFrameworkCore.Design           | 9.0.4       | 10.0.0      | 04a58f55  |
| Microsoft.EntityFrameworkCore.SqlServer        | 9.0.4       | 10.0.0      | 04a58f55  |
| Microsoft.VisualStudio.Azure.Containers.Tools.Targets | 1.21.2    | Removed     | 04a58f55  |
| Microsoft.AspNetCore.Mvc.Testing               | 9.0.5       | 10.0.0      | a99ff7be  |
| Microsoft.Data.Sqlite.Core                     | 9.0.5       | 10.0.0      | a99ff7be  |
| Microsoft.EntityFrameworkCore.InMemory         | 9.0.5       | 10.0.0      | a99ff7be  |
| Microsoft.EntityFrameworkCore.Sqlite.Core      | 9.0.5       | 10.0.0      | a99ff7be  |

## All commits

| Commit ID | Description |
|:---------:|:------------|
| b744cec3  | Commit upgrade plan |
| 70bd8142  | Update target framework to net10.0 in API.csproj |
| 04a58f55  | Update NuGet package versions in Torc.BookLibrary.API.csproj |
| b27a4723  | Update target framework to net10.0 in Tests.csproj |
| a99ff7be  | Update package versions in Torc.BookLibrary.Tests.csproj |
| 86d8fb44  | Store final changes for step 'Upgrade Torc.BookLibrary.API.csproj' |

## Project feature upgrades

Contains summary of modifications made to the project assets during different upgrade stages.

### Torc.BookLibrary.API

Here is what changed for the project during upgrade:

- Upgraded ASP.NET Core OpenAPI dependencies and fixed namespace/type resolution: replaced Microsoft.OpenApi.Models OpenApiInfo usage with Microsoft.OpenApi.OpenApiInfo and updated Swashbuckle.AspNetCore to 10.0.1.
- Removed Microsoft.VisualStudio.Azure.Containers.Tools.Targets as it has no supported version for .NET 10.

### Torc.BookLibrary.Tests

Here is what changed for the project during upgrade:

- Upgraded test dependencies to 10.0.0 versions compatible with .NET 10.

## Next steps

- Review Docker compose/build tooling after removal of Containers.Tools.Targets.
- Verify runtime behavior locally (Swagger UI, DB migrations, logging).