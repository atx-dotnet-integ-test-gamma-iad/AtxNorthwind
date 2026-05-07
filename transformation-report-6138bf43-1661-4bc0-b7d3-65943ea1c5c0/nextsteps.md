# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the root of the solution to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors are produced during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of any compilation errors:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the migration:

```bash
dotnet test
```

Review the test results and investigate any failures.

### 4. Run the Application Locally

Start the web application locally to confirm it runs as expected on the new cross-platform .NET runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate to the application in a browser and exercise the core functionality, paying attention to:

- Database connectivity and query results
- Authentication and authorization flows, if present
- Any file system operations that may have path separator differences between Windows and Linux/macOS

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure this aligns with the .NET version installed on your target runtime environment.

### 6. Check for Removed or Changed APIs

Review the codebase for usage of any APIs that were available in .NET Framework but have changed behavior or been removed in cross-platform .NET. Common areas to check include:

- `System.Web` references, which are not available in cross-platform .NET
- `ConfigurationManager` usage, which should be replaced with `Microsoft.Extensions.Configuration`
- Windows-specific APIs such as the registry or certain `System.Drawing` methods
- `HttpContext` and related types, which have moved to `Microsoft.AspNetCore.Http`

### 7. Validate Configuration Files

Confirm that `appsettings.json` (and environment-specific variants such as `appsettings.Production.json`) contain all configuration values that were previously held in `Web.config` or `App.config`. Verify connection strings and application settings are correct.

### 8. Test on Target Operating System

If the intent is to run the application on Linux or macOS, test the application on that operating system specifically. Pay attention to:

- File path casing, as Linux file systems are case-sensitive
- Line ending differences in any file processing logic
- Permissions on directories the application needs to read from or write to

### 9. Publish the Application

Once local validation is complete, publish the application using the following command, adjusting the runtime identifier as needed:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all expected files are present before deploying to the target environment.