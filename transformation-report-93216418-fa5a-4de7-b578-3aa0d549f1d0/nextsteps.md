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

Review the output for any warnings that may indicate deprecated APIs, missing references, or compatibility concerns that did not surface as hard errors.

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may indicate behavioral differences introduced by the migration to cross-platform .NET.

### 4. Run the Application Locally
Start the web application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following at runtime:
- The application starts without exceptions.
- All routes and endpoints respond correctly.
- Database connectivity functions as expected (connection strings may need to be updated for the new environment).
- Any authentication or authorization flows work correctly.

### 5. Review Configuration Files
Cross-platform .NET projects rely on `appsettings.json` rather than `Web.config` or `App.config`. Confirm that:
- All necessary configuration values have been migrated to `appsettings.json` or `appsettings.{Environment}.json`.
- Environment-specific settings are correctly separated.
- Secrets are not stored in source-controlled configuration files; use `dotnet user-secrets` for local development.

### 6. Check Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` is set to a currently supported version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Refer to the [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy) to ensure you are targeting a version that is not end-of-life.

### 7. Review Removed or Changed APIs
Even without build errors, some APIs behave differently on cross-platform .NET compared to .NET Framework. Pay particular attention to:
- `System.Drawing` usage (not fully supported cross-platform without additional packages such as `System.Drawing.Common`).
- Windows-specific registry or file path assumptions.
- Any use of `HttpContext` or `HttpRuntime` that may have changed in ASP.NET Core.

### 8. Deployment
Once local validation is complete, publish the application using:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory and deploy them to your target environment according to your hosting setup (IIS, Kestrel, Linux service, etc.).