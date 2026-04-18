using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.Login
{
    [Table("Roles")]
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; } // Admin, HR, Employee

        public bool IsDeleted { get; set; } = false;

        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
