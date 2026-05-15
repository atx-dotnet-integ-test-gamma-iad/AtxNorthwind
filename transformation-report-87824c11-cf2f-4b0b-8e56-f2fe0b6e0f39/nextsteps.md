# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation and Testing

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are correctly restored:

```bash
dotnet restore
```

Review the output for any warnings related to missing packages or version conflicts.

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Release
```

Check the output for any warnings that, while non-blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify runtime behavior:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests and address them before proceeding further.

### 4. Run the Application Locally
Start the `Northwind.Web` project and verify it runs as expected on the target runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the application and confirm that core functionality behaves as it did prior to the transformation.

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all dependent projects in the solution target a compatible framework version.

### 6. Review Removed or Changed APIs
Cross-platform .NET removes or changes certain APIs that were available in .NET Framework. Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.PlatformAbstractions` tooling to identify any runtime-level compatibility issues that do not surface as build errors.

### 7. Check Configuration and Middleware
If the project uses `web.config`, confirm that the relevant settings have been migrated to `appsettings.json` and that the middleware pipeline in `Program.cs` or `Startup.cs` reflects the intended behavior.

### 8. Validate Data Access
If the project uses Entity Framework or direct database access, run any available database migrations and confirm that queries execute correctly against the target database:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 9. Verify Static Assets and Routing
Load the application in a browser and confirm that static files, routing, and any API endpoints respond correctly. Pay particular attention to any paths that may have been case-sensitive on Windows but are case-sensitive on Linux.

### 10. Review Logging and Error Handling
Confirm that the logging configuration (e.g., `ILogger`, Serilog, NLog) is functioning and that unhandled exceptions are surfaced appropriately in the new hosting model.