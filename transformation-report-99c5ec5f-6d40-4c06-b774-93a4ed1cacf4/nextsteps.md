# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected in any of the projects within the solution, including the primary project `Northwind.Web.csproj`.

## Validation

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or missing packages.

### 2. Build the Solution

Perform a full solution build to confirm there are no errors:

```bash
dotnet build --configuration Release
```

Address any warnings that surface during the build, particularly those related to nullable reference types or deprecated APIs, as these can indicate areas that may cause runtime issues.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken by the migration:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Verify Runtime Behavior

Run the web application locally and navigate through its core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following areas manually:

- **Database connectivity**: Confirm that connection strings in `appsettings.json` (or equivalent) are correctly configured for the target environment.
- **Authentication and authorization**: Verify that any middleware or authentication schemes function as expected under ASP.NET Core.
- **Static files**: Confirm that static assets (CSS, JavaScript, images) are being served correctly.
- **Routing**: Navigate through all major routes to confirm they resolve correctly.
- **Logging**: Review application logs for any runtime exceptions or warnings that were not present during the build phase.

### 5. Review Configuration Files

Cross-platform .NET uses `appsettings.json` rather than `Web.config` or `App.config`. Confirm that:

- All configuration values from the legacy config files have been migrated.
- Environment-specific settings are handled using `appsettings.{Environment}.json` files.
- Any `Web.config` transforms that existed in the legacy project have been accounted for.

### 6. Check for Windows-Specific Dependencies

Even when a build succeeds, there may be runtime dependencies on Windows-specific APIs or libraries. Review the codebase for usage of:

- `Microsoft.Win32` namespaces
- Windows Registry access
- COM interop
- `System.Drawing` (which has platform limitations outside of Windows)

If cross-platform deployment is a goal, these areas will need to be addressed with platform-neutral alternatives.

### 7. Validate Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure this version is consistent across all projects in the solution to avoid inter-project compatibility issues.

### 8. Publish the Application

Once validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all required files are present, including configuration files, static assets, and dependent assemblies.