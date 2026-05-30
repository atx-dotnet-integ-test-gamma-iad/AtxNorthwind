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

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the transformation:

```bash
dotnet test --configuration Release
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the application locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Manually exercise the key areas of the application, such as routing, database access, and any external service integrations.

### 5. Verify Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended cross-platform .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target compatible frameworks.

### 6. Check for Removed or Changed APIs

Review the code for any usage of APIs that existed in .NET Framework but have changed behavior in modern .NET. Common areas to check include:

- `System.Web` references (should be fully removed)
- `HttpContext` and middleware usage (should use ASP.NET Core equivalents)
- Configuration APIs (should use `Microsoft.Extensions.Configuration`)
- Any `app.config` or `web.config` settings that need to be migrated to `appsettings.json`

### 7. Validate Database Connectivity

If the application uses Entity Framework or direct database access, confirm that connection strings in `appsettings.json` are correctly configured and that the application can connect to the database at runtime.

### 8. Review Publish Output

Publish the application to a local folder and inspect the output to ensure all required assets, static files, and configuration files are present:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory before deploying to any environment.