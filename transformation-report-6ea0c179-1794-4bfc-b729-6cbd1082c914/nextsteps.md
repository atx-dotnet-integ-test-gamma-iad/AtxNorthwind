# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation and Testing

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while non-breaking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may indicate behavioral differences introduced by the migration to cross-platform .NET even when the build itself succeeds.

### 4. Run the Application Locally
Start the `Northwind.Web` project and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following at runtime:
- Application starts without exceptions
- All routes and endpoints respond correctly
- Database connectivity functions as expected (connection strings may need to be updated for the new environment)
- Any static files, views, or Razor pages render correctly

### 5. Review Configuration Files
Cross-platform .NET no longer uses `Web.config` for application configuration. Confirm that:
- Settings have been migrated to `appsettings.json` and `appsettings.{Environment}.json`
- Environment-specific configuration (e.g., connection strings) is correctly set for each target environment
- Any configuration previously handled by `system.web` or IIS-specific settings has been replaced with the appropriate ASP.NET Core middleware or hosting configuration

### 6. Review Middleware and HTTP Pipeline
If the project previously used `HttpModules` or `HttpHandlers`, confirm they have been replaced with the equivalent ASP.NET Core middleware registered in `Program.cs` or `Startup.cs`.

### 7. Validate Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 8. Publish the Application
Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all expected files are present before deploying to the target environment.