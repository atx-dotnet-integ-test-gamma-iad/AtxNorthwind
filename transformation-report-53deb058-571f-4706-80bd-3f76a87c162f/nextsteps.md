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
If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may indicate behavioral differences introduced by the migration to cross-platform .NET.

### 4. Run the Application Locally
Start the web application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following at runtime:
- The application starts without exceptions.
- All routes and endpoints respond correctly.
- Database connections (if applicable) are functioning.
- Any static files, views, or Razor pages render as expected.

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If an older or unintended version is present, update it to the desired long-term support (LTS) release.

### 6. Audit Removed or Changed APIs
Review the code for usage of APIs that were available in .NET Framework but have changed behavior in cross-platform .NET. Key areas to check include:

- `System.Web` dependencies — these are not available in cross-platform .NET and should have been replaced with ASP.NET Core equivalents.
- Windows-specific APIs (e.g., registry access, Windows identity, MSMQ) that may compile but fail at runtime on non-Windows platforms.
- Configuration — ensure `web.config` based configuration has been replaced with `appsettings.json` and the `IConfiguration` system.

### 7. Verify Database Connectivity
If the project uses a database (as suggested by the Northwind naming convention), confirm the connection strings in `appsettings.json` are correct and that the chosen data access library (e.g., Entity Framework Core) is properly configured and can connect to the target database.

### 8. Check Middleware and Startup Configuration
Confirm that `Program.cs` (and `Startup.cs` if present) correctly registers all required services and middleware. Ensure the middleware pipeline order is correct, particularly for authentication, authorization, routing, and static files.

### 9. Test on Target Platforms
If cross-platform support is a goal, run and validate the application on each intended target operating system (e.g., Linux, macOS) to surface any platform-specific runtime issues that would not appear on Windows.