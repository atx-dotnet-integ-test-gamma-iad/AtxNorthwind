# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. There are no build errors present in the solution. The following steps outline how to validate, test, and deploy the migrated project.

## 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or deprecated packages targeting older frameworks.

## 2. Build the Solution

Perform a full solution build to confirm the absence of errors in a clean build environment:

```bash
dotnet build --configuration Release
```

Address any warnings that surface, particularly those related to nullable reference types, obsolete APIs, or platform compatibility.

## 3. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version (e.g., `net8.0`). Verify that all dependent projects in the solution target compatible frameworks.

## 4. Run Unit and Integration Tests

If the solution contains test projects, execute them to confirm existing functionality has not regressed:

```bash
dotnet test --configuration Release
```

Review test output carefully, paying attention to any tests that were previously passing under the legacy framework but now fail under the new target framework.

## 5. Verify Runtime Behavior

Run the web application locally and manually verify core functionality:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Check the following areas specifically, as they are common sources of runtime issues after migration:

- **Authentication and Authorization**: Middleware configuration in `Program.cs` or `Startup.cs` may need adjustment.
- **Entity Framework Core**: If the project uses EF Core, verify that migrations are up to date by running `dotnet ef migrations list`.
- **Configuration**: Confirm that `appsettings.json` values, connection strings, and environment-specific settings are loading correctly.
- **Static Files and Routing**: Verify that routes resolve as expected and static assets are served correctly.

## 6. Check for Removed or Changed APIs

Review the code for usage of any APIs that were available in .NET Framework but have changed or been removed in modern .NET. The [.NET Upgrade Assistant compatibility analyzer](https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview) or the `Microsoft.DotNet.UpgradeAssistant` tool can assist with identifying remaining compatibility concerns.

## 7. Publish the Application

Once validation is complete, publish the application to confirm the output is as expected:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all required files, assemblies, and configuration files are present before deploying to the target environment.