# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most critical project `Northwind.Web`.

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

If the solution contains test projects, execute them to verify runtime behavior:

```bash
dotnet test --configuration Release --verbosity normal
```

Review any failing tests and address them before proceeding.

### 4. Run the Application Locally

Start the web application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following:
- The application starts without runtime exceptions.
- All routes and pages load correctly.
- Database connections (if applicable) are functioning properly.

### 5. Verify Configuration Files

Review `appsettings.json` and any environment-specific configuration files (e.g., `appsettings.Development.json`) to ensure:
- Connection strings are updated and valid for the target environment.
- Any legacy `Web.config` or `App.config` settings have been correctly migrated to the new configuration system.

### 6. Check for Deprecated or Removed APIs

Even without build errors, some APIs may have behavioral differences in modern .NET. Review the code for usage of:
- `System.Web` namespaces, which are not available in cross-platform .NET.
- Any third-party libraries that may have been updated and contain breaking changes.

Run the .NET Upgrade Assistant compatibility analyzer if a deeper audit is needed:

```bash
dotnet tool install -g upgrade-assistant
upgrade-assistant analyze src/Northwind.Web/Northwind.Web.csproj
```

### 7. Test Against Target Runtime

Confirm the application runs on the intended target platform (Linux, macOS, or Windows) by executing the steps above on that specific operating system, as some issues only surface at runtime on non-Windows environments (e.g., file path casing, platform-specific APIs).

### 8. Publish the Application

Once validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory and confirm all required assets, static files, and configuration files are present.