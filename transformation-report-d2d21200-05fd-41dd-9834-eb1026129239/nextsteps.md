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

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the transformation:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review the test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the `Northwind.Web` project locally to confirm it runs as expected on the new cross-platform .NET runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Verify that the application starts without runtime exceptions.
- Navigate through the key pages or endpoints and confirm they respond correctly.
- Check the application logs for any runtime warnings or errors that may not have surfaced at build time.

### 5. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is set to an older or unintended version, update it accordingly and re-run the build and tests.

### 6. Check for Removed or Changed APIs

Even when a project builds without errors, some .NET Framework APIs behave differently or have been removed in cross-platform .NET. Pay particular attention to:

- **`System.Web` dependencies** — these are not available in cross-platform .NET. Confirm that any prior usage has been fully replaced (e.g., with ASP.NET Core equivalents).
- **Windows-specific APIs** — if the application uses registry access, Windows authentication, or other Windows-only features, verify they are either replaced or conditionally compiled.
- **Configuration** — confirm that `web.config`-based configuration has been migrated to `appsettings.json` and the ASP.NET Core configuration system.
- **Entity Framework** — if the project uses Entity Framework, confirm it has been migrated from EF 6 to EF Core and that all migrations and queries function correctly.

### 7. Verify Database Connectivity

If the application connects to a database, confirm the connection string in `appsettings.json` is correct for the target environment and that the application can successfully connect and query data at runtime.

### 8. Cross-Platform Smoke Test

If one of the goals of the transformation is to run on non-Windows operating systems, perform a basic smoke test on the target OS (Linux or macOS):

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Confirm that file paths, casing sensitivity, and any OS-specific behavior do not cause runtime failures.