# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The solution has no build errors following the transformation. All projects compiled successfully, including `Northwind.Web`.

## Validation Steps

### 1. Verify Target Framework

Open each `.csproj` file and confirm the target framework is set to a supported cross-platform .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 2. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages restore cleanly:

```bash
dotnet restore
```

Review the output for any warnings about deprecated packages or version conflicts.

### 3. Build the Solution

Perform a full solution build to confirm there are no errors:

```bash
dotnet build --configuration Release
```

### 4. Run Unit Tests

If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release
```

Review test output for any failures that may indicate behavioral differences introduced by the migration.

### 5. Run the Application Locally

Start the `Northwind.Web` project and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following:
- The application starts without runtime exceptions.
- All routes respond correctly.
- Database connections (if applicable) are functional.
- Static assets load properly.

### 6. Review `Program.cs` and `Startup` Configuration

If the project was migrated from ASP.NET to ASP.NET Core, confirm that middleware, dependency injection registrations, and configuration sources have been correctly ported. Look for:
- Replaced `Global.asax` logic now in `Program.cs`.
- `web.config` settings migrated to `appsettings.json`.
- Authentication and authorization middleware correctly ordered.

### 7. Check for Removed or Changed APIs

Review any use of APIs that are known to differ between .NET Framework and cross-platform .NET. Pay particular attention to:
- `System.Web` references, which are not available in .NET Core and later.
- `HttpContext` usage patterns.
- Any Windows-specific APIs (e.g., registry access, WCF, MSMQ).

Run the .NET Upgrade Assistant compatibility analyzer if a deeper audit is needed:

```bash
dotnet tool install -g upgrade-assistant
upgrade-assistant analyze src/Northwind.Web/Northwind.Web.csproj
```

### 8. Validate Configuration Files

Ensure `appsettings.json` contains all necessary configuration values, including connection strings and environment-specific settings. Confirm that `appsettings.Development.json` is present for local development overrides.

### 9. Test on a Non-Windows Platform (if applicable)

If cross-platform support is a goal, run the application on Linux or macOS to surface any platform-specific issues:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Look for path separator issues, case-sensitive file references, or platform-specific dependencies.