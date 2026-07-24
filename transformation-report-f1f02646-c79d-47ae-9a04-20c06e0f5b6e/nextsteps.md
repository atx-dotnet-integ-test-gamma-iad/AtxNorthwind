# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected in any of the projects within the solution, including the primary project `Northwind.Web`.

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

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the web application locally to confirm it runs as expected on the cross-platform .NET runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Verify the following:
- The application starts without runtime exceptions.
- All routes and pages load correctly.
- Database connections (if applicable) function as expected.
- Any authentication or authorization flows behave correctly.

### 5. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure this aligns with the version of the .NET SDK installed on all development and production machines.

### 6. Check for Removed or Changed APIs

Review the code for any use of APIs that existed in .NET Framework but have changed behavior in cross-platform .NET. Common areas to inspect include:

- `System.Web` usages, which are not available in cross-platform .NET.
- `HttpContext` and related types, which should now come from `Microsoft.AspNetCore.Http`.
- Configuration APIs, which should use `Microsoft.Extensions.Configuration` rather than `System.Configuration`.
- Any Windows-specific APIs such as the registry or Windows Identity Foundation.

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.PlatformAbstractions` tooling to identify remaining compatibility concerns.

### 7. Verify Static Files and Configuration

- Confirm that `wwwroot` contains all expected static assets.
- Verify that `appsettings.json` (and `appsettings.Production.json` if applicable) contains the correct configuration values that were previously in `Web.config` or `App.config`.
- Confirm that connection strings have been migrated to `appsettings.json` or environment variables.

### 8. Validate on Target Operating Systems

If cross-platform support is a requirement, test the application on each target operating system (e.g., Linux, macOS) to identify any platform-specific runtime issues that would not surface during a Windows build.

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Pay particular attention to:
- File path separators.
- Case sensitivity in file and directory names.
- Any P/Invoke or native library dependencies.