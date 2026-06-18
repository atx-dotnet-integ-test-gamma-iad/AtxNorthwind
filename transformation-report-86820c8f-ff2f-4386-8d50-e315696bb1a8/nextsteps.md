# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. The following steps outline how to validate, test, and deploy the migrated project.

## 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

## 2. Build the Solution

Perform a full build to confirm the absence of errors in a clean environment:

```bash
dotnet build --configuration Release
```

Address any warnings that surface during the build, particularly those related to nullable reference types or obsolete APIs, as these can indicate areas that may cause runtime issues.

## 3. Run Unit and Integration Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --verbosity normal
```

Review the test output carefully. Any failing tests should be investigated to determine whether they are caused by behavioral differences between the legacy .NET Framework runtime and the current .NET runtime.

## 4. Verify Runtime Behavior

Run the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Pay particular attention to the following areas, which are common sources of runtime differences after migration:

- **Authentication and Authorization**: Middleware configuration and cookie handling may differ.
- **Entity Framework**: If using EF Core, query behavior and migration compatibility should be confirmed.
- **Configuration**: Ensure `appsettings.json` contains all values previously held in `Web.config` or `App.config`, including connection strings.
- **Static Files and Routing**: Verify that routes resolve correctly and static assets are served as expected.

## 5. Review Removed or Changed APIs

Cross-reference the codebase against the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/) or the [.NET API compatibility documentation](https://learn.microsoft.com/en-us/dotnet/core/compatibility/) to identify any APIs that behave differently in modern .NET. Common areas include:

- `System.Web` dependencies that were replaced with ASP.NET Core equivalents.
- `BinaryFormatter` usage, which is disabled by default in modern .NET.
- `Thread.Abort` and `AppDomain` APIs that are no longer supported.

## 6. Check Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` is set to a currently supported version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Refer to the [.NET support lifecycle](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core) to confirm the targeted version is within its support window.

## 7. Publish the Application

Once validation is complete, publish the application to a local folder to confirm the output is correct before deploying to a target environment:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all required files, configuration, and assets are present.