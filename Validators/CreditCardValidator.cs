using FluentValidation;
using MusicShopBackend.Models;

namespace MusicShopBackend.Validators
{
    public class CreditCardValidator : AbstractValidator<CreditCardDto>
    {
        public CreditCardValidator()
        {
            RuleFor(n => n.CreditCardNumber).NotEmpty().Matches(@"^[0-9]+$").Length(16)
                .WithMessage("Credit card number must contain only numbers and be 16 characters long!");
            RuleFor(c => c.Cvv).NotEmpty().LessThanOrEqualTo(999);
            RuleFor(e => e.ExpireDate).NotEmpty().WithMessage("Expire date cannot be empty!");
        }
    }
}
