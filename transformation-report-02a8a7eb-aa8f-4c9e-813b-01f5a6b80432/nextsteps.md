# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the root of the solution to ensure all NuGet packages are restored correctly:

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
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests and address them before proceeding.

### 4. Run the Application Locally

Start the application locally to verify it runs as expected on the new cross-platform .NET runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate to the application in a browser and exercise the core functionality, particularly any areas that relied on Windows-specific APIs in the legacy project.

### 5. Check for Runtime Compatibility Issues

Even with a clean build, certain legacy patterns can cause runtime failures. Pay attention to the following areas:

- **Database connectivity**: Verify that connection strings are correctly configured for the target environment and that the database provider (e.g., SQL Server, SQLite) is compatible with the new .NET version.
- **Authentication and authorization**: Confirm that any middleware or identity configurations are functioning correctly.
- **File system paths**: Ensure no hardcoded Windows-style paths (e.g., `C:\`) remain in configuration files or code.
- **Configuration files**: Confirm that `appsettings.json` and any environment-specific variants are present and correctly structured.

### 6. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a consistent and compatible framework version.

### 7. Review Removed or Changed APIs

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.PlatformAbstractions` tooling to scan for any usage of APIs that were removed or significantly changed between the legacy .NET Framework and the current .NET version.

```bash
dotnet tool install -g upgrade-assistant
upgrade-assistant analyze src/Northwind.Web/Northwind.Web.csproj
```

Address any flagged compatibility issues before deploying.

### 8. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory and confirm all required assets, configuration files, and binaries are present.