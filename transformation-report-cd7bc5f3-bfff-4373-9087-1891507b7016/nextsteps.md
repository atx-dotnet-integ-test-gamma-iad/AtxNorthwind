# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Verify that no warnings or errors are reported during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of errors:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while non-blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken by the migration:

```bash
dotnet test --configuration Release
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the web application locally to confirm it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Verify the following:
- The application starts without runtime exceptions.
- All routes and pages load correctly.
- Any database connections (e.g., to a Northwind database) are functioning properly.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it targets an older version such as `net6.0` or `net7.0`, consider updating to the latest supported LTS release.

### 6. Review Removed or Changed APIs

Check the codebase for any usage of APIs that were available in .NET Framework but have changed behavior in cross-platform .NET. Common areas to review include:

- `System.Web` references (should have been replaced with ASP.NET Core equivalents).
- `HttpContext`, `HttpRequest`, and `HttpResponse` usage.
- Configuration APIs (e.g., `ConfigurationManager` replaced with `IConfiguration`).
- Any Windows-specific APIs that may not behave correctly on non-Windows platforms.

### 7. Verify Static Assets and Configuration Files

Confirm that the following files are present and correctly configured:

- `appsettings.json` and `appsettings.{Environment}.json` for application configuration.
- `wwwroot` folder for static assets such as CSS, JavaScript, and images.
- `Program.cs` for the application entry point and middleware configuration.

### 8. Test on Target Platform

If the intent is to run the application on a non-Windows platform (Linux or macOS), deploy and run the application on that platform to identify any remaining platform-specific issues:

```bash
dotnet publish --configuration Release --runtime linux-x64 --self-contained false
```

Then transfer the published output to the target machine and run it to confirm cross-platform compatibility.