# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. The following steps outline how to validate, test, and deploy the migrated project.

## 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages. If any packages targeting the old .NET Framework are still referenced, locate them in the `.csproj` files and replace them with their .NET-compatible equivalents.

## 2. Build the Solution

Perform a full build to confirm there are no compilation issues:

```bash
dotnet build --configuration Release
```

Address any warnings that surface during the build, particularly those related to nullable reference types, obsolete APIs, or platform compatibility analyzers.

## 3. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Ensure no legacy `<TargetFrameworkVersion>` elements remain from the original .NET Framework project format.

## 4. Run Unit Tests

If the solution contains test projects, execute them to verify that existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review test output carefully. Failures may indicate behavioral differences between .NET Framework and modern .NET that require code adjustments.

## 5. Validate Runtime Behavior

Run the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Check the following areas in particular:
- Database connectivity and query results (especially if using Entity Framework)
- Authentication and authorization flows
- Any middleware or HTTP pipeline configuration in `Program.cs` or `Startup.cs`
- Static file serving and routing

## 6. Check for Removed or Changed APIs

Modern .NET removed several APIs that were present in .NET Framework. Use the .NET Upgrade Assistant compatibility analyzer or the `Microsoft.DotNet.PlatformAbstractions` compatibility tooling to identify any runtime-level incompatibilities that do not surface as build errors:

```bash
dotnet tool install -g upgrade-assistant
upgrade-assistant analyze src/Northwind.Web/Northwind.Web.csproj
```

## 7. Review Configuration Files

Confirm that `appsettings.json` contains all configuration values that were previously stored in `Web.config` or `App.config`. The `System.Configuration.ConfigurationManager` API behaves differently on modern .NET, and connection strings or application settings may need to be migrated to the `appsettings.json` format and accessed via `IConfiguration`.

## 8. Publish the Application

Once validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files, assemblies, and static assets are present before deploying to the target environment.