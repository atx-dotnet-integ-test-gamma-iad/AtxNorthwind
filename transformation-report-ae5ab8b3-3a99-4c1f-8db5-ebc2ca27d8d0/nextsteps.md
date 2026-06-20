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

Perform a full solution build to confirm the absence of errors in a clean build context:

```bash
dotnet build --configuration Release
```

Address any warnings that surface during this step, particularly those related to nullable reference types or obsolete APIs, as these can indicate areas where the migration may have introduced subtle issues.

## 3. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended .NET version (e.g., `net8.0`). Ensure all dependent projects in the solution target a compatible framework version.

## 4. Run the Application Locally

Start the application using:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate to the application in a browser and verify that core functionality behaves as expected. Pay particular attention to:

- Database connectivity and Entity Framework migrations (if applicable)
- Authentication and authorization flows
- Any static file serving or middleware that was previously handled by IIS or System.Web

## 5. Check for Removed or Replaced APIs

Review the codebase for any usage of APIs that were available in .NET Framework but have changed or been removed in cross-platform .NET. Common areas to inspect include:

- `System.Web` references (these should have been replaced with ASP.NET Core equivalents)
- `ConfigurationManager` usage (should be replaced with `IConfiguration`)
- `HttpContext.Current` (should be replaced with injected `IHttpContextAccessor`)
- Windows-specific APIs such as the registry or certain cryptography providers

## 6. Execute Unit and Integration Tests

If the solution contains test projects, run them to validate that existing behavior is preserved:

```bash
dotnet test
```

Review any failing tests carefully. Failures may indicate behavioral differences between .NET Framework and cross-platform .NET rather than bugs in the original code.

## 7. Validate Configuration Files

Confirm that `appsettings.json` (and environment-specific variants such as `appsettings.Development.json`) contain all configuration values that were previously held in `Web.config` or `App.config`. Connection strings, application settings, and custom configuration sections should all be accounted for.

## 8. Inspect Middleware and Startup Configuration

Review `Program.cs` (and `Startup.cs` if present) to ensure that all middleware components are registered in the correct order. Verify that the request pipeline matches the intended behavior of the original application.

## 9. Test on Target Operating Systems

Since the goal of the migration is cross-platform support, run and test the application on each operating system you intend to support (Windows, Linux, macOS) to surface any platform-specific issues that may not appear in a single-environment test.

## 10. Review Publish Output

Perform a publish to confirm the output is complete and correct:

```bash
dotnet publish --configuration Release --output ./publish
```

Inspect the output directory to ensure all required files, static assets, and configuration files are present before deploying to the target environment.