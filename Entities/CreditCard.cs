using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShopBackend.Entities
{
    public class CreditCard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CreditCardId { get; set; }
        public string CreditCardNumber { get; set; }
        public int Cvv { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
