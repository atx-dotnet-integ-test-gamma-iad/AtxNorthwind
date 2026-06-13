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
Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs, missing nullable annotations, or other compatibility concerns that did not surface as hard errors.

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify runtime behavior is intact:

```bash
dotnet test --configuration Release --logger trx
```

Review the `.trx` result files or console output for any failing or skipped tests.

### 4. Run the Application Locally
Start the `Northwind.Web` project and verify it runs as expected on the target platform:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following:
- The application starts without runtime exceptions.
- All routes and endpoints respond correctly.
- Database connections (if applicable) are functional and connection strings are configured for the new environment.
- Static assets, views, or Razor pages render as expected.

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it still references `netcoreapp*` or an older version, update it accordingly and rebuild.

### 6. Review Platform-Specific Code
Even without build errors, inspect the codebase for any usage of APIs that were available in .NET Framework but may behave differently on cross-platform .NET:

- `System.Web` references (these should have been removed or replaced).
- Windows Registry access (`Microsoft.Win32.Registry`).
- Windows-specific file path assumptions (backslashes, drive letters).
- `AppDomain` usage that relied on .NET Framework-specific behavior.

### 7. Verify Configuration Migration
Confirm that `web.config` settings have been properly migrated to `appsettings.json` or environment variables, and that the application reads them correctly at runtime.

### 8. Check for Nullable Reference Type Warnings
If the project has `<Nullable>enable</Nullable>` in the project file, review compiler warnings related to nullability. While these are not errors, addressing them improves code reliability on the new platform.

```bash
dotnet build --configuration Release /warnaserror:nullable
```

This will surface any nullable warnings as errors so they can be systematically addressed.