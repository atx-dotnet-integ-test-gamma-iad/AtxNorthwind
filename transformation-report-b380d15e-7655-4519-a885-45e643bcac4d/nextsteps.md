# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of errors:

```bash
dotnet build --configuration Release
```

Review the build output and confirm that all projects report a successful build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the transformation:

```bash
dotnet test --configuration Release
```

Review the test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the web application locally to verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Once running, navigate to the application in a browser and verify that core functionality behaves as expected.

### 5. Verify Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm that the `<TargetFramework>` element targets the intended cross-platform .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Repeat this check for all other projects in the solution to ensure consistency across the solution.

### 6. Review Removed or Changed APIs

Cross-platform .NET removes or changes certain APIs that were available in .NET Framework. Use the .NET Upgrade Assistant compatibility analyzer or the following command to check for any remaining compatibility concerns:

```bash
dotnet build /p:EnableNETAnalyzers=true /p:AnalysisMode=All
```

Review any warnings produced and address those that relate to deprecated or platform-specific APIs.

### 7. Check Runtime Behavior

Pay particular attention to the following areas that commonly differ between .NET Framework and cross-platform .NET:

- **Configuration**: Ensure `web.config` settings have been migrated to `appsettings.json` where applicable.
- **Authentication and Authorization**: Verify middleware is configured correctly in `Program.cs` or `Startup.cs`.
- **Entity Framework**: If using Entity Framework, confirm the correct version (EF Core) is referenced and that migrations are functioning.
- **Static Files and Routing**: Confirm that static file serving and route mappings work as expected under the new hosting model.

### 8. Test Against the Target Database

If the application connects to a database, run the application against the target database and verify that:

- Connections are established successfully.
- Queries return expected results.
- Any migrations have been applied correctly.