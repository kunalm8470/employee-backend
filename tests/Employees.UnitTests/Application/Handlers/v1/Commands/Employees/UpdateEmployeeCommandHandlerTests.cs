using AutoFixture;
using Employees.Application.Handlers.v1.Commands.Employees;
using Employees.Domain.Interfaces;
using Employees.Domain.Models;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Employees.UnitTests.Application.Handlers.v1.Commands.Employees
{
    public class UpdateEmployeeCommandHandlerTests
    {
        private readonly UpdateEmployeeCommandHandler _sut;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        public UpdateEmployeeCommandHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _sut = new UpdateEmployeeCommandHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCallUpdateAsync()
        {
            // Arrange
            Guid code = Guid.NewGuid();

            Employee employee = new Fixture()
                .Build<Employee>()
                .With(e => e.Code, code)
                .Create();

            UpdateEmployeeCommand updateEmployeeCommand = new() { Employee = employee };
            _mockEmployeeRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(employee);

            // Act
            await _sut.Handle(updateEmployeeCommand, CancellationToken.None).ConfigureAwait(false);

            // Assert
            _mockEmployeeRepository.Verify(x => x.UpdateAsync(It.IsAny<Employee>(), CancellationToken.None), Times.Once());
        }

        [Fact]
        public async Task Handle_ValidCommand_UpdateEmployee()
        {
            // Arrange
            Guid code = Guid.NewGuid();

            Employee employee = new Fixture()
            .Build<Employee>()
            .With(e => e.Code, code)
            .Create();

            UpdateEmployeeCommand updateEmployeeCommand = new() 
            { 
                Employee = new Employee
                {
                    Code = code,
                    FirstName = "Jane",
                    LastName = "Doe",
                    GenderAbbreviation = 'F',
                    Salary = 70000.01M,
                    ManagerId = default,
                    DepartmentId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            _mockEmployeeRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync(employee);
            _mockEmployeeRepository.Setup(x => x.UpdateAsync(
                    It.Is<Employee>(em => em.Code == updateEmployeeCommand.Employee.Code),
                    CancellationToken.None
                )
            ).Returns(Task.CompletedTask);

            // Act
            await _sut.Handle(updateEmployeeCommand, CancellationToken.None).ConfigureAwait(false);

            // Assert
            _mockEmployeeRepository.VerifyAll();
        }
    }
}
