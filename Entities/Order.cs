using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShopBackend.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool  OrderArrival { get; set; }
        public string PaymentType { get; set; }
        public string OrderStatus { get; set; }

        [ForeignKey("FK_UserId")]
        public int UserId { get; set; }

        [ForeignKey("FK_CreditCardId")]
        public int CreditCardId { get; set; }

        [ForeignKey("FK_DestinationAddressId")]
        public int DestinationAddressId { get; set; }
    }
}
