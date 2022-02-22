using Employees.Domain.Exceptions;
using Employees.Domain.Interfaces;
using Employees.Domain.Models;
using MediatR;

namespace Employees.Application.Handlers.v1.Commands.Employees
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee? found = await _employeeRepository.GetByIdAsync(request.Id).ConfigureAwait(false);

            if (found == default)
            {
                throw new ItemNotFoundException();
            }

            await _employeeRepository.DeleteAsync(found, cancellationToken).ConfigureAwait(false);
            return Unit.Value;
        }
    }
}
