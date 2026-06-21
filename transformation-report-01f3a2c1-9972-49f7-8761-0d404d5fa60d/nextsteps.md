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
Perform a full solution build to confirm the absence of any compilation errors:

```bash
dotnet build --configuration Release
```

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify that existing functionality behaves as expected after the migration:

```bash
dotnet test --configuration Release --verbosity normal
```

Review the test output for any failures or regressions introduced during the transformation.

### 4. Run the Application Locally
Start the `Northwind.Web` project locally and verify that it runs without runtime errors:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the application's core workflows manually, paying particular attention to any areas that relied on Windows-specific APIs or legacy .NET Framework behaviors.

### 5. Review Removed or Changed APIs
Cross-platform .NET removes or changes certain APIs that were available in .NET Framework. Review the following areas for potential runtime issues that would not surface as build errors:

- **`System.Web` dependencies** — These are not available in cross-platform .NET. Ensure any HTTP context, session, or request/response handling has been migrated to ASP.NET Core equivalents.
- **`ConfigurationManager`** — If previously used, confirm it has been replaced with the `Microsoft.Extensions.Configuration` model.
- **`BinaryFormatter`** — This is disabled by default in modern .NET. If serialization is used, verify it has been replaced with a supported alternative.
- **Windows Registry or COM interop** — If any components relied on these, they will fail at runtime on non-Windows platforms.

### 6. Check Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` is set to a current and supported version, such as:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Refer to the [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core) to ensure the targeted version is within its support window.

### 7. Verify Runtime Behavior on Target Platform
If the intent is to run on Linux or macOS, test the application on the target operating system explicitly. File path casing, line endings, and platform-specific library availability can cause issues that are not visible on Windows.

### 8. Review Warnings
Even without errors, the build may have produced warnings. Review them with:

```bash
dotnet build --configuration Release 2>&1 | grep -i warning
```

Address any warnings related to deprecated APIs, nullable reference types, or obsolete packages, as these may indicate future compatibility issues.