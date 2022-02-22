using Employees.Domain.Interfaces;
using Employees.Domain.Models;
using MediatR;

namespace Employees.Application.Handlers.v1.Queries.Employees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, (List<Employee>, int)>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<(List<Employee>, int)> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return _employeeRepository.GetByPageAsync(request.Page, request.Limit, cancellationToken);
        }
    }
}
