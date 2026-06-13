# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the root of the solution to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors are reported during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of any build-time issues:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs, missing references, or compatibility concerns that did not surface as hard errors.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality behaves as expected:

```bash
dotnet test --configuration Release
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the `Northwind.Web` project locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Confirm that:
- The application starts without runtime exceptions.
- All routes and endpoints respond as expected.
- Database connections (if applicable) are functioning correctly.
- Any static assets, middleware, or configuration that was previously handled by the legacy framework is working under the new setup.

### 5. Review Configuration Files

Check the following files for correctness after the transformation:

- `appsettings.json` and `appsettings.{Environment}.json` — Ensure connection strings, logging settings, and any environment-specific values are accurate.
- `Program.cs` — Verify that middleware registration, service configuration, and the request pipeline are set up correctly for cross-platform .NET.
- `.csproj` files — Confirm that target frameworks, package references, and any remaining project references are appropriate.

### 6. Verify Target Framework

Confirm that all projects are targeting the intended .NET version:

```bash
dotnet --version
```

Cross-reference this with the `<TargetFramework>` element in each `.csproj` file to ensure consistency.

### 7. Check for Removed or Changed APIs

Even without build errors, some APIs behave differently between .NET Framework and cross-platform .NET. Manually review areas of the code that use:

- `System.Web` namespaces (these are not available in cross-platform .NET and may have been substituted during transformation).
- Windows-specific APIs such as the registry, WCF, or certain cryptography providers.
- Any third-party libraries that may have been updated to newer major versions with breaking changes.

### 8. Validate Data Access Layer

If the project uses Entity Framework or another ORM, verify that:

- Migrations are up to date and can be applied successfully.
- Queries return expected results against the target database.
- Connection strings are correctly configured for the deployment environment.

### 9. Publish the Application

Once all validation steps pass, publish the application using:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all required files are present before deploying to the target environment.