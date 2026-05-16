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

Address any warnings that surface during the build, particularly those related to nullable reference types or obsolete APIs, as these may indicate areas that need attention.

## 3. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`). Verify that all dependent projects in the solution target a compatible framework version.

## 4. Run the Application Locally

Start the application using:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate through the application's primary routes and verify that pages render correctly and that data access operations function as expected.

## 5. Check Runtime Configuration

Review the following files for correctness after the migration:

- `appsettings.json` and `appsettings.Development.json` — confirm connection strings, logging configuration, and any environment-specific settings are accurate.
- `Program.cs` — if the project previously used `Startup.cs`, confirm that the middleware pipeline and service registrations have been correctly consolidated.

## 6. Validate Data Access

If the project uses Entity Framework, run the following to verify the model is consistent with the database schema:

```bash
dotnet ef migrations list
```

If there are pending migrations or model mismatches, resolve them before proceeding. If the project uses a different data access strategy (e.g., Dapper, ADO.NET), manually test the relevant queries against a development database.

## 7. Execute Existing Tests

If the solution contains test projects, run them with:

```bash
dotnet test
```

Review any failing tests. Failures after migration often indicate behavioral differences in the new framework version, changed default configurations, or missing compatibility shims.

## 8. Review Removed or Changed APIs

Cross-reference the project's code against the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/) or the official [breaking changes documentation](https://learn.microsoft.com/en-us/dotnet/core/compatibility/) for the target .NET version. Pay particular attention to:

- `System.Web` usages that may have been replaced
- Changes in HTTP middleware behavior
- Authentication and authorization API differences

## 9. Publish the Application

Once validation is complete, publish the application using:

```bash
dotnet publish --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory and confirm the application runs correctly from the published output before deploying to the target environment.