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
Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Release
```

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify that existing logic behaves as expected after the migration:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as failures may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Review Target Framework
Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a current and supported version, such as:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is targeting an older or end-of-life version (e.g., `net5.0`, `net6.0`), consider updating it to the latest Long-Term Support (LTS) release.

### 5. Run the Application Locally
Start the application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following manually:
- Application starts without runtime exceptions.
- All routes and endpoints respond correctly.
- Database connections (if applicable) are functional.
- Any authentication or authorization flows work as intended.

### 6. Check for Removed or Changed APIs
Even without build errors, some APIs behave differently on cross-platform .NET compared to .NET Framework. Pay particular attention to:

- **`System.Web` dependencies** — these are not available in cross-platform .NET and may have been replaced with middleware equivalents.
- **Windows-specific APIs** — any calls to Windows Registry, Windows Authentication, or COM interop should be tested on the target deployment OS.
- **Configuration system** — ensure `web.config` settings have been properly migrated to `appsettings.json` or environment variables.
- **Entity Framework** — if migrated from EF 6 to EF Core, verify that queries return the same results and that migrations are in a valid state.

### 7. Verify Static Files and Views
If the project uses Razor views or serves static files, manually navigate through the application to confirm that:
- Views render without errors.
- Static assets (CSS, JS, images) are served correctly.
- Layout and partial views are functioning as expected.

### 8. Review Middleware Pipeline
Open `Program.cs` (or `Startup.cs` if still present) and confirm that the middleware pipeline is correctly ordered. Common issues after migration include:
- Missing `app.UseStaticFiles()`.
- Missing `app.UseAuthentication()` before `app.UseAuthorization()`.
- Incorrect routing configuration.

### 9. Check Logging Output
Run the application and review the console or log output for any warnings that may indicate deprecated API usage or misconfigurations that do not surface as build errors.