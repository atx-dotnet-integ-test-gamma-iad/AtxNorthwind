# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including `Northwind.Web`.

## Validation and Testing

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Verify that no warnings or errors appear during the restore process, particularly around package compatibility or missing dependencies.

### 2. Build the Solution
Perform a full solution build to confirm the absence of errors in a clean build environment:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that, while non-blocking, may indicate deprecated APIs or compatibility concerns worth addressing.

### 3. Run Unit and Integration Tests
If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review any failing tests and determine whether they are caused by behavioral differences between the legacy .NET Framework and the new cross-platform .NET runtime.

### 4. Verify Runtime Behavior
Start the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following areas specifically, as they are common sources of runtime issues after migration:

- **Authentication and Authorization** – Middleware and cookie behavior can differ between .NET Framework and modern .NET.
- **Entity Framework / Database Access** – Confirm that connection strings are correctly configured and that queries execute as expected.
- **Static Files and Routing** – Verify that routes resolve correctly and static assets are served properly.
- **Configuration** – Ensure `appsettings.json` and any environment-specific configuration files are present and correctly read, replacing any legacy `Web.config` or `App.config` values.
- **HTTP Client / External Service Calls** – Confirm that any outbound HTTP calls function correctly under the new runtime.

### 5. Check for Removed or Changed APIs
Review the [.NET Upgrade Assistant compatibility analyzer results](https://learn.microsoft.com/en-us/dotnet/core/porting/) or run the compatibility analyzer manually to identify any API usage that may compile but behave differently at runtime:

```bash
dotnet add package Microsoft.DotNet.UpgradeAssistant.Extensions.Default.Analyzers
```

### 6. Review `Northwind.Web` Configuration Files
Confirm that the following files are present and correctly configured in `src/Northwind.Web/`:

- `appsettings.json` and `appsettings.{Environment}.json`
- `Program.cs` (and `Startup.cs` if applicable) — ensure middleware registration order is correct
- Any remaining `Web.config` transformations that may need to be ported to the new configuration system

### 7. Target Framework Verification
Open each `.csproj` file and confirm the `TargetFramework` element reflects the intended version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure consistency across all projects in the solution to avoid cross-targeting issues.

### 8. Publish the Application
Once validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to verify all expected files, assemblies, and static assets are present before deploying to the target environment.