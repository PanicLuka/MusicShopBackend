using MusicShopBackend.Entities;

namespace MusicShopBackend.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int EmployeeId { get; set; }
    }
}
