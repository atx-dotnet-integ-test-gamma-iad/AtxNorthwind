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

Perform a full solution build to confirm the absence of errors:

```bash
dotnet build --configuration Release
```

Review the output and confirm that the build succeeds with zero errors and review any warnings that may require attention.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality is intact after the migration:

```bash
dotnet test --configuration Release
```

Review test results and investigate any failing tests, as they may indicate behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Verify Runtime Behavior

Run the application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Walk through the primary user-facing features and confirm they behave as expected. Pay particular attention to:

- Database connectivity and query results
- Authentication and authorization flows
- Any file system or path-dependent operations, as these can behave differently across operating systems

### 5. Review Removed or Changed APIs

Cross-platform .NET does not include certain APIs that were available in .NET Framework. Review the following areas manually:

- **`System.Web` dependencies**: Confirm that all `System.Web` usages have been replaced with their ASP.NET Core equivalents
- **WCF or Remoting**: If any WCF services or .NET Remoting was in use, verify that replacements such as `CoreWCF` or gRPC have been applied
- **Configuration**: Confirm that `web.config`-based configuration has been migrated to `appsettings.json` and the `IConfiguration` system
- **Entity Framework**: If using Entity Framework 6, consider whether a migration to Entity Framework Core is warranted for full cross-platform support

### 6. Check Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the target framework is set to a currently supported version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If the project is targeting an older version such as `net5.0` or `net6.0`, consider updating to `net8.0`, which is the current Long Term Support (LTS) release.

### 7. Test on Target Operating Systems

If cross-platform support is a goal, run and validate the application on each intended operating system (Windows, Linux, macOS) to catch any platform-specific issues such as:

- Case-sensitive file paths on Linux
- Platform-specific line endings
- OS-specific environment variable behavior

### 8. Review Startup and Middleware Configuration

Confirm that `Program.cs` and any middleware configuration follows the current ASP.NET Core patterns. Legacy `Startup.cs` patterns are still supported but the minimal hosting model introduced in .NET 6 is the current standard approach.