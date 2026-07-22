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

Perform a full build to confirm the absence of errors in a clean environment:

```bash
dotnet build --configuration Release
```

Address any warnings that surface during the build, particularly those related to nullable reference types or obsolete APIs, as these can indicate areas where the migration introduced subtle issues.

## 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`). Verify that all dependent projects in the solution target a compatible framework version.

## 4. Run the Application Locally

Start the application using:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate through the application's primary workflows to confirm that runtime behavior matches expectations from the legacy version.

## 5. Check for Removed or Changed APIs

Review the code for usage of APIs that were available in .NET Framework but have changed or been removed in modern .NET. Common areas to check include:

- `System.Web` references (these are not available in modern .NET and should have been replaced with ASP.NET Core equivalents)
- `HttpContext`, `HttpRequest`, and `HttpResponse` usage
- `ConfigurationManager` (replaced by `Microsoft.Extensions.Configuration`)
- `Global.asax` logic (should have been moved to `Program.cs` or `Startup.cs`)

## 6. Validate Configuration

Confirm that `appsettings.json` contains all configuration values that were previously in `Web.config` or `App.config`. Pay particular attention to:

- Connection strings
- Application-specific settings
- Authentication and authorization configuration

## 7. Database Connectivity

If the project uses Entity Framework, verify the version in use:

```bash
dotnet list package
```

If the project was using Entity Framework 6 (EF6), consider whether migration to Entity Framework Core is appropriate. If EF Core is already in use, run a test query against the database to confirm connectivity and that the schema is compatible.

## 8. Execute Tests

If the solution contains test projects, run them to validate functional correctness:

```bash
dotnet test
```

Review any failing tests and determine whether failures are due to migration-related changes or pre-existing issues.

## 9. Review Middleware and Startup Configuration

In ASP.NET Core, the request pipeline is configured in `Program.cs`. Confirm that all middleware components (authentication, routing, static files, error handling, etc.) are registered in the correct order.

## 10. Static Files and wwwroot

Verify that static assets (CSS, JavaScript, images) have been placed under the `wwwroot` folder, as this is the expected location for static files in ASP.NET Core.

## 11. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all required files are present before deploying to the target environment.