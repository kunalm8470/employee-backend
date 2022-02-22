using Employees.Domain.Interfaces;
using Employees.Domain.Models;
using MediatR;

namespace Employees.Application.Handlers.v1.Queries.Employees
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee?>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Employee?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return _employeeRepository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
