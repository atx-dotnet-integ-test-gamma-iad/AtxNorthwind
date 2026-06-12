# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while not blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality behaves as expected after the migration:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Check for Runtime Compatibility Issues

Some APIs that compile successfully may behave differently or throw at runtime on cross-platform .NET. Pay particular attention to:

- **Windows-specific APIs**: Any usage of `System.Drawing`, registry access, or Windows authentication that may not be fully supported on non-Windows platforms.
- **Configuration**: Verify that `appsettings.json` and any environment-specific configuration files are correctly replacing legacy `Web.config` or `App.config` values.
- **Connection Strings**: Confirm that database connection strings in `appsettings.json` are correct and accessible from the new runtime environment.

### 5. Run the Application Locally

Start the application using the .NET CLI:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the key areas of the application and verify that pages load, data is retrieved correctly, and no unhandled exceptions occur.

### 6. Review Nullable Reference Type Warnings

If the project has nullable reference types enabled, review any compiler warnings related to nullability. These are not build errors but can indicate potential null reference exceptions at runtime:

```bash
dotnet build --configuration Release /warnaserror:nullable
```

Address any issues found to improve runtime stability.

### 7. Verify Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure this matches the .NET SDK version installed on your machine and your intended deployment target.

### 8. Publish the Application

Once local validation is complete, produce a published output:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected assemblies, static files, and configuration files are present before deploying to the target environment.