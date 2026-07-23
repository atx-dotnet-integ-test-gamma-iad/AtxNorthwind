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

Verify that no warnings or errors appear related to missing or incompatible packages.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify runtime behavior:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Address any failing tests before proceeding further.

### 4. Run the Application Locally

Start the web application locally to verify it runs as expected on the cross-platform .NET runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate to the application in a browser and exercise the primary workflows to confirm expected behavior.

### 5. Check for Runtime Compatibility Issues

Even with a clean build, certain areas warrant manual review:

- **Database connectivity**: Confirm that connection strings and database providers (e.g., Entity Framework Core, ADO.NET) are correctly configured for the target environment.
- **Authentication and Authorization**: Verify that any authentication middleware (e.g., cookies, JWT) is functioning correctly under ASP.NET Core.
- **Static files and routing**: Confirm that static assets are served correctly and all routes resolve as expected.
- **Configuration**: Ensure `appsettings.json` contains all necessary keys that were previously in `Web.config` or `App.config`, as these are not automatically migrated.

### 6. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

If an older or unintended version is present, update it and re-run the build and tests.

### 7. Review Removed or Changed APIs

Check the code for any usage of APIs that were available in .NET Framework but behave differently in cross-platform .NET, including:

- `System.Web` references (should have been replaced with ASP.NET Core equivalents)
- `HttpContext` usage patterns
- Any Windows-specific APIs (e.g., registry access, Windows identity) that may fail on non-Windows platforms

### 8. Publish the Application

Once local validation is complete, produce a published output:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory and confirm the application starts correctly from that output using:

```bash
dotnet ./publish/Northwind.Web.dll
```