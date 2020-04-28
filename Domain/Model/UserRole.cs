using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("UserRole")]
    public class UserRole : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
