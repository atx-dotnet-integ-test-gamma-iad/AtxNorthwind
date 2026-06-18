# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Verify that no warnings or errors appear related to missing or incompatible packages.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality behaves as expected:

```bash
dotnet test --configuration Release
```

Review test results and investigate any failures, as they may indicate behavioral differences introduced by the migration to cross-platform .NET.

### 4. Run the Application Locally

Start the web application and verify it runs correctly:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following:
- The application starts without runtime exceptions.
- All routes and pages load as expected.
- Database connections (if applicable) function correctly on the target platform.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version, such as `net8.0`. Versions such as `net6.0` or `net7.0` are either end-of-life or approaching it.

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 6. Review Removed or Changed APIs

Cross-platform .NET does not support certain APIs that were available in .NET Framework. Manually review the codebase for usage of the following common problem areas:

- `System.Web` namespace references (not supported in .NET Core/.NET 5+)
- `HttpContext` usage outside of the request pipeline
- Windows-specific APIs such as the registry, WMI, or COM interop
- `BinaryFormatter` (deprecated and disabled by default)
- `AppDomain.CreateDomain` (not supported)

### 7. Check Runtime Behavior on Target OS

If the intent is to run on Linux or macOS, test the application on that operating system explicitly. Pay attention to:

- File path separators (`\` vs `/`)
- Case-sensitive file system differences
- Any P/Invoke or native library dependencies that are Windows-specific

### 8. Review Configuration and Environment Variables

Ensure that `appsettings.json` and environment-specific configuration files (`appsettings.Development.json`, etc.) are correctly set up for the new hosting model. Confirm that connection strings and other settings are valid in the target environment.