using Employees.Domain.Models;
using MediatR;

namespace Employees.Application.Handlers.v1.Commands.Employees
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public Employee Employee { get; set; }
    }
}
