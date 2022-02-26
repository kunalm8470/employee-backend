using Employees.Domain.Exceptions;
using Employees.Domain.Interfaces;
using Employees.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employees.Application.Handlers.v1.Commands.Employees
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Employee? found = await _employeeRepository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
                if (found == default)
                {
                    throw new ItemNotFoundException();
                }

                found.FirstName = request.Employee.FirstName;
                found.LastName = request.Employee.LastName;
                found.GenderAbbreviation = request.Employee.GenderAbbreviation;
                found.Salary = request.Employee.Salary;
                found.ManagerId = request.Employee.ManagerId;
                found.DepartmentId = request.Employee.DepartmentId;

                await _employeeRepository.UpdateAsync(found, cancellationToken).ConfigureAwait(false);
                return Unit.Value;
            }
            catch (DbUpdateException ex)
            {
                throw new FailedItemUpdateException("Couldn't update employee due to an error", ex);
            }
        }
    }
}
