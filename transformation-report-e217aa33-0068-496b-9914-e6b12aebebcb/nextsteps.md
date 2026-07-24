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

## 3. Run Existing Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review the test results carefully. Pay particular attention to any tests that interact with file paths, database connections, or platform-specific APIs, as these are common sources of cross-platform issues.

## 4. Verify Runtime Behavior

Run the web application locally and navigate through its core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following areas manually:

- **Database connectivity**: Confirm that the connection strings in `appsettings.json` are correct and that the application can connect to the database.
- **Static files**: Verify that CSS, JavaScript, and image assets are served correctly.
- **Routing**: Navigate through the application routes to confirm pages render as expected.
- **Authentication/Authorization**: If the application uses any auth mechanisms, test login and access control flows.

## 5. Review Configuration Files

Compare the original `Web.config` (if it existed) with the new `appsettings.json` to ensure all configuration values were carried over, including:

- Connection strings
- Application settings
- Logging configuration

## 6. Check for Platform-Specific Code

Search the codebase for any remaining usage of Windows-specific APIs or patterns that may not behave correctly on Linux or macOS:

- `System.Web` references
- Windows registry access
- Hardcoded backslash file path separators (`\`) — replace with `Path.Combine` or `Path.DirectorySeparatorChar`
- `HttpContext.Current` usage

## 7. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `TargetFramework` is set to a current and supported version of .NET:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If the project is targeting an older version such as `net6.0` or `net7.0`, consider updating to `net8.0` as those versions are approaching or have reached end of support.

## 8. Publish the Application

Once validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all required files, including static assets and configuration files, are present before deploying to the target environment.