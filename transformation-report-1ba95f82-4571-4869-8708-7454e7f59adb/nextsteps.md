# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

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

If the solution contains any test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the `Northwind.Web` project locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Confirm that the application starts without exceptions and that core functionality behaves as expected.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm that the `<TargetFramework>` element is set to the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is set to an older or unexpected version, update it accordingly and rebuild.

### 6. Verify Runtime Configuration

Check the following files for correctness after the transformation:

- `appsettings.json` and `appsettings.Production.json` — confirm connection strings, logging settings, and any environment-specific values are accurate.
- `Program.cs` — confirm the application startup and middleware pipeline are configured correctly for the target .NET version.

### 7. Check for Removed or Changed APIs

Even without build errors, some APIs behave differently between .NET Framework and modern .NET. Manually review areas of the code that rely on any of the following, as these are common sources of runtime issues after migration:

- `System.Web` references or HTTP context usage
- Windows-specific APIs (registry, WCF, remoting)
- Third-party NuGet packages that may have breaking changes between versions

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.UpgradeAssistant` tool to identify any remaining compatibility concerns.

### 8. Test Against a Staging Environment

Before deploying to production, run the application against a staging environment that mirrors production data and configuration. Validate:

- Database connectivity and query results
- Authentication and authorization flows
- Any external service integrations

### 9. Publish the Application

Once validation is complete, publish the application using:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all required assets are present before deploying to the target environment.