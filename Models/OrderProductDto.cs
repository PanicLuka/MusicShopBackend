using MusicShopBackend.Entities;

namespace MusicShopBackend.Models
{
    public class OrderProductDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }
    }
}
