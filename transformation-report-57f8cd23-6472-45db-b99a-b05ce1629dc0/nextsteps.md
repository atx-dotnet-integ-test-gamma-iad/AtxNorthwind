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

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the migration:

```bash
dotnet test --configuration Release
```

Review test results and investigate any failures, as they may indicate behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Check Runtime Behavior

Run the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Specifically check:
- Application startup with no runtime exceptions
- Database connectivity if the project uses Entity Framework or ADO.NET
- All primary routes and endpoints return expected responses
- Any authentication or authorization flows behave correctly

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended modern .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it targets an older version such as `net6.0` or `net7.0`, consider upgrading to the current LTS release.

### 6. Review Removed or Changed APIs

Cross-reference the project's usage of any APIs that are known to behave differently in cross-platform .NET compared to .NET Framework. Common areas to check include:

- `System.Web` references, which are not available in cross-platform .NET
- Windows-specific APIs such as the registry or Windows identity impersonation
- `HttpContext` and related ASP.NET pipeline APIs if the project was migrated from Web Forms or older MVC

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the [Platform Compatibility Analyzer](https://learn.microsoft.com/en-us/dotnet/standard/analyzers/platform-compat-analyzer) to surface any remaining concerns.

### 7. Validate Configuration Files

Confirm that `appsettings.json` contains all configuration values that were previously held in `web.config` or `app.config`. Pay particular attention to:

- Connection strings
- Application-specific settings
- Logging configuration

### 8. Publish the Application

Once local validation is complete, produce a published output to verify the deployment artifact builds correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all expected files are present before deploying to the target environment.