# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including the most critical project `Northwind.Web`.

## Validation

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

### 2. Build the Solution
Confirm the solution builds cleanly in Release configuration:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify existing functionality is intact:

```bash
dotnet test --configuration Release
```

Review test results and address any failures before proceeding.

### 4. Run the Application Locally
Start the application and verify it runs as expected on the target runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the application and confirm that core functionality behaves the same as it did in the legacy version.

### 5. Check Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is still referencing `netcoreapp3.1` or `net5.0`, update it to a current supported version and rebuild.

### 6. Review Removed or Changed APIs
Check the application for any use of APIs that were available in .NET Framework but have changed behavior in modern .NET. Pay particular attention to:

- `System.Web` usages (not available in modern .NET)
- `HttpContext` and related middleware patterns
- Configuration and dependency injection patterns
- Entity Framework version compatibility if applicable

### 7. Verify Database Connectivity
If the application connects to a database, confirm that connection strings in `appsettings.json` are correctly configured for the target environment and that the database is reachable.

### 8. Review Startup and Middleware Configuration
Confirm that `Program.cs` and any middleware configuration follows the modern .NET hosting model. If the project still uses a `Startup.cs` class with the older `ConfigureServices`/`Configure` pattern, consider migrating to the minimal hosting model introduced in .NET 6:

```csharp
var builder = WebApplication.CreateBuilder(args);
// Register services
var app = builder.Build();
// Configure middleware
app.Run();
```

### 9. Static Files and wwwroot
Verify that static assets under `wwwroot` are being served correctly and that any bundling or minification tooling is compatible with the current setup.

### 10. Publish the Application
Once validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all required files are present before deploying to the target environment.