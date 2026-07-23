# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. The following steps outline how to validate, test, and deploy the migrated project.

## 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

## 2. Build the Solution

Perform a full solution build to confirm the absence of errors in a clean build environment:

```bash
dotnet build --configuration Release
```

Address any warnings that surface during this step, particularly those related to nullable reference types or obsolete APIs, as these can indicate areas where the modernization may be incomplete.

## 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a current and supported version of .NET, such as `net8.0`. If it is targeting an older version like `net6.0` or `net7.0`, consider updating it:

```xml
<TargetFramework>net8.0</TargetFramework>
```

After changing the target framework, re-run `dotnet restore` and `dotnet build`.

## 4. Run the Application Locally

Start the application using the .NET CLI to verify it runs without runtime errors:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate through the application's key pages and endpoints to confirm expected behavior.

## 5. Check for Replaced or Removed APIs

Review the codebase for any usage of APIs that were available in .NET Framework but have changed or been removed in cross-platform .NET. Common areas to inspect include:

- `System.Web` references (these are not available in .NET Core/.NET 5+)
- `HttpContext` usage patterns
- Windows-specific APIs such as the registry, WCF, or `System.Drawing` (on non-Windows platforms)
- Entity Framework 6 vs. Entity Framework Core differences if the project uses a data layer

## 6. Verify Database Connectivity

If the project connects to a database (as expected given the Northwind naming convention), confirm the connection strings in `appsettings.json` are correctly configured for the new environment and that the database provider NuGet package is compatible with the target framework.

```bash
dotnet ef database update
```

If Entity Framework Core migrations are in use, verify the migrations are up to date:

```bash
dotnet ef migrations list
```

## 7. Execute Unit and Integration Tests

If the solution contains test projects, run them to validate functional correctness:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether failures are due to the migration or pre-existing issues.

## 8. Review `Program.cs` and Startup Configuration

In .NET 6 and later, the `Startup.cs` pattern was replaced with a minimal hosting model in `Program.cs`. Confirm that middleware, services, and configuration are registered correctly. Key areas to check:

- Authentication and authorization middleware
- Static file serving
- Routing configuration
- Dependency injection registrations

## 9. Validate `appsettings.json` Configuration

Confirm that all configuration values previously held in `Web.config` have been correctly migrated to `appsettings.json` or `appsettings.{Environment}.json`. Pay particular attention to:

- Connection strings
- Application-specific settings
- Logging configuration

## 10. Publish the Application

Once validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all required files are present before deploying to the target environment.