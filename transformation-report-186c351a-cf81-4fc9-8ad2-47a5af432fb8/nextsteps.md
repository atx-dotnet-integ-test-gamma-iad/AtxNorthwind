# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including `Northwind.Web`.

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

Check the output for any warnings that, while non-blocking, may indicate areas that need attention (e.g., nullable reference warnings, obsolete API usage).

### 3. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review test results and address any failures before proceeding.

### 4. Run the Application Locally

Start the web application locally to verify it runs as expected on the new runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

- Confirm the application starts without runtime exceptions.
- Navigate through the key pages and endpoints to verify expected behavior.
- Check application logs for any runtime warnings or errors.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure all other projects in the solution target a compatible framework version.

### 6. Review Removed or Changed APIs

Cross-platform .NET removes or changes certain APIs that were available in .NET Framework. Manually review the following areas:

- **Configuration**: Ensure `System.Configuration.ConfigurationManager` usages have been replaced with `Microsoft.Extensions.Configuration`.
- **HTTP**: Confirm any `System.Web` references have been replaced with `Microsoft.AspNetCore` equivalents.
- **Database access**: Verify connection strings and database provider packages are correctly configured for the new runtime.
- **Authentication/Authorization**: Confirm middleware is correctly registered in `Program.cs` or `Startup.cs`.

### 7. Check for Platform-Specific Code

Search the codebase for any remaining Windows-specific APIs that may not behave correctly on Linux or macOS:

```bash
grep -rn "Registry\|Environment.SpecialFolder\|System.Drawing" src/
```

Replace or conditionally compile any identified platform-specific code.

### 8. Publish the Application

Once validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and confirm all expected assets, configuration files, and binaries are present before deploying to the target environment.