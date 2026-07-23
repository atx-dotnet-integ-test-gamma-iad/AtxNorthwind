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

Verify that no warnings or errors appear related to missing or incompatible packages.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while non-blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit Tests

If the solution contains test projects, execute them to validate that existing functionality behaves as expected after the transformation:

```bash
dotnet test --configuration Release
```

Review test results and investigate any failures, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Check Runtime Behavior

Start the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following areas specifically, as they are common sources of runtime issues after migration even when the build succeeds:

- **Database connectivity**: Confirm connection strings in `appsettings.json` are correct and that the target database is reachable.
- **Authentication and authorization**: Verify that any authentication middleware is configured correctly for ASP.NET Core.
- **Static files**: Confirm that static assets (CSS, JS, images) are being served correctly.
- **Configuration**: Ensure that any values previously stored in `Web.config` or `App.config` have been migrated to `appsettings.json` or environment variables.
- **HTTP endpoints**: Walk through the primary routes and pages of the application to confirm they respond correctly.

### 5. Review Removed or Changed APIs

Cross-platform .NET does not support certain APIs that existed in .NET Framework. Even if the build succeeds, review the following areas for potential runtime exceptions:

- **`System.Web` dependencies**: Any remaining indirect reliance on `System.Web` types will fail at runtime.
- **WCF client usage**: If the application consumes WCF services, confirm the `System.ServiceModel` client libraries are in place.
- **Registry or Windows-specific APIs**: These will throw `PlatformNotSupportedException` on non-Windows environments.
- **`BinaryFormatter`**: This type is disabled by default in modern .NET. Replace any serialization relying on it with a supported alternative such as `System.Text.Json` or `System.Xml.Serialization`.

### 6. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If a newer Long-Term Support (LTS) version of .NET is available and desired, update this value and re-run the restore and build steps.

### 7. Deployment

Once local validation is complete, publish the application using the following command:

```bash
dotnet publish --configuration Release --output ./publish
```

Confirm the contents of the `./publish` directory are complete, then deploy those artifacts to the target host environment. Ensure the target machine has the appropriate .NET runtime installed, or use the `--self-contained` flag with a specified `--runtime` identifier to bundle the runtime with the output:

```bash
dotnet publish --configuration Release --self-contained true --runtime win-x64 --output ./publish
```

Replace `win-x64` with the appropriate runtime identifier for your target environment (e.g., `linux-x64`, `osx-x64`).