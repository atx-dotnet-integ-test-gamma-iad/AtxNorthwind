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

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the transformation:

```bash
dotnet test --configuration Release
```

Review test results and address any failures before proceeding.

### 4. Run the Application Locally

Start the application locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Manually verify the following:
- The application starts without exceptions.
- All routes and endpoints respond as expected.
- Database connectivity functions correctly if applicable.
- Any authentication or authorization flows behave as intended.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If the target framework is not at the desired version, update it and re-run the build and test steps above.

### 6. Review Removed or Changed APIs

Check the codebase for any usage of APIs that were available in .NET Framework but have changed behavior in cross-platform .NET, including:

- `System.Web` references, which are not available in cross-platform .NET.
- `HttpContext` usage patterns that differ between ASP.NET and ASP.NET Core.
- Windows-specific APIs such as the registry, WMI, or Windows-only cryptography providers.
- Any third-party NuGet packages that may have been targeting .NET Framework only. Verify their compatibility with the new target framework on [NuGet.org](https://www.nuget.org).

### 7. Verify Configuration System

.NET cross-platform projects use `appsettings.json` rather than `Web.config` or `App.config`. Confirm that:

- All connection strings have been migrated to `appsettings.json`.
- All application settings have been moved to the appropriate configuration file.
- The `Web.config` file, if still present, only contains IIS-specific settings that are still required.

### 8. Verify Static Files and wwwroot

Ensure that all static assets such as JavaScript, CSS, and image files are located under the `wwwroot` folder, as this is the expected convention for ASP.NET Core applications.

### 9. Publish the Application

Once the above steps are completed and validated, publish the application to confirm the output is correct:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all expected files are present before deploying to the target environment.