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
Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Release
```

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify that existing logic behaves as expected after the migration:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any test failures carefully, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Check for Runtime Compatibility Issues
Build errors are not the only indicator of migration problems. Review the following areas manually:

- **APIs marked as obsolete or unsupported**: Some APIs available in .NET Framework are present in modern .NET but throw `PlatformNotSupportedException` at runtime. Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.PlatformAbstractions` tooling to identify these.
- **`app.config` / `web.config`**: Configuration in modern ASP.NET Core is handled via `appsettings.json` and the `IConfiguration` system. Verify that any configuration values previously read from `web.config` have been migrated correctly.
- **HTTP pipeline and middleware**: If `Northwind.Web` was previously an ASP.NET MVC or Web Forms project, confirm that routing, authentication, authorization, and middleware are correctly configured using the ASP.NET Core equivalents.

### 5. Run the Application Locally
Start the application and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the key areas of the application and confirm that pages load, data is retrieved correctly, and no unhandled exceptions occur.

### 6. Review Target Framework
Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If a newer LTS version of .NET is available and desired, update this value and re-run the build and test steps above.

### 7. Review Nullable Reference Types
Modern .NET projects often enable nullable reference type analysis. If the project has `<Nullable>enable</Nullable>` in the `.csproj`, review any resulting warnings, as they can surface potential null-reference issues that existed in the original code.

### 8. Verify Database Connectivity
If the application uses Entity Framework or direct ADO.NET connections, confirm that:

- The connection strings in `appsettings.json` are correct for the target environment.
- The correct EF Core provider package is referenced (e.g., `Microsoft.EntityFrameworkCore.SqlServer`).
- Any pending migrations are applied:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

## Deployment

### 1. Publish the Application
Use the `dotnet publish` command to produce deployment artifacts:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

### 2. Verify the Published Output
Inspect the `./publish` directory to confirm all expected files are present, including static assets, configuration files, and the compiled assemblies.

### 3. Configure the Target Environment
Ensure the target server or hosting environment has the correct .NET runtime installed. You can verify the required runtime version from the `<TargetFramework>` value in the `.csproj` file and download the appropriate runtime from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).

### 4. Deploy the Published Output
Copy the contents of the `./publish` directory to the target hosting environment and configure the web server (IIS, Nginx, or Apache) to serve the application using the appropriate ASP.NET Core hosting module or reverse proxy setup, as documented at [https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy).