using FluentValidation;
using MusicShopBackend.Models;

namespace MusicShopBackend.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty().WithMessage("Category name must be defined!");

        }
    }
}
