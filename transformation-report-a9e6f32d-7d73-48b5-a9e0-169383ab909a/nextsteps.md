# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Verify that the output confirms zero errors and review any warnings that may indicate compatibility concerns.

### 3. Run Unit Tests

If the solution contains test projects, execute them to validate that existing functionality is intact after the transformation:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review test results and investigate any failures, as they may indicate behavioral differences between the legacy framework and the new cross-platform .NET runtime.

### 4. Verify Runtime Behavior

Run the web application locally to confirm it starts and operates correctly:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following at runtime:
- The application starts without exceptions
- Database connections are established successfully
- Key routes and endpoints return expected responses
- Any authentication or authorization flows behave as expected

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all dependent projects in the solution target a compatible framework version.

### 6. Check for Removed or Changed APIs

Review the code for usage of APIs that existed in .NET Framework but have changed behavior in cross-platform .NET. Common areas to inspect include:

- `System.Web` references, which are not available in cross-platform .NET and should have been replaced with ASP.NET Core equivalents
- `HttpContext`, `HttpRequest`, and `HttpResponse` usages
- Configuration APIs, replacing `ConfigurationManager` with `Microsoft.Extensions.Configuration`
- Any Windows-specific APIs such as the registry or Windows identity model

### 7. Validate Static Files and Configuration

- Confirm that `appsettings.json` contains the correct configuration values previously held in `Web.config` or `App.config`
- Verify that static files, bundling, and middleware are configured correctly in `Program.cs` or `Startup.cs`

### 8. Database and Entity Framework Validation

If the project uses Entity Framework, confirm the following:

- Migrations are present and up to date by running:

```bash
dotnet ef migrations list --project src/Northwind.Web/Northwind.Web.csproj
```

- Apply migrations against a test database to verify schema compatibility:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 9. Publish a Test Build

Produce a published output to verify the application can be packaged correctly:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish-output
```

Inspect the `./publish-output` directory to confirm all expected assemblies, static files, and configuration files are present.