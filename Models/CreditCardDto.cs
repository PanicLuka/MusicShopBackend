using System;

namespace MusicShopBackend.Models
{
    public class CreditCardDto
    {
        public int CreditCardId { get; set; }
        public string CreditCardNumber { get; set; }
        public int Cvv { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
