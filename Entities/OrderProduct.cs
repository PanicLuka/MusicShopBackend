using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShopBackend.Entities
{
    public class OrderProduct
    {
        [ForeignKey("FK_OrderId")]
        public int OrderId { get; set; }

        [ForeignKey("FK_ProductId")]
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }

    }
}
