using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace AhmadHRMSBackend.Models.Position
{
    [Table("Position")]
    public class Position
    {
        [Key]
        public int PositionID { get; set; }



        [Required]
        [StringLength(50)]
        public string PositionName {  get; set; }



        public ICollection<EmployeeList.EmployeeList> EmployeeList { get; set; } = new List<EmployeeList.EmployeeList>();


    }
}
