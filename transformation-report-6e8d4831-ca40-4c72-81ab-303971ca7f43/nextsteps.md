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

Address any warnings that surface during this step, particularly those related to nullable reference types or obsolete APIs, as these may indicate areas where the migration introduced subtle issues.

## 3. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`). Verify that all dependent projects in the solution target compatible frameworks.

## 4. Run the Application Locally

Start the application using:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate through the application's primary features to confirm runtime behavior matches the legacy version. Pay particular attention to:

- Database connectivity and query results
- Authentication and authorization flows
- Any HTTP endpoints or API routes

## 5. Execute Existing Tests

If the solution contains test projects, run them with:

```bash
dotnet test
```

Review test results carefully. Failures may indicate behavioral differences introduced by the framework migration that were not caught at compile time.

## 6. Check for Removed or Changed APIs

Review the code for usage of APIs that were present in .NET Framework but have changed behavior in modern .NET. Common areas to inspect include:

- `System.Web` references (these do not exist in modern .NET and should have been replaced with ASP.NET Core equivalents)
- `ConfigurationManager` usage replaced by `IConfiguration`
- `HttpContext` and related types, which have different APIs in ASP.NET Core
- Any use of `BinaryFormatter`, which is disabled by default in modern .NET

## 7. Validate Configuration Files

Confirm that `appsettings.json` (and environment-specific variants such as `appsettings.Development.json`) contain all configuration values that were previously held in `Web.config` or `App.config`. Verify connection strings, application settings, and any custom configuration sections have been correctly migrated.

## 8. Review Static Files and Middleware

In ASP.NET Core, static files and middleware must be explicitly configured. Confirm that `Program.cs` or `Startup.cs` includes the appropriate middleware registrations, such as:

- `app.UseStaticFiles()`
- `app.UseRouting()`
- `app.UseAuthentication()` / `app.UseAuthorization()` if applicable

## 9. Inspect Logging Configuration

Ensure that logging is configured correctly in `appsettings.json` under the `Logging` section and that any legacy logging frameworks (e.g., log4net, NLog) have been updated to their .NET-compatible versions or replaced with the built-in `Microsoft.Extensions.Logging` infrastructure.

## 10. Publish the Application

Once validation is complete, publish the application using:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the output directory to confirm all required files are present, then deploy the contents of the `./publish` folder to the target hosting environment.