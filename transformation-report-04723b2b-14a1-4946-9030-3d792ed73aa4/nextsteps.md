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

Perform a full solution build to confirm the absence of any compile-time errors:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not prevent a successful build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review the test results and investigate any failures, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Verify Runtime Behavior

Run the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following areas specifically, as they are common sources of runtime issues after migration:

- **Database connectivity**: Confirm that connection strings are correctly configured for the target environment and that the database provider (e.g., Entity Framework Core) is functioning as expected.
- **Authentication and Authorization**: Verify that any authentication middleware is behaving correctly, particularly if the project previously used `System.Web` based authentication.
- **Static files and routing**: Confirm that static assets are served correctly and that all routes resolve as expected.
- **Configuration**: Ensure that `appsettings.json` contains all necessary settings that were previously held in `Web.config` or `App.config`.

### 5. Review Removed or Changed APIs

Cross-reference the original project's dependencies against the new project file to confirm that all necessary packages are present. Pay particular attention to:

- Any packages that were auto-replaced during transformation, as the replacement may have a different API surface.
- APIs that existed in .NET Framework but have been removed or changed in cross-platform .NET, which may only surface at runtime rather than at compile time.

### 6. Check Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` value is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure this aligns with the .NET version installed on your target deployment environment.

### 7. Publish the Application

Once the above steps have been validated, publish the application to confirm the output is complete and correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected files, including configuration files and static assets, are present before deploying to the target environment.