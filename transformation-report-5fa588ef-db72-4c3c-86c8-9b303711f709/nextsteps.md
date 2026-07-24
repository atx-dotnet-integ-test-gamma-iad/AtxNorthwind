# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected in any of the projects within the solution, including the primary project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or missing packages.

### 2. Build the Solution

Perform a full build to confirm there are no compilation issues:

```bash
dotnet build --configuration Release
```

Verify that the build completes with zero errors and review any warnings that may indicate deprecated APIs or compatibility concerns.

### 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended cross-platform .NET version (e.g., `net8.0`). Ensure no legacy `<TargetFrameworkVersion>` elements remain.

### 4. Run the Application Locally

Start the application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate to the application in a browser and exercise the core functionality to confirm runtime behavior matches the legacy version.

### 5. Run Existing Tests

If the solution contains test projects, execute them to validate business logic and integration points:

```bash
dotnet test
```

Review the test results and investigate any failures, as they may indicate behavioral differences introduced during the migration.

### 6. Check for Removed or Changed APIs

Review the code for any usage of APIs that were available in .NET Framework but have changed or been removed in cross-platform .NET. Common areas to check include:

- `System.Web` references (these do not exist in cross-platform .NET)
- `HttpContext` and related types (now accessed via `IHttpContextAccessor`)
- `ConfigurationManager` (replaced by `Microsoft.Extensions.Configuration`)
- Windows-specific APIs such as the registry or WCF server-side components

### 7. Validate Configuration Files

Confirm that `web.config` settings have been migrated to `appsettings.json` or `appsettings.{Environment}.json` where applicable. The `web.config` file is no longer the primary configuration mechanism in cross-platform .NET web applications.

### 8. Verify Database Connectivity

If the application uses a database (as suggested by the Northwind naming convention), confirm that the connection strings in `appsettings.json` are correct and that the application can successfully connect to the database at runtime.

### 9. Test on a Non-Windows Platform (Optional but Recommended)

Since the goal is cross-platform compatibility, consider running the application on Linux or macOS to surface any remaining platform-specific dependencies:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Address any exceptions or errors that appear only on non-Windows platforms.

### 10. Review Startup and Middleware Configuration

Confirm that `Startup.cs` or the top-level `Program.cs` file correctly configures middleware, services, and routing in accordance with the ASP.NET Core pipeline. Legacy `Global.asax` logic should have been moved into this file during transformation.