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

Verify that no warnings or errors appear related to missing or incompatible packages.

### 2. Build the Solution

Perform a full solution build to confirm the absence of errors in a clean build context:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs, nullable reference issues, or platform compatibility concerns that could cause runtime problems even if the build succeeds.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality behaves as expected after the migration:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether they are caused by behavioral differences in the new target framework.

### 4. Run the Application Locally

Start the web application locally to verify it runs correctly:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following:
- The application starts without runtime exceptions.
- All routes and endpoints respond as expected.
- Database connections (if applicable) are functioning correctly.
- Static files, middleware, and authentication behave as intended.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target compatible frameworks.

### 6. Check for Removed or Changed APIs

Review the code for any usage of APIs that were available in .NET Framework but have changed behavior in cross-platform .NET. Key areas to inspect include:

- `System.Web` references (these are not available in modern .NET and should have been replaced).
- `HttpContext`, `HttpRequest`, and `HttpResponse` usages, which have different APIs in ASP.NET Core.
- Configuration patterns (e.g., `Web.config` should be replaced by `appsettings.json` and the `IConfiguration` system).
- Any Windows-specific APIs such as the registry, WCF, or MSMQ.

### 7. Review Application Configuration

Confirm that `Web.config` has been replaced or supplemented by `appsettings.json`. Verify that connection strings, application settings, and environment-specific configuration are correctly defined and loaded at runtime.

### 8. Verify Database Connectivity

If the application uses a database, confirm the connection string in `appsettings.json` is correct for the target environment and that the data access layer (e.g., Entity Framework Core) is functioning as expected by performing basic CRUD operations through the application.

### 9. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to ensure all required files, assemblies, and static assets are present.

### 10. Deploy to Target Environment

Copy the published output to the target server or hosting environment. Ensure the target machine has the appropriate .NET runtime installed:

```bash
dotnet --list-runtimes
```

If the runtime is not present, download and install it from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download). Start the application on the target environment and perform the same validation checks outlined in step 4.