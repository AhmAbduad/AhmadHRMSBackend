using AhmadHRMSBackend.Models.Departments;
using AhmadHRMSBackend.Models.Position;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace AhmadHRMSBackend.Models.EmployeeList
{
    [Table("EmployeeList")]
    public class EmployeeList
    {
        [Key]
        public int EmployeeID { get; set; }

        // FK
        [Required]
        public int DepartmentID { get; set; }

        // FK
        [Required]
        public int PositionID { get; set; }

        // FK
        [Required]
        public int StatusID { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }


        [Required]
        public DateTime JoinDate { get; set; }

        [Required]
        [StringLength(100)]
        public string avatar { get; set; }



        // 🔗 Navigation Property
        [ForeignKey(nameof(DepartmentID))]
        public Departments.Departments Departments { get; set; }



        // 🔗 Navigation Property
        [ForeignKey(nameof(PositionID))]
        public Position.Position Position { get; set; }


        [ForeignKey(nameof(StatusID))]
        public Status.Status Status { get; set; }
    }
}
