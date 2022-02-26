using AutoFixture;
using Employees.Application.Handlers.v1.Queries.Employees;
using Employees.Domain.Interfaces;
using Employees.Domain.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Employees.UnitTests.Application.Handlers.v1.Queries.Employees
{
    public class GetEmployeesQueryHandlerTests
    {
        private readonly GetEmployeesQueryHandler _sut;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        public GetEmployeesQueryHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _sut = new GetEmployeesQueryHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_EmployeesReturned()
        {
            // Arrange
            int count = 10;
            int page = 1;
            int limit = 10;

            GetEmployeesQuery query = new() { Page = page, Limit = limit };

            List<Employee> employees = new Fixture()
                .Build<Employee>()
                .CreateMany(count)
                .ToList();
                
            _mockEmployeeRepository.Setup(x => x.GetByPageAsync(page, limit, CancellationToken.None)).ReturnsAsync((employees, count));

            // Act
            var (list, total) = await _sut.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(list);
            Assert.Equal(count, list.Count);
            Assert.All(list, (x) => Assert.IsType<Employee>(x));
            _mockEmployeeRepository.Verify(x => x.GetByPageAsync(page, limit, CancellationToken.None), Times.Once());
        }
    }
}
