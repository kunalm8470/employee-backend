using AutoMapper;
using Employees.Api.Models.v1.Employee.Requests;
using Employees.Api.Models.v1.Employee.Responses;
using Employees.Application.Handlers.v1.Queries.Employees;
using Employees.Domain.Constants;
using Employees.Domain.Extensions;
using Employees.Domain.Models;

namespace Employees.Api.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            _ = CreateMap<ListEmployee, GetEmployeesQuery>();

            _ = CreateMap<CreateEmployee, Employee>()
                .ForMember(dest => dest.Id, options => options.Ignore())
                .ForMember(dest => dest.GenderAbbreviation, options => options.MapFrom(src => char.ToUpper(src.Gender)))
                .ForMember(dest => dest.Manager, options => options.Ignore())
                .ForMember(dest => dest.Department, options => options.Ignore())
                .ForMember(dest => dest.CreatedAt, options => options.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, options => options.MapFrom(_ => default(DateTime?)));

            _ = CreateMap<UpdateEmployee, Employee>()
                .ForMember(dest => dest.Id, options => options.Ignore())
                .ForMember(dest => dest.GenderAbbreviation, options => options.MapFrom(src => char.ToUpper(src.Gender)))
                .ForMember(dest => dest.Email, options => options.Ignore())
                .ForMember(dest => dest.Manager, options => options.Ignore())
                .ForMember(dest => dest.Department, options => options.Ignore())
                .ForMember(dest => dest.CreatedAt, options => options.Ignore())
                .ForMember(dto => dto.UpdatedAt, options => options.MapFrom(_ => DateTime.UtcNow));

            _ = CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, options => options.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, options => options.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.GenderAbbreviation, options => options.MapFrom(src => src.GenderAbbreviation))
                .ForMember(dest => dest.Salary, options => options.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Department, options => options.MapFrom(src => src.Department == default(Department?)
                ? default
                : new EmployeeDepartment
                {
                    Id = src.Department.Id,
                    Name = src.Department.Name
                }))
                .ForMember(dest => dest.Manager, options => options.MapFrom(src => (src.Manager == default(Employee?))
                ? default
                : new EmployeeManager
                {
                    Id = src.Manager.Id,
                    Email = src.Manager.Email,
                    FirstName = src.Manager.FirstName,
                    LastName = src.Manager.LastName,
                    Department = (src.Manager.Department == default)
                    ? default
                    : new EmployeeDepartment
                    {
                        Id = src.Manager.Department.Id,
                        Name = src.Manager.Department.Name
                    }
                }))
                .ForMember(dest => dest.CreatedAt, options => options.MapFrom(src => src.CreatedAt.ConvertToLocalFromUTC(TimeZoneConstants.INDIA_TIMEZONE_ID)))
                .ForMember(dest => dest.UpdatedAt, options => options.MapFrom(src => src.UpdatedAt.HasValue
                        ? src.UpdatedAt.Value.ConvertToLocalFromUTC(TimeZoneConstants.INDIA_TIMEZONE_ID)
                        : default(DateTime?)));
        }
    }
}
