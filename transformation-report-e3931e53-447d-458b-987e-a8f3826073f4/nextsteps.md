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

Verify that no warnings or errors are reported during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the transformation:

```bash
dotnet test --configuration Release
```

Review test results and investigate any failures before proceeding.

### 4. Run the Application Locally

Start the application locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Manually verify the following:
- The application starts without runtime exceptions.
- Key routes and pages load as expected.
- Database connectivity is functioning if applicable.
- Any authentication or authorization flows behave correctly.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a compatible framework version.

### 6. Review Removed or Changed APIs

Check the codebase for any usage of APIs that were available in .NET Framework but have changed behavior in modern .NET, including:

- `System.Web` references (these are not available in modern .NET)
- `HttpContext` usage patterns
- Configuration APIs (e.g., `ConfigurationManager` replaced by `Microsoft.Extensions.Configuration`)
- Any platform-specific APIs that may not behave identically on Linux or macOS

### 7. Verify Configuration Files

Confirm that `appsettings.json` (and environment-specific variants such as `appsettings.Production.json`) contain all necessary configuration values that were previously held in `web.config` or `app.config`.

### 8. Check Static Assets and Middleware

For a web project, verify that:
- Static files are served correctly.
- Middleware is registered in the correct order in `Program.cs` or `Startup.cs`.
- Any HTTP modules or HTTP handlers from the legacy project have been replaced with equivalent ASP.NET Core middleware.

### 9. Publish the Application

Once local validation is complete, publish the application to confirm the output is clean:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all expected files are present.

### 10. Deploy to Target Environment

Copy the published output to the target server or hosting environment. Ensure the target machine has the appropriate .NET runtime installed:

```bash
dotnet --list-runtimes
```

The runtime version should match or be compatible with the `TargetFramework` specified in the project file.