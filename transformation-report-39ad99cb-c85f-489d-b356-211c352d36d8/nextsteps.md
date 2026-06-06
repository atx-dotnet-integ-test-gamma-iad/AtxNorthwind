# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the transformation:

```bash
dotnet test --configuration Release
```

Review test results and investigate any failures, as they may indicate behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Check Runtime Behavior

Run the web application locally to confirm it starts and operates correctly:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

- Verify that all routes respond as expected.
- Check that database connections (if applicable) are functioning correctly.
- Review application logs for any runtime exceptions that would not surface at build time.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to an actively supported version of .NET, such as `net8.0`. If it is targeting an older version like `net6.0` or `net7.0`, consider updating to a long-term support (LTS) release.

### 6. Review Removed or Changed APIs

Cross-platform .NET removes certain APIs that were available in .NET Framework. Use the .NET Upgrade Assistant compatibility analyzer or the following command to check for any remaining compatibility concerns:

```bash
dotnet build /p:EnableNETAnalyzers=true
```

Pay particular attention to:
- `System.Web` namespace usage, which is not available in cross-platform .NET.
- Windows-specific APIs that may not function on Linux or macOS.
- Any use of `BinaryFormatter`, which is disabled by default in modern .NET.

### 7. Verify Configuration Migration

Ensure that `web.config` settings have been properly migrated to `appsettings.json` or `appsettings.{Environment}.json`. Confirm that connection strings, application settings, and any custom configuration sections are present and correctly structured.

### 8. Test on Target Platform

If the goal is cross-platform deployment, run and test the application on the intended target operating system (Linux or macOS) to surface any platform-specific issues that would not appear when building on Windows.