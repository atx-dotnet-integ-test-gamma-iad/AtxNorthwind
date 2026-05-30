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

Address any warnings that surface during the build, particularly those related to nullable reference types or obsolete APIs, as these may indicate areas where the code relies on legacy behavior.

## 3. Run Unit and Integration Tests

If the solution contains test projects, execute them to verify that existing functionality is preserved:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests carefully. Failures may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime, such as differences in globalization, string handling, or reflection.

## 4. Verify Runtime Behavior of Northwind.Web

Launch the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following areas specifically:
- Routing and middleware pipeline behavior
- Database connectivity and Entity Framework (or other ORM) queries
- Authentication and authorization flows
- Static file serving
- Any areas that previously relied on `System.Web` or other Windows-specific APIs

## 5. Review Configuration Files

Cross-platform .NET uses `appsettings.json` rather than `Web.config` or `App.config` for most configuration. Confirm that:
- All connection strings have been migrated correctly
- Environment-specific settings are in place (e.g., `appsettings.Development.json`)
- Any configuration previously in `Web.config` HTTP handlers, modules, or custom sections has been accounted for in the middleware pipeline

## 6. Check for Platform-Specific Dependencies

Review the project's NuGet packages and any P/Invoke or COM interop calls for Windows-only dependencies. Run the .NET Compatibility Analyzer if not already done:

```bash
dotnet add package Microsoft.DotNet.Analyzers.Compatibility
```

This will surface any API usage that is not supported on non-Windows platforms.

## 7. Test on Target Platforms

If cross-platform support is a goal, run and validate the application on each intended target operating system (e.g., Linux, macOS) to catch any platform-specific runtime issues that do not appear on Windows.

## 8. Publish the Application

Once validation is complete, publish the application to the target environment:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the output directory to confirm all required files, including static assets and configuration files, are present before deploying to the target host.