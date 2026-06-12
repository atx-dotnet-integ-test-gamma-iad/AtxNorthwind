# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The solution build output contains no errors across all projects, including `Northwind.Web`. This indicates that the transformation to cross-platform .NET has completed without any compilation issues.

## Validation Steps

### 1. Restore and Build the Solution

Run the following commands from the solution root to confirm a clean restore and build:

```bash
dotnet restore
dotnet build --configuration Release
```

Verify that the output reports `0 Error(s)` for all projects.

### 2. Run Unit Tests

If the solution contains test projects, execute them to confirm existing functionality is intact:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review the test output for any failures or skipped tests that may indicate behavioral regressions introduced during the transformation.

### 3. Run the Web Application Locally

Start the `Northwind.Web` project and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Confirm the application starts without runtime exceptions.
- Navigate through the application's key pages and endpoints to verify they respond correctly.
- Check the console output for any warnings or unhandled exceptions at runtime.

### 4. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended cross-platform .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a consistent and compatible framework version.

### 5. Check for Removed or Changed APIs

Even with a clean build, some APIs behave differently between .NET Framework and modern .NET. Pay particular attention to:

- `System.Web` usages that may have been replaced with ASP.NET Core equivalents.
- Any HTTP pipeline middleware that was migrated from `HttpModule`/`HttpHandler` patterns.
- Configuration access patterns migrated from `System.Configuration` to `Microsoft.Extensions.Configuration`.

Test each of these areas manually or through integration tests.

### 6. Review NuGet Package Versions

Check that all NuGet packages referenced across the solution are up to date and compatible with the target framework:

```bash
dotnet list package --outdated
```

Update packages where appropriate, then rebuild and retest.

### 7. Verify Database Connectivity

If the application uses a database (as is typical for a Northwind-based project), confirm that:

- Connection strings in `appsettings.json` are correctly configured for the target environment.
- Entity Framework or ADO.NET migrations (if applicable) run without errors:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 8. Publish the Application

Once the above steps are validated, produce a published output:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all expected assets, configuration files, and binaries are present before deploying to the target environment.