using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShopBackend.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string ImgPath { get; set; }

        [ForeignKey("FK_CategoryId")]
        public int CategoryId { get; set; }

        [ForeignKey("FK_BrandId")]
        public int BrandId { get; set; }

        [ForeignKey("FK_EmployeeId")]
        public int EmployeeId { get; set; }

    }
}
