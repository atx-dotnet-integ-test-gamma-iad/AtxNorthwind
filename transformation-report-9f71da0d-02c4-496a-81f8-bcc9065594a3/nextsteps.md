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

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Check the output for any warnings that, while non-blocking, may indicate areas of concern such as nullable reference type warnings or obsolete API usage.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify runtime behavior is intact after the transformation:

```bash
dotnet test --configuration Release
```

Review test results for any failures that may indicate behavioral regressions introduced during the migration.

### 4. Run the Application Locally

Start the application and verify it runs as expected on the target platform:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Manually verify the following:
- The application starts without runtime exceptions.
- Key routes and pages load correctly.
- Database connectivity is functional (if applicable).
- Any authentication or authorization flows behave as expected.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all dependent projects in the solution target a compatible framework version.

### 6. Review Removed or Changed APIs

Cross-platform .NET removes or changes certain APIs that were available in .NET Framework. Manually review the following areas:

- **Configuration**: Ensure `System.Configuration.ConfigurationManager` usages have been replaced with `Microsoft.Extensions.Configuration`.
- **HTTP**: Confirm there are no remaining references to `System.Web.HttpContext` or related types; these should be replaced with `Microsoft.AspNetCore.Http`.
- **WCF or Remoting**: If the original project used WCF services or .NET Remoting, verify appropriate replacements are in place.
- **Global.asax**: Confirm any logic from `Global.asax` has been migrated to `Program.cs` or `Startup.cs`.

### 7. Check Runtime Configuration Files

Verify that `appsettings.json` (and environment-specific variants such as `appsettings.Production.json`) contain all configuration values that were previously held in `Web.config` or `App.config`.

### 8. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all expected assets, static files, and configuration files are present.

### 9. Verify on Target Operating System

If the goal of the migration was cross-platform support, run the published output on the intended target operating system (Linux or macOS) to confirm there are no platform-specific runtime issues:

```bash
./publish/Northwind.Web
```

Pay particular attention to:
- File path separators (use `Path.Combine` rather than hardcoded separators).
- Case-sensitive file system behavior on Linux.
- Any P/Invoke or Windows-specific interop calls that may not be available.