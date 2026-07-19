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

Review the output and confirm that the build succeeds with zero errors and review any warnings that may still be present.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Address any failing tests before proceeding.

### 4. Run the Application Locally

Start the web application locally to verify it runs as expected on the new .NET runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate to the application in a browser and manually verify that core functionality, routing, and data access behave as expected.

### 5. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to your intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is targeting an older version such as `net6.0` or `net7.0`, consider updating to the current LTS release (`net8.0`) and re-running the build and tests.

### 6. Review Removed or Changed APIs

Cross-platform .NET removes certain APIs that were available in .NET Framework. Use the .NET Upgrade Assistant compatibility analyzer or the following command to check for any remaining compatibility concerns:

```bash
dotnet build /p:EnableNETAnalyzers=true /p:AnalysisMode=All
```

Review any diagnostic warnings related to platform compatibility or obsolete API usage.

### 7. Verify Configuration and Middleware

If the project previously used `Web.config`, confirm that settings have been correctly migrated to `appsettings.json` and that middleware previously configured via `Global.asax` or `Startup.cs` is functioning correctly under the current pipeline.

### 8. Check Database Connectivity

If the application uses Entity Framework or direct database connections, verify that:

- Connection strings in `appsettings.json` are correct for the target environment.
- Migrations (if applicable) are up to date by running:

```bash
dotnet ef database update
```

- Data is returned correctly through the application UI or API endpoints.

### 9. Review Static Files and Bundling

Confirm that static assets such as CSS, JavaScript, and images are being served correctly. If the project previously used `System.Web.Optimization` for bundling, verify that an equivalent mechanism such as `WebOptimizer` or a front-end build tool has been configured.

### 10. Inspect Runtime Logs

Run the application and inspect the console output and any log files for runtime exceptions or deprecation warnings that would not surface at build time.