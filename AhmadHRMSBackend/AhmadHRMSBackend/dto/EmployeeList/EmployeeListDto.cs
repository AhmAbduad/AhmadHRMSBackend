namespace AhmadHRMSBackend.dto.EmployeeList
{
    public class EmployeeListDto
    {
        public int EmployeeID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string StatusName { get; set; }
        public DateTime JoinDate { get; set; }
        public string Avatar { get; set; }
    }
}