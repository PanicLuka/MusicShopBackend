using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShopBackend.Entities
{
    public class Category
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

    }
}
