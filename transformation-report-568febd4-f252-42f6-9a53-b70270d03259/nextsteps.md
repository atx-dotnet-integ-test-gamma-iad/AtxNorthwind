# Next Steps

## Issues resolved
- Transformed Northwind.Web.csproj to net8.0

The transformation appears to have completed successfully. No build errors were detected across any of the projects in the solution, including the most independent project `Northwind.Web`.

## Validation

### 1. Restore Dependencies
Run the following command from the solution root to ensure all NuGet packages are restored correctly:

```bash
dotnet restore
```

### 2. Build the Solution
Perform a full solution build to confirm the error-free state is consistent:

```bash
dotnet build --configuration Release
```

### 3. Run Unit Tests
If the solution contains test projects, execute them to verify that existing logic behaves as expected after the transformation:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

### 4. Run the Application Locally
Start the application and verify it runs as expected:

```bash
dotnet run --project src/Northwind.Web/Northwind.Web.csproj --configuration Release
```

Navigate to the application URL printed in the console output and confirm the expected pages and functionality are accessible.

### 5. Review Target Framework
Open `src/Northwind.Web/Northwind.Web.csproj` and confirm the `<TargetFramework>` element is set to the intended modern .NET version, for example:

```xml
<TargetFramework>net8.0</TargetFramework>
```

Repeat this check for all other projects in the solution.

### 6. Review Removed or Changed APIs
Check for any usage of APIs that were available in .NET Framework but have changed behavior in modern .NET. Pay particular attention to:

- `System.Web` usages, which are not available in modern .NET and should be replaced with ASP.NET Core equivalents.
- `HttpContext`, `HttpRequest`, and `HttpResponse` usages, ensuring they reference `Microsoft.AspNetCore.Http` types.
- Any reflection-based code or serialization logic that may behave differently across framework versions.

### 7. Verify Database Connectivity
If the application uses Entity Framework or direct database connections, confirm the connection strings in `appsettings.json` are correct and that the application can connect to the database:

```bash
dotnet ef database update --project src/Northwind.Web/Northwind.Web.csproj
```

### 8. Review Configuration Files
Ensure that `appsettings.json` and any environment-specific variants (`appsettings.Development.json`, etc.) contain all configuration values that were previously held in `Web.config` or `App.config`. The `System.Configuration.ConfigurationManager` approach used in .NET Framework is replaced by `Microsoft.Extensions.Configuration` in modern .NET.

### 9. Static Files and Web Assets
Confirm that static files such as CSS, JavaScript, and images are placed under the `wwwroot` folder, as this is the expected location for static assets in ASP.NET Core.

### 10. Deployment
Once all validation steps above pass, publish the application using the following command:

```bash
dotnet publish --configuration Release --output ./publish
```

Review the contents of the `./publish` folder and deploy them to the target hosting environment, such as IIS, Azure App Service, or a self-hosted environment. Ensure the hosting environment has the appropriate .NET runtime installed that matches the target framework of the application.