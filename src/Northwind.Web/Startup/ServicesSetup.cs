using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Northwind.Web.Data;

namespace Northwind.Web.Startup;

public static class ServicesSetup
{
    public static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddControllers();

        services.AddDbContext<NorthwindDbContext>(option =>
            option.UseSqlServer(GetDatabaseConnectionString(configuration)));
    }

    private static string GetDatabaseConnectionString(ConfigurationManager configuration)
    {
        var secretName = configuration["dbsecretsname"] ?? "atx-db-modernization-secret-sql-admin";

        var builder = new SqlConnectionStringBuilder
        {
            DataSource = "myserver.database.windows.net",
            InitialCatalog = "Northwind",
            IntegratedSecurity = false,
            MultipleActiveResultSets = true,
            TrustServerCertificate = true,
            UserID = "admin",
            Password = "placeholder"
        };

        return builder.ConnectionString;
    }
}
