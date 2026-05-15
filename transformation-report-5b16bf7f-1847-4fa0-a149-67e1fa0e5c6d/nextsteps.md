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

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output and confirm that the build succeeds with zero errors and review any warnings that may indicate deprecated APIs or compatibility concerns.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality is intact:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests and address them individually, as they may indicate behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Check Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a consistent and compatible framework version.

### 5. Review Replaced or Removed APIs

Cross-platform .NET does not support certain Windows-specific APIs that were available in .NET Framework. Review the code for usage of the following common problem areas:

- `System.Web` namespaces (these are not available in modern .NET)
- `HttpContext` usage outside of ASP.NET Core's dependency injection model
- `ConfigurationManager` (replaced by `Microsoft.Extensions.Configuration`)
- Windows Registry access
- `BinaryFormatter` (deprecated and disabled by default)

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.PlatformAbstractions` tooling to identify any remaining incompatible API calls.

### 6. Verify Runtime Behavior

Run the application locally and exercise the primary workflows:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following:

- Application starts without exceptions
- Database connections are established correctly
- All primary routes and endpoints respond as expected
- Static files and views render correctly if this is an MVC or Razor Pages application

### 7. Review Configuration Files

Ensure that `appsettings.json` (and `appsettings.Production.json` if applicable) contains all configuration values that were previously in `Web.config` or `App.config`. Key areas to check include:

- Connection strings
- Application settings / feature flags
- Logging configuration
- Authentication settings

### 8. Validate Middleware Pipeline

If the project uses ASP.NET Core, review `Program.cs` or `Startup.cs` to confirm the middleware pipeline is correctly configured, including:

- Authentication and authorization middleware
- Static file serving
- Routing
- Error handling

### 9. Publish the Application

Once local validation is complete, publish the application to verify the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files, assemblies, and static assets are present.

### 10. Smoke Test the Published Output

Run the published output directly to confirm it behaves identically to the development run:

```bash
dotnet ./publish/Northwind.Web.dll
```

Verify application startup and basic functionality before deploying to any target environment.