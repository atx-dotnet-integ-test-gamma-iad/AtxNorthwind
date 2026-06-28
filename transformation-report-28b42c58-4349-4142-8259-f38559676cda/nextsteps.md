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

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Release
```

Check that all projects compile under both `Debug` and `Release` configurations.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed during the transformation:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Check for Runtime Compatibility Issues

Some APIs behave differently or are unavailable on cross-platform .NET even when they compile without errors. Pay attention to the following areas:

- **Windows-specific APIs**: Any usage of `System.Web`, Windows Registry, COM interop, or WCF server-side components may compile but fail at runtime on non-Windows platforms.
- **Configuration**: Ensure `Web.config` or `App.config` settings have been migrated to `appsettings.json` and are being read correctly via `IConfiguration`.
- **Authentication/Authorization**: Verify that any Forms Authentication or Windows Authentication configurations have been updated to ASP.NET Core equivalents.
- **Entity Framework**: If the project uses EF6, confirm whether it has been migrated to EF Core or if EF6 is being used under .NET intentionally.

### 5. Run the Application Locally

Start the application and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the primary workflows of the application and confirm that pages render, data loads, and operations complete as expected.

### 6. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If the project is targeting `net6.0` or `net7.0`, consider updating to `net8.0` as those versions are out of long-term support.

### 7. Review Nullable Reference Type Warnings

Cross-platform .NET projects often enable nullable reference types by default. Run a build with warnings treated as informational to identify any nullability issues that may cause runtime null reference exceptions:

```bash
dotnet build --configuration Release /p:TreatWarningsAsErrors=false
```

Address any `CS8600`, `CS8602`, or `CS8603` warnings that indicate potential null dereference paths.

### 8. Verify Static Files and wwwroot

If `Northwind.Web` is a web application, confirm that static assets (CSS, JavaScript, images) are present under the `wwwroot` folder and are being served correctly when the application runs.

### 9. Check Logging and Error Handling

Confirm that the logging configuration in `Program.cs` or `appsettings.json` is correctly set up and that unhandled exceptions are surfaced appropriately during local testing.