namespace AhmadHRMSBackend.dto.CreateEmployee
{
    public class CreateEmployeeDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
        public int StatusId { get; set; }
        public DateTime JoinDate { get; set; }
        public string Avatar { get; set; }
    }
}
