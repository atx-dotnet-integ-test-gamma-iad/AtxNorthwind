# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. The following steps outline how to validate, test, and deploy the migrated project.

## 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

## 2. Build the Solution

Perform a full solution build to confirm the absence of errors in a clean build environment:

```bash
dotnet build --configuration Release
```

Address any warnings that surface, particularly those related to nullable reference types, obsolete APIs, or platform compatibility.

## 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`). Ensure all dependent projects in the solution target a compatible framework version.

## 4. Run Unit and Integration Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --logger trx
```

Review the `.trx` output files for any failing tests. Pay particular attention to tests covering data access, middleware, and any platform-specific code paths that were part of the migration.

## 5. Verify Runtime Behavior Locally

Run the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following areas specifically:
- Application startup and middleware pipeline configuration
- Database connectivity and Entity Framework Core migrations (if applicable)
- Authentication and authorization flows
- Any static file serving or routing behavior

## 6. Check for Replaced or Removed APIs

Review the code for any uses of APIs that were available in .NET Framework but have changed or been removed in modern .NET. Common areas to inspect include:

- `System.Web` namespace usages (should have been replaced with ASP.NET Core equivalents)
- `HttpContext` and `HttpRequest` usage patterns
- Configuration APIs (e.g., `ConfigurationManager` replaced by `IConfiguration`)
- Any Windows-specific APIs if cross-platform support is required

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) to surface any remaining compatibility concerns.

## 7. Review Application Configuration

Confirm that configuration previously held in `Web.config` or `App.config` has been correctly migrated to `appsettings.json` and that environment-specific configuration (e.g., `appsettings.Development.json`) is in place.

## 8. Validate Database Migrations

If the project uses Entity Framework Core, verify that all migrations are up to date and can be applied cleanly:

```bash
dotnet ef migrations list --project src/Northwind.Web/Northwind.Web.csproj
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

## 9. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all required assets, configuration files, and binaries are present before deploying to the target environment.