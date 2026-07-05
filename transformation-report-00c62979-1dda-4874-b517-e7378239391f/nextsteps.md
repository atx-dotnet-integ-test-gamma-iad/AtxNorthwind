# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Build Status

The solution has no build errors following the transformation. All projects compiled successfully, including `Northwind.Web`.

## Validation Steps

### 1. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the target framework is set to the intended cross-platform version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure no legacy framework monikers such as `net48` or `net472` remain.

### 2. Restore and Build Locally

Run the following commands from the solution root to confirm a clean restore and build:

```bash
dotnet restore
dotnet build
```

Verify that no warnings are present that could indicate deprecated APIs or packages that may cause runtime issues.

### 3. Run the Application Locally

Start the web application and confirm it launches without errors:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate to the application in a browser and verify that core pages and functionality load as expected.

### 4. Execute Existing Tests

If the solution contains test projects, run them to confirm no regressions were introduced:

```bash
dotnet test
```

Review any failing tests and address them individually before proceeding.

### 5. Check for Removed or Changed APIs

Review the code for usage of APIs that may have been removed or changed in modern .NET, such as:

- `HttpContext.Current`
- `System.Web` namespaces
- `ConfigurationManager` (replaced by `Microsoft.Extensions.Configuration`)
- `Global.asax` lifecycle methods

Use the [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.UpgradeAssistant` tool to surface any remaining compatibility issues.

### 6. Validate Configuration

Confirm that `appsettings.json` contains all configuration values that were previously in `Web.config` or `App.config`, including:

- Connection strings
- Application settings
- Logging configuration

### 7. Verify Database Connectivity

If the application uses a database, confirm the connection string in `appsettings.json` is correct and that the application can connect and query data at runtime.

### 8. Test on a Non-Windows Platform (if applicable)

Since the goal is cross-platform compatibility, run the application on Linux or macOS if those are target environments:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Watch for any platform-specific issues such as file path casing, Windows-only APIs, or registry access.

### 9. Review NuGet Package Versions

Check that all NuGet packages referenced in the project are compatible with the target .NET version. Replace any packages that target only .NET Framework with their cross-platform equivalents.

### 10. Publish the Application

Once validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj -c Release -o ./publish
```

Review the contents of the `./publish` directory and confirm all expected files are present before deploying to the target environment.