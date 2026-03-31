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
        public string StatusName { get; set; }


        public ICollection<EmployeeList.EmployeeList> EmployeeList { get; set; } = new List<EmployeeList.EmployeeList>();
    }
}
