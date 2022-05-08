using System;

namespace MusicShopBackend.Models
{
    public class CreditCardDto
    {
        public string CreditCardNumber { get; set; }
        public int Cvv { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
