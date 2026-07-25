# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. The following steps outline how to validate, test, and deploy the migrated project.

## 1. Restore and Build the Solution

Run the following commands from the solution root to confirm a clean restore and build:

```bash
dotnet restore
dotnet build --configuration Release
```

Verify that no warnings or errors are produced. Address any warnings that could indicate compatibility issues, such as deprecated API usage or nullable reference warnings.

## 2. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended .NET version (e.g., `net8.0`). Ensure all dependent projects and NuGet packages are compatible with this target framework.

```xml
<TargetFramework>net8.0</TargetFramework>
```

## 3. Check for Removed or Changed APIs

Even without build errors, runtime issues can arise from APIs that changed behavior between .NET Framework and modern .NET. Review the following areas:

- **Configuration**: Ensure `appsettings.json` is used in place of `web.config` or `app.config` where applicable.
- **HTTP Pipeline**: Confirm middleware registration in `Program.cs` or `Startup.cs` follows the modern ASP.NET Core pattern.
- **Entity Framework**: If Entity Framework is used, confirm it has been migrated to EF Core and that migrations are up to date.
- **Authentication/Authorization**: Verify that any authentication middleware is correctly configured for ASP.NET Core.

## 4. Run Unit Tests

If the solution contains test projects, execute them to catch any runtime regressions:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether they indicate a functional regression or a test that requires updating due to API changes.

## 5. Run the Application Locally

Start the application locally and exercise its primary functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Navigate through the application and verify that pages render correctly.
- Check that database connections are established and queries return expected results.
- Review the console output and application logs for any runtime exceptions or warnings.

## 6. Verify Static Files and Assets

Confirm that static files (CSS, JavaScript, images) are served correctly. In ASP.NET Core, static files must be placed in the `wwwroot` folder and the `UseStaticFiles()` middleware must be registered.

## 7. Review Connection Strings and Environment Configuration

Confirm that connection strings and environment-specific settings are correctly defined in `appsettings.json` and `appsettings.Production.json`. Sensitive values should be managed using the .NET Secret Manager for local development:

```bash
dotnet user-secrets init --project src/Northwind.Web/Northwind.Web.csproj
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your_connection_string"
```

## 8. Publish the Application

Once local validation is complete, publish the application to verify the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all required files are present before deploying to the target environment.