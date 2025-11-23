# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade Torc.BookLibrary.API\Torc.BookLibrary.API.csproj
4. Upgrade Torc.BookLibrary.Tests\Torc.BookLibrary.Tests.csproj
5. Run unit tests to validate upgrade in the projects listed below:
  Torc.BookLibrary.Tests\Torc.BookLibrary.Tests.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|
| docker-compose.dcproj                          | Explicitly excluded         |

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                   | Current Version            | New Version | Description                                                      |
|:-----------------------------------------------|:--------------------------:|:-----------:|:-----------------------------------------------------------------|
| Microsoft.AspNetCore.Mvc.Testing               | 9.0.5                      | 10.0.0      | Recommended for .NET 10.0                                        |
| Microsoft.AspNetCore.OpenApi                   | 9.0.4                      | 10.0.0      | Recommended for .NET 10.0                                        |
| Microsoft.Data.Sqlite.Core                     | 9.0.5                      | 10.0.0      | Recommended for .NET 10.0                                        |
| Microsoft.EntityFrameworkCore.Design           | 9.0.4                      | 10.0.0      | Recommended for .NET 10.0                                        |
| Microsoft.EntityFrameworkCore.InMemory         | 9.0.5                      | 10.0.0      | Recommended for .NET 10.0                                        |
| Microsoft.EntityFrameworkCore.SqlServer        | 9.0.4                      | 10.0.0      | Recommended for .NET 10.0                                        |
| Microsoft.EntityFrameworkCore.Sqlite.Core      | 9.0.5                      | 10.0.0      | Recommended for .NET 10.0                                        |
| Microsoft.VisualStudio.Azure.Containers.Tools.Targets | 1.21.2               |             | No supported version found; package will be removed              |

### Project upgrade details
This section contains details about each project upgrade and modifications that need to be done in the project.

#### Torc.BookLibrary.API\Torc.BookLibrary.API.csproj modifications

Project properties changes:
  - Target framework should be changed from `net9.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.OpenApi should be updated from `9.0.4` to `10.0.0` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.Design should be updated from `9.0.4` to `10.0.0` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.SqlServer should be updated from `9.0.4` to `10.0.0` (*recommended for .NET 10.0*)
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets should be removed (no supported version for .NET 10.0)

Other changes:
  - Review removal of container tools targets and adjust any related build or container workflow if necessary.

#### Torc.BookLibrary.Tests\Torc.BookLibrary.Tests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net9.0` to `net10.0`

NuGet packages changes:
  - Microsoft.AspNetCore.Mvc.Testing should be updated from `9.0.5` to `10.0.0` (*recommended for .NET 10.0*)
  - Microsoft.Data.Sqlite.Core should be updated from `9.0.5` to `10.0.0` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.InMemory should be updated from `9.0.5` to `10.0.0` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.Sqlite.Core should be updated from `9.0.5` to `10.0.0` (*recommended for .NET 10.0*)

Other changes:
  - Ensure test setup remains compatible with EF Core 10 and ASP.NET Core 10 APIs.
