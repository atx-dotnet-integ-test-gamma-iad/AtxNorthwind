# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the root of the solution to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear related to missing or incompatible packages.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality is intact after the transformation:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Run the Application Locally

Start the `Northwind.Web` project locally to confirm it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following:
- The application starts without runtime exceptions.
- All routes and endpoints respond correctly.
- Database connections (if applicable) are functioning. Verify connection strings in `appsettings.json` are correct for your environment.
- Any authentication or authorization middleware behaves as expected.

### 5. Review Replaced or Removed APIs

Cross-platform .NET does not support certain APIs that were available in .NET Framework. Manually review the codebase for usage of the following common problem areas:

- `System.Web` namespace references — these are not available in modern .NET.
- `HttpContext` usage outside of a request pipeline context.
- Windows-specific APIs such as the registry, WCF server-side hosting, or `System.Drawing` (use `System.Drawing.Common` or an alternative like `SkiaSharp`).
- `BinaryFormatter` — this is disabled by default in modern .NET due to security concerns.

### 6. Verify Configuration and Environment Settings

- Confirm that `appsettings.json` and `appsettings.{Environment}.json` contain all settings previously held in `Web.config` or `App.config`.
- Ensure environment variables and secrets are configured appropriately for each target environment.
- Validate that logging configuration (e.g., Serilog, NLog, or the built-in `Microsoft.Extensions.Logging`) is set up correctly.

### 7. Check Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` is set to the intended version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Adjust this value if a different supported LTS version is required.

### 8. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to ensure all expected files, static assets, and dependencies are present.