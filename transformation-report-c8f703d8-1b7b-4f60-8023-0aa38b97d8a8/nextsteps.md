# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the root of the solution to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of any compilation errors:

```bash
dotnet build --configuration Release
```

Review the output and confirm that all projects report a successful build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the migration:

```bash
dotnet test --configuration Release
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the web application locally to verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Confirm the application starts without runtime errors and that core functionality behaves as expected.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If the target framework needs to be updated to a newer LTS release, update it here and re-run the build and tests.

### 6. Review Removed or Changed APIs

Cross-platform .NET removes or changes certain APIs that were available in .NET Framework. Manually review the following areas:

- **Configuration**: Ensure `System.Configuration.ConfigurationManager` usages have been replaced with `Microsoft.Extensions.Configuration` where applicable.
- **HTTP**: Confirm that any `System.Web` dependencies have been fully replaced with ASP.NET Core equivalents.
- **Database access**: If Entity Framework is used, confirm the project is using Entity Framework Core and that migrations are up to date.
- **Windows-specific APIs**: Check for any remaining usage of Windows-only APIs such as the registry, WCF, or `System.Drawing` that may cause runtime failures on non-Windows platforms.

### 7. Check Runtime Behavior

Exercise the main workflows of the application manually or through integration tests, paying particular attention to:

- Database connectivity and query results
- Authentication and authorization flows
- Any file system interactions, ensuring paths are constructed using `Path.Combine` rather than hardcoded separators

### 8. Review Warnings

Even with a clean build, review any compiler warnings produced during the build step. Warnings related to nullable reference types, deprecated APIs, or obsolete members should be addressed to improve long-term maintainability.

### 9. Publish the Application

Once validation is complete, publish the application using the following command:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all expected assets are present before deploying to the target environment.