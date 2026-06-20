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

Address any warnings that surface during the build, particularly those related to nullable reference types or obsolete APIs, as these may indicate areas where the migrated code could behave differently from the original.

## 3. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all dependent projects in the solution target a compatible framework version.

## 4. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --logger trx
```

Review the `.trx` output files for any failing tests and investigate failures that may be caused by behavioral differences between the legacy framework and the new cross-platform .NET runtime.

## 5. Verify Runtime Behavior

Run the application locally and exercise the primary workflows:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Pay particular attention to:

- **Database connectivity**: Connection strings and database drivers may need to be updated. Confirm that the appropriate cross-platform ADO.NET provider or Entity Framework Core provider is in use.
- **Authentication and authorization**: Any legacy `System.Web`-based authentication mechanisms must be replaced with ASP.NET Core middleware equivalents.
- **Configuration**: Verify that `web.config` settings have been correctly migrated to `appsettings.json` or environment variables.
- **Static files and routing**: Confirm that static file serving and route definitions behave as expected under the ASP.NET Core pipeline.

## 6. Check for Removed or Changed APIs

Review the code for any usage of APIs that exist in .NET but behave differently from .NET Framework. The [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the [Platform Compatibility Analyzer](https://learn.microsoft.com/en-us/dotnet/standard/analyzers/platform-compat-analyzer) can assist with identifying these areas.

## 7. Test on Target Platforms

Since the goal is cross-platform support, run the application on each intended operating system (Windows, Linux, macOS) to surface any platform-specific issues such as:

- File path casing sensitivity on Linux
- Windows-only APIs that may have been inadvertently retained
- Differences in culture and encoding defaults

## 8. Publish the Application

Once validation is complete, publish the application using the appropriate runtime identifier for your target environment:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --runtime linux-x64 --self-contained false --output ./publish
```

Adjust `--runtime` and `--self-contained` according to your deployment requirements. Review the contents of the `./publish` directory to confirm all expected files are present before deploying to the target environment.