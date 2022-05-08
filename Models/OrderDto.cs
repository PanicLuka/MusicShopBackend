using System;

namespace MusicShopBackend.Models
{
    public class OrderDto
    {
        public DateTime OrderDate { get; set; }
        public bool OrderArrival { get; set; }
        public string PaymentType { get; set; }
        public string OrderStatus { get; set; }
        public int UserId { get; set; }
        public int CreditCardId { get; set; }
        public int DestinationAddressId { get; set; }
    }
}
