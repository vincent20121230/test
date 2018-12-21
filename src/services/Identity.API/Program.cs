using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using API.Customization;
using IdentityServer4.EntityFramework.DbContexts;
using Identity.API.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()    
                .MigrateDbContext<PersistedGrantDbContext>((a,b) => { })
                .MigrateDbContext<ApplicationDbContext>((context, services) => {
                    var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();

                    new ApplicationDbContextSeed().SeedAsync(context, logger).Wait();
                })
                .MigrateDbContext<ConfigurationDbContext>((context, service)=> { })
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseKestrel()
            .UseIISIntegration()
            .UseContentRoot(Directory.GetCurrentDirectory());
    }
}
