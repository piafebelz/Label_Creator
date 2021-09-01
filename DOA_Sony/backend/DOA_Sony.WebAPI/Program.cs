using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreAutoMigrator;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DOA_Sony.BusinessLayer;
using DOA_Sony.BusinessLayer.Interfaces;
using DOA_Sony.DataLayer;

namespace DOA_Sony.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost server = BuildWebHost(args);
            using (var serviceScope = server.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<SonyServiceContext>();
                var seedService = serviceScope.ServiceProvider.GetService<ISeedService>();
                new AutoMigrator(dbContext, new BlacklistedSource[] { })
                    .Migrate(false, MigrationModelHashStorageMode.Database, () =>
                    {
                        seedService.SeedProductType();
                        seedService.SeedControl();
                        seedService.SeedProductTypeControl();
                    });
            }

            server.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
