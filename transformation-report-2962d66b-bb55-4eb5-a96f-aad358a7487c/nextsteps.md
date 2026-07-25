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

Address any warnings that surface during the build, particularly those related to nullable reference types or obsolete APIs, as these can indicate areas where the migration may have introduced subtle issues.

## 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`). Verify that all dependent projects in the solution target compatible frameworks.

## 4. Run the Application Locally

Start the application using:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate through the application's key pages and features to confirm that runtime behavior matches the legacy version. Pay particular attention to:

- Database connectivity and query results
- Authentication and authorization flows
- Any areas that previously relied on `System.Web` or other Windows-specific libraries

## 5. Check for Runtime Configuration

Review `appsettings.json` and `appsettings.Development.json` to ensure connection strings and other configuration values have been correctly carried over from the legacy `web.config` or `app.config` files. Confirm that environment-specific settings are functioning as expected.

## 6. Execute Unit and Integration Tests

If the solution contains test projects, run them with:

```bash
dotnet test
```

Review test results carefully. Failures may indicate behavioral differences introduced by the migration to cross-platform .NET, such as changes in globalization defaults, path handling, or cryptography APIs.

## 7. Validate Database Interactions

If the project uses Entity Framework, confirm that:

- Migrations are up to date by running `dotnet ef migrations list`
- The database schema matches expectations by running `dotnet ef database update` against a development database
- Queries return correct results and there are no EF Core compatibility issues carried over from EF6

## 8. Review Middleware and HTTP Pipeline

In ASP.NET Core, the HTTP pipeline is configured in `Program.cs` or `Startup.cs`. Confirm that all middleware components (e.g., authentication, static files, routing, error handling) are registered in the correct order and that no legacy `HttpModule` or `HttpHandler` equivalents were missed during transformation.

## 9. Test on a Non-Windows Environment (Optional but Recommended)

Since one goal of the migration is cross-platform support, consider running the application on Linux or macOS to surface any remaining platform-specific dependencies:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Watch for exceptions related to file path casing, Windows registry access, or Windows-only APIs.

## 10. Publish the Application

Once validation is complete, publish the application using:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the output directory to confirm all required assets, static files, and configuration files are present before deploying to the target environment.