# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the root of the solution to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs, missing references, or compatibility concerns that were not caught as errors.

### 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all dependent projects in the solution target a compatible framework version.

### 4. Run the Application Locally

Start the application using the .NET CLI:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate to the URL shown in the console output and verify that the application loads and behaves as expected.

### 5. Run Existing Tests

If the solution contains test projects, execute them to validate that existing functionality has not regressed:

```bash
dotnet test
```

Review the test results for any failures and address them before proceeding.

### 6. Check for Removed or Changed APIs

Review any usage of APIs that were available in the legacy .NET Framework but have changed behavior or been removed in cross-platform .NET. Pay particular attention to:

- `System.Web` dependencies, which are not available in modern .NET
- Windows-specific APIs (e.g., registry access, Windows authentication)
- Any third-party libraries that may have been targeting .NET Framework exclusively

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.ApiCompat` tooling to surface any remaining compatibility concerns.

### 7. Validate Configuration

Confirm that configuration files have been properly migrated. In modern .NET, `Web.config` is replaced by `appsettings.json`. Verify that:

- All connection strings are present in `appsettings.json`
- Application settings have been transferred correctly
- Environment-specific configuration (e.g., `appsettings.Development.json`) is in place

### 8. Verify Database Connectivity

If the application uses a database (as implied by the Northwind naming convention), confirm that:

- The connection string is correctly configured
- The database provider NuGet package (e.g., `Microsoft.EntityFrameworkCore.SqlServer`) is referenced and up to date
- Any database migrations run successfully:

```bash
dotnet ef database update
```

### 9. Publish the Application

Once the above steps are validated, produce a published output to confirm the application can be packaged correctly:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all required files are present.

### 10. Smoke Test the Published Output

Run the published output directly to confirm it behaves the same as the development build:

```bash
dotnet ./publish/Northwind.Web.dll
```

Verify the application starts and responds to requests as expected.