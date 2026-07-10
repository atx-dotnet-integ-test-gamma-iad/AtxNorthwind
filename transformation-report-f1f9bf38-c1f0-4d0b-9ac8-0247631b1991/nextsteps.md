# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

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

Review the output for any warnings that may indicate deprecated APIs, missing references, or compatibility concerns that did not surface as hard errors.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality is intact after the migration:

```bash
dotnet test --configuration Release
```

Review the test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the web application locally to confirm it runs as expected on the new .NET runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Navigate to the application in a browser and verify that pages load correctly.
- Check the console output for any runtime exceptions or unhandled errors.
- Review application logs for warnings related to obsolete middleware, configuration, or service registration.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a compatible framework version.

### 6. Verify Configuration Files

- Confirm that `appsettings.json` and any environment-specific variants (e.g., `appsettings.Development.json`) are present and contain the correct configuration values.
- If the legacy project used `Web.config` or `App.config`, verify that the relevant settings have been migrated to `appsettings.json` or the appropriate .NET configuration provider.

### 7. Check for Replaced or Removed APIs

Even without build errors, some APIs may have been replaced with compatibility shims or may behave differently on cross-platform .NET. Review the following areas manually:

- Any use of `System.Web` namespaces, which are not available in cross-platform .NET.
- File path handling — ensure `Path.Combine` is used rather than hardcoded path separators.
- Any Windows-specific APIs (e.g., registry access, Windows authentication) that may not function on non-Windows platforms.

### 8. Test on Target Platform

If the goal is to run on a non-Windows operating system, run the application on that platform explicitly to surface any remaining platform-specific issues:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Check for runtime errors that would not appear during a Windows build.

### 9. Review Database Connectivity

If the application uses a database (as is typical for a Northwind-based project), verify that:

- The connection string in `appsettings.json` is correct for the target environment.
- The database provider NuGet package (e.g., `Microsoft.EntityFrameworkCore.SqlServer`) is compatible with the target .NET version.
- Any database migrations are up to date by running:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```