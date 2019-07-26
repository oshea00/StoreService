using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StoreService.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreService.Web
{
    public static class MigrateDatabase
    {
        public static IWebHost Migrate (this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                using (var context = services.GetRequiredService<StoreContext>())
                {
                    try
                    {
                        logger.LogInformation("Checking and running required migrations");
                        context.Database.Migrate();
                        // Seed data
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error while migrating db.");
                        throw;
                    }
                }
            }
            return webHost;
        }
    }
}
