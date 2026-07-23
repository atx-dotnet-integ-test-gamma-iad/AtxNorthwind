# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors are reported during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while non-breaking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit Tests

If the solution contains test projects, execute them to confirm runtime behavior is intact:

```bash
dotnet test --configuration Release --verbosity normal
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Run the Application Locally

Start the web application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate to the application in a browser and exercise the primary workflows to confirm functionality.

### 5. Check for Runtime Compatibility Issues

Even with a clean build, certain areas require manual verification at runtime:

- **Database connectivity**: Confirm that connection strings are correctly configured for the target environment and that the database provider (e.g., SQL Server, SQLite) is compatible with the new runtime.
- **Authentication and authorization**: If the application uses Windows Authentication or legacy ASP.NET membership, verify these work correctly under the new hosting model.
- **Static files and routing**: Confirm that static assets are served correctly and that all routes resolve as expected.
- **Configuration**: Verify that `appsettings.json` (or environment variables) correctly replaces any values previously held in `Web.config` or `App.config`.

### 6. Review Removed or Changed APIs

Cross-reference the application code against the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the [.NET API compatibility documentation](https://learn.microsoft.com/en-us/dotnet/core/compatibility/) to identify any APIs that may have changed behavior between .NET Framework and modern .NET, even if they compiled without errors.

### 7. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If a newer Long-Term Support (LTS) version of .NET is available and desired, update this value and re-run the build and test steps above.

### 8. Publish the Application

Once local validation is complete, produce a published output:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory and confirm all expected files are present before deploying to the target environment.