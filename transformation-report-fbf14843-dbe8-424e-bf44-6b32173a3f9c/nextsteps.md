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

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of errors:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended cross-platform .NET version (e.g., `net8.0`). Ensure all dependent projects target a compatible framework.

### 4. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review any failing tests and address them before proceeding.

### 5. Run the Application Locally

Start the application locally to confirm runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Manually verify core application functionality, including:
- Application startup without exceptions
- Database connectivity (if applicable)
- Key application routes or endpoints returning expected responses

### 6. Check for Removed or Changed APIs

Review the codebase for any usage of APIs that were available in .NET Framework but have changed behavior in cross-platform .NET. Pay particular attention to:
- `System.Web` references (these are not available in cross-platform .NET)
- Windows-specific APIs such as the registry, WCF server-side, or Windows Authentication configurations
- Any third-party libraries that may still target .NET Framework only

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.ApiCompat` tooling to surface any remaining compatibility issues.

### 7. Validate Configuration Files

Confirm that `appsettings.json` (and environment-specific variants such as `appsettings.Production.json`) contain all configuration values that were previously held in `Web.config` or `App.config`. The `<connectionStrings>` and `<appSettings>` sections from those files should be migrated to the JSON-based configuration system.

### 8. Verify Static Files and Web Assets

If the project serves static content, confirm that files are located under the `wwwroot` folder and that the static file middleware is correctly configured in `Program.cs` or `Startup.cs`.

### 9. Test on a Non-Windows Platform (Optional but Recommended)

Since the goal is cross-platform compatibility, consider running the application on Linux or macOS to surface any remaining platform-specific dependencies:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Address any `PlatformNotSupportedException` or similar runtime errors that appear.