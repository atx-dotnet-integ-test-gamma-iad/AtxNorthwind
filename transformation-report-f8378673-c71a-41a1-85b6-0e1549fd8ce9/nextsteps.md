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
Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Release
```

Check the output for any warnings that, while non-blocking, may indicate compatibility concerns worth addressing.

### 3. Run the Application Locally
Start the application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate to the application in a browser and exercise the primary workflows to confirm runtime behavior matches the legacy project.

### 4. Run Existing Tests
If the solution contains test projects, execute them to verify functional correctness:

```bash
dotnet test
```

Review any failing tests and determine whether they represent regressions introduced during the transformation or pre-existing issues.

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended modern .NET version (e.g., `net8.0`). Do the same for all other projects in the solution.

### 6. Review Removed or Changed APIs
Cross-platform .NET removes or changes certain APIs that existed in .NET Framework. Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.PlatformAbstractions` compatibility tooling to surface any runtime-level API usage that may not have been caught at compile time.

### 7. Check Configuration and Middleware
If this is an ASP.NET Core project, verify the following:

- `Program.cs` and/or `Startup.cs` have been correctly migrated to the ASP.NET Core hosting model.
- Any `web.config` settings that were relied upon have been moved to `appsettings.json` or the appropriate ASP.NET Core configuration providers.
- Middleware registration order in the request pipeline is correct.

### 8. Verify Data Access
If the project uses Entity Framework, confirm the version in use is Entity Framework Core and that:

- Migrations are present and up to date.
- The connection string is correctly configured for the target environment.
- A test database operation (e.g., a simple query) succeeds at runtime.

## Deployment

### 1. Publish the Application
Generate a publish output using:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

### 2. Verify the Publish Output
Inspect the `./publish` directory to confirm all expected files are present, including static assets, configuration files, and runtime dependencies.

### 3. Test the Published Output
Run the published output directly to confirm it behaves identically to the development build:

```bash
dotnet ./publish/Northwind.Web.dll
```

### 4. Deploy to Target Environment
Copy the publish output to the target server or hosting environment. Ensure the correct .NET runtime version is installed on the target machine. You can verify the runtime requirement by checking the `<TargetFramework>` in the project file and confirming a matching runtime is available via:

```bash
dotnet --list-runtimes
```