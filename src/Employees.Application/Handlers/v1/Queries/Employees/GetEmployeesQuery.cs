using Employees.Domain.Models;
using MediatR;

namespace Employees.Application.Handlers.v1.Queries.Employees
{
    public class GetEmployeesQuery : IRequest<(List<Employee>, int)>
    {
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
