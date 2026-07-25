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

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Debug
dotnet build --configuration Release
```

### 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a current and supported .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is targeting an older version such as `net6.0` or `net7.0`, consider updating it to `net8.0` as those versions are approaching or have reached end of life.

### 4. Run the Application Locally

Start the application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate to the application URL printed in the console output and confirm the expected pages or endpoints respond correctly.

### 5. Run Existing Tests

If the solution contains test projects, execute them to confirm no regressions were introduced during the transformation:

```bash
dotnet test
```

Review the test output for any failures or skipped tests that may indicate compatibility issues.

### 6. Review Removed or Changed APIs

Cross-platform .NET removes or changes certain Windows-specific APIs. Manually review the following areas if they are used in the project:

- **Registry access** (`Microsoft.Win32.Registry`) — not available on Linux/macOS without additional packages.
- **Windows Authentication** — requires additional configuration on non-Windows hosts.
- **`System.Drawing`** — GDI+ based drawing is not fully supported cross-platform; consider migrating to `SkiaSharp` or `ImageSharp` if used.
- **`HttpContext.Current`** — not available in ASP.NET Core; ensure all middleware and request handling uses injected `IHttpContextAccessor` instead.

### 7. Review Configuration Files

Ensure that `appsettings.json` contains all configuration values that were previously in `Web.config` or `App.config`. Pay particular attention to:

- Connection strings
- Application settings keys
- Custom configuration sections

### 8. Verify Static Files and Bundling

If the project previously used ASP.NET bundling and minification (`System.Web.Optimization`), confirm that a replacement such as `WebOptimizer` or a front-end build tool has been configured and that static assets are served correctly.

### 9. Check Database Connectivity

If the project uses Entity Framework, confirm the correct provider package is referenced (e.g., `Microsoft.EntityFrameworkCore.SqlServer`) and that migrations are up to date:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 10. Publish the Application

Once all validation steps pass, produce a published output to confirm the release artifact builds cleanly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all expected files are present before deploying to the target environment.