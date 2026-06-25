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
Perform a full solution build to confirm the absence of errors in a clean build environment:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs, missing references, or compatibility concerns that did not surface as hard errors.

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review test results carefully. Any failing tests should be investigated before proceeding further.

### 4. Run the Application Locally
Start the `Northwind.Web` project locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Verify that the application starts without runtime exceptions.
- Navigate through the application and exercise the primary workflows to confirm expected behavior.
- Check application logs for any runtime warnings or errors.

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended modern .NET version (e.g., `net8.0`). Ensure all other projects in the solution target compatible frameworks.

### 6. Review Removed or Changed APIs
Cross-platform .NET removes or changes certain APIs that existed in .NET Framework. Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.PlatformAbstractions` tooling to scan for any suppressed compatibility warnings:

```bash
dotnet build /p:EnableNETAnalyzers=true /p:AnalysisMode=All
```

Address any analyzer diagnostics that are relevant to your application's behavior.

### 7. Verify Configuration and Middleware
If the project uses `Web.config`, confirm that the relevant settings have been migrated to `appsettings.json` or the appropriate `Program.cs` / `Startup.cs` middleware configuration. `Web.config` is not used for application configuration in cross-platform .NET web applications.

### 8. Validate Data Access
If the project uses Entity Framework, confirm the version in use:

- **Entity Framework Core** is the supported version for cross-platform .NET.
- Run any pending migrations and verify database connectivity:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 9. Check Static Files and Bundling
If the project previously used `System.Web.Optimization` or similar bundling libraries, confirm that static file serving and bundling have been replaced with the appropriate ASP.NET Core middleware or a compatible alternative such as `WebOptimizer`.

### 10. Deploy to Target Environment
Once local validation is complete, publish the application targeting the intended runtime:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory and deploy to the target environment according to your standard deployment process.