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
Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

### 3. Run the Application Locally
Start the web application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate to the URL printed in the console output (typically `http://localhost:5000` or `https://localhost:5001`) and confirm the application loads correctly.

### 4. Review Target Framework
Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it still references `net48` or another legacy framework moniker, update it accordingly and rebuild.

### 5. Run Existing Tests
If the solution contains test projects, execute them to verify that existing behavior has been preserved:

```bash
dotnet test
```

Review any failing tests carefully, as failures may indicate behavioral differences between the legacy framework and the new target framework rather than build issues.

### 6. Check Runtime Behavior
- Verify database connectivity if the project uses Entity Framework or ADO.NET, as connection string formats or provider packages may need adjustment for cross-platform .NET.
- Exercise all major application routes or endpoints and confirm responses are correct.
- Review application logs for any runtime warnings or exceptions that did not surface at build time.

### 7. Review Removed or Changed APIs
Cross-reference any usages of APIs that were available in .NET Framework but have changed or been removed in modern .NET. The [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) and the [.NET API compatibility documentation](https://learn.microsoft.com/en-us/dotnet/core/compatibility/) are useful references for this step.

### 8. Publish the Application
Once validation is complete, publish the application to confirm the output is as expected:

```bash
dotnet publish --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all required files, static assets, and configuration files are present before deploying to the target environment.