# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Build Status

The solution has no build errors following the transformation. All projects compiled successfully.

## Validation Steps

### 1. Review Project Structure
- Confirm that all projects in the solution are targeting the correct .NET version (e.g., `net8.0` or `net9.0`) by inspecting each `.csproj` file and verifying the `<TargetFramework>` element.
- Confirm that any previously Windows-specific NuGet packages have been replaced with their cross-platform equivalents or removed if no longer necessary.

### 2. Restore and Build Locally
Run the following commands from the root of the solution to confirm a clean restore and build:

```bash
dotnet restore
dotnet build
```

Ensure there are no warnings that could indicate deprecated APIs or packages that may cause runtime issues.

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test
```

Review the test output for any failures or skipped tests that may need attention.

### 4. Run the Web Application Locally
Start the `Northwind.Web` project and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

- Open the application in a browser and navigate through the key pages.
- Check the console output for any runtime exceptions or warnings.
- Verify that database connections, if applicable, are functioning correctly with the updated configuration.

### 5. Review `Program.cs` and `Startup` Configuration
- If the project was migrated from .NET Framework, confirm that the old `Startup.cs` pattern has been correctly converted to the minimal hosting model used in modern .NET, or that the `Startup.cs` class is still correctly wired up if retained.
- Verify middleware ordering (authentication, authorization, routing, etc.) is correct.

### 6. Check `appsettings.json` and Configuration
- Confirm that connection strings and other configuration values from the legacy `web.config` or `app.config` have been correctly transferred to `appsettings.json`.
- Verify environment-specific configuration files (e.g., `appsettings.Development.json`) are present and correct.

### 7. Verify Static Files and Bundling
- Confirm that static assets (CSS, JavaScript, images) are being served correctly.
- If the legacy project used `System.Web.Optimization` for bundling and minification, verify that a replacement mechanism (e.g., LibMan, npm-based tooling, or built-in middleware) is in place and functioning.

### 8. Cross-Platform Verification
- If possible, run the application on a non-Windows operating system (Linux or macOS) to confirm there are no remaining platform-specific dependencies causing runtime errors.
- Pay particular attention to file path separators, case-sensitive file references, and any use of Windows-specific APIs.

### 9. Review NuGet Package Versions
- Run the following command to check for outdated packages:

```bash
dotnet list package --outdated
```

Update any packages that have newer stable versions available, particularly those that were part of the migration.

### 10. Deployment
Once all local validation steps pass:
- Publish the application using the appropriate runtime identifier for your target environment:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj -c Release -r linux-x64 --self-contained false
```

- Deploy the contents of the `publish` output folder to your target server or hosting environment.
- Verify the deployed application starts correctly and review the application logs for any issues.