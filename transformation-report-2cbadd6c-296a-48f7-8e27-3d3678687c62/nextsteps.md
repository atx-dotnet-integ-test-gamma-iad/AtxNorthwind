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

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not prevent a successful build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality behaves as expected after the transformation:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Check Runtime Behavior

Run the application locally and exercise its core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Pay attention to the following areas that commonly surface runtime issues after migration:

- **Database connectivity**: Verify connection strings are correct and the appropriate database driver (e.g., `Microsoft.Data.SqlClient`) is being used.
- **Configuration**: Confirm that `appsettings.json` or environment variables have replaced any legacy `Web.config` or `App.config` values correctly.
- **Authentication and Authorization**: If the application uses Windows Authentication, Forms Authentication, or similar mechanisms, verify they function correctly under ASP.NET Core.
- **Static files and routing**: Confirm that all routes resolve correctly and static assets are served as expected.

### 5. Review Removed or Changed APIs

Cross-reference the migrated code against the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/) or the [.NET API compatibility documentation](https://learn.microsoft.com/en-us/dotnet/core/compatibility/) to identify any APIs that were available in .NET Framework but behave differently or are absent in cross-platform .NET.

Common areas to check:

- `System.Web` usages that may have been replaced with ASP.NET Core equivalents
- `HttpContext` access patterns
- Session and caching mechanisms
- Any use of `AppDomain`, `Remoting`, or `BinaryFormatter`

### 6. Validate Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` is set to the intended version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure this aligns with your organization's supported .NET version.

### 7. Deployment

Once local validation is complete, publish the application using:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory and confirm all required assets, configuration files, and binaries are present before deploying to the target environment.