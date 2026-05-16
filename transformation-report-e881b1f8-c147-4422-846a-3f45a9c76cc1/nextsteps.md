# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The solution has no build errors following the transformation. All projects compiled successfully, including `Northwind.Web`.

## Validation Steps

### 1. Verify Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the target framework is set to a supported cross-platform .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 2. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages restore cleanly:

```bash
dotnet restore
```

Resolve any warnings about deprecated or unlisted packages by updating them in the relevant `.csproj` files.

### 3. Build the Solution

Perform a full build to confirm there are no errors or warnings that may have been suppressed:

```bash
dotnet build --configuration Release
```

Review any warnings in the output, as some may indicate runtime issues even if the build succeeds.

### 4. Run Unit Tests

If the solution contains test projects, execute them to verify existing functionality is intact:

```bash
dotnet test --configuration Release
```

Review test output for any failures or skipped tests that may indicate broken functionality after the migration.

### 5. Run the Application Locally

Start the web application and verify it runs on the expected port:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate to the displayed URL in a browser and confirm the application loads and behaves as expected.

### 6. Check for Removed or Changed APIs

Review the code for usage of any APIs that were available in .NET Framework but have changed or been removed in cross-platform .NET. Common areas to check include:

- `System.Web` references (not available in .NET Core/5+)
- `HttpContext` and related types (replaced by `Microsoft.AspNetCore.Http`)
- `ConfigurationManager` (replaced by `Microsoft.Extensions.Configuration`)
- Windows-specific APIs such as the registry or WCF server-side components

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) if a thorough API compatibility check is needed.

### 7. Verify Configuration Files

Ensure that `appsettings.json` (and environment-specific variants such as `appsettings.Development.json`) contain all configuration values that were previously held in `Web.config` or `App.config`. Connection strings, application settings, and logging configuration should all be accounted for.

### 8. Check Static Files and Middleware

Confirm that static files (CSS, JavaScript, images) are located under the `wwwroot` folder and that the middleware pipeline in `Program.cs` or `Startup.cs` includes:

```csharp
app.UseStaticFiles();
```

### 9. Validate Database Connectivity

If the application uses a database, confirm the connection string in `appsettings.json` is correct and that the application can connect successfully at runtime. Run any Entity Framework Core migrations if applicable:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 10. Review Publish Output

Perform a publish to verify the output is complete and self-contained:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files are present before deploying to the target environment.