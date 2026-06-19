# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Debug
dotnet build --configuration Release
```

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed during the transformation:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Check for Runtime Compatibility Issues

Even with a clean build, certain APIs behave differently or are unsupported on cross-platform .NET. Pay attention to the following areas:

- **Windows-specific APIs**: Any usage of `System.Drawing`, `Microsoft.Win32`, or P/Invoke calls may not function correctly on Linux or macOS.
- **Configuration**: Ensure `Web.config` settings have been properly migrated to `appsettings.json` and that `IConfiguration` is used throughout.
- **Entity Framework**: If the project uses Entity Framework, confirm it has been updated to Entity Framework Core and that migrations are compatible.
- **Session and Authentication**: Verify that any authentication middleware (e.g., Forms Authentication) has been replaced with the ASP.NET Core equivalents.

### 5. Run the Application Locally

Start the application using the .NET CLI:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate through the application's key pages and features to confirm they behave as expected. Check the console output and application logs for any runtime exceptions or warnings.

### 6. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is targeting `net6.0` or `net7.0`, consider upgrading to `net8.0` as those versions are no longer receiving long-term support.

### 7. Review Nullable Reference Types and Warnings

The cross-platform .NET SDK enables stricter compiler analysis by default. Review any compiler warnings in the build output, particularly those related to nullable reference types, and address them to improve code reliability.

```bash
dotnet build --configuration Release -warnaserror
```

This command will surface any warnings that should be resolved before considering the migration complete.

### 8. Verify Static Files and wwwroot

For web projects, confirm that static assets (CSS, JavaScript, images) are located in the `wwwroot` folder and are being served correctly when the application runs. Legacy ASP.NET projects may have had these files in different locations.