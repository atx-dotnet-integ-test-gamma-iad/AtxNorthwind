# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

### 2. Build the Solution
Confirm the solution builds cleanly in Release configuration:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns that did not surface as hard errors.

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release
```

Review test results carefully. A successful build does not guarantee correct runtime behavior, especially after a cross-platform migration.

### 4. Check for Platform-Specific Code
Even without build errors, review the codebase for any remaining platform-specific assumptions:

- Usage of `System.Web` types that may have been shimmed or wrapped
- Windows registry access (`Microsoft.Win32.Registry`)
- Windows-specific file path separators (use `Path.Combine` and `Path.DirectorySeparatorChar`)
- `HttpContext.Current` usage, which behaves differently in ASP.NET Core

### 5. Run the Application Locally
Start the web application and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the application and confirm that routing, data access, and any authentication or session handling work as expected.

### 6. Review `appsettings.json` and Configuration
Confirm that connection strings and any environment-specific configuration previously held in `Web.config` have been correctly migrated to `appsettings.json` or environment variables. Verify that the configuration is being read correctly at runtime.

### 7. Verify Database Connectivity
If the project uses Entity Framework or direct ADO.NET, confirm the database connection is functional and that any migrations are up to date:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

This step applies only if Entity Framework Core is in use.

### 8. Review Logging and Error Handling
Confirm that the logging infrastructure (e.g., `ILogger`, Serilog, NLog) is configured correctly in `Program.cs` or `Startup.cs` and that errors are surfaced appropriately in both development and production environments.