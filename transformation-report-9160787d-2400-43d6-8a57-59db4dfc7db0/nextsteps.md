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
If the solution contains test projects, execute them to verify that existing functionality behaves as expected after the migration:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any test failures carefully, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Review Runtime Dependencies
Check for any dependencies that were conditionally compiled or relied on Windows-specific APIs (e.g., `System.Web`, COM interop, Windows Registry access, MSMQ). These will not produce build errors but may cause runtime failures on non-Windows platforms.

### 5. Run the Application Locally
Start the application and exercise its primary workflows:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Verify that:
- All routes and endpoints respond correctly.
- Database connections are established successfully.
- Authentication and authorization behave as expected.
- Static assets are served correctly.

### 6. Review `appsettings.json` and Configuration
Confirm that configuration values previously stored in `Web.config` or `App.config` have been correctly migrated to `appsettings.json` or environment-specific variants such as `appsettings.Production.json`. Pay particular attention to:
- Connection strings
- Application-specific keys and settings
- Logging configuration

### 7. Validate Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all dependent projects target a compatible framework version.

### 8. Check for Nullable Reference Type Warnings
If nullable reference types are enabled, review any compiler warnings that surfaced during the build. While these are not errors, they can indicate potential null-reference issues at runtime:

```bash
dotnet build --configuration Release /warnaserror
```

### 9. Publish the Application
Once local validation is complete, produce a published output to verify the deployment artifact is generated correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files, including static content and configuration files, are present.