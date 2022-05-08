using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShopBackend.Entities
{
    public class DestinationAddress
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DestinationAddressId { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
