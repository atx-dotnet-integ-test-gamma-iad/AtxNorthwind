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
Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while non-blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Run the Application Locally
Start the `Northwind.Web` project and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following manually:
- Application startup completes without exceptions.
- All routes and endpoints respond correctly.
- Database connectivity is functional (connection strings may need to be updated for the new environment).
- Any static assets, views, or Razor pages render as expected.

### 5. Review Configuration Files
Cross-platform .NET uses `appsettings.json` rather than `Web.config` or `App.config` for most configuration. Confirm that:
- All connection strings have been migrated correctly.
- Environment-specific settings (e.g., `appsettings.Development.json`) are in place.
- Any configuration previously handled by `Web.config` transforms has been replicated.

### 6. Check for Runtime-Only Issues
Some issues do not surface at build time but appear at runtime. Pay particular attention to:
- Reflection-based code that may behave differently under .NET's stricter type loading.
- Any use of `System.Web` APIs that may have been stubbed or replaced during transformation.
- Third-party libraries that may have been updated to newer major versions with breaking changes.

### 7. Validate Target Framework
Confirm that all projects are targeting the intended .NET version by inspecting each `.csproj` file for the `<TargetFramework>` element, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure consistency across all projects in the solution to avoid compatibility issues between assemblies.