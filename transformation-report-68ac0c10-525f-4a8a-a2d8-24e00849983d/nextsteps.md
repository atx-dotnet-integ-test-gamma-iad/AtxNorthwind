# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. The following steps outline how to validate, test, and deploy the migrated project.

## 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages that may need to be updated.

## 2. Build the Solution

Perform a full build to confirm the absence of errors in a clean build environment:

```bash
dotnet build --configuration Release
```

Address any warnings that surface during the build, particularly those related to nullable reference types or obsolete APIs, as these may indicate areas of the code that could cause runtime issues.

## 3. Run Unit and Integration Tests

If the solution contains test projects, execute all tests to verify that existing functionality has not been broken during the migration:

```bash
dotnet test --configuration Release --verbosity normal
```

Review any failing tests carefully. Failures may indicate behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

## 4. Verify Runtime Behavior

Run the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Pay particular attention to the following areas that commonly surface issues after migration:

- **Database connectivity**: Confirm that connection strings are correctly configured and that the chosen data provider (e.g., Entity Framework Core) operates as expected.
- **Authentication and Authorization**: Verify that any authentication middleware is functioning correctly, as this area often differs between .NET Framework and modern .NET.
- **Static files and routing**: Confirm that static assets are served correctly and that all routes resolve as expected.
- **Configuration**: Ensure that `appsettings.json` and environment-specific configuration files are being read correctly, replacing any legacy `Web.config` or `App.config` dependencies.

## 5. Review Removed or Changed APIs

Cross-reference the codebase against the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/) or the [.NET API compatibility documentation](https://learn.microsoft.com/en-us/dotnet/core/compatibility/) to identify any APIs that were available in .NET Framework but behave differently in modern .NET. Common areas include:

- `System.Web` dependencies (should be fully replaced by ASP.NET Core equivalents)
- `HttpContext` usage
- WCF or Remoting dependencies
- Binary serialization via `BinaryFormatter`

## 6. Target Framework Verification

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm that the target framework is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all dependent projects in the solution target compatible framework versions.

## 7. Publish the Application

Once validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all required files, assets, and dependencies are present before deploying to the target environment.