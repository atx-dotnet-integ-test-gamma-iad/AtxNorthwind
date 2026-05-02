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

If the solution contains test projects, execute them to verify runtime behavior is intact:

```bash
dotnet test
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the web application locally to verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

- Confirm the application starts without runtime exceptions.
- Navigate through the application's key pages and endpoints.
- Check application logs for any runtime errors or unhandled exceptions.

### 5. Verify Database Connectivity

If the application uses a database (which is expected given the Northwind naming convention):

- Confirm the connection string in `appsettings.json` or `appsettings.Production.json` is correct for the target environment.
- Verify that any Entity Framework Core migrations are up to date by running:

```bash
dotnet ef migrations list --project src/Northwind.Web/Northwind.Web.csproj
```

- Apply any pending migrations if necessary:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 6. Review Target Framework

Open each `.csproj` file and confirm the `<TargetFramework>` element is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure consistency across all projects in the solution.

### 7. Check for Removed or Changed APIs

Even without build errors, some APIs behave differently in cross-platform .NET compared to .NET Framework. Pay particular attention to:

- `System.Web` usages that may have been replaced with ASP.NET Core equivalents.
- Any Windows-specific APIs (registry access, Windows authentication, etc.) that may fail on non-Windows platforms.
- HTTP module or HTTP handler patterns that should now be implemented as ASP.NET Core middleware.

### 8. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files are present, including static assets and configuration files.