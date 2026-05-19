# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Check the output for any warnings that, while non-breaking, may indicate deprecated APIs or patterns that could cause issues at runtime.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality behaves as expected after the transformation:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Check Runtime Compatibility

Some issues do not surface at build time but appear at runtime. Pay attention to the following areas:

- **Configuration**: Ensure `appsettings.json` is correctly replacing any legacy `Web.config` or `App.config` values. Confirm connection strings, app settings, and environment-specific overrides are in place.
- **Authentication and Authorization**: If the project uses Windows Authentication, Forms Authentication, or legacy membership providers, verify these have been correctly mapped to their ASP.NET Core equivalents.
- **HTTP Modules and Handlers**: If any legacy HTTP modules or handlers were present, confirm they have been converted to ASP.NET Core middleware.
- **Static Files**: Verify that static file serving is configured correctly via `UseStaticFiles()` in the middleware pipeline.
- **Entity Framework**: If the project uses Entity Framework, confirm whether it was migrated to EF Core and run any pending migrations:

```bash
dotnet ef database update
```

### 5. Run the Application Locally

Start the application locally and manually exercise the primary workflows:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

- Navigate through the application's key pages and endpoints.
- Check browser developer tools and application logs for runtime exceptions or missing resources.
- Verify database connectivity and that data is being read and written correctly.

### 6. Review Logging Output

Ensure the logging infrastructure is functioning. Check that log output appears as expected in the console or configured log sinks. Look for any unhandled exceptions or warnings during startup and normal operation.

### 7. Validate Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all dependent projects in the solution target a compatible framework version.

### 8. Review Removed or Changed APIs

Cross-reference any usages of APIs that are known to be removed or changed in modern .NET. The [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.UpgradeAssistant` tool can assist in identifying remaining compatibility concerns even after a successful build.