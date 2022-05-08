using FluentValidation;
using MusicShopBackend.Models;

namespace MusicShopBackend.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Product name must be defined!");
            RuleFor(p => p.ProductPrice).NotEmpty();
            RuleFor(a => a.EmployeeId).NotEmpty().WithMessage("Employee must be defined!");
            RuleFor(a => a.CategoryId).NotEmpty().WithMessage("Category must be defined!");
            RuleFor(a => a.BrandId).NotEmpty().WithMessage("Brand must be defined!");

        }

    }
}
