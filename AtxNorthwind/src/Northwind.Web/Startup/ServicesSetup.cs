using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Web.Data;

namespace Northwind.Web.Startup;

public static class ServicesSetup
{
    public static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddControllers();

        services.AddDbContext<NorthwindDbContext>(option =>
            option.UseNpgsql(GetDatabaseConnectionString(configuration)));
    }

    private static string GetDatabaseConnectionString(ConfigurationManager configuration)
    {
        var secretName = configuration["dbsecretsname"] ?? "atx-db-modernization-secret-sql-admin";

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = "myserver.database.windows.net",
            Database = "Northwind",
            Username = "admin",
            Password = "placeholder"
        };

        return builder.ConnectionString;
    }
}
