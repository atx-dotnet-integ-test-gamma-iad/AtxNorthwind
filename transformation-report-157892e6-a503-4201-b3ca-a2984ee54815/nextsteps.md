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

Review test output carefully for any failures or skipped tests that may indicate compatibility issues introduced during migration.

### 4. Review Target Framework

Open each `.csproj` file and confirm the `<TargetFramework>` element is set to the intended cross-platform .NET version (e.g., `net8.0`). Ensure no projects are still referencing `net48` or other legacy framework monikers unintentionally.

### 5. Check for Removed or Replaced APIs

Even without build errors, some APIs may have changed behavior in cross-platform .NET. Review the following areas manually:

- **Database access**: If Entity Framework 6 was used, confirm whether it has been migrated to EF Core, as EF6 has limited support on cross-platform .NET.
- **Configuration**: Verify that `Web.config` or `App.config` based configuration has been replaced with `appsettings.json` and the `Microsoft.Extensions.Configuration` stack.
- **HTTP modules and handlers**: Confirm that any legacy ASP.NET HTTP modules or handlers have been replaced with ASP.NET Core middleware.
- **Session and authentication**: Verify that any `System.Web` based session or authentication mechanisms have been replaced with their ASP.NET Core equivalents.

### 6. Run the Application Locally

Start the application using the .NET CLI and navigate through its core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Test the following manually:
- Application startup without exceptions
- Core page or endpoint responses return expected data
- Database connectivity is functional
- Any authentication or authorization flows behave correctly

### 7. Review Runtime Warnings

Even if the application runs, check the console output and application logs for runtime warnings such as:

- Obsolete API usage
- Missing middleware registrations
- Configuration values that were not loaded correctly

### 8. Validate Static Assets and Views

If the project uses Razor views or serves static files, confirm that:

- Static files (CSS, JS, images) are located under `wwwroot` and are being served correctly
- Razor views render without errors
- Any Razor syntax that relied on `System.Web.Mvc` has been updated to ASP.NET Core Razor conventions

### 9. Check Platform-Specific Behavior

Since the goal is cross-platform support, if possible, run the application on both Windows and a Linux or macOS environment to surface any platform-specific issues such as:

- File path casing sensitivity
- Windows-only registry or COM dependencies
- Platform-specific cryptography or certificate handling