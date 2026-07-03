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

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Debug
dotnet build --configuration Release
```

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing logic behaves as expected after the migration:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Check for Runtime Compatibility Issues

Some APIs that compiled successfully may behave differently or throw at runtime on cross-platform .NET. Pay particular attention to:

- **Windows-specific APIs**: Any usage of `System.Drawing`, `System.Windows.Forms`, registry access, or Windows-only interop that may not function on non-Windows platforms.
- **Configuration**: Verify that `Web.config` or `App.config` settings have been properly migrated to `appsettings.json` and that the application reads them correctly at runtime.
- **Connection strings**: Confirm that database connection strings in `appsettings.json` are correct and accessible in the new environment.

### 5. Run the Web Application Locally

Start the application using the .NET CLI and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate to the application in a browser and exercise the primary workflows to check for runtime errors or unexpected behavior.

### 6. Review Middleware and HTTP Pipeline

If this is an ASP.NET Core project migrated from ASP.NET (System.Web), verify that:

- Authentication and authorization middleware is correctly configured in `Program.cs` or `Startup.cs`.
- Custom HTTP handlers or modules from the legacy project have been replaced with the appropriate ASP.NET Core middleware equivalents.
- Static file serving is configured correctly.

### 7. Review Logging

Confirm that the logging framework is properly configured. If the legacy project used `log4net`, `NLog`, or similar, verify that the provider has been wired into the ASP.NET Core logging pipeline or replaced with `Microsoft.Extensions.Logging`.

### 8. Validate Database Access

If the project uses Entity Framework, confirm the version in use:

- **Entity Framework Core**: Run any pending migrations with `dotnet ef database update` and verify that queries return expected results.
- **Entity Framework 6**: Confirm that the `EntityFramework6.Npgsql` or `EntityFramework6` NuGet package is being used, as EF6 has limited support on cross-platform .NET.

### 9. Target Framework Verification

Open each `.csproj` file and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure no projects are still referencing `net4x` target frameworks unintentionally.

### 10. Publish the Application

Once all runtime validation steps pass, produce a published output:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all expected assets, configuration files, and binaries are present before deploying to the target environment.