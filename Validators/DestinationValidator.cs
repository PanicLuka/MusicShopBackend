using FluentValidation;
using MusicShopBackend.Models;

namespace MusicShopBackend.Validators
{
    public class DestinationValidator : AbstractValidator<DestinationAddressDto>
    {
        public DestinationValidator()
        {
            RuleFor(c => c.City).NotEmpty().WithMessage("City must be defined!");
            RuleFor(z => z.ZipCode).NotEmpty().Matches(@"^[0-9]+$").Length(5)
                .WithMessage("Zip code must be 5 characters long and contain only numbers!");
            RuleFor(c => c.Country).NotEmpty().WithMessage("Country must be defined!");
            RuleFor(p => p.PhoneNumber).NotEmpty().Matches(@"^[0-9]+$")
                .WithMessage("Phone number must contain from numbers only and cannot be empty!");
            RuleFor(a => a.Address).NotEmpty().WithMessage("Address must be defined!");
        }
    }
}
