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
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly CreateEmployeeCommandHandler _sut;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        public CreateEmployeeCommandHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _sut = new CreateEmployeeCommandHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCallAddAsync()
        {
            // Arrange
            CreateEmployeeCommand createEmployeeCommand = new() { Employee = It.IsAny<Employee>() };

            // Act
            await _sut.Handle(createEmployeeCommand, CancellationToken.None).ConfigureAwait(false);

            // Assert
            _mockEmployeeRepository.Verify(x => x.AddAsync(It.IsAny<Employee>(), CancellationToken.None), Times.Once());
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnCreatedEmployee()
        {
            // Arrange
            Guid code = Guid.NewGuid();

            Employee employee = new()
            {
                Code = code,
                FirstName = "John",
                LastName = "Doe",
                GenderAbbreviation = 'M',
                Salary = 70000.01M,
                ManagerId = default,
                DepartmentId = 1,
                CreatedAt = DateTime.UtcNow
            };

            CreateEmployeeCommand createEmployeeCommand = new() { Employee = employee };

            _mockEmployeeRepository.Setup(x => x.AddAsync(It.Is<Employee>(e => e.Code == code), CancellationToken.None)).ReturnsAsync(employee);

            // Act
            Employee result = await _sut.Handle(createEmployeeCommand, CancellationToken.None).ConfigureAwait(false);

            // Assert
            _mockEmployeeRepository.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal(code, result.Code);
        }
    }
}
