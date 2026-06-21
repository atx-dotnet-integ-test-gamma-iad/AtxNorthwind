# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including the primary `Northwind.Web` project.

## Validation Steps

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while not blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

### 4. Run the Application Locally
Start the web application and verify it runs as expected on the cross-platform runtime:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following at runtime:
- Application starts without exceptions
- All routes and endpoints respond correctly
- Database connections (if applicable) are functional
- Authentication and authorization flows behave as expected
- Static files and views render correctly

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended modern .NET version (e.g., `net8.0`):

```xml
<TargetFramework>net8.0</TargetFramework>
```

### 6. Check for Removed or Changed APIs
Even without build errors, some APIs behave differently in modern .NET. Review the following areas manually:

- **Configuration**: Ensure `appsettings.json` is used in place of `Web.config` or `App.config` where applicable.
- **HTTP pipeline**: Confirm middleware registration in `Program.cs` or `Startup.cs` follows the current .NET conventions.
- **Entity Framework**: If EF is used, verify migrations are compatible with the current EF Core version.
- **Session and caching**: Confirm any in-process session or caching mechanisms are supported in the target runtime.

### 7. Validate on Target Operating Systems
Since the goal is cross-platform support, test the application on each intended operating system (e.g., Windows, Linux, macOS) to surface any platform-specific issues such as:

- File path casing sensitivity (Linux is case-sensitive)
- Platform-specific APIs that may have been inadvertently retained
- File system permissions

### 8. Review Publish Output
Perform a publish to confirm the output is complete and correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to ensure all required assets, configuration files, and binaries are present.