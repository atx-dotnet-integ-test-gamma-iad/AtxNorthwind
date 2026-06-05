# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The solution has no build errors following the transformation. All projects compiled successfully, including `Northwind.Web`.

## Validation Steps

### 1. Verify Target Framework

Open each `.csproj` file and confirm the `TargetFramework` element targets the intended cross-platform .NET version (e.g., `net8.0`), rather than a legacy `net48` or `netcoreapp` moniker.

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 2. Restore and Build Locally

Run the following commands from the solution root to confirm a clean restore and build:

```bash
dotnet restore
dotnet build --configuration Release
```

Ensure there are no warnings that could indicate deprecated APIs or packages that may cause runtime issues.

### 3. Run the Test Suite

If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests and address them before proceeding.

### 4. Check for Replaced or Removed APIs

Even with a clean build, some APIs that existed in .NET Framework may have changed behavior in cross-platform .NET. Review the following areas manually:

- **Configuration**: Ensure `System.Configuration.ConfigurationManager` usage has been replaced with `Microsoft.Extensions.Configuration` where applicable.
- **HTTP**: Confirm `System.Web.HttpContext` and related types are fully replaced with ASP.NET Core equivalents.
- **Database access**: If Entity Framework 6 was used, verify whether a migration to EF Core is needed or if the `EntityFramework` NuGet package has been replaced with `Microsoft.EntityFrameworkCore`.
- **Windows-only APIs**: Check for any remaining usage of Windows-specific APIs (e.g., registry access, WCF server-side hosting) that will not function on Linux or macOS.

### 5. Run the Web Application Locally

Start the web application and exercise its primary functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate to the application in a browser and verify that pages load correctly, database queries return expected data, and no unhandled exceptions appear in the console output.

### 6. Review `appsettings.json` and Connection Strings

Confirm that connection strings and other configuration values previously stored in `Web.config` or `App.config` have been correctly migrated to `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=Northwind;..."
  }
}
```

### 7. Check Static Files and wwwroot

Verify that static assets (CSS, JavaScript, images) are located under the `wwwroot` folder and are being served correctly by the ASP.NET Core static files middleware.

### 8. Review Middleware Pipeline

Open `Program.cs` (or `Startup.cs` if still present) and confirm the middleware pipeline is configured correctly, including authentication, authorization, routing, and any custom middleware that was part of the original application.

### 9. Validate Logging

Confirm that logging is functioning as expected. ASP.NET Core uses `Microsoft.Extensions.Logging` by default. If the original project used a third-party logger (e.g., NLog, log4net), verify its configuration has been updated for .NET compatibility.

### 10. Publish a Release Build

Once local validation is complete, produce a published output to confirm the application can be packaged correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files are present, including the executable, dependencies, and static assets.