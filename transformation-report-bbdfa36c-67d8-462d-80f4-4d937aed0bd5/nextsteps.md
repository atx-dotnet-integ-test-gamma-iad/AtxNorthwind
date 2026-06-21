# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of errors:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while non-blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review test results and address any failures before proceeding.

### 4. Run the Application Locally

Start the `Northwind.Web` project locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Manually verify the following:
- The application starts without exceptions.
- Key pages or endpoints return expected responses.
- Database connectivity is functioning if applicable (check connection strings in `appsettings.json` for any environment-specific values that may need updating).

### 5. Review Configuration Files

Check the following files for any values that were tied to the legacy environment and may need to be updated for the new cross-platform target:

- `appsettings.json` / `appsettings.Production.json`
- Any remaining `web.config` or `app.config` files (these should generally be replaced or removed in cross-platform .NET)
- Environment variables or secrets configuration

### 6. Check Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Repeat this check for all other projects in the solution.

### 7. Review Removed or Replaced APIs

Cross-platform .NET does not support certain legacy .NET Framework APIs. Search the codebase for any runtime usage of the following that may not have been caught at compile time:

- `System.Web` types
- Windows-specific registry or COM interop calls
- `ConfigurationManager` (should be replaced with `Microsoft.Extensions.Configuration`)

### 8. Verify Static Assets and Middleware

For `Northwind.Web`, confirm that static file serving, routing, and any middleware registered in `Program.cs` or `Startup.cs` behave as expected when the application is run on a non-Windows host if cross-platform deployment is intended.