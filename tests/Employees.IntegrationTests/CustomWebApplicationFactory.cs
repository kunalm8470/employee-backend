using Employees.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Employees.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                ServiceDescriptor descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EmployeesContext>))
                ?? throw new ArgumentNullException("Could not find the DB context");

                // Remove real DB context
                services.Remove(descriptor);

                services.AddDbContext<EmployeesContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using IServiceScope scope = sp.CreateScope();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                EmployeesContext db = scope.ServiceProvider.GetRequiredService<EmployeesContext>();

                logger.LogInformation("Migration started at {0}", DateTime.UtcNow);
                
                logger.LogInformation("Migration ended at {0}", DateTime.UtcNow);

                try
                {
                    logger.LogInformation("Seeding data started at {0}", DateTime.UtcNow);
                    Utilities.ReinitializeDbForTests(db);
                    logger.LogInformation("Seeding data ended at {0}", DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}", ex.Message);
                }
            });
        }
    }
}
