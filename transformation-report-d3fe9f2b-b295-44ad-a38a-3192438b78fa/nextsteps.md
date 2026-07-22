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

Address any warnings that surface during the build, particularly those related to nullable reference types or obsolete APIs, as these may indicate areas where the modernization is incomplete.

## 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a supported version of .NET, such as `net8.0`. Ensure no references to `net4x` or `netstandard` remain unless intentionally required.

## 4. Run the Application Locally

Start the application using the .NET CLI:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate through the application in a browser and verify that core functionality behaves as expected compared to the legacy version.

## 5. Check for Replaced or Removed APIs

Review the codebase for any usage of APIs that were available in .NET Framework but have changed or been removed in cross-platform .NET. Common areas to inspect include:

- `System.Web` references, which are not available in cross-platform .NET and should have been replaced with ASP.NET Core equivalents.
- `HttpContext`, `HttpRequest`, and `HttpResponse` usages to confirm they reference `Microsoft.AspNetCore.Http` types.
- Configuration APIs to confirm migration from `Web.config` to `appsettings.json` and `IConfiguration`.
- Any use of `AppDomain`, `BinaryFormatter`, or Windows-specific APIs.

## 6. Validate Database Connectivity

If the project uses Entity Framework or direct database access, confirm the connection strings in `appsettings.json` are correct and that the application can connect to the database:

```bash
dotnet ef database update
```

Run any existing migrations and verify the schema is consistent with expectations.

## 7. Execute Existing Tests

If a test project exists within the solution, run the test suite to validate that behavior has not regressed:

```bash
dotnet test
```

Review any failing tests and determine whether they reflect actual regressions or tests that require updating due to API changes in the new framework.

## 8. Review Static Files and Middleware Pipeline

In ASP.NET Core, static files and middleware must be explicitly configured in `Program.cs` or `Startup.cs`. Confirm the following are present where applicable:

- `app.UseStaticFiles()`
- `app.UseRouting()`
- `app.UseAuthentication()` and `app.UseAuthorization()` if authentication is used
- `app.MapControllers()` or `app.MapRazorPages()` depending on the project type

## 9. Publish the Application

Once validation is complete, publish the application to a target directory:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the output directory to confirm all required files are present, then deploy the contents to the target hosting environment.