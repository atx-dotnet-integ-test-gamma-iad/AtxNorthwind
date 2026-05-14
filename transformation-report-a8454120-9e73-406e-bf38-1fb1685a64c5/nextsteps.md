# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. The following steps outline how to validate, test, and deploy the migrated project.

## 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process. If any packages are flagged as incompatible or deprecated, review them in each `.csproj` file and update to the appropriate cross-platform compatible versions.

## 2. Build the Solution

Perform a full build to confirm the solution compiles cleanly:

```bash
dotnet build --configuration Release
```

Review the output for any warnings, particularly those related to nullable reference types, deprecated APIs, or target framework compatibility.

## 3. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a supported modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If it is still referencing `net48` or any other .NET Framework moniker, update it accordingly and re-run the build.

## 4. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether they are caused by behavioral differences between .NET Framework and modern .NET, such as changes in globalization, JSON serialization defaults, or HTTP client behavior.

## 5. Verify Runtime Behavior

Run the web application locally to confirm it starts and operates correctly:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Check the following areas manually:

- Application startup and middleware pipeline initialization
- Database connectivity, particularly if using Entity Framework — confirm migrations are up to date by running `dotnet ef database update`
- Authentication and authorization flows
- Any file I/O operations, as path handling differs between Windows and Linux/macOS

## 6. Check for Removed or Changed APIs

Review the code for usage of APIs that were removed or significantly changed in modern .NET. Common areas to inspect include:

- `System.Web` references, which are not available outside of .NET Framework
- `BinaryFormatter`, which is disabled by default in modern .NET
- `HttpContext` and related types if the project previously used ASP.NET Web Forms or older MVC patterns
- Any P/Invoke calls or Windows-specific interop

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.UpgradeAssistant` tool to identify remaining compatibility issues if needed.

## 7. Validate Configuration

Confirm that `appsettings.json` contains all necessary configuration values that were previously stored in `Web.config` or `App.config`. Pay particular attention to:

- Connection strings
- Application-specific settings
- Logging configuration

## 8. Publish the Application

Once validation is complete, publish the application using:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the output directory to confirm all required files are present, then deploy the contents to the target environment.