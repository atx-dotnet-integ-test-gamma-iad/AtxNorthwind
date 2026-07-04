# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

## Summary

The solution build output contains no errors across all projects, including `Northwind.Web`. This indicates the transformation to cross-platform .NET has completed successfully.

## Validation Steps

### 1. Restore Dependencies

Run the following command from the solution root to ensure all NuGet packages are restored cleanly:

```bash
dotnet restore
```

Review the output for any warnings related to package compatibility or version conflicts and resolve them if present.

### 2. Build the Solution

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Verify that the build output reports zero errors and review any warnings that may indicate deprecated APIs or compatibility concerns.

### 3. Run Unit Tests

If the solution contains test projects, execute them to confirm runtime behavior is correct after the transformation:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review test results and address any failing tests, as they may indicate behavioral differences between the legacy .NET Framework runtime and the new cross-platform .NET runtime.

### 4. Run the Application Locally

Start the `Northwind.Web` project and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following:
- The application starts without runtime exceptions.
- All routes and endpoints respond correctly.
- Database connections and queries function as expected.
- Any authentication or session handling behaves correctly.

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element targets an actively supported version of .NET (e.g., `net8.0`). If it targets an older version such as `net5.0` or `net6.0`, consider updating to a current long-term support release.

### 6. Check for Runtime-Specific API Usage

Even with a successful build, some APIs behave differently or are unavailable at runtime on non-Windows platforms. Review the code for usage of the following and test on the intended target platform:

- `System.Drawing` (requires `libgdiplus` on Linux/macOS or replacement with a supported library)
- Windows Registry access
- Windows-specific file path assumptions
- COM interop or P/Invoke calls targeting Windows-only native libraries

### 7. Review Configuration and Environment Variables

Confirm that `appsettings.json` and any environment-specific configuration files (`appsettings.Development.json`, etc.) are present and correctly configured. Verify that connection strings and other settings that may have been stored in `Web.config` in the legacy project have been migrated to the appropriate .NET configuration system.

### 8. Verify Static Files and wwwroot

Confirm that all static assets (CSS, JavaScript, images) are present under the `wwwroot` folder and are being served correctly when the application runs.

### 9. Review Middleware Pipeline

If the project uses ASP.NET Core, review `Program.cs` (and `Startup.cs` if present) to ensure the middleware pipeline is correctly configured, including:
- Exception handling middleware
- Static file middleware
- Routing and endpoint mapping
- Authentication and authorization middleware, in the correct order

### 10. Publish the Application

Once local validation is complete, publish the application to confirm the output is clean:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory and verify all expected files are present before deploying to the target environment.