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
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

### 5. Check for Removed or Changed APIs

Even without build errors, some APIs behave differently on cross-platform .NET compared to .NET Framework. Pay particular attention to:

- **`System.Web` dependencies**: These are not available on cross-platform .NET. Confirm no runtime references remain.
- **Windows-specific APIs**: Any use of the Windows Registry, `System.Drawing`, or COM interop may fail at runtime on non-Windows platforms.
- **Configuration system**: Ensure `web.config` transforms have been replaced with `appsettings.json` and the `Microsoft.Extensions.Configuration` pattern.
- **Authentication/Authorization middleware**: Confirm the middleware pipeline in `Program.cs` or `Startup.cs` is correctly ordered.

### 6. Run the Application Locally

Start the application using the .NET CLI and navigate through its core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Test all major routes and features, particularly any that relied on legacy ASP.NET HTTP modules or handlers, as these do not have a direct equivalent in ASP.NET Core middleware.

### 7. Verify Database Connectivity

If the project uses Entity Framework or direct ADO.NET connections, confirm the connection strings in `appsettings.json` are correct and that the database provider package (e.g., `Microsoft.EntityFrameworkCore.SqlServer`) is the appropriate version for the target framework.

### 8. Publish a Release Build

Once local validation is complete, produce a published output to verify the publish process works correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected assets, static files, and configuration files are present.