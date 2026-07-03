# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Check the output for any warnings that, while not blocking, may indicate compatibility concerns (e.g., nullable reference warnings, obsolete API usage).

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework APIs and their cross-platform .NET equivalents.

### 4. Review Runtime Configuration

- Confirm that `appsettings.json` (and environment-specific variants such as `appsettings.Development.json`) are present and correctly configured.
- Verify that connection strings, previously stored in `web.config` or `app.config`, have been migrated to `appsettings.json` or environment variables.
- Check that any `<system.web>` or `<httpModules>` configuration from `web.config` has been replaced with the appropriate ASP.NET Core middleware in `Program.cs` or `Startup.cs`.

### 5. Verify Static Files and wwwroot

- Confirm that static assets (CSS, JavaScript, images) have been moved to the `wwwroot` folder.
- Ensure `app.UseStaticFiles()` is present in the middleware pipeline.

### 6. Check Entity Framework or Data Access Layer

If the project uses Entity Framework:

- Confirm the project is using EF Core rather than EF 6.
- Run any pending migrations:

```bash
dotnet ef database update
```

- Verify that the database schema matches expectations after migration.

### 7. Run the Application Locally

Start the application using the .NET CLI:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

- Navigate through the application's key pages and endpoints.
- Check browser developer tools and application logs for runtime exceptions or missing resources.
- Verify that authentication, authorization, and session behavior work as expected if applicable.

### 8. Review Replaced or Removed APIs

Cross-platform .NET does not support certain .NET Framework APIs. Review the following areas manually:

- Any usage of `System.Web` namespaces should be fully replaced with ASP.NET Core equivalents.
- `HttpContext.Current` should be replaced with injected `IHttpContextAccessor`.
- `Server.MapPath` should be replaced with `IWebHostEnvironment.WebRootPath` or `ContentRootPath`.
- Windows-specific APIs (e.g., registry access, WCF server-side) may require alternative implementations.

### 9. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the target framework is set to a currently supported version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If the project is targeting `net6.0` or `net7.0`, consider upgrading to `net8.0` as those versions are out of support or approaching end of life.