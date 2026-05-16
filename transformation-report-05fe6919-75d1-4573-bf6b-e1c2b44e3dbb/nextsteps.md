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
Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Release
```

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --verbosity normal
```

Review the test output for any failures or skipped tests that may indicate behavioral differences introduced by the migration.

### 4. Run the Application Locally
Start the web application and verify it runs as expected on the cross-platform runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following:
- The application starts without runtime exceptions.
- All routes and pages load correctly.
- Database connections (if any) are functional and connection strings are valid for the target environment.
- Any static assets, middleware, or configuration files (e.g., `appsettings.json`) are being loaded correctly.

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all dependent projects in the solution target a compatible framework version.

### 6. Check for Removed or Changed APIs
Even without build errors, some APIs behave differently between .NET Framework and modern .NET. Pay particular attention to:
- `System.Web` dependencies, which are not available in modern .NET and may have been replaced with ASP.NET Core equivalents.
- `HttpContext`, `HttpRequest`, and `HttpResponse` usage patterns.
- Any configuration or dependency injection patterns that differ from the legacy approach.
- `BinaryFormatter` or other serialization APIs that have been removed or disabled by default.

### 7. Review Warnings
Build warnings can indicate deprecated APIs or compatibility concerns that will become errors in future .NET versions. Review them with:

```bash
dotnet build --configuration Release 2>&1 | grep -i warning
```

Address any warnings related to obsolete APIs or platform compatibility.

### 8. Validate Runtime Behavior on Target Platform
If the intended deployment platform is Linux or macOS, test the application explicitly on that platform to catch any remaining Windows-specific assumptions, such as:
- File path separators.
- Windows Registry access.
- Windows-only authentication mechanisms (e.g., NTLM/Windows Authentication).