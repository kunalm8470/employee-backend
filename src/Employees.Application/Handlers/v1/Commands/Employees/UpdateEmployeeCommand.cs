using Employees.Domain.Models;
using MediatR;

namespace Employees.Application.Handlers.v1.Commands.Employees
{
    public class UpdateEmployeeCommand : IRequest<Unit>
    {
        public Employee Employee { get; set; }
    }
}
