# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation and Testing

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while non-blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify runtime behavior matches expectations:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may surface runtime issues that were not caught at compile time.

### 4. Run the Application Locally
Start the `Northwind.Web` project and verify it runs as expected on the target platform:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Verify that all routes and endpoints respond correctly.
- Check that database connections and any external service integrations function as expected.
- Review application logs for runtime exceptions or warnings.

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is still referencing `net48` or another legacy framework, update it accordingly and rebuild.

### 6. Check for Removed or Changed APIs
Even without build errors, some APIs behave differently in modern .NET compared to .NET Framework. Pay particular attention to:

- `System.Web` usages, which are not available in cross-platform .NET and may have been replaced with ASP.NET Core equivalents.
- Any HTTP pipeline middleware that was migrated from `HttpModules` or `HttpHandlers`.
- Configuration patterns that previously relied on `web.config` and may now need to use `appsettings.json` and the `IConfiguration` abstraction.

### 7. Verify Static Files and Configuration
Ensure that `appsettings.json`, `appsettings.Production.json`, and any static web assets are present and correctly referenced. Confirm that environment-specific configuration is loading as expected at runtime.

### 8. Test on Target Platform
If the goal of the migration was to support Linux or macOS, run the application on the target operating system to catch any platform-specific issues such as:

- File path case sensitivity.
- Windows-specific registry or COM dependencies that may have been overlooked.
- Platform-specific cryptography or security API differences.