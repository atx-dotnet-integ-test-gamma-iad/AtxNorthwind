# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors are reported during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output and confirm that the build succeeds with zero errors and review any warnings that may indicate deprecated APIs or compatibility concerns.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality is preserved:

```bash
dotnet test --configuration Release --verbosity normal
```

Review the test results and address any failing tests before proceeding.

### 4. Review Target Framework

Open `Northwind.Web.csproj` and confirm that the `<TargetFramework>` element is set to the intended cross-platform .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is targeting an older version such as `net6.0` or `net7.0`, consider upgrading to the current LTS release (`net8.0`).

### 5. Review Removed Windows-Specific APIs

Check the codebase for any usage of APIs that are Windows-specific and may have been silently carried over, such as:

- `System.Web` references
- `HttpContext` usage patterns from classic ASP.NET (non-Core)
- Windows Registry access
- `System.Drawing` (requires additional packages on Linux/macOS)

Use the .NET Upgrade Assistant compatibility analyzer or the `Microsoft.DotNet.PlatformAbstractions` tooling to identify any remaining platform-specific calls.

### 6. Run the Application Locally

Start the web application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate to the reported local URL and manually verify core application routes and functionality.

### 7. Verify Configuration and Middleware

Review `Program.cs` and any `Startup.cs` (if still present) to confirm that:

- Middleware is registered in the correct order
- Connection strings in `appsettings.json` are valid and point to accessible database instances
- Environment-specific configuration (`appsettings.Development.json`, etc.) is correct

### 8. Database Connectivity

If the application uses Entity Framework Core or direct database access, verify the database connection:

- Run any pending migrations:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

- Confirm the schema matches the expected state.

### 9. Cross-Platform Smoke Test

If the goal is full cross-platform support, run the application on a non-Windows environment (Linux or macOS) to surface any remaining platform-specific issues that do not appear as build errors but may cause runtime failures.