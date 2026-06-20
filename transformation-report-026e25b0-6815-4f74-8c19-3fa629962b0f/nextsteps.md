# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation and Testing

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Release
```

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

### 4. Run the Web Application Locally
Start the `Northwind.Web` project and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Navigate to the URL printed in the console output (e.g., `https://localhost:5001` or `http://localhost:5000`).
- Walk through the core application workflows to confirm pages load and data is returned correctly.

### 5. Verify Database Connectivity
If the application uses Entity Framework Core or direct database access, confirm that:

- Connection strings in `appsettings.json` (and `appsettings.Production.json`) are updated to target the correct database server.
- Any pending migrations are applied:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 6. Review Target Framework
Open each `.csproj` file and confirm the `<TargetFramework>` element targets the intended .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 7. Check for Removed or Changed APIs
Even without build errors, runtime behavior can differ from .NET Framework. Pay particular attention to:

- `System.Web` usages that may have been replaced with ASP.NET Core equivalents.
- `HttpContext`, `HttpRequest`, and `HttpResponse` API differences.
- Any use of `BinaryFormatter`, which is disabled by default in modern .NET.
- `ConfigurationManager` replaced by `IConfiguration` / `appsettings.json`.

### 8. Review Warnings
Build warnings can indicate deprecated APIs or compatibility concerns that will not cause immediate failures but may cause issues at runtime:

```bash
dotnet build --configuration Release 2>&1 | grep -i warning
```

Address any warnings related to nullable reference types, obsolete members, or platform compatibility attributes.

### 9. Publish the Application
Once local validation is complete, publish the application to confirm the output is as expected:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to verify all expected files are present, including static assets and configuration files.

### 10. Smoke Test the Published Output
Run the published output directly to ensure it behaves identically to the development run:

```bash
dotnet ./publish/Northwind.Web.dll
```