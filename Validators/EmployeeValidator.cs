using FluentValidation;
using MusicShopBackend.Models;

namespace MusicShopBackend.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidator()
        {
            RuleFor(f => f.FirstName).NotEmpty().Matches(@"^[a-zA-Z]+$")
                .WithMessage("First name must be defined and be characters only!");
            RuleFor(l => l.LastName).NotEmpty().Matches(@"^[a-zA-Z]+$")
                .WithMessage("Last name  must be defined and be characters only!");
            RuleFor(e => e.Email).NotEmpty().EmailAddress()
                .WithMessage("Email must be defined!");

        }
    }
}
