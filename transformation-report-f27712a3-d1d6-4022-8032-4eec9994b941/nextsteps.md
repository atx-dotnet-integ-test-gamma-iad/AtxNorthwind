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

Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

Verify that the build output confirms zero errors and review any warnings that may indicate compatibility concerns.

### 3. Run Unit Tests

If the solution contains test projects, execute them to validate that existing functionality behaves as expected after the transformation:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Review the test results and address any failing tests before proceeding.

### 4. Verify Runtime Behavior

Run the web application locally to confirm it starts and operates correctly:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Check the following:
- The application starts without runtime exceptions
- Database connections (if applicable) are functioning correctly
- Key application routes and pages load as expected
- Any authentication or authorization flows work correctly

### 5. Review Target Framework

Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to a currently supported version of .NET, such as `net8.0`. Microsoft's [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy) can be used as a reference.

### 6. Check for Deprecated or Compatibility APIs

Run the .NET Upgrade Assistant compatibility analyzer or the API compatibility tool to identify any usage of APIs that may be deprecated or behave differently on cross-platform .NET:

```bash
dotnet tool install -g dotnet-apicompat
```

Additionally, review any usages of `System.Web`, Windows-specific registry access, or platform-specific file path assumptions, as these are common sources of cross-platform runtime issues that do not always surface as build errors.

### 7. Validate Configuration and Environment Settings

- Confirm that `appsettings.json` and `appsettings.{Environment}.json` files contain the correct configuration values for the target environment.
- Ensure that any configuration previously stored in `Web.config` has been properly migrated to the ASP.NET Core configuration system.
- Verify that connection strings and environment-specific settings are accurate.

### 8. Publish the Application

Once the above steps are validated, publish the application to confirm the output is complete and correct:

```bash
dotnet publish src/Northwind.Web/Northwind.Web.csproj --configuration Release --output ./publish
```

Review the contents of the `./publish` directory to ensure all expected files, static assets, and dependencies are present.