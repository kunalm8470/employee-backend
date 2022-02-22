using Employees.Api.Models.v1.Employee.Requests;
using FluentValidation;

namespace Employees.Api.Validators.v1.Employee
{
    public class ListEmployeeValidator : AbstractValidator<ListEmployee>
    {
        public ListEmployeeValidator()
        {
            RuleFor(r => r.Page)
                .NotNull()
                .Must(x => x > 0)
                .WithMessage("Page cannot be less than 0");

            RuleFor(r => r.Limit)
                .NotNull()
                .Must(x => x > 0)
                .WithMessage("Limit cannot be less than 0");
        }
    }
}
