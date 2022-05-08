using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShopBackend.Entities
{
    public class Brand
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
