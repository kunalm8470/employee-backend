using Employees.Infrastructure.Data;

namespace Employees.IntegrationTests
{
    public class Utilities
    {
        public static void InitializeDbForTests(EmployeesContext db)
        {
            db.Database.EnsureCreated();

            // Seed departments
            db.Departments.AddRange(EmployeeContextSeed.GetPreconfiguredDepartments());

            // Seed employees
            db.Employees.AddRange(EmployeeContextSeed.GetPreconfiguredEmployees());

            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(EmployeesContext db)
        {
            db.Database.EnsureDeleted();

            InitializeDbForTests(db);
        }
    }
}
