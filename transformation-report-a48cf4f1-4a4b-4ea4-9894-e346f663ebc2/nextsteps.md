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

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Check for Runtime Compatibility Issues
Some APIs behave differently or are unsupported on cross-platform .NET even when they compile without errors. Pay attention to the following areas:

- **Windows-specific APIs**: Any usage of `System.Web`, Windows Registry, COM interop, or WCF server-side components may compile but fail at runtime on non-Windows platforms.
- **Configuration**: Ensure `web.config` settings have been migrated to `appsettings.json` or equivalent and that `IConfiguration` is wired up correctly.
- **Authentication and Authorization**: Verify that any previously used `FormsAuthentication` or Windows Authentication has been replaced with the appropriate ASP.NET Core middleware.
- **Entity Framework**: If migrating from EF6 to EF Core, validate that all queries, relationships, and migrations behave as expected.

### 5. Run the Application Locally
Start the application and manually exercise its primary workflows:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the application and verify that routing, data access, and rendering all function correctly.

### 6. Review Target Framework
Confirm that the target framework in `Northwind.Web.csproj` is set to a currently supported version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is targeting `net6.0` or `net7.0`, consider upgrading to `net8.0` as those versions are out of long-term support or approaching end of life.

### 7. Static Assets and wwwroot
Verify that all static assets (CSS, JavaScript, images) are present under the `wwwroot` folder and are being served correctly by the Kestrel or IIS Express host.

### 8. Database Migrations and Connection Strings
- Confirm connection strings in `appsettings.json` point to the correct database.
- If using EF Core, run any pending migrations:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 9. Deployment
Once local validation is complete, publish the application to your target environment:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` folder and deploy to your target host (IIS, Azure App Service, or a Linux server with the .NET runtime installed).