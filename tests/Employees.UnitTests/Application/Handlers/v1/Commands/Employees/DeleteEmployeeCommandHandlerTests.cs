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
    public class DeleteEmployeeCommandHandlerTests
    {
        private readonly DeleteEmployeeCommandHandler _sut;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        public DeleteEmployeeCommandHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _sut = new DeleteEmployeeCommandHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCallDeleteAsync()
        {
            // Arrange
            Employee employee = new Fixture()
                .Build<Employee>()
                .Create();

            DeleteEmployeeCommand updateEmployeeCommand = new() { Id = It.IsAny<int>() };
            _mockEmployeeRepository.Setup(x => x.GetByIdAsync(updateEmployeeCommand.Id, CancellationToken.None)).ReturnsAsync(employee);

            // Act
            await _sut.Handle(updateEmployeeCommand, CancellationToken.None).ConfigureAwait(false);

            // Assert
            _mockEmployeeRepository.Verify(x => x.DeleteAsync(employee, CancellationToken.None), Times.Once());
        }

        [Fact]
        public async Task Handle_ValidCommand_DeleteEmployee()
        {
            // Arrange
            int id = 1;

            Employee employee = new Fixture()
                .Build<Employee>()
                .Create();

            DeleteEmployeeCommand deleteEmployeeCommand = new() { Id = It.IsAny<int>() };

            _mockEmployeeRepository.Setup(x => x.GetByIdAsync(deleteEmployeeCommand.Id, CancellationToken.None)).ReturnsAsync(employee);
            _mockEmployeeRepository.Setup(x => x.DeleteAsync(
                    It.Is<Employee>(em => em.Id == id),
                    CancellationToken.None
                )
            ).Returns(Task.CompletedTask);

            // Act
            await _sut.Handle(deleteEmployeeCommand, CancellationToken.None).ConfigureAwait(false);

            // Assert
            _mockEmployeeRepository.Verify(x => x.DeleteAsync(employee, CancellationToken.None), Times.Once());
        }
    }
}
