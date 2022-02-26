using Employees.Domain.Models;
using MediatR;

namespace Employees.Application.Handlers.v1.Commands.Employees
{
    public class UpdateEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
    }
}
