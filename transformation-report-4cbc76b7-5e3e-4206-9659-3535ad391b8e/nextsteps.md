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

Review any failing tests and determine whether they are caused by the migration or were pre-existing issues.

### 4. Run the Application Locally

Start the application to confirm it runs as expected on the new runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the application and verify that core functionality, routing, and data access behave correctly.

### 5. Check for Removed or Changed APIs

Review the code for usage of any APIs that were available in .NET Framework but have changed behavior in cross-platform .NET. Pay particular attention to:

- `System.Web` dependencies, which are not available in cross-platform .NET
- Windows-specific APIs such as the registry, WCF server-side components, or Windows Identity Foundation
- Any third-party libraries that may have been targeting .NET Framework exclusively

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.ApiCompat` tooling to identify any remaining compatibility concerns.

### 6. Verify Configuration Files

Confirm that `appsettings.json` (and environment-specific variants such as `appsettings.Development.json`) are correctly configured, as the legacy `Web.config` and `App.config` patterns are replaced in cross-platform .NET. Ensure that:

- Connection strings are correctly defined
- Any environment-specific settings are present
- Logging configuration is appropriate

### 7. Validate Database Connectivity

If the application uses a database (as is typical for a Northwind-based project), confirm that:

- The connection string points to the correct database instance
- Migrations (if using Entity Framework Core) are up to date by running:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

- Data is returned correctly through the application's data access layer

### 8. Review Target Framework

Open each `.csproj` file and confirm the `<TargetFramework>` element is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure consistency across all projects in the solution.

### 9. Publish the Application

Once local validation is complete, produce a published output to confirm the application can be packaged correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected files are present.