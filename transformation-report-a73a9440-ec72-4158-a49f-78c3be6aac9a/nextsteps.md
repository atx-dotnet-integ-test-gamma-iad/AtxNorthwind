# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state observed during transformation holds consistently:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while non-breaking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality behaves as expected after the migration:

```bash
dotnet test --configuration Release
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version, such as `net8.0`. Microsoft's [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy) can be used as a reference.

### 5. Check for Windows-Specific APIs

Even without build errors, some APIs that compiled successfully may rely on Windows-specific behavior. Run the .NET compatibility analyzer if cross-platform deployment is a goal:

```bash
dotnet build /p:EnableNETAnalyzers=true /p:AnalysisMode=All
```

Review any `CA1416` platform compatibility warnings in the output.

### 6. Review `System.Web` Usages

If the original project used `System.Web` (common in legacy ASP.NET projects), verify that all such usages have been replaced with their ASP.NET Core equivalents, such as:

- `HttpContext` → `Microsoft.AspNetCore.Http.HttpContext`
- `HttpRequest` → `Microsoft.AspNetCore.Http.HttpRequest`
- `Session` → `ISession` via `Microsoft.AspNetCore.Http`

### 7. Validate Configuration Files

Ensure that any `Web.config` or `App.config` files have been migrated to `appsettings.json` and that the application reads configuration correctly using `IConfiguration` at runtime.

### 8. Run the Application Locally

Start the application using the .NET CLI and navigate through its primary workflows to confirm runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Check application logs for any runtime exceptions or deprecation notices that would not surface at compile time.

### 9. Verify Database Connectivity

If the application uses Entity Framework or direct database access, confirm that connection strings in `appsettings.json` are correct and that the application can successfully connect to and query the database at runtime.

### 10. Review Middleware and Startup Configuration

In ASP.NET Core, confirm that `Program.cs` (or `Startup.cs` if still in use) correctly registers all required services and middleware, and that the request pipeline is configured in the appropriate order.