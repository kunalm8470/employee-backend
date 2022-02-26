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
            string email = "John.Doe@fakecompany.com";

            Employee employee = new()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = email,
                GenderAbbreviation = 'M',
                Salary = 70000.01M,
                ManagerId = default,
                DepartmentId = 1,
                CreatedAt = DateTime.UtcNow
            };

            CreateEmployeeCommand createEmployeeCommand = new() { Employee = employee };

            _mockEmployeeRepository.Setup(x => x.AddAsync(It.Is<Employee>(e => e.Email == email), CancellationToken.None)).ReturnsAsync(employee);

            // Act
            Employee result = await _sut.Handle(createEmployeeCommand, CancellationToken.None).ConfigureAwait(false);

            // Assert
            _mockEmployeeRepository.Verify(x => x.AddAsync(employee, CancellationToken.None), Times.Once());
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
        }
    }
}
