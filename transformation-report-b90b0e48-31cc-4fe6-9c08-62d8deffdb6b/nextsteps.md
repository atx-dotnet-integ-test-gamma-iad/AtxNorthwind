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

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify that existing logic behaves as expected after the transformation:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any test failures carefully, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Review Runtime Behavior
Certain issues do not surface at build time. Pay attention to the following areas at runtime:

- **Configuration**: Verify that `appsettings.json` (and environment-specific variants) are correctly replacing any legacy `Web.config` or `App.config` values.
- **Connection Strings**: Confirm that database connection strings are valid and accessible in the new environment.
- **Authentication and Authorization**: If the application uses any authentication middleware, test login flows and role-based access thoroughly.
- **Static Files and Routing**: Navigate through the application to confirm that routing, static assets, and views render correctly.

### 5. Check for Removed or Changed APIs
Some .NET Framework APIs were removed or changed in cross-platform .NET. Run the .NET Upgrade Assistant compatibility analyzer or the Platform Compatibility Analyzer to surface any runtime-only compatibility concerns:

```bash
dotnet add package Microsoft.DotNet.PlatformAbstractions
```

Alternatively, review the [.NET breaking changes documentation](https://learn.microsoft.com/en-us/dotnet/core/compatibility/breaking-changes) relevant to your target version.

### 6. Run the Application Locally
Start the application and perform manual smoke testing:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Walk through the primary user-facing workflows to confirm functional correctness.

### 7. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm that the `<TargetFramework>` element is set to the intended version (e.g., `net8.0`), and that no unintended legacy references remain:

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 8. Verify Publish Output
Perform a publish to confirm the output is complete and self-consistent:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to ensure all expected files, assets, and dependencies are present.