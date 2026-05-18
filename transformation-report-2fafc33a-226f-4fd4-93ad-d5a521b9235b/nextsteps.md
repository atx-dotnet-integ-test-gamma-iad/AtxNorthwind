# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. The following steps outline how to validate, test, and deploy the migrated project.

## 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process, particularly around package compatibility or missing packages.

## 2. Build the Solution

Perform a full build to confirm the absence of errors in a clean build environment:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs, nullable reference issues, or platform compatibility concerns that were not caught previously.

## 3. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a supported and intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure any other projects in the solution are targeting compatible frameworks.

## 4. Run Unit and Integration Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

## 5. Check for Runtime Compatibility Issues

Some APIs that compiled successfully may behave differently or throw at runtime on cross-platform .NET. Pay particular attention to:

- **Windows-specific APIs**: Features such as `System.Drawing`, registry access, or WCF server-side components may require additional NuGet packages (e.g., `System.Drawing.Common`) or alternative implementations.
- **Configuration**: Ensure `appsettings.json` and any environment-specific configuration files are present and correctly structured, replacing any legacy `Web.config` or `App.config` reliance where applicable.
- **Connection Strings**: Verify that database connection strings are valid and that the target database is accessible from the new runtime environment.

## 6. Run the Application Locally

Start the application locally to perform smoke testing:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the key areas of the application to confirm that routing, data access, and rendering behave as expected.

## 7. Review Middleware and HTTP Pipeline

If this is an ASP.NET Core project, review `Program.cs` (and `Startup.cs` if still present) to confirm that middleware is registered in the correct order and that any legacy HTTP modules or handlers have been properly migrated to ASP.NET Core middleware equivalents.

## 8. Validate Static Files and Assets

Confirm that static files (CSS, JavaScript, images) are served correctly. In ASP.NET Core, static files must reside in the `wwwroot` folder and the `UseStaticFiles()` middleware must be registered.

## 9. Publish the Application

Once local validation is complete, publish the application to verify the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all required files, assemblies, and assets are present before deploying to the target environment.