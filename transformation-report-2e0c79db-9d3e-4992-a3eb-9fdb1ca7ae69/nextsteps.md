# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. Below are steps to validate, test, and deploy the migrated project.

## 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to deprecated or incompatible packages. If any packages targeting the old .NET Framework are still present, locate them in the `.csproj` files and replace them with their .NET-compatible equivalents via [NuGet](https://www.nuget.org/).

## 2. Build the Solution

Perform a full build to confirm there are no compilation issues:

```bash
dotnet build --configuration Release
```

Address any warnings that surface during the build, particularly those related to nullable reference types, obsolete APIs, or platform compatibility.

## 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review the test output carefully. Any failing tests should be investigated to determine whether they are caused by behavioral differences between .NET Framework and cross-platform .NET.

## 4. Verify Runtime Behavior

Run the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following areas in particular:
- Routing and middleware behavior, as there are differences between ASP.NET and ASP.NET Core pipelines.
- Authentication and authorization flows if applicable.
- Database connectivity and Entity Framework migrations if the project uses a data access layer.
- Static file serving and any embedded resources.

## 5. Check for Platform-Specific Code

Search the codebase for any APIs that were available in .NET Framework but may behave differently or be unavailable in cross-platform .NET. Common areas to inspect include:

- `System.Web` references (these are not available in .NET Core/.NET 5+).
- Windows Registry access (`Microsoft.Win32.Registry`).
- Windows Communication Foundation (WCF) client or server usage.
- `AppDomain` usage beyond what is supported in .NET Core.
- Any P/Invoke calls targeting Windows-specific native libraries.

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the [Platform Compatibility Analyzer](https://learn.microsoft.com/en-us/dotnet/standard/analyzers/platform-compat-analyzer) to assist with this review.

## 6. Review Configuration

Confirm that configuration files have been migrated correctly:

- `Web.config` settings should have been moved to `appsettings.json` or `appsettings.{Environment}.json`.
- Connection strings should be present and correctly formatted in the new configuration structure.
- Any environment-specific settings should be validated against the environments where the application will run.

## 7. Publish the Application

Once validation is complete, publish the application to a target folder:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected files, static assets, and configuration files are present before deploying to the target environment.