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
        public async Task Handle_ValidCommand_UpdateEmployee()
        {
            // Arrange
            int id = 1;

            Employee employee = new Fixture()
            .Build<Employee>()
            .Create();

            UpdateEmployeeCommand updateEmployeeCommand = new() 
            { 
                Id = id,
                Employee = new Employee
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "Jane.Doe@fakecompany.com",
                    GenderAbbreviation = 'F',
                    Salary = 70000.01M,
                    ManagerId = default,
                    DepartmentId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            _mockEmployeeRepository.Setup(x => x.GetByIdAsync(It.Is<int>(x => x == id), CancellationToken.None)).ReturnsAsync(employee);
            _mockEmployeeRepository.Setup(x => x.UpdateAsync(
                    It.Is<Employee>(em => em.Id == updateEmployeeCommand.Id),
                    CancellationToken.None
                )
            ).Returns(Task.CompletedTask);

            // Act
            await _sut.Handle(updateEmployeeCommand, CancellationToken.None).ConfigureAwait(false);

            // Assert
            _mockEmployeeRepository.Verify(x => x.UpdateAsync(It.IsAny<Employee>(), CancellationToken.None), Times.Once());
        }
    }
}
