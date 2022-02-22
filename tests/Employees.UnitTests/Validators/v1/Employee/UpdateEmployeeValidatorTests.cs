using Employees.Api.Models.v1.Employee.Requests;
using Employees.Application.Validators.v1.Employee;
using FluentValidation.TestHelper;
using Xunit;

namespace Employees.UnitTests.Validators.v1.Employee
{
    public class UpdateEmployeeValidatorTests
    {
        private readonly UpdateEmployeeValidator _sut;

        public UpdateEmployeeValidatorTests()
        {
            _sut = new UpdateEmployeeValidator();
        }

        [Theory]
        [MemberData(nameof(EmployeeTestDataGenerator.ValidFirstNameAndLastName), MemberType = typeof(EmployeeTestDataGenerator))]
        public void FirstNameAndLastName_WhenMoreThanOrEqualToTwoCharacters_ShouldNotHaveValidationError(string firstName, string lastName)
        {
            // Arrange
            UpdateEmployee model = new()
            {
                FirstName = firstName,
                LastName = lastName
            };

            // Act
            var result = _sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
        }

        [Theory]
        [MemberData(nameof(EmployeeTestDataGenerator.InvalidFirstNameAndLastName), MemberType = typeof(EmployeeTestDataGenerator))]
        public void FirstNameAndLastName_WhenLessThanTwoCharactersOrInvalid_ShouldHaveValidationError(string firstName, string lastName)
        {
            // Arrange
            UpdateEmployee model = new()
            {
                FirstName = firstName,
                LastName = lastName
            };

            // Act
            var result = _sut.TestValidate(model);

            // Assert
            _ = result.ShouldHaveValidationErrorFor(x => x.FirstName);
            _ = result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Theory]
        [MemberData(nameof(EmployeeTestDataGenerator.ValidGenders), MemberType = typeof(EmployeeTestDataGenerator))]
        public void Gender_WhenGivenValidKnownValues_ShouldNotHaveValidationError(char gender)
        {
            // Arrange
            UpdateEmployee model = new()
            {
                Gender = gender
            };

            // Act
            var result = _sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Gender);
        }


        [Theory]
        [MemberData(nameof(EmployeeTestDataGenerator.InvalidGenders), MemberType = typeof(EmployeeTestDataGenerator))]
        public void Gender_WhenGivenInvalidValues_ShouldHaveValidationError(char gender)
        {
            // Arrange
            UpdateEmployee model = new()
            {
                Gender = gender
            };

            // Act
            var result = _sut.TestValidate(model);

            // Assert
            _ = result.ShouldHaveValidationErrorFor(x => x.Gender);
        }

        [Theory]
        [MemberData(nameof(EmployeeTestDataGenerator.ValidSalaries), MemberType = typeof(EmployeeTestDataGenerator))]
        public void Salary_WhenGivenValidValues_ShouldNotHaveValidationError(decimal salary)
        {
            // Arrange
            UpdateEmployee model = new()
            {
                Salary = salary
            };

            // Act
            var result = _sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Salary);
        }


        [Theory]
        [MemberData(nameof(EmployeeTestDataGenerator.InvalidSalaries), MemberType = typeof(EmployeeTestDataGenerator))]
        public void Salary_WhenGivenInvalidSalaries_ShouldHaveValidationError(decimal salary)
        {
            // Arrange
            UpdateEmployee model = new()
            {
                Salary = salary
            };

            // Act
            var result = _sut.TestValidate(model);

            // Assert
            _ = result.ShouldHaveValidationErrorFor(x => x.Salary);
        }

        [Theory]
        [MemberData(nameof(EmployeeTestDataGenerator.ValidManagerId), MemberType = typeof(EmployeeTestDataGenerator))]
        public void ManagerId_WhenGivenValidValues_ShouldNotHaveValidationError(int? managerId)
        {
            // Arrange
            UpdateEmployee model = new()
            {
                ManagerId = managerId
            };

            // Act
            var result = _sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ManagerId);
        }


        [Theory]
        [MemberData(nameof(EmployeeTestDataGenerator.InvalidManagerId), MemberType = typeof(EmployeeTestDataGenerator))]
        public void ManagerId_WhenGivenInvalidSalaries_ShouldHaveValidationError(int? managerId)
        {
            // Arrange
            UpdateEmployee model = new()
            {
                ManagerId = managerId
            };

            // Act
            var result = _sut.TestValidate(model);

            // Assert
            _ = result.ShouldHaveValidationErrorFor(x => x.ManagerId);
        }

        [Theory]
        [MemberData(nameof(EmployeeTestDataGenerator.ValidDepartmentId), MemberType = typeof(EmployeeTestDataGenerator))]
        public void DepartmentId_WhenGivenValidValues_ShouldNotHaveValidationError(int departmentId)
        {
            // Arrange
            UpdateEmployee model = new()
            {
                DepartmentId = departmentId
            };

            // Act
            var result = _sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.DepartmentId);
        }


        [Theory]
        [MemberData(nameof(EmployeeTestDataGenerator.InvalidDepartmentId), MemberType = typeof(EmployeeTestDataGenerator))]
        public void DepartmentId_WhenGivenInvalidSalaries_ShouldHaveValidationError(int departmentId)
        {
            // Arrange
            UpdateEmployee model = new()
            {
                DepartmentId = departmentId
            };

            // Act
            var result = _sut.TestValidate(model);

            // Assert
            _ = result.ShouldHaveValidationErrorFor(x => x.DepartmentId);
        }
    }
}
