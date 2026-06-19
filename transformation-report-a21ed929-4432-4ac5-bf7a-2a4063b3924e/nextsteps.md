# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully — no build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are properly restored:

```bash
dotnet restore
```

### 2. Build the Solution
Confirm the solution builds cleanly in Release configuration:

```bash
dotnet build --configuration Release
```

Review the output for any warnings that may indicate deprecated APIs, missing references, or compatibility concerns that were not caught as errors.

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify runtime behavior has not regressed:

```bash
dotnet test --configuration Release
```

Review test results carefully. A passing build does not guarantee correct runtime behavior, especially after a cross-platform migration.

### 4. Check for Platform-Specific Code
Even without build errors, review the codebase for any APIs that may behave differently across platforms. Common areas to check include:

- File path separators (`\` vs `/`) — use `Path.Combine` and `Path.DirectorySeparatorChar` where applicable
- Windows Registry access (`Microsoft.Win32.Registry`) — not available on Linux/macOS
- `System.Drawing` usage — has limited cross-platform support; consider replacing with a library such as `SkiaSharp` or `ImageSharp`
- COM interop or P/Invoke calls targeting Windows-specific native libraries

### 5. Run the Application Locally
Start the web application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate through the application and confirm that core functionality, routing, and data access behave correctly.

### 6. Verify Configuration and Environment Settings
Check that `appsettings.json` and any environment-specific configuration files (`appsettings.Development.json`, etc.) have been correctly carried over. Pay particular attention to:

- Connection strings
- Authentication settings
- Logging configuration

### 7. Test on the Target Platform
If the goal is to run on Linux or macOS, perform the above validation steps on the target operating system to surface any remaining platform-specific issues that may not appear on Windows.

## Deployment

### 1. Publish the Application
Use the `dotnet publish` command to produce deployment artifacts:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

### 2. Verify the Published Output
Inspect the `./publish` directory to confirm all required files are present, including static assets, configuration files, and dependent assemblies.

### 3. Configure the Web Server
Deploy the published output to the target server and configure a reverse proxy if needed (e.g., IIS, Nginx, or Apache) to forward requests to the Kestrel process. Refer to the [official ASP.NET Core hosting documentation](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/) for platform-specific guidance.