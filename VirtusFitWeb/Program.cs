using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using VirtusFitWeb.Services;

namespace VirtusFitWeb
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           // .AddJsonFile(
           //     $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
          //      optional: true)
            .Build();

        public static void Main(string[] args)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            var columnOptions = new ColumnOptions()
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn("User", SqlDbType.VarChar),
                    new SqlColumn("Date time", SqlDbType.DateTime)
                }
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(connectionString, new MSSqlServerSinkOptions { TableName = "Exceptions", AutoCreateSqlTable = true }, restrictedToMinimumLevel: LogEventLevel.Fatal, columnOptions: columnOptions)
                //sinkOptions:
                .Enrich.With<LogEnricher>()
                .CreateLogger();


            CreateHostBuilder(args).Build().Run();

            

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
