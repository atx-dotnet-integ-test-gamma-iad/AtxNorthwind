# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected in any of the projects within the solution, including the primary project `Northwind.Web`.

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

Review the output and confirm that the build reports `0 Error(s)`.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the migration:

```bash
dotnet test --configuration Release --verbosity normal
```

Review the test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the application locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the application and verify that core functionality, routing, and data access behave as expected.

### 5. Verify Target Framework

Open `Northwind.Web.csproj` and confirm that the `TargetFramework` element is set to the intended cross-platform .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it references a Windows-specific framework such as `net48` or `net472`, the migration may be incomplete.

### 6. Check for Platform-Specific APIs

Search the codebase for any remaining usage of Windows-specific APIs or libraries that may not surface as build errors but could cause runtime failures on non-Windows platforms. Common areas to check include:

- `System.Drawing` (replace with a cross-platform alternative such as `SkiaSharp` if needed)
- Windows Registry access via `Microsoft.Win32.Registry`
- `System.Windows.Forms` or `System.Web` references
- Any P/Invoke calls targeting Windows-only native libraries

You can use the .NET Upgrade Assistant compatibility analyzer or the `Microsoft.DotNet.PlatformAbstractions` tooling to assist with this check.

### 7. Review Configuration Files

Confirm that `appsettings.json` and any environment-specific configuration files (`appsettings.Development.json`, etc.) are present and correctly structured. Verify that connection strings and other settings have been carried over from any legacy `Web.config` or `App.config` files.

### 8. Validate Database Connectivity

If the application uses a database, confirm that the connection string is valid in the new environment and that any Entity Framework migrations or database schema requirements are satisfied:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 9. Test on a Non-Windows Platform (Optional but Recommended)

Since the goal of the migration is cross-platform support, consider running the application on Linux or macOS to confirm there are no hidden platform dependencies:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Address any runtime exceptions that appear exclusively on non-Windows environments.

### 10. Review Deprecated or Obsolete API Usage

Even when the build succeeds, warnings about deprecated APIs should be addressed to ensure long-term maintainability. Run the build with warnings treated as informational and review the output:

```bash
dotnet build --configuration Release /p:TreatWarningsAsErrors=false
```

Prioritize resolving any `CS0618` (obsolete) or `CS0612` warnings in the output.