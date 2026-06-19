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

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Check the output for any warnings that, while non-breaking, may indicate deprecated APIs or patterns that should be addressed.

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy framework and the new .NET runtime.

### 4. Verify Runtime Behavior
Run the application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Specifically, check the following areas that are commonly affected by cross-platform migrations:

- **File paths**: Ensure no hardcoded Windows-style paths (e.g., `C:\` or backslashes) exist in configuration or code.
- **Database connectivity**: Confirm connection strings are valid and the target database is reachable.
- **Authentication and authorization**: Verify any middleware or security configurations function as expected.
- **Static files and routing**: Confirm all routes resolve correctly and static assets are served properly.

### 5. Review Configuration Files
Inspect `appsettings.json` and any environment-specific variants (e.g., `appsettings.Production.json`) to ensure:

- Connection strings are updated for the target environment.
- Any references to legacy configuration sections (e.g., `<system.web>` from `Web.config`) have been properly migrated to the new configuration system.

### 6. Check for Removed or Changed APIs
Review the code for usage of APIs that were removed or had behavioral changes in modern .NET. The [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the [Microsoft.DotNet.UpgradeAssistant](https://github.com/dotnet/upgrade-assistant) tool can assist in identifying these.

## Deployment

### 1. Publish the Application
Publish the application to a target folder using:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

### 2. Verify the Published Output
Inspect the `./publish` directory to confirm all expected files are present, including:

- The application binary and its dependencies.
- The `appsettings.json` configuration file.
- Static web assets (if applicable).

### 3. Run the Published Output
Test the published output directly before deploying to a server:

```bash
dotnet ./publish/Northwind.Web.dll
```

Confirm the application starts without errors and behaves as expected under the published configuration.