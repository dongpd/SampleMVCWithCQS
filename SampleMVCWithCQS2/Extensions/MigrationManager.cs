using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using SampleMVCWithCQS2Core.DataAccess;
using System;
namespace SampleMVCWithCQS2.Migrations
{
    public static class MigrationManager
    {
        public static IWebHost MigrationDatabase(this IWebHost host)
        {
            // Migrate
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }
            return host;
        }
    }
}