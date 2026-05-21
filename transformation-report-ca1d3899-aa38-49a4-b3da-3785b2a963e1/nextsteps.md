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

Review test results and address any failures before proceeding.

### 4. Run the Application Locally

Start the application locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the application and confirm that core functionality, routing, and data access behave as expected.

### 5. Verify Database Connectivity

If the project uses a database (which is expected given the Northwind naming convention), confirm that:

- Connection strings in `appsettings.json` or `appsettings.Production.json` are updated to reflect the target environment.
- Any Entity Framework migrations are up to date by running:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 6. Review Target Framework

Open each `.csproj` file and confirm the `<TargetFramework>` element targets the intended .NET version (e.g., `net8.0`). Ensure consistency across all projects in the solution.

### 7. Check for Removed or Changed APIs

Even without build errors, some APIs behave differently in modern .NET compared to .NET Framework. Manually review areas such as:

- HTTP pipeline configuration (`Program.cs` / `Startup.cs`)
- Authentication and authorization middleware
- Session and caching configuration
- Any use of `System.Web` namespaces, which are not available in cross-platform .NET

### 8. Review `appsettings.json` Configuration

Confirm that all configuration values previously stored in `Web.config` or `App.config` have been correctly migrated to `appsettings.json` and that the application reads them using the `IConfiguration` interface.

### 9. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to ensure all expected files, static assets, and dependencies are present.

### 10. Smoke Test the Published Output

Run the published output directly to confirm it operates correctly outside of the development environment:

```bash
dotnet ./publish/Northwind.Web.dll
```

Verify that the application starts without errors and responds to requests as expected.