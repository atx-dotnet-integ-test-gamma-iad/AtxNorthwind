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

### 3. Run Unit and Integration Tests
If test projects exist in the solution, execute them to verify that existing functionality behaves as expected after the migration:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any test failures carefully, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Run the Application Locally
Start the web application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following manually:
- Application starts without runtime exceptions
- All routes and pages load correctly
- Database connections (if any) are functional
- Authentication and authorization flows work as expected

### 5. Review Runtime Behavior Differences
Even without build errors, certain areas may behave differently under cross-platform .NET compared to .NET Framework. Pay particular attention to:

- **`System.Web` dependencies**: Any code that previously relied on `System.Web` may have been replaced with ASP.NET Core equivalents. Verify that HTTP context access, session state, and request/response handling work correctly.
- **Configuration**: Ensure `appsettings.json` (and environment-specific variants) contain all settings that were previously in `web.config` or `app.config`.
- **Entity Framework**: If the project uses Entity Framework, confirm whether it was migrated to EF Core and validate that all queries return correct results, as some LINQ translations differ between EF6 and EF Core.
- **Globalization and encoding**: Cross-platform .NET may handle certain culture and encoding scenarios differently. Test any locale-sensitive functionality.
- **File paths**: Verify that any hardcoded or constructed file paths use `Path.Combine` or equivalent to ensure cross-platform compatibility.

### 6. Check the `web.config` / `appsettings.json`
Confirm that all connection strings, application settings, and any middleware configuration have been correctly carried over from the legacy `web.config` into the appropriate ASP.NET Core configuration files.

### 7. Verify Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a compatible framework version.

### 8. Publish the Application
Once local validation is complete, produce a published output to confirm the deployment artifact is generated correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all expected files, static assets, and configuration files are present before deploying to your target environment.