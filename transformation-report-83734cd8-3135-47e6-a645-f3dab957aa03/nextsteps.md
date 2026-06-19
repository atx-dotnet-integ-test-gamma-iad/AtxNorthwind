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

Review test results and address any failing tests before proceeding.

### 4. Run the Application Locally

Start the application locally to verify runtime behavior:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Manually verify the following:
- The application starts without runtime exceptions.
- Key routes and pages load as expected.
- Database connectivity functions correctly if applicable.
- Any authentication or authorization flows behave as expected.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

If the target framework needs to be updated, change the value and re-run the build and test steps above.

### 6. Check for Deprecated or Obsolete API Usage

Run a build with warnings treated carefully:

```bash
dotnet build --configuration Release /warnaserror
```

Address any obsolete API usages surfaced by this step to ensure forward compatibility.

### 7. Review Configuration Files

- Confirm that `appsettings.json` and `appsettings.Production.json` contain the correct configuration values for the target environment.
- Verify that any configuration previously held in `Web.config` has been correctly migrated to the appropriate `appsettings.json` structure or middleware configuration in `Program.cs` / `Startup.cs`.

### 8. Verify Static Files and wwwroot

Confirm that all static assets (CSS, JavaScript, images) are present under the `wwwroot` folder and are being served correctly when the application runs locally.

### 9. Publish the Application

Once local validation is complete, publish the application to confirm the output is correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Inspect the `./publish` directory to confirm all expected files are present.

### 10. Deploy to Target Environment

Copy the contents of the `./publish` directory to the target hosting environment. Ensure the target environment has the correct .NET runtime installed by running:

```bash
dotnet --info
```

Confirm the runtime version matches the target framework used in the project.