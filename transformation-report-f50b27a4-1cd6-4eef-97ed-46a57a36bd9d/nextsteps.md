# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are properly restored:

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

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended cross-platform .NET version (e.g., `net8.0`). Ensure no legacy `<TargetFrameworkVersion>` elements remain from the original project format.

### 4. Run the Application Locally

Start the application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Check the console output for any runtime errors, middleware configuration issues, or missing configuration values that would not surface at build time.

### 5. Check Configuration Files

- Verify that `appsettings.json` and `appsettings.Development.json` contain all necessary configuration keys that were previously in `Web.config` or `App.config`.
- Confirm that connection strings, application settings, and environment-specific values have been correctly migrated.

### 6. Run Unit and Integration Tests

If the solution contains test projects, execute them to validate runtime behavior:

```bash
dotnet test
```

Review any failing tests, as they may indicate behavioral differences introduced by the migration to cross-platform .NET.

### 7. Verify Static Files and Middleware

For a web project, confirm the following:

- Static files (CSS, JS, images) are served correctly.
- Any previously registered `HttpModules` or `HttpHandlers` from the legacy project have been replaced with the equivalent ASP.NET Core middleware.
- Authentication and authorization configuration has been correctly ported.

### 8. Check for Runtime Platform-Specific Code

Search the codebase for any APIs that were Windows-specific in the original project, such as:

- `System.Web` references
- Windows Registry access
- Windows-only file path assumptions

Use the .NET Compatibility Analyzer or review the output of:

```bash
dotnet build /p:EnableNETAnalyzers=true
```

### 9. Review Publish Output

Perform a publish to verify the output is complete and correct before any deployment:

```bash
dotnet publish --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files, including views, static assets, and configuration files, are present.