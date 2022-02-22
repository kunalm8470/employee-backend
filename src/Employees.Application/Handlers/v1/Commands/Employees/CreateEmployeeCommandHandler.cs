using Employees.Domain.Exceptions;
using Employees.Domain.Interfaces;
using Employees.Domain.Models;
using EntityFramework.Exceptions.Common;
using MediatR;

namespace Employees.Application.Handlers.v1.Commands.Employees
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return _employeeRepository.AddAsync(request.Employee, cancellationToken);
            }
            catch (UniqueConstraintException ex)
            {
                throw new DuplicateItemException("Employee already exists with same employee code", ex);
            }
            
        }
    }
}
