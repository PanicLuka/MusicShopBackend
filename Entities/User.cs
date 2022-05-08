using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShopBackend.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("FK_RoleId")]
        public int RoleId { get; set; } = 1;
    }
}
