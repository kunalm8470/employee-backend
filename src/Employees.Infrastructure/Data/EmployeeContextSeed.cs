using Employees.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Employees.Infrastructure.Data
{
    public class EmployeeContextSeed
    {
        public static async Task SeedAsync(EmployeesContext context, ILogger logger)
        {
            logger.LogInformation("Migration started at {0}", DateTime.UtcNow);
            context.Database.EnsureCreated();
            logger.LogInformation("Migration ended at {0}", DateTime.UtcNow);

            if (!await context.Departments.AnyAsync().ConfigureAwait(false))
            {
                logger.LogInformation("Seeding department data started at {0}", DateTime.UtcNow);
                await context.Departments.AddRangeAsync(GetPreconfiguredDepartments()).ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
                logger.LogInformation("Seeding department data ended at {0}", DateTime.UtcNow);
            }
        }

        static IEnumerable<Department> GetPreconfiguredDepartments()
        {
            yield return new Department("Board", DateTime.UtcNow, default);
            yield return new Department("Human Resources", DateTime.UtcNow, default);
            yield return new Department("Finance", DateTime.UtcNow, default);
            yield return new Department("Admin", DateTime.UtcNow, default);
            yield return new Department("Engineering", DateTime.UtcNow, default);
        }
    }
}
