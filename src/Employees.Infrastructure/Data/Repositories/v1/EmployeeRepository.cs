using Employees.Domain.Interfaces;
using Employees.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Infrastructure.Data.Repositories.v1
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeesContext context) : base(context)
        {
        }

        public Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return FirstOrDefaultAsync(
                predicate: em => em.Id == id,
                include: em => em.Include(e => e.Manager)
                                .Include(e => e.Department),
                CancellationToken.None);
        }

        public async Task<(List<Employee>, int)> GetByPageAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            int skip = (page - 1) * limit;

            List<Employee> employees = await ListAsync(skip, 
                limit,
                predicate: default,
                include: em => em.Include(e => e.Manager)
                                 .Include(e => e.Department),
                orderBy: em => em.OrderBy(e => e.Id),
                cancellationToken)
                .ConfigureAwait(false);

            int count = await CountAsync(predicate: default,
                include: default,
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return (employees, count);
        }
    }
}
