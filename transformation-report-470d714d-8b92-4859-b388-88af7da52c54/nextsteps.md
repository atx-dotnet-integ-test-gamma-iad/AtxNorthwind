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

Perform a full solution build to confirm the error-free state is consistent across all configurations:

```bash
dotnet build --configuration Debug
dotnet build --configuration Release
```

### 3. Review Target Framework

Open `Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version of .NET (e.g., `net8.0`). Avoid `net5.0` or `net6.0` as these are out of support.

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 4. Run Unit Tests

If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=normal"
```

Review any failing tests carefully, as they may indicate behavioral differences introduced by the framework migration.

### 5. Run the Application Locally

Start the web application and verify it runs without runtime exceptions:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

- Navigate to the application in a browser.
- Exercise the primary routes and features.
- Check the console output for any unhandled exceptions or middleware errors.

### 6. Review Removed or Changed APIs

Cross-platform .NET removes or changes certain APIs that were available in .NET Framework. Manually review the following areas:

- **`System.Web` dependencies**: These are not available in cross-platform .NET. Confirm no remaining references exist.
- **`HttpContext` usage**: Ensure access is done via dependency injection rather than `HttpContext.Current`.
- **Configuration**: Verify `web.config` settings have been migrated to `appsettings.json` and are being read via `IConfiguration`.
- **Authentication/Authorization**: Confirm middleware is registered correctly in `Program.cs` or `Startup.cs`.

### 7. Check for Runtime-Only Issues

Some issues do not surface at build time. Pay attention to:

- Database connection strings in `appsettings.json`.
- Entity Framework migrations, if applicable. Run `dotnet ef database update` to confirm the schema is current.
- Static file serving and routing behavior.

### 8. Review Warnings

Even without errors, build warnings can indicate future problems. Run the following and address any relevant warnings:

```bash
dotnet build --configuration Release /warnaserror
```

Use this output to identify any deprecated API usage or nullable reference type violations that should be resolved.

## Deployment

Once local validation is complete, publish the application using:

```bash
dotnet publish --configuration Release --output ./publish
```

Verify the contents of the `./publish` directory and confirm the application runs correctly from that output before deploying to the target environment.