# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including the most critical project `Northwind.Web`.

## Validation

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to missing packages or version conflicts.

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Check the output for any warnings that may indicate deprecated APIs or compatibility concerns that were not caught as errors.

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally
Start the application locally to confirm it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Verify that the application starts without runtime exceptions.
- Navigate through the application's core functionality and confirm expected behavior.
- Check application logs for any runtime warnings or errors.

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended modern .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

If the target framework is not at the desired version, update it and re-run the build and tests.

### 6. Review Removed or Changed APIs
Cross-platform .NET removes certain APIs that were available in .NET Framework. Manually review the following areas for potential runtime issues that do not surface as build errors:

- **Configuration**: Ensure `System.Configuration.ConfigurationManager` usage has been replaced with `Microsoft.Extensions.Configuration` where applicable.
- **Security and Identity**: Verify any authentication or authorization logic functions correctly under ASP.NET Core's middleware pipeline.
- **Database access**: Confirm connection strings and database providers (e.g., Entity Framework Core) are configured correctly for the target environment.
- **File paths**: Ensure no hardcoded Windows-style paths exist that would break on Linux or macOS.

### 7. Check for Compatibility Warnings with the .NET Upgrade Analyzer
Install and run the `dotnet-upgrade-assistant` or the `Microsoft.DotNet.UpgradeAssistant` tool to surface any remaining compatibility concerns:

```bash
dotnet tool install -g upgrade-assistant
upgrade-assistant analyze src/Northwind.Web/Northwind.Web.csproj
```

Review the generated report and address any flagged items.

### 8. Validate Application Behavior Against the Legacy Version
Where possible, compare the output and behavior of the migrated application against the original .NET Framework version to confirm functional parity. Pay particular attention to:

- HTTP response codes and content
- Data access results
- Any scheduled or background tasks