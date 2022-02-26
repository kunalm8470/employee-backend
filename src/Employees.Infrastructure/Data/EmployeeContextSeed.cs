using Employees.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Employees.Infrastructure.Data
{
    public class EmployeeContextSeed
    {
        public static async Task SeedAsync(EmployeesContext context, ILogger logger)
        {
            context.Database.EnsureDeleted();

            logger.LogInformation("Migration started at {0}", DateTime.UtcNow);
            context.Database.EnsureCreated();
            logger.LogInformation("Migration ended at {0}", DateTime.UtcNow);

            logger.LogInformation("Seeding department data started at {0}", DateTime.UtcNow);
            await context.Departments.AddRangeAsync(GetPreconfiguredDepartments()).ConfigureAwait(false);
            await context.Employees.AddRangeAsync(GetPreconfiguredEmployees()).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
            logger.LogInformation("Seeding department data ended at {0}", DateTime.UtcNow);
        }

        public static IEnumerable<Department> GetPreconfiguredDepartments()
        {
            yield return new Department("Board", DateTime.UtcNow, default);
            yield return new Department("Human Resources", DateTime.UtcNow, default);
            yield return new Department("Finance", DateTime.UtcNow, default);
            yield return new Department("Admin", DateTime.UtcNow, default);
            yield return new Department("Engineering", DateTime.UtcNow, default);
        }

        public static IEnumerable<Employee> GetPreconfiguredEmployees()
        {
            yield return new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "John.Doe@fakecompany.com",
                GenderAbbreviation = 'M',
                Salary = 40005.11M,
                ManagerId = default,
                DepartmentId = 1,
                CreatedAt = DateTime.UtcNow
            };

            yield return new Employee
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "Jane.Doe@fakecompany.com",
                GenderAbbreviation = 'F',
                Salary = 38922.14M,
                ManagerId = 1,
                DepartmentId = 1,
                CreatedAt = DateTime.UtcNow
            };

            yield return new Employee
            {
                FirstName = "Janey",
                LastName = "Doe",
                Email = "Janey.Doe@fakecompany.com",
                GenderAbbreviation = 'F',
                Salary = 22454.09M,
                ManagerId = 2,
                DepartmentId = 2,
                CreatedAt = DateTime.UtcNow
            };

            yield return new Employee
            {
                FirstName = "Joe",
                LastName = "Doe",
                Email = "Joe.Doe@fakecompany.com",
                GenderAbbreviation = 'M',
                Salary = 12388.55M,
                ManagerId = 2,
                DepartmentId = 5,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
