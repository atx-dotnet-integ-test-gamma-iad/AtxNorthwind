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
If test projects exist in the solution, execute them to verify that existing functionality has not been broken by the migration:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any test failures carefully, as behavioral differences can exist between .NET Framework and cross-platform .NET even when the build succeeds.

### 4. Run the Web Application Locally
Start the application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Check the following at runtime:
- Application starts without exceptions
- All routes and endpoints respond correctly
- Database connections and queries function as expected
- Authentication and authorization behave as intended

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure this aligns with your organization's supported .NET version.

### 6. Check for Removed or Changed APIs
Even without build errors, some APIs behave differently or have been removed in cross-platform .NET. Review the following areas manually:

- **Configuration**: Ensure `System.Configuration.ConfigurationManager` usage has been replaced with `Microsoft.Extensions.Configuration` where applicable.
- **HTTP modules and handlers**: These do not exist in ASP.NET Core; confirm any such components were properly migrated to middleware.
- **`System.Web` dependencies**: Confirm no remaining references exist, as this namespace is not available in cross-platform .NET.
- **Entity Framework**: If using EF6, confirm whether a migration to EF Core is needed or if the EF6 cross-platform package is in use.

### 7. Review Startup and Middleware Configuration
Confirm that `Program.cs` and any middleware configuration correctly replaces what was previously in `Global.asax` and `Web.config`. Pay particular attention to:

- Service registration
- Middleware pipeline order
- Environment-specific configuration loading

### 8. Validate Configuration Files
`Web.config` is not used for application configuration in cross-platform .NET. Confirm that settings have been moved to `appsettings.json` or environment variables, and that connection strings are accessible at runtime.

### 9. Publish the Application
Once local validation is complete, publish the application to verify the output is complete and correct:

```bash
dotnet publish --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files are present, then deploy the output to your target environment.