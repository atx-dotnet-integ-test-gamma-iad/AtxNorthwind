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
If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review the test output for any failures or skipped tests that may indicate behavioral differences introduced by the migration.

### 4. Run the Application Locally
Start the web application and verify it runs as expected on the cross-platform runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following manually:
- The application starts without runtime exceptions.
- Key routes and pages load correctly.
- Database connections (if any) are functional and connection strings are correctly configured for the new environment.
- Any authentication or session-based functionality behaves as expected.

### 5. Review `appsettings.json` and Environment Configuration
Confirm that configuration files have been updated appropriately:
- Connection strings reference the correct database server and credentials for the target environment.
- Any environment-specific settings (e.g., `appsettings.Development.json`) are present and correct.
- Secrets are not hardcoded and are managed via environment variables or a secrets manager.

### 6. Check for Runtime-Only Issues
Some issues do not surface at build time. Pay attention to:
- Reflection-based code that may behave differently under .NET.
- Any use of `System.Web` types that may have been shimmed or replaced — verify the replacements work correctly at runtime.
- File path handling, as path separators differ between Windows and Linux/macOS.
- Any third-party libraries that were updated as part of the migration — review their changelogs for breaking changes.

### 7. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a compatible framework version.

### 8. Publish the Application
Once local validation is complete, produce a published output to confirm the deployment artifact builds correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected files are present, including static assets and configuration files.