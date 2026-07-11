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

Verify that no warnings or errors are reported during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs or compatibility concerns, even if they do not block the build.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not been broken during the transformation:

```bash
dotnet test --configuration Release
```

Review any failing tests and address them before proceeding.

### 4. Run the Application Locally

Start the application locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the application and verify that core functionality behaves as expected compared to the legacy version.

### 5. Verify Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a compatible framework version.

### 6. Review Removed or Changed APIs

Check the codebase for any usage of APIs that were available in .NET Framework but have changed behavior in modern .NET. Key areas to review include:

- `System.Web` usages, which are not available in modern .NET and should have been replaced with ASP.NET Core equivalents.
- `HttpContext`, `HttpRequest`, and `HttpResponse` usages to confirm they reference `Microsoft.AspNetCore.Http` types.
- Any configuration previously handled via `Web.config` should now be handled through `appsettings.json` and the `IConfiguration` system.
- `Global.asax` logic should have been migrated to `Program.cs` or `Startup.cs`.

### 7. Check Database Connectivity

If the application uses a database, verify that connection strings in `appsettings.json` are correct and that the application can connect successfully at runtime.

If Entity Framework is used, run the following to verify the model is consistent with the database schema:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 8. Review Middleware and Request Pipeline

Open `Program.cs` or `Startup.cs` and confirm that the middleware pipeline is configured correctly, including authentication, authorization, routing, and static file serving, as these are commonly affected during migration from .NET Framework to modern .NET.

### 9. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and verify that all expected files are present before deploying to the target environment.