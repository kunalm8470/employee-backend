using Employees.Domain.Models;
using MediatR;

namespace Employees.Application.Handlers.v1.Queries.Employees
{
    public class GetEmployeeByIdQuery : IRequest<Employee?>
    {
        public int Id { get; set; }
    }
}
