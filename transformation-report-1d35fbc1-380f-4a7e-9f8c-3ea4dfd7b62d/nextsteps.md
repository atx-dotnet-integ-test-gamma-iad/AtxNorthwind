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

Review the output for any warnings that, while non-breaking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify runtime behavior matches expectations:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may surface runtime issues that were not caught at compile time.

### 4. Run the Application Locally
Start the `Northwind.Web` project locally and verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Navigate through the application and exercise the primary workflows.
- Check browser developer tools and application logs for any runtime exceptions or unexpected behavior.
- Verify that database connections, authentication, and any external service integrations function correctly.

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended modern .NET version (e.g., `net8.0`). Ensure all dependent projects target a compatible framework version.

### 6. Check for Removed or Changed APIs
Even without build errors, some APIs behave differently in modern .NET compared to .NET Framework. Pay particular attention to:

- **System.Web** usages that may have been shimmed or replaced — confirm replacements behave correctly at runtime.
- **Configuration** — verify `appsettings.json` and environment-specific configuration files are loading correctly, replacing any legacy `Web.config` or `App.config` reliance.
- **Authentication/Authorization middleware** — confirm middleware ordering in `Program.cs` or `Startup.cs` is correct.
- **Entity Framework** — if migrated from EF 6 to EF Core, run queries against the actual database and validate results.

### 7. Validate Static Assets and Middleware
For the web project specifically:

- Confirm static files (CSS, JS, images) are being served correctly.
- Verify routing behaves as expected, particularly if the project moved from Web Forms or classic MVC routing to modern ASP.NET Core routing.

### 8. Review Application Logs
Run the application and inspect logs for warnings or errors at the `Warning` level or above:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj -- --Logging:LogLevel:Default=Warning
```

Address any logged issues before proceeding to deployment.

## Deployment

### 1. Publish the Application
Once validation is complete, publish the application to a self-contained or framework-dependent deployment:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

### 2. Verify the Published Output
Inspect the `./publish` directory to confirm all expected files are present, including configuration files and static assets.

### 3. Deploy to Target Environment
Copy the published output to the target server or hosting environment and configure the web server (e.g., IIS, Nginx, or Kestrel as a standalone host) according to the [ASP.NET Core hosting documentation](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/).

- If deploying to **IIS**, ensure the ASP.NET Core Hosting Bundle is installed on the server.
- Confirm environment variables (e.g., `ASPNETCORE_ENVIRONMENT`) are set correctly on the target machine.
- Validate connection strings and secrets are configured appropriately for the production environment and are not sourced from development-only files such as `appsettings.Development.json`.