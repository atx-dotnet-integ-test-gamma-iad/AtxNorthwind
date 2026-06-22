# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the root of the solution to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of errors in a clean build context:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not prevent a successful build.

### 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended cross-platform .NET version (e.g., `net8.0`). Ensure all dependent projects target a compatible framework.

### 4. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review any failing tests and address them before proceeding.

### 5. Run the Application Locally

Start the application locally to confirm it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Verify that the application starts without runtime exceptions.
- Navigate through the key areas of the application to confirm expected behavior.
- Check that database connections, configuration bindings, and middleware are functioning correctly.

### 6. Review Removed Windows-Specific Dependencies

Check that no Windows-specific APIs or packages (e.g., `System.Web`, `Microsoft.Web.Infrastructure`, or `System.Drawing.Common` without a cross-platform shim) remain in use. Search the codebase for any such references:

```bash
grep -r "System.Web" src/
```

Address any findings by replacing them with their cross-platform equivalents.

### 7. Verify Configuration Migration

Confirm that `Web.config` or `App.config` settings have been properly migrated to `appsettings.json` or `appsettings.{Environment}.json`. Ensure that environment-specific configuration values (connection strings, API keys, etc.) are correctly set for each target environment.

### 8. Check Static Files and Middleware

If the application serves static files, confirm that the `wwwroot` folder is present and that the static file middleware is configured in `Program.cs` or `Startup.cs`.

### 9. Validate Database Connectivity

If the application uses Entity Framework or another data access layer, run any pending migrations and verify connectivity:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

Confirm that data reads and writes function correctly against the target database.

### 10. Test on Target Platform

If the goal is cross-platform support, run the application on the intended non-Windows platform (Linux or macOS) to surface any remaining platform-specific issues that may not appear during a Windows build.