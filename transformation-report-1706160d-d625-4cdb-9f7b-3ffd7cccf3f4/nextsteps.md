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

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of any compile-time errors:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality behaves as expected after the transformation:

```bash
dotnet test --configuration Release
```

Review test results and investigate any failures, as they may indicate behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Check for Runtime Compatibility Issues

Some issues do not surface at compile time. Pay attention to the following areas that commonly differ between .NET Framework and cross-platform .NET:

- **Configuration**: Ensure `Web.config` or `App.config` settings have been migrated to `appsettings.json` or environment variables where applicable.
- **Database connectivity**: Verify that connection strings are correct and that the chosen database provider (e.g., `Microsoft.Data.SqlClient`) is compatible with your target environment.
- **File paths**: Confirm that any hardcoded file paths use `Path.Combine` or are otherwise platform-neutral.
- **Windows-specific APIs**: Check for any usage of Windows-only APIs (e.g., `System.Drawing`, registry access, COM interop) that may not function on Linux or macOS.

### 5. Run the Application Locally

Start the application locally and exercise its primary workflows:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the application and verify that pages render correctly, data is retrieved as expected, and no unhandled exceptions occur.

### 6. Review Nullable Reference Type Warnings

Cross-platform .NET projects often enable nullable reference types by default. Review any nullable warnings in the build output and address them to improve code robustness:

```bash
dotnet build --configuration Release /warnaserror:nullable
```

### 7. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure this aligns with your organization's supported .NET version.

### 8. Publish the Application

Once local validation is complete, produce a published output to verify the deployment artifact builds correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files are present, including static assets and configuration files.