using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("Roles")]
    public class Role : BaseModel
    {
        [Key]
        public int RoleId { get; set; }

        [Required, StringLength(50)]
        public string RoleName { get; set; }

        [Required, StringLength(200)]
        public string RoleDescription { get; set; }
    }
}
