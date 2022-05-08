using FluentValidation;
using MusicShopBackend.Models;
using System.Collections.Generic;

namespace MusicShopBackend.Validators
{
    public class RoleValidator : AbstractValidator<RoleDto> 
    {
        public RoleValidator()
        {
            List<string> roles = new List<string>() { "User", "Employee", "Admin" };

            RuleFor(x => x.RoleName).Must(x => roles.Contains(x)).NotEmpty()
                .WithMessage("Roles can only be admin, user, or employee and cannot be null!");
        }

    }
}
