# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the root of the solution to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of any compile-time errors:

```bash
dotnet build --configuration Release
```

Review the output and confirm that all projects report a successful build.

### 3. Run Unit Tests

If the solution contains any test projects, execute them to verify that existing functionality has not been broken during the transformation:

```bash
dotnet test --configuration Release
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the `Northwind.Web` project locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Confirm that the application starts without exceptions and that core functionality behaves as expected.

### 5. Verify Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm that the `<TargetFramework>` element references the intended cross-platform .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a consistent and compatible framework version.

### 6. Check for Removed or Changed APIs

Review any usage of APIs that were available in .NET Framework but have changed or been removed in modern .NET. Pay particular attention to:

- `System.Web` namespace usages, which are not available in cross-platform .NET
- `HttpContext`, `HttpRequest`, and `HttpResponse` usages that may need to be updated to ASP.NET Core equivalents
- Any Windows-specific APIs such as the registry, WMI, or Windows identity APIs that may not function on non-Windows platforms

### 7. Review Configuration Files

Confirm that `web.config` has been replaced or supplemented by `appsettings.json` and that the application reads configuration correctly at runtime using the `IConfiguration` abstraction.

### 8. Verify Database Connectivity

If the application connects to a database, confirm that the connection strings in `appsettings.json` are correct and that the application can successfully connect and perform queries in the local environment.

### 9. Review Static Files and Middleware

Ensure that static file serving, routing, and middleware configuration in `Program.cs` or `Startup.cs` are correctly set up for ASP.NET Core and that all expected routes are accessible.

### 10. Deploy to Target Environment

Once local validation is complete, publish the application using the following command:

```bash
dotnet publish --configuration Release --output ./publish
```

Copy the contents of the `./publish` directory to the target hosting environment and verify that the application starts and operates correctly there.