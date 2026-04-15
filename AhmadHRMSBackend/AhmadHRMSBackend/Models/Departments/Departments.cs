using AhmadHRMSBackend.Models.EmployeeList;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AhmadHRMSBackend.Models.Departments
{
    [Table("Departments")]
    public class Departments
    {
        [Key]
        public int DepartmentsID { get; set; }

        [Required]
        [StringLength(200)]
        public string Value { get; set; }


        [Required]
        [StringLength(200)]
        public string Label { get; set; }

        public bool IsDeleted { get; set; } = false;


        public ICollection<EmployeeList.EmployeeList> EmployeeList { get; set; } = new List<EmployeeList.EmployeeList>();

        public ICollection<Reports.Reports> Reports { get; set; } = new List<Reports.Reports>();
    }
}
