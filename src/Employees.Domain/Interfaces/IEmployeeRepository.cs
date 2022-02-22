using Employees.Domain.Models;

namespace Employees.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<(List<Employee>, int)> GetByPageAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
