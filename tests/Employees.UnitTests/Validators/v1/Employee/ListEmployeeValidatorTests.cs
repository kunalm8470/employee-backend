using Employees.Api.Models.v1.Employee.Requests;
using Employees.Api.Validators.v1.Employee;
using FluentValidation.TestHelper;
using Xunit;

namespace Employees.UnitTests.Validators.v1.Employee
{
    public class ListEmployeeValidatorTests
    {
        private readonly ListEmployeeValidator _sut;

        public ListEmployeeValidatorTests()
        {
            _sut = new ListEmployeeValidator();
        }

        [Theory]
        [MemberData(nameof(EmployeeTestDataGenerator.InvalidPageAndLimit), MemberType = typeof(EmployeeTestDataGenerator))]
        public void PageAndLimit_WhenLessThanOrEqualToZero_ShouldHaveValidationError(int page, int limit)
        {
            // Arrange
            ListEmployee model = new()
            { 
                Page = page,
                Limit = limit
            };

            // Act
            var result = _sut.TestValidate(model);

            // Assert
            _ = result.ShouldHaveValidationErrorFor(x => x.Page);
            _ = result.ShouldHaveValidationErrorFor(x => x.Limit);
        }
    }
}
