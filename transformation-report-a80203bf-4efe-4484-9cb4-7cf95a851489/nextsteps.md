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
If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

### 4. Run the Web Application Locally
Start the `Northwind.Web` project and verify it runs as expected on the new runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following once the application is running:

- The application starts without runtime exceptions.
- All routes and pages load correctly.
- Database connectivity (if applicable) is functioning as expected.
- Any authentication or authorization flows behave correctly.

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended modern .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 6. Review Removed or Changed APIs
Cross-platform .NET removes certain APIs that existed in .NET Framework. Manually review the following areas for any runtime issues that would not appear as build errors:

- `System.Web` usages replaced with ASP.NET Core equivalents.
- Any `HttpContext`, session, or caching APIs that may behave differently.
- Windows-specific APIs (e.g., registry access, Windows authentication) that may not function on non-Windows platforms.

### 7. Check Application Configuration
Verify that configuration files have been correctly migrated:

- `Web.config` settings should have been moved to `appsettings.json`.
- Connection strings should be present and correct in `appsettings.json` or environment variables.
- Any `system.web` configuration sections should now be handled via ASP.NET Core middleware registration in `Program.cs` or `Startup.cs`.

### 8. Deploy to Target Environment
Once local validation is complete, publish the application using the following command:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory and deploy them to the target server or hosting environment. Ensure the target environment has the correct .NET runtime installed by running:

```bash
dotnet --info
```