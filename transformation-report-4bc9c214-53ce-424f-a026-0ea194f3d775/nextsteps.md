# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including `Northwind.Web`.

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

### 3. Run Unit Tests
If the solution contains any test projects, execute them to verify that existing logic behaves as expected after the migration:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review the test output for any failures or unexpected results that may indicate behavioral regressions introduced during the transformation.

### 4. Run the Application Locally
Start the `Northwind.Web` project and verify it runs as expected on the target platform:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following:
- The application starts without runtime exceptions.
- All routes and endpoints respond correctly.
- Database connections (if applicable) are functioning properly.
- Any static assets, views, or Razor pages render as expected.

### 5. Verify Target Framework
Confirm that all projects are targeting the intended .NET version by inspecting each `.csproj` file for the `<TargetFramework>` element, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure consistency across all projects in the solution.

### 6. Review Removed or Changed APIs
Cross-platform .NET removes or changes certain APIs that were available in .NET Framework. Manually review the following areas for potential runtime issues that would not surface as build errors:

- **`System.Web` dependencies**: Any remaining indirect usage may cause runtime failures.
- **Configuration**: Ensure `web.config`-based configuration has been fully replaced with `appsettings.json` and the `Microsoft.Extensions.Configuration` stack.
- **Authentication and Authorization**: Verify middleware is correctly configured in `Program.cs` or `Startup.cs`.
- **Entity Framework**: If using EF, confirm the correct version of EF Core is referenced and that migrations are up to date.

### 7. Platform-Specific Behavior
If the application is being run on a non-Windows platform, test the following explicitly:

- File path handling (use `Path.Combine` rather than hardcoded separators).
- Case-sensitive file references in views or static files.
- Any Windows-specific registry or COM interop calls that may have been carried over.

### 8. Publish the Application
Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all required files are present before deploying to the target environment.