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

Check the output for any warnings that, while non-blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify runtime behavior matches expectations:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may indicate behavioral differences introduced during the migration that do not surface as build errors.

### 4. Run the Application Locally

Start the web application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Confirm the application starts without runtime exceptions.
- Navigate through the key areas of the application to verify pages load and data is returned correctly.
- Check application logs for any runtime warnings or errors.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version of .NET, such as `net8.0`. Microsoft's support lifecycle can be found at [https://dotnet.microsoft.com/en-us/platform/support/policy](https://dotnet.microsoft.com/en-us/platform/support/policy).

### 6. Review Removed or Changed APIs

Cross-platform .NET removes or changes certain APIs that were available in .NET Framework. Review the following areas manually:

- **Configuration**: Ensure `System.Configuration.ConfigurationManager` usages have been replaced with `Microsoft.Extensions.Configuration`.
- **HTTP Context**: Verify any use of `System.Web.HttpContext` has been replaced with `Microsoft.AspNetCore.Http.IHttpContextAccessor` or equivalent.
- **Database access**: If Entity Framework is used, confirm the project is using EF Core rather than EF 6 (unless EF 6 compatibility was intentionally retained).
- **Security and Identity**: Verify any membership or role provider implementations have been migrated to ASP.NET Core Identity.

### 7. Check Static Files and Middleware Pipeline

In ASP.NET Core, middleware must be explicitly configured. Review `Program.cs` or `Startup.cs` to confirm:

- Static file middleware (`UseStaticFiles`) is present if the application serves static assets.
- Authentication and authorization middleware are ordered correctly.
- Routing middleware (`UseRouting`, `UseEndpoints`) is configured properly.

### 8. Validate Configuration Files

- Confirm `appsettings.json` contains all necessary configuration values previously held in `Web.config` or `App.config`.
- Verify connection strings are present and correctly formatted under the `ConnectionStrings` section in `appsettings.json`.

### 9. Test Against a Staging Environment

Before deploying to production, deploy the application to a staging environment that mirrors production as closely as possible. Perform functional testing against real data sources and infrastructure to identify any environment-specific issues.

### 10. Publish the Application

Once validation is complete, publish the application using:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected files, static assets, and configuration files are present before deploying to the target server.