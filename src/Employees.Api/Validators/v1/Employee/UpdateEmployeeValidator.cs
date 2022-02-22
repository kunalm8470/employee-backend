using Employees.Api.Models.v1.Employee.Requests;
using FluentValidation;

namespace Employees.Application.Validators.v1.Employee
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployee>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(e => e.FirstName)
                .NotNull()
                .WithMessage("The first name must be at least 2 character long");

            RuleFor(e => e.FirstName)
                .MinimumLength(2)
                .WithMessage("The first name must be at least 2 character long");

            RuleFor(e => e.FirstName)
                .MaximumLength(250)
                .WithMessage("The first name must be less than 250 characters");

            RuleFor(e => e.LastName)
                .NotNull()
                .WithMessage("The last name must be at least 2 character long");

            RuleFor(e => e.LastName)
                .MinimumLength(2)
                .WithMessage("The last name must be at least 2 character long");

            RuleFor(e => e.LastName)
                .MaximumLength(250)
                .WithMessage("The last name must be less than 250 characters");

            RuleFor(e => e.Gender)
                .Must(e => char.ToUpper(e) == 'M' || char.ToUpper(e) == 'F' || char.ToUpper(e) == 'O')
                .WithMessage("Gender can only be Male (M), Female (F) or Others (O)");

            RuleFor(e => e.Salary)
                .Must(e => e > 0)
                .WithMessage("Salary must be greater than 0");

            RuleFor(e => e.ManagerId)
                .Must(e => e > 0 || e == default(int?))
                .WithMessage("Invalid Manager Id");

            RuleFor(e => e.DepartmentId)
                .Must(e => e > 0)
                .WithMessage("Invalid Department Id");
        }
    }
}
