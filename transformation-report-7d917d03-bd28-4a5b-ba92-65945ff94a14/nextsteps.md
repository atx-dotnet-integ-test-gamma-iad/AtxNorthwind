# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of any build errors:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the `Northwind.Web` project locally to confirm it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Verify that the application starts without runtime exceptions.
- Navigate through the key areas of the application to confirm basic functionality.
- Check that database connections, if any, are functioning correctly with the updated configuration.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution are targeting a consistent framework version.

### 6. Review Removed or Changed APIs

Check the code for any usage of APIs that were available in .NET Framework but have changed behavior in modern .NET, including:

- `System.Web` references, which are not available in modern .NET and should have been replaced with ASP.NET Core equivalents.
- `HttpContext`, `HttpRequest`, and `HttpResponse` usages to confirm they reference the ASP.NET Core versions.
- Any configuration previously handled via `Web.config` should now be handled through `appsettings.json` and the `IConfiguration` system.

### 7. Review Static Files and Middleware Pipeline

In `Northwind.Web`, confirm that the middleware pipeline in `Program.cs` or `Startup.cs` is correctly configured, including:

- Static file serving via `app.UseStaticFiles()`
- Routing via `app.UseRouting()`
- Authentication and authorization middleware, if applicable

### 8. Verify Runtime Behavior Against the Original

Compare the behavior of the migrated application against the original legacy application on key workflows to identify any functional discrepancies that would not surface as build errors.