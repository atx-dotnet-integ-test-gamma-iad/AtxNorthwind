# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation and Testing

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are correctly restored:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Release
```

Check the output for any warnings that, while non-blocking, may indicate compatibility concerns worth addressing.

### 3. Review Target Framework
Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version of .NET (e.g., `net8.0`). Avoid targeting `net5.0` or `net6.0` as these are out of support.

### 4. Run the Application Locally
Start the application using the .NET CLI:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate through the application and verify that core functionality behaves as expected compared to the legacy version.

### 5. Run Existing Tests
If the solution contains test projects, execute them to validate business logic and integration points:

```bash
dotnet test
```

Review any failing tests carefully, as failures may indicate behavioral differences introduced during the migration rather than pre-existing issues.

### 6. Check for Removed or Changed APIs
Review any usage of APIs that were available in .NET Framework but have changed behavior in cross-platform .NET. Pay particular attention to:

- `System.Web` dependencies (these do not exist in cross-platform .NET)
- Windows-specific APIs (e.g., registry access, Windows identity)
- Any third-party libraries that may have been updated to newer major versions with breaking changes

### 7. Validate Configuration
Confirm that `appsettings.json` (and environment-specific variants such as `appsettings.Development.json`) contain all configuration values that were previously held in `web.config` or `app.config`. Verify connection strings, application settings, and any custom configuration sections have been correctly migrated.

### 8. Test Database Connectivity
If the application uses a database, verify that connection strings are correct and that the application can successfully connect and perform read/write operations against the target database.

### 9. Review Static Files and Middleware
Ensure that static file serving, routing, and any custom middleware are functioning correctly by manually testing key routes and endpoints in the application.

### 10. Publish the Application
Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all required files, assets, and dependencies are present before deploying to the target environment.