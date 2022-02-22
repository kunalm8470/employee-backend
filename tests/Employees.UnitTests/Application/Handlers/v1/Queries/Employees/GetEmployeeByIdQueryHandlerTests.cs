using AutoFixture;
using Employees.Application.Handlers.v1.Queries.Employees;
using Employees.Domain.Interfaces;
using Employees.Domain.Models;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Employees.UnitTests.Application.Handlers.v1.Queries.Employees
{
    public class GetEmployeeByIdQueryHandlerTests
    {
        private readonly GetEmployeeByIdQueryHandler _sut;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        public GetEmployeeByIdQueryHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _sut = new GetEmployeeByIdQueryHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ShouldReturnEmployee()
        {
            // Arrange
            Guid code = Guid.NewGuid();

            Employee employee = new Fixture()
                .Build<Employee>()
                .With(e => e.Code, code)
                .Create();

            GetEmployeeByIdQuery getByIdQuery = new() { Id = It.IsAny<int>() };
            _mockEmployeeRepository.Setup(x => x.GetByIdAsync(getByIdQuery.Id, CancellationToken.None)).ReturnsAsync(employee);

            // Act
            Employee? found = await _sut.Handle(getByIdQuery, CancellationToken.None).ConfigureAwait(false);

            // Assert
            Assert.NotNull(found);
            Assert.Equal(code, found?.Code);
            _mockEmployeeRepository.VerifyAll();
        }
    }
}
