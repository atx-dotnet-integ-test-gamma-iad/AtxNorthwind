# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including the most critical project `Northwind.Web`.

## Validation

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

Check the output for any warnings that, while non-blocking, may indicate compatibility concerns worth addressing.

### 3. Run the Application Locally
Start the application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj
```

Navigate through the application and confirm that core functionality behaves the same as it did in the legacy project.

### 4. Run Existing Tests
If the solution contains test projects, execute them to verify that existing behavior has not regressed:

```bash
dotnet test
```

Review any failing tests and determine whether they are caused by the migration or were pre-existing issues.

### 5. Review Target Framework
Open each `.csproj` file and confirm the `<TargetFramework>` element is set to the intended cross-platform .NET version (e.g., `net8.0`). Ensure no projects are still targeting `net48` or another Windows-only framework unintentionally.

### 6. Check for Windows-Specific APIs
Even without build errors, some APIs may have been carried over that only function correctly on Windows. Use the .NET Compatibility Analyzer to surface these:

```bash
dotnet add package Microsoft.DotNet.Analyzers.Compatibility
```

Review any diagnostics produced during the next build.

### 7. Review Configuration Files
- Confirm that `appsettings.json` contains all settings that were previously in `web.config` or `app.config`.
- Verify connection strings, authentication settings, and environment-specific configuration are correctly structured for the new hosting model.

### 8. Verify Static Files and Middleware
For `Northwind.Web`, confirm that static file serving, routing, and any middleware previously configured in `Global.asax` or `Startup.cs` (legacy) have been correctly mapped to the current `Program.cs` or `Startup.cs` pattern used by the target framework.

## Deployment

### 1. Publish the Application
Once validation is complete, publish the application to a folder for deployment:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

### 2. Verify the Publish Output
Inspect the `./publish` directory to confirm all expected files are present, including static assets, configuration files, and the compiled binaries.

### 3. Test the Published Output
Run the published output directly to confirm it behaves identically to the development build:

```bash
dotnet ./publish/Northwind.Web.dll
```

### 4. Select a Hosting Environment
Deploy the published output to your target environment. Common options include:
- **IIS on Windows** — configure using the ASP.NET Core Module (ANCM) and an appropriate `web.config` generated during publish.
- **Linux host** — use a reverse proxy such as Nginx or Apache in front of the Kestrel server.
- **Direct Kestrel** — suitable for internal or controlled network environments.

Ensure the target machine has the correct .NET runtime version installed, or use a self-contained publish:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --self-contained true --runtime win-x64 --output ./publish-selfcontained
```

Replace `win-x64` with the appropriate runtime identifier for your target platform (e.g., `linux-x64`, `osx-x64`).