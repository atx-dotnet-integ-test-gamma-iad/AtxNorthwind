# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Verify that no warnings or errors are reported during the restore process.

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs, missing references, or compatibility concerns that did not surface as hard errors.

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --verbosity normal
```

Review test results carefully, paying attention to any tests that were previously passing in the legacy project.

### 4. Run the Application Locally
Start the web application locally to verify it runs as expected on the cross-platform runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate to the application in a browser and exercise the core functionality, particularly any areas that relied on Windows-specific APIs or libraries in the legacy project (e.g., authentication, file I/O, database access).

### 5. Review Target Framework
Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version of .NET:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is targeting an older version such as `net6.0` or `net7.0`, consider upgrading to `net8.0` or the latest LTS release, as those versions have reached or are approaching end-of-life.

### 6. Check for Removed or Changed APIs
Even without build errors, some APIs behave differently on cross-platform .NET compared to .NET Framework. Review the following areas manually:

- **Database connectivity**: Confirm connection strings and providers (e.g., `System.Data.SqlClient` vs `Microsoft.Data.SqlClient`) are correct for the target environment.
- **Configuration**: Verify that `Web.config` transformations have been replaced with `appsettings.json` and that all configuration values are present.
- **Authentication/Authorization**: If the legacy project used Windows Authentication or `System.Web` membership providers, confirm these have been replaced with ASP.NET Core equivalents.
- **Static files and routing**: Confirm middleware ordering in `Program.cs` or `Startup.cs` is correct.

### 7. Review Compiler Warnings
Run the build with warnings treated carefully:

```bash
dotnet build --configuration Release /warnaserror
```

Address any warnings that surface, as they may indicate future breaking changes or unintended behavior.

### 8. Verify `Northwind.Web` Publish Output
Publish the application to a local folder and inspect the output to confirm all required assets are included:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish-output
```

Check that static assets, configuration files, and runtime dependencies are present in the output directory.