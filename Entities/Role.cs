using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShopBackend.Entities
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
