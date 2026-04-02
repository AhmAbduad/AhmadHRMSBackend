namespace AhmadHRMSBackend.Interfaces
{
    public interface IEmployeeList
    {
        Task<List<AhmadHRMSBackend.Models.EmployeeList.EmployeeList>> GetAllEmployees();

        Task<List<AhmadHRMSBackend.Models.Departments.Departments>> GetAllDepartments();

        Task<List<AhmadHRMSBackend.Models.Position.Position>> GetAllPosition();

        Task<List<AhmadHRMSBackend.Models.Status.Status>> GetAllStatus();

        Task<AhmadHRMSBackend.Models.EmployeeList.EmployeeList> CreateEmployee( string Email, string Name, int DepartmentId, int PositionId, int StatusId, DateTime JoinDate, string Avatar);

        Task<AhmadHRMSBackend.Models.EmployeeList.EmployeeList> UpdateEmployee(int Id,string Email, string Name, int DepartmentId, int PositionId, int StatusId, DateTime JoinDate, string Avatar);

        Task<bool> DeleteEmployee (int Id);
    }
}
