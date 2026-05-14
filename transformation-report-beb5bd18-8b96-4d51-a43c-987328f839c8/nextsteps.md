# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including the most critical project `Northwind.Web`.

## Validation

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

### 2. Build the Solution
Confirm the solution builds cleanly in Release configuration:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify runtime behavior is consistent with the original:

```bash
dotnet test --configuration Release
```

Review any failing tests carefully, as they may indicate behavioral differences introduced by the migration to cross-platform .NET.

### 4. Run the Application Locally
Start the web application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following manually:
- Application starts without runtime exceptions
- All routes and pages load correctly
- Database connections (if any) are functional
- Authentication and authorization flows work as expected
- Static assets are served correctly

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended modern .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is targeting an older version such as `net6.0` or `net7.0`, consider updating to the current LTS release.

### 6. Review Removed or Changed APIs
Check the code for use of any APIs that were available in .NET Framework but have changed behavior in cross-platform .NET, including:

- `System.Web` references (these are not available in cross-platform .NET)
- `HttpContext` usage patterns
- Windows-specific APIs (registry, WCF, etc.)
- Any `#if` preprocessor directives that may have masked issues during the build

### 7. Check Configuration Files
Ensure that `appsettings.json` (and environment-specific variants such as `appsettings.Production.json`) contain all configuration values that were previously held in `Web.config` or `App.config`. Verify:

- Connection strings
- Application settings
- Logging configuration

### 8. Verify Runtime on Target Operating System
If the intent is to run on Linux or macOS, test the application explicitly on that platform to catch any remaining OS-specific dependencies that would not surface on Windows.