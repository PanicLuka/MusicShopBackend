using FluentValidation;
using MusicShopBackend.Models;
using System.Collections.Generic;

namespace MusicShopBackend.Validators
{
    public class OrderValidator : AbstractValidator<OrderDto>
    {
        public OrderValidator()
        {
            List<string> statusTypes = new List<string>() { "Pending", "Awaiting payment", "Awaiting fullfilment",
            "Awaiting shipment", "Awaiting pickup", "Partially shipped", "Completed", "Shipped", "Cancelled"
            ,"Declined", "Refunded", "Disputed"};

            RuleFor(o => o.OrderDate).NotEmpty().WithMessage("Order date must be defined!");
            RuleFor(o => o.OrderArrival).NotEmpty().WithMessage("order arrival must be defined!");
            RuleFor(p => p.PaymentType).NotEmpty().WithMessage("Payment type must be defined!");
            RuleFor(x => x.OrderStatus).Must(x => statusTypes.Contains(x)).NotEmpty();
            RuleFor(u => u.UserId).NotEmpty().WithMessage("User must be defined!");
            RuleFor(u => u.DestinationAddressId).NotEmpty().WithMessage("Destination must be defined!");
            RuleFor(u => u.CreditCardId).NotEmpty().WithMessage("Credit card must be defined!");

        }
    }
}
