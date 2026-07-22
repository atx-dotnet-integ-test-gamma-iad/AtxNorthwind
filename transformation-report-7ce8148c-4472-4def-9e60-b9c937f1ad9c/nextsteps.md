# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output and confirm that the build reports `0 Error(s)`.

### 3. Run Unit Tests

If the solution contains test projects, execute them to validate that existing functionality has not regressed:

```bash
dotnet test --configuration Release --verbosity normal
```

Review the test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the `Northwind.Web` project locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Navigate to the application in a browser and exercise the primary workflows.
- Check the console output for any runtime exceptions or warnings.

### 5. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `TargetFramework` is set to the intended cross-platform .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is targeting an older version such as `net6.0` or `net7.0`, consider updating to the current LTS release.

### 6. Review Removed or Changed APIs

Check for any use of APIs that were available in .NET Framework but have changed behavior in cross-platform .NET, particularly:

- `System.Web` references (these are not available in cross-platform .NET).
- Windows-specific APIs such as the registry, WCF server-side components, or `System.Drawing` (GDI+).
- Any `App.config` or `Web.config` settings that should be migrated to `appsettings.json`.

### 7. Verify Database Connectivity

If the application uses a database (as suggested by the Northwind naming convention):

- Confirm the connection string in `appsettings.json` is correctly configured.
- Run the application and verify that database read and write operations function as expected.
- If Entity Framework is in use, confirm that migrations are up to date:

```bash
dotnet ef migrations list --project src/Northwind.Web/Northwind.Web.csproj
```

### 8. Check Static Files and Middleware Configuration

For a web project, verify the following in `Program.cs` or `Startup.cs`:

- Static file middleware is configured (`app.UseStaticFiles()`).
- Routing and endpoint mapping are correctly set up.
- Authentication and authorization middleware, if present, are in the correct order.

### 9. Test on Target Platform

If the goal of the migration was to support a non-Windows platform (Linux or macOS), run the application on that target platform to confirm there are no platform-specific runtime issues.