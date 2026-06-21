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

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing logic behaves as expected after the transformation:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Check for Runtime Compatibility Issues

Some issues do not surface at build time but appear at runtime. Pay attention to the following areas:

- **Windows-specific APIs**: Any usage of APIs such as the registry, `System.Drawing`, or certain `System.Windows.Forms` components may require additional NuGet packages (e.g., `System.Drawing.Common`) or may not be supported cross-platform.
- **Configuration system**: Ensure that `web.config` or `app.config` based configuration has been properly migrated to `appsettings.json` and the `Microsoft.Extensions.Configuration` system.
- **Connection strings**: Verify that database connection strings are correctly defined and accessible in the new configuration structure.

### 5. Run the Web Application Locally

Start the application using the .NET CLI and navigate through its core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

- Test all major routes and endpoints.
- Check browser developer tools and application logs for any runtime exceptions.
- Verify that static files, middleware, and authentication (if applicable) are functioning correctly.

### 6. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Refer to the [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core) to ensure you are targeting a version that is not end-of-life.

### 7. Review Nullable Reference Type Warnings

Cross-platform .NET projects often enable nullable reference types by default. Run a build with warnings treated as informational to identify any nullable-related warnings that could lead to runtime null reference exceptions:

```bash
dotnet build --configuration Release /p:TreatWarningsAsErrors=false
```

Address any `CS8600`, `CS8602`, or `CS8603` warnings as appropriate.

### 8. Publish the Application

Once local validation is complete, produce a published output to verify the deployment artifact is generated correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected assemblies, static files, and configuration files are present before deploying to your target environment.