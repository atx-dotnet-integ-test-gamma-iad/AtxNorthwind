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

Review the test results and investigate any failures before proceeding.

### 4. Run the Application Locally

Start the application locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Manually exercise the key areas of the application, such as database connectivity, routing, and any external service integrations.

### 5. Verify Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended cross-platform .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a compatible framework version.

### 6. Check for Removed or Changed APIs

Review the code for any usage of APIs that were available in .NET Framework but have changed behavior in modern .NET. Key areas to check include:

- `System.Web` references (these are not available in modern .NET)
- `HttpContext` and related types (now found in `Microsoft.AspNetCore.Http`)
- Configuration APIs (`System.Configuration` vs `Microsoft.Extensions.Configuration`)
- Any Windows-specific APIs if cross-platform support is required

### 7. Review Runtime Configuration Files

Confirm that `appsettings.json` (and environment-specific variants such as `appsettings.Development.json`) are present and correctly configured, replacing any legacy `web.config` or `app.config` settings where applicable.

### 8. Validate Database Connectivity

If the project uses Entity Framework or direct database access, verify that:

- The connection strings in `appsettings.json` are correct
- The database provider NuGet package targets .NET-compatible versions
- Any pending migrations are applied using:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 9. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all required files are present.

### 10. Test the Published Output

Run the published output directly to confirm it behaves consistently with the local development run:

```bash
dotnet ./publish/Northwind.Web.dll
```