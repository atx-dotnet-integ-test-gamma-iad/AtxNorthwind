# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most critical project `Northwind.Web`.

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

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not prevent a successful build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the transformation:

```bash
dotnet test --configuration Release
```

Review the test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the application locally to confirm it runs as expected on the new cross-platform .NET runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Manually verify the following:
- The application starts without runtime exceptions.
- Core routes and pages load correctly.
- Database connectivity is functioning if applicable.
- Any authentication or authorization flows behave as expected.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version of .NET, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is targeting an older version such as `net6.0` or `net7.0`, consider updating to the latest Long Term Support (LTS) release and re-running the build and tests.

### 6. Review Removed or Changed APIs

Check the codebase for any usage of APIs that were available in .NET Framework but have changed behavior in cross-platform .NET. Common areas to review include:

- `System.Web` references, which are not available in cross-platform .NET.
- `HttpContext` and related ASP.NET APIs, which have changed in ASP.NET Core.
- Windows-specific APIs such as the registry, WCF server-side components, or `System.Drawing` without a compatible replacement.
- Configuration, which has moved from `Web.config` to `appsettings.json` and the `IConfiguration` system.

### 7. Verify Configuration Files

Ensure that any `Web.config` settings that were relevant to application behavior have been migrated to `appsettings.json` or the appropriate ASP.NET Core configuration mechanism. Confirm that connection strings, logging settings, and application-specific keys are present and correct.

### 8. Test on Target Operating Systems

If cross-platform support is a goal, test the application on each intended operating system (for example, Windows, Linux, or macOS) to identify any platform-specific runtime issues that would not surface during a build.