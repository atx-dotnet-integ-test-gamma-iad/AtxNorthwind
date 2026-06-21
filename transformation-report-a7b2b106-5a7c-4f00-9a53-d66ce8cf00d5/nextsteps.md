# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

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

If the solution contains test projects, execute them to verify that existing functionality behaves as expected after the migration:

```bash
dotnet test
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the `Northwind.Web` project locally to confirm it runs as expected on the new .NET runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Verify that:
- The application starts without runtime exceptions.
- All routes and endpoints respond correctly.
- Database connections (if applicable) are established successfully.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all dependent projects in the solution target a compatible framework version.

### 6. Check for Deprecated or Removed APIs

Even without build errors, some APIs available in .NET Framework may behave differently or produce runtime errors in cross-platform .NET. Review the following areas manually:

- **Configuration**: Ensure `System.Configuration.ConfigurationManager` usage has been replaced with `Microsoft.Extensions.Configuration` where applicable.
- **HTTP pipeline**: Confirm that any middleware previously using `System.Web` has been fully replaced with ASP.NET Core equivalents.
- **Database access**: Verify connection strings and data access libraries (e.g., Entity Framework Core) are configured correctly for the target environment.

### 7. Test on Target Operating Systems

If cross-platform support is a goal, run and test the application on each intended operating system (e.g., Linux, macOS) to surface any platform-specific runtime issues such as:

- File path casing sensitivity.
- Platform-specific API calls that are not supported outside of Windows.

### 8. Review Application Logs

After running the application, review the output logs for any runtime warnings or errors that do not surface at build time, particularly around:

- Middleware ordering.
- Authentication and authorization configuration.
- Static file serving.