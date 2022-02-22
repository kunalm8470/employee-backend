using Employees.Domain.Exceptions;
using Employees.Domain.Interfaces;
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
                if (await _employeeRepository.GetByIdAsync(request.Employee.Id, cancellationToken).ConfigureAwait(false) == default)
                {
                    throw new ItemNotFoundException();
                }

                await _employeeRepository.UpdateAsync(request.Employee, cancellationToken).ConfigureAwait(false);
                return Unit.Value;
            }
            catch (DbUpdateException ex)
            {
                throw new FailedItemUpdateException("Couldn't update employee due to an error", ex);
            }
        }
    }
}
