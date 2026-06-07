# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The solution has no build errors following the transformation. All projects compiled successfully, including `Northwind.Web`.

## Validation Steps

### 1. Verify Target Framework

Open each `.csproj` file and confirm the target framework is set to a supported cross-platform .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 2. Restore NuGet Packages

Run the following command from the solution root to ensure all dependencies are restored cleanly:

```bash
dotnet restore
```

Verify there are no warnings about deprecated or missing packages in the output.

### 3. Build the Solution

Perform a full solution build to confirm there are no errors or warnings:

```bash
dotnet build --configuration Release
```

Review any warnings that appear, particularly those related to nullable reference types, obsolete APIs, or platform compatibility.

### 4. Run Unit Tests

If the solution contains test projects, execute them to confirm existing functionality is intact:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review test output for any failures that may indicate behavioral regressions introduced during the migration.

### 5. Run the Web Application Locally

Start the `Northwind.Web` project and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Navigate to the application URL shown in the console output.
- Verify that all pages load and core functionality behaves correctly.
- Check the console and browser developer tools for any runtime errors.

### 6. Review Middleware and Configuration

Inspect `Program.cs` (and `Startup.cs` if still present) to confirm:

- Middleware is registered in the correct order.
- Configuration sources (e.g., `appsettings.json`, environment variables) are loading properly.
- Any legacy `web.config` settings have been migrated to `appsettings.json` or equivalent.

### 7. Verify Database Connectivity

If the application uses a database:

- Confirm the connection string in `appsettings.json` is correct for the target environment.
- Run any pending Entity Framework Core migrations if applicable:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 8. Check Static Files and Assets

Confirm that static files (CSS, JavaScript, images) are being served correctly by verifying they are located under the `wwwroot` folder and that the static files middleware is enabled in `Program.cs`:

```csharp
app.UseStaticFiles();
```

### 9. Review Removed Windows-Specific Dependencies

Check that no references to Windows-specific APIs or packages remain. Use the .NET Compatibility Analyzer to assist:

```bash
dotnet add package Microsoft.DotNet.Analyzers.Compatibility
```

Review any diagnostics it surfaces and replace Windows-only APIs with cross-platform alternatives where needed.

### 10. Test on Target Platform

If the intended deployment platform is Linux or macOS, run the application on that operating system to surface any remaining platform-specific issues before deployment:

```bash
dotnet publish --configuration Release --runtime linux-x64 --self-contained false
```

Verify the published output runs correctly on the target machine.