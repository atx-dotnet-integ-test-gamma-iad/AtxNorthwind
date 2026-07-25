# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. The following steps outline how to validate, test, and deploy the migrated project.

## 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages that may need to be updated.

## 2. Build the Solution

Perform a full build to confirm the absence of errors in a clean build environment:

```bash
dotnet build --configuration Release
```

Address any warnings that surface during the build, particularly those related to nullable reference types or obsolete APIs, as these may indicate areas that need attention.

## 3. Run Unit and Integration Tests

If the solution contains test projects, execute them to verify that existing functionality has been preserved after the migration:

```bash
dotnet test --configuration Release
```

Review the test output carefully. Any failing tests should be investigated to determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

## 4. Verify Runtime Behavior

Run the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Specifically check the following areas, as they are common sources of runtime issues after migration:

- **Database connectivity**: Confirm that connection strings are correctly configured and that Entity Framework or any other data access layer functions as expected.
- **Authentication and Authorization**: Verify that any middleware related to identity or security is operating correctly.
- **Static files and routing**: Confirm that all routes resolve correctly and that static assets are being served.
- **Configuration**: Ensure that `appsettings.json` and any environment-specific configuration files are being read correctly, replacing any legacy `Web.config` or `App.config` reliance.

## 5. Review Removed or Changed APIs

Cross-platform .NET does not support certain APIs that were available in .NET Framework. Review the following areas manually:

- Any use of `System.Web` namespaces, which are not available in cross-platform .NET.
- `HttpContext` usage patterns that may differ between ASP.NET and ASP.NET Core.
- Any Windows-specific APIs (e.g., registry access, Windows identity impersonation) that may not function on non-Windows platforms.

The [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the [Platform Compatibility Analyzer](https://learn.microsoft.com/en-us/dotnet/standard/analyzers/platform-compat-analyzer) can assist in identifying these issues.

## 6. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If a newer Long-Term Support (LTS) version of .NET is available and desired, update this value and re-run the restore and build steps.

## 7. Publish the Application

Once validation is complete, publish the application to prepare it for deployment:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected files, including configuration files and static assets, are present before deploying to the target environment.