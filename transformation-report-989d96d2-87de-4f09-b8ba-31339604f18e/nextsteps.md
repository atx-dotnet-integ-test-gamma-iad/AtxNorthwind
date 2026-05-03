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

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Check the output for any warnings that, while non-blocking, may indicate areas of concern such as nullable reference type warnings or obsolete API usage.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Verify Runtime Behavior

Run the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Pay attention to the following areas that commonly differ after migration:

- **Authentication and Authorization**: Middleware configuration and cookie behavior may differ.
- **Configuration**: Ensure `appsettings.json` correctly replaces any legacy `Web.config` or `App.config` values.
- **Database Connectivity**: Verify connection strings are correct and that the target database is reachable.
- **Static Files**: Confirm that static assets are served correctly under the new middleware pipeline.
- **HTTP Redirects and Routing**: Validate that routes resolve as expected and that any attribute or convention-based routing is functioning correctly.

### 5. Review Removed or Changed APIs

Cross-reference your code against the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/) or the [.NET API compatibility documentation](https://learn.microsoft.com/en-us/dotnet/core/compatibility/) to identify any APIs that were removed or changed in behavior between .NET Framework and modern .NET. Even without build errors, some APIs have different runtime semantics.

### 6. Check Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a compatible framework version.

### 7. Review Remaining `Web.config` or `App.config` Files

If any `Web.config` or `App.config` files remain in the project, determine whether their settings have been fully migrated to `appsettings.json` or the `Program.cs` / `Startup.cs` configuration pipeline. Leftover configuration files are not processed by the modern .NET runtime.

### 8. Publish the Application

Once local validation is complete, produce a published output to confirm the deployment artifact is generated correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all expected files, including views, static assets, and configuration files, are present before deploying to the target environment.