using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Web.Data;
using Npgsql;

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
            Host = configuration["DatabaseSettings:Host"] ?? "myserver.postgres.database.azure.com",
            Database = configuration["DatabaseSettings:Database"] ?? "Northwind",
            Username = configuration["DatabaseSettings:Username"] ?? "admin",
            Password = configuration["DatabaseSettings:Password"] ?? "placeholder",
            SslMode = SslMode.Require,
            TrustServerCertificate = true
        };

        return builder.ConnectionString;
    }
}
