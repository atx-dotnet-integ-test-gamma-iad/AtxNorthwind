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

Address any warnings that surface during the build, particularly those related to nullable reference types or obsolete APIs, as these can indicate areas of the code that may behave differently under cross-platform .NET.

## 3. Run Unit and Integration Tests

If the solution contains test projects, execute them to verify that existing functionality is preserved:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review all test results carefully. Any failing tests should be investigated to determine whether they are caused by behavioral differences between .NET Framework and cross-platform .NET (e.g., changes in `System.Web`, `HttpContext`, globalization, or file path handling).

## 4. Verify Runtime Behavior

Launch the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following areas specifically:
- Routing and middleware pipeline behavior
- Database connectivity and Entity Framework (if applicable) query results
- Authentication and authorization flows
- Static file serving
- Any areas that previously relied on `System.Web` or Windows-specific APIs

## 5. Review Configuration Files

Cross-platform .NET uses `appsettings.json` rather than `Web.config` for most configuration. Confirm that:
- All connection strings have been migrated to `appsettings.json`
- Environment-specific settings are handled via `appsettings.{Environment}.json` or environment variables
- Any remaining `Web.config` entries are only those required by IIS (e.g., `system.webServer` handlers)

## 6. Check for Platform-Specific Code

Search the codebase for APIs that may not behave consistently across operating systems:

- File path separators (`\` vs `/`) — use `Path.Combine` and `Path.DirectorySeparatorChar`
- Registry access (`Microsoft.Win32.Registry`) — not available on Linux/macOS
- Windows-specific authentication (NTLM/Kerberos) — requires additional configuration on non-Windows hosts
- `System.Drawing` — has limitations on non-Windows platforms; consider replacing with a cross-platform alternative such as `SkiaSharp` if image processing is used

## 7. Target Framework Verification

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the target framework is set to a currently supported version:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If the project is targeting `net6.0` or `net7.0`, consider upgrading to `net8.0` as those versions are out of support or approaching end of life.

## 8. Publish the Application

Once validation is complete, publish the application to a self-contained or framework-dependent deployment:

**Framework-dependent:**
```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

**Self-contained (example for Linux x64):**
```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --runtime linux-x64 --self-contained true --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected files are present before deploying to the target environment.