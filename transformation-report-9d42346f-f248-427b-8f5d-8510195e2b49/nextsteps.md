# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

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

### 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version of .NET (e.g., `net8.0`). Avoid `netcoreapp*` or `net5.0` / `net6.0` if long-term support is a requirement.

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 4. Run Unit Tests

If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger trx
```

Review the `.trx` output files for any failed or skipped tests.

### 5. Run the Application Locally

Start the web application locally and verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Navigate to the application in a browser.
- Exercise the primary routes and features.
- Check the console output for any runtime exceptions or warnings.

### 6. Check for Removed or Changed APIs

Even without build errors, some APIs behave differently in cross-platform .NET compared to .NET Framework. Pay particular attention to:

- **`System.Web` dependencies**: These are not available in .NET Core/5+. Confirm no runtime references remain.
- **Windows-specific APIs**: Any calls to the Windows Registry, `System.Drawing` (GDI+), or COM interop may fail on non-Windows platforms at runtime.
- **Configuration**: Ensure `Web.config` transforms have been replaced with `appsettings.json` and the appropriate `IConfiguration` setup.
- **Authentication/Authorization middleware**: Confirm middleware ordering in `Program.cs` or `Startup.cs` is correct.

### 7. Database Connectivity

If the application uses a database (as expected for a Northwind-based project):

- Verify the connection string in `appsettings.json` is correct for the target environment.
- Run the application and confirm database queries execute without error.
- If Entity Framework is in use, check for any pending migrations:

```bash
dotnet ef migrations list --project src/Northwind.Web/Northwind.Web.csproj
```

### 8. Static Files and Bundling

Confirm that static assets (CSS, JavaScript, images) are served correctly. If the project previously used `System.Web.Optimization` (Bundling & Minification), verify it has been replaced with a supported alternative such as `WebOptimizer` or a front-end build tool.

### 9. Publish the Application

Once local validation is complete, produce a published output to verify the release artifact:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the `./publish` directory to confirm all expected files are present, including `appsettings.json` and any required static content.

### 10. Smoke Test the Published Output

Run the published output directly to confirm it behaves identically to the development run:

```bash
dotnet ./publish/Northwind.Web.dll
```

Navigate to the application and repeat the core functionality checks performed in step 5.