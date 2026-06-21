# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. The following steps outline how to validate, test, and deploy the migrated project.

## 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages that may need to be updated.

## 2. Build the Solution

Perform a full build to confirm the absence of errors and review any warnings:

```bash
dotnet build --configuration Release
```

Address any warnings that could indicate runtime issues, such as nullable reference warnings or obsolete API usage.

## 3. Review Configuration Files

- Verify that `appsettings.json` (and `appsettings.Development.json`) contain all configuration values that were previously in `Web.config` or `App.config`.
- Confirm that connection strings, application settings, and environment-specific values have been correctly migrated.
- Ensure that any secrets (e.g., connection strings, API keys) are stored using the .NET Secret Manager or environment variables rather than being hardcoded.

## 4. Run Unit and Integration Tests

If a test project exists in the solution, execute the tests to verify that existing functionality is preserved:

```bash
dotnet test
```

Review test results and investigate any failures, as they may indicate behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

## 5. Verify Database Connectivity

Since this is a Northwind-based project, confirm that:

- The database connection string in `appsettings.json` is correct and points to the intended database instance.
- Entity Framework migrations (if applicable) are up to date by running:

```bash
dotnet ef database update
```

- The application can successfully query the database at runtime.

## 6. Run the Application Locally

Start the application locally and verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

- Navigate through the application and confirm that pages load and data is returned as expected.
- Check the console output and application logs for any runtime exceptions or warnings.
- Test any API endpoints using a tool such as a browser, `curl`, or an API client.

## 7. Check for Removed or Changed APIs

Cross-platform .NET does not include certain APIs that were available in .NET Framework. Verify the following:

- Any use of `System.Web` has been replaced with the appropriate ASP.NET Core equivalents.
- `HttpContext`, session handling, and authentication middleware are configured correctly in `Program.cs` or `Startup.cs`.
- Any Windows-specific APIs (e.g., registry access, Windows identity impersonation) have been replaced or conditionally compiled if cross-platform support is required.

## 8. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the target framework is set to the intended version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Update to a newer Long-Term Support (LTS) version of .NET if the current target is outdated.

## 9. Static Files and wwwroot

Confirm that all static assets (CSS, JavaScript, images) are located in the `wwwroot` folder and that static file serving is enabled in the middleware pipeline:

```csharp
app.UseStaticFiles();
```

## 10. Publish the Application

Once validation is complete, publish the application to a target directory:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the output directory to confirm all required files are present before deploying to the target environment.