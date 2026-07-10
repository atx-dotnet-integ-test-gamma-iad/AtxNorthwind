# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the absence of errors:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate compatibility issues, even if they do not prevent a successful build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the transformation:

```bash
dotnet test --configuration Release
```

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the web application locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Navigate to the application in a browser and verify that pages load correctly.
- Check that database connections, if any, are functioning as expected.
- Review application logs for any runtime exceptions or warnings.

### 5. Verify Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `TargetFramework` is set to the intended cross-platform .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution are targeting the same or a compatible framework version.

### 6. Review Removed or Changed APIs

Check for any usage of APIs that were available in .NET Framework but have changed behavior in modern .NET. Pay particular attention to:

- `System.Web` references, which are not available in modern .NET and should be replaced with `Microsoft.AspNetCore` equivalents.
- Any use of `HttpContext`, `HttpRequest`, or `HttpResponse` that may have changed signatures.
- Entity Framework version compatibility if the project uses a data access layer.

### 7. Check Configuration Files

Verify that configuration has been migrated correctly:

- Confirm that `Web.config` settings have been moved to `appsettings.json` where applicable.
- Ensure connection strings are present and correctly formatted in `appsettings.json`.
- Verify that environment-specific configuration files such as `appsettings.Development.json` are in place.

### 8. Static Files and Middleware

If the application serves static files or uses custom middleware, confirm that the middleware pipeline in `Program.cs` or `Startup.cs` is correctly configured, including calls to:

```csharp
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
```

### 9. Publish the Application

Once local validation is complete, publish the application to verify the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to confirm all expected files are present before deploying to the target environment.