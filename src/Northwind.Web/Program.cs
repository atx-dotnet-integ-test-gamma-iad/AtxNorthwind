using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
Northwind.Web.Startup.ServicesSetup.ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();
app.MapControllers();
app.Run();
