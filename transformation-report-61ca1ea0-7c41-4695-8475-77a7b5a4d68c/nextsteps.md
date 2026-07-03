# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the root of the solution to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of any compilation errors:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not prevent compilation.

### 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended cross-platform .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a compatible framework version.

### 4. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review any failing tests and address them before proceeding.

### 5. Run the Application Locally

Start the web application locally to confirm it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate to the application in a browser and exercise the core functionality, particularly any areas that relied on Windows-specific APIs or legacy .NET Framework features prior to the transformation.

### 6. Check for Runtime Compatibility Issues

Pay attention to the following areas that commonly surface as runtime issues rather than build errors after a cross-platform migration:

- **File path separators**: Ensure no hardcoded backslashes (`\`) are used in file path logic. Use `Path.Combine` or `Path.DirectorySeparatorChar` instead.
- **Windows Registry access**: Any code referencing `Microsoft.Win32.Registry` will not function on Linux or macOS.
- **Windows-specific authentication**: If the application used Windows Authentication, verify the replacement mechanism is configured correctly.
- **Configuration files**: Confirm that `appsettings.json` contains all settings that were previously in `Web.config` or `App.config`.
- **Entity Framework or data access**: If the project uses a database, verify the connection strings and provider packages are correct for the target platform.

### 7. Validate Static Assets and Views

If the project uses Razor views, static files, or bundling, verify that:

- Static files are served correctly under `wwwroot`.
- Any bundling or minification configuration has been migrated from `BundleConfig.cs` to the appropriate tooling (e.g., LibMan or npm-based tooling).

### 8. Review Removed or Changed APIs

Cross-reference any use of APIs that were available in .NET Framework but have changed or been removed in modern .NET. The [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the [Platform Compatibility Analyzer](https://learn.microsoft.com/en-us/dotnet/standard/analyzers/platform-compat-analyzer) can assist with identifying these at development time.