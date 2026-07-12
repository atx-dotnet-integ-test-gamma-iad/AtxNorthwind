# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process, particularly around package compatibility or missing packages.

### 2. Build the Solution

Perform a full solution build to confirm the absence of errors in a clean build context:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality behaves as expected after the migration:

```bash
dotnet test
```

Review test results carefully. Any failing tests should be investigated as they may indicate behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Verify Runtime Behavior

Run the web application locally to confirm it starts and operates correctly:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Check the following:
- The application starts without runtime exceptions.
- All routes and endpoints respond as expected.
- Database connections (if applicable) are functioning correctly.
- Any static files, middleware, or configuration files (e.g., `appsettings.json`) are loading properly.

### 5. Review Replaced or Removed APIs

Cross-platform .NET does not support certain APIs that were available in .NET Framework. Manually review the codebase for any usage of the following common incompatible areas:

- `System.Web` namespace references (these are not available in modern .NET).
- Windows-specific APIs such as the registry, WCF server-side components, or `System.Drawing` without the appropriate compatibility package.
- `app.config` or `web.config` configurations that may need to be migrated to `appsettings.json` or environment variables.

### 6. Check Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` element is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target compatible frameworks to avoid any runtime assembly loading issues.

### 7. Validate Configuration and Environment Settings

- Confirm that connection strings and application settings have been correctly migrated from `web.config` to `appsettings.json`.
- Verify that environment-specific configuration files (e.g., `appsettings.Development.json`) are present and correct.
- Ensure that any environment variables required by the application are documented and set appropriately.

### 8. Review Dependency Versions

Check that all NuGet packages referenced in the project files are compatible with the target .NET version. Use the following command to identify outdated packages:

```bash
dotnet list package --outdated
```

Update packages where necessary, being cautious of breaking changes in major version upgrades.