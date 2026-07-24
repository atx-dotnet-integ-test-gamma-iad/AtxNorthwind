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

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version of .NET (e.g., `net8.0`). Avoid `netcoreapp*` or `net5.0` / `net6.0` if long-term support is a requirement.

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 4. Run Unit Tests

If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

### 5. Run the Application Locally

Start the web application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate to the URL shown in the console output (typically `https://localhost:5001` or `http://localhost:5000`) and verify core functionality.

### 6. Check for Runtime-Only Issues

Build errors do not capture all potential problems. Manually exercise the following areas at runtime:

- **Database connectivity**: Confirm connection strings in `appsettings.json` are correct and that any Entity Framework migrations are up to date. Run `dotnet ef database update` if applicable.
- **Authentication/Authorization**: If the project uses ASP.NET Core Identity or external providers, verify login and role-based access still functions correctly.
- **Static files and routing**: Confirm that all pages, API routes, and static assets (CSS, JS) load without 404 errors.
- **Configuration binding**: Ensure all values previously read from `Web.config` or `app.config` have been correctly migrated to `appsettings.json` and are being read at runtime.

### 7. Review Removed or Changed APIs

Cross-reference any usages of APIs that were available in .NET Framework but have changed behavior or been removed in cross-platform .NET. The [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the [Platform Compatibility Analyzer](https://learn.microsoft.com/en-us/dotnet/standard/analyzers/platform-compat-analyzer) can assist with identifying these at the code level.

### 8. Publish the Application

Once local validation is complete, produce a published output:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all expected files are present before deploying to the target environment.