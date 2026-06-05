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

If the solution contains test projects, execute them to verify that existing logic behaves correctly after the migration:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Check for Runtime Compatibility Issues

Some APIs behave differently or are unsupported on cross-platform .NET even when they compile without errors. Pay attention to the following areas:

- **Windows-specific APIs**: Any usage of `System.Drawing`, `System.Windows.Forms`, or COM interop may compile but fail at runtime on non-Windows platforms.
- **Registry access**: `Microsoft.Win32.Registry` is Windows-only.
- **Configuration**: Ensure `System.Configuration.ConfigurationManager` has been replaced with `Microsoft.Extensions.Configuration` where appropriate, or that the NuGet package `System.Configuration.ConfigurationManager` is explicitly referenced if still needed.
- **Reflection and serialization**: Verify that any runtime reflection-based logic or binary serialization (`BinaryFormatter`) still functions as expected, noting that `BinaryFormatter` is disabled by default in modern .NET.

### 5. Run the Web Application Locally

Start the `Northwind.Web` project and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Navigate through the primary routes and pages.
- Verify database connectivity and that queries return expected results.
- Check that authentication and authorization flows work correctly if applicable.
- Review browser console and server logs for any runtime exceptions.

### 6. Review Target Framework

Confirm that the target framework in `Northwind.Web.csproj` and all dependent projects is set to the intended version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If any project still targets `net48` or another legacy moniker, update it accordingly and re-run the build.

### 7. Review Deprecated or Obsolete API Warnings

Even without errors, the build may have produced warnings. Run the build with warnings treated as informational and review them:

```bash
dotnet build --configuration Release /p:TreatWarningsAsErrors=false > build_output.txt
```

Address any `[Obsolete]` API usages or platform compatibility warnings (`CA1416`) to ensure long-term maintainability.

### 8. Verify `web.config` vs `appsettings.json`

Ensure that configuration previously held in `web.config` has been properly migrated to `appsettings.json` or `appsettings.{Environment}.json`. Confirm that connection strings, app settings, and environment-specific values are all accounted for.

### 9. Publish the Application

Once local validation is complete, produce a published output to verify the deployment artifact builds correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected assemblies, static assets, and configuration files are present before deploying to the target environment.