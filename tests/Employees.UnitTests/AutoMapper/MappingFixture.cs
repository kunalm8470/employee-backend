using AutoMapper;
using Employees.Api.Configurations;
using Employees.Api.Models.v1.Employee.Requests;

namespace Employees.UnitTests.AutoMapper
{
    public class MappingFixture
    {
        public IConfigurationProvider ConfigurationProvider { get; }
        public IMapper Mapper { get; }
        public CreateEmployee CreateEmployeeModel { get; }
        public UpdateEmployee UpdateCustomerModel { get; }

        public MappingFixture()
        {
            CreateEmployeeModel = new()
            {
                FirstName = "John",
                LastName = "Doe",
                Gender = 'm',
                Salary = 75421.12M,
                ManagerId = null,
                DepartmentId = 1
            };

            UpdateCustomerModel = new()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Gender = 'm',
                Salary = 87564.43M,
                ManagerId = 1,
                DepartmentId = 2
            };

            ConfigurationProvider = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }
    }
}
