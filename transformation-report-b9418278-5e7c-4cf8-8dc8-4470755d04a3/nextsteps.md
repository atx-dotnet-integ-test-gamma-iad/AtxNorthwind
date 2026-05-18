# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation and Testing

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Release
```

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Review Runtime Behavior
Build errors being absent does not guarantee correct runtime behavior. Start the web application and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Pay particular attention to:
- Database connectivity and any Entity Framework migrations that may need to be re-applied.
- Authentication and authorization flows, as middleware configuration changed significantly between .NET Framework and modern .NET.
- Any areas of the application that relied on `System.Web`, as those APIs do not exist in cross-platform .NET and may have been substituted during transformation.

### 5. Check for Removed or Replaced APIs
Review the transformed code for any uses of compatibility shims or packages such as `Microsoft.AspNetCore.SystemWebAdapters`. If these were introduced during transformation, evaluate whether the underlying code can be refactored to use native ASP.NET Core equivalents to avoid long-term compatibility risk.

### 6. Review Configuration Files
Confirm that `appsettings.json` (and environment-specific variants such as `appsettings.Production.json`) contain all settings that were previously held in `Web.config` or `App.config`. Connection strings, application settings, and custom configuration sections should all be accounted for.

### 7. Validate Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all dependent projects in the solution target a compatible framework version.

### 8. Publish a Release Build
Once the above steps are satisfactory, produce a published output to verify the deployment artifact is generated correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files, static assets, and configuration files are present.