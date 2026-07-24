# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

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

### 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version of .NET (e.g., `net8.0`). Avoid `netcoreapp*` or `net5.0` / `net6.0` if long-term support is a requirement.

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 4. Run Unit Tests

If the solution contains test projects, execute them to verify that runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

### 5. Run the Application Locally

Start the web application locally and verify it responds as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate to the URL printed in the console output (typically `https://localhost:5001` or `http://localhost:5000`) and verify the application loads and functions correctly.

### 6. Check for Removed or Changed APIs

Even without build errors, certain APIs behave differently in cross-platform .NET compared to .NET Framework. Manually review the following areas if they are used in the project:

- **`System.Web` dependencies**: These do not exist in cross-platform .NET. Confirm all usages have been replaced with ASP.NET Core equivalents.
- **`HttpContext` and session handling**: Verify middleware and session configuration in `Program.cs` or `Startup.cs`.
- **`Web.config`**: Configuration should now reside in `appsettings.json`. Confirm all relevant settings (connection strings, app settings) have been migrated.
- **Entity Framework**: If the project uses Entity Framework, confirm it has been updated to Entity Framework Core and that migrations are functional.
- **Windows-specific APIs**: Any use of the Windows registry, `System.Drawing`, or COM interop may fail at runtime on non-Windows platforms even if they compile successfully.

### 7. Validate Database Connectivity

If the application connects to a database, confirm the connection string in `appsettings.json` is correct for the target environment and that the database schema is compatible with any ORM being used.

### 8. Review Middleware Pipeline

In ASP.NET Core, the middleware pipeline is configured explicitly. Open `Program.cs` (or `Startup.cs`) and confirm the following are present and ordered correctly where applicable:

- `app.UseHttpsRedirection()`
- `app.UseStaticFiles()`
- `app.UseRouting()`
- `app.UseAuthentication()` / `app.UseAuthorization()`
- `app.MapControllers()` or `app.MapRazorPages()`

### 9. Publish the Application

Once local validation is complete, publish the application to confirm the output is complete and self-contained:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and verify all expected assets, views, and static files are present.