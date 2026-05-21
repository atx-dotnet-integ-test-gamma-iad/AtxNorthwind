# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation and Testing

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process, particularly around package compatibility or missing target frameworks.

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while non-blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify that existing functionality behaves as expected after the transformation:

```bash
dotnet test
```

Review test results and investigate any failures, as they may indicate behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Run the Application Locally
Start the `Northwind.Web` project and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Check the following:
- The application starts without runtime exceptions.
- All routes and pages load correctly.
- Database connections (if applicable) are functional.
- Any middleware or startup configuration behaves as intended.

### 5. Review Configuration Files
Inspect `appsettings.json` and any environment-specific configuration files (`appsettings.Development.json`, etc.) to confirm that:
- Connection strings are correct for the target environment.
- Any settings that were previously in `Web.config` have been properly migrated.
- Environment variables are set appropriately.

### 6. Check for Runtime Compatibility Issues
Even without build errors, certain APIs behave differently or are unavailable on cross-platform .NET. Manually test or review code that uses:
- `System.Web` namespaces (these are not available in modern .NET).
- Windows-specific APIs (e.g., registry access, Windows Authentication).
- Third-party libraries that may have been updated or replaced during transformation.

### 7. Verify Static Assets and Views
For a web project, confirm that:
- Static files (CSS, JavaScript, images) are served correctly.
- Razor views or pages render without errors.
- Any bundling or minification configuration is functioning.

### 8. Review Target Framework
Confirm the target framework in `Northwind.Web.csproj` is set to the intended version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If a newer LTS version of .NET is available and desired, update this value and re-run the build and tests.

### 9. Publish the Application
Once local validation is complete, produce a published output to verify the deployment artifact:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files are present, then deploy the contents to the target environment.