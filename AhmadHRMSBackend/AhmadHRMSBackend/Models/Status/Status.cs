using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.Status
{
    [Table("Status")]
    public class Status
    {
        [Key]
        public int StatusID { get; set; }


        [Required]
        [StringLength(200)]
        public string StatusName { get; set; }



        public bool IsDeleted { get; set; } = false;

        public ICollection<EmployeeList.EmployeeList> EmployeeList { get; set; } = new List<EmployeeList.EmployeeList>();
    }
}
