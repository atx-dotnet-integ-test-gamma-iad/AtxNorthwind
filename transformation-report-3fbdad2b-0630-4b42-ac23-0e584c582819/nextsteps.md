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

Verify that no warnings or errors are produced during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that runtime behavior is consistent with the original project:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Check for Compatibility Warnings

Use the .NET Upgrade Assistant or the `dotnet-compatibility` analyzer to identify any runtime compatibility issues that may not surface as build errors:

```bash
dotnet tool install -g dotnet-compatibility
```

Pay particular attention to:
- APIs that were removed or changed between .NET Framework and modern .NET
- Windows-specific APIs that may not function on non-Windows platforms
- Any use of `app.config` or `web.config` that may need to be migrated to `appsettings.json`

### 5. Review Configuration Files

Confirm that `appsettings.json` contains all necessary configuration values that were previously held in `web.config` or `app.config`, including:
- Connection strings
- Application settings
- Authentication or authorization settings

### 6. Run the Application Locally

Start the application locally and verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the application and confirm that:
- All routes resolve correctly
- Database connections are functional
- Static assets are served as expected
- Authentication flows work as intended

### 7. Verify Target Framework

Open the `.csproj` file and confirm the `TargetFramework` is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If the project is a web application, confirm it references `Microsoft.NET.Sdk.Web`:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
```

### 8. Review Nullable Reference Type Warnings

If nullable reference types are enabled, review any related warnings that could indicate potential null reference issues at runtime:

```xml
<Nullable>enable</Nullable>
```

Address any warnings to improve overall code reliability.

### 9. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory and confirm all expected files are present before deploying to the target environment.