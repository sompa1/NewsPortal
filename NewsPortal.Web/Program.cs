using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewsPortal.Web.Hosting;
using NewsPortal.Dal;

namespace NewsPortal.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            (await CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<NewsPortalDbContext>())
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
