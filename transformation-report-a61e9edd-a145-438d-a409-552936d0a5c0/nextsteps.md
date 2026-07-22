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

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while non-blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit Tests

If the solution contains test projects, execute them to confirm runtime behavior is intact:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Check Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets an appropriate modern version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If the project is a web application, ensure it targets `net8.0` or later and references `Microsoft.AspNetCore.App` where appropriate.

### 5. Verify Runtime Behavior

Run the application locally using the .NET CLI:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Manually exercise the key application workflows, particularly any that relied on Windows-specific APIs or legacy ASP.NET features such as `HttpContext`, `Session`, or `MembershipProvider`, as these may have behavioral differences even when no build errors are present.

### 6. Review Removed or Changed APIs

Cross-reference the application code against the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the [.NET API compatibility tool](https://learn.microsoft.com/en-us/dotnet/standard/analyzers/api-compat) to identify any runtime-only compatibility concerns that do not surface as build errors.

### 7. Review Configuration Files

Ensure that any configuration previously held in `Web.config` or `App.config` has been properly migrated to `appsettings.json` or `appsettings.{Environment}.json`. Legacy `<system.web>` configuration sections are not supported in cross-platform .NET.

### 8. Validate Database Connectivity

If the application uses Entity Framework or direct ADO.NET connections, confirm that connection strings in `appsettings.json` are correct and that the application can connect to the database successfully at runtime.

### 9. Publish the Application

Once runtime validation is complete, publish the application to confirm the output is as expected:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all required assets, static files, and configuration files are present before deploying to the target environment.