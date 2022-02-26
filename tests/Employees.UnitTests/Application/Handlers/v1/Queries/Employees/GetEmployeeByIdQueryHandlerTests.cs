using AutoFixture;
using Employees.Application.Handlers.v1.Queries.Employees;
using Employees.Domain.Interfaces;
using Employees.Domain.Models;
using Moq;
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
            int id = 1;

            Employee employee = new Fixture()
                .Build<Employee>()
                .With(x => x.Id, id)
                .Create();

            GetEmployeeByIdQuery getByIdQuery = new() { Id = id };
            _mockEmployeeRepository.Setup(x => x.GetByIdAsync(It.Is<int>(x => x == id), CancellationToken.None)).ReturnsAsync(employee);

            // Act
            Employee? found = await _sut.Handle(getByIdQuery, CancellationToken.None).ConfigureAwait(false);

            // Assert
            Assert.NotNull(found);
            Assert.Equal(id, found?.Id);
            _mockEmployeeRepository.Verify(x => x.GetByIdAsync(It.Is<int>(x => x == id), CancellationToken.None), Times.Once());
        }
    }
}
