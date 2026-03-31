namespace AhmadHRMSBackend.Interfaces
{
    public interface IEmployeeList
    {
        Task<List<AhmadHRMSBackend.Models.EmployeeList.EmployeeList>> GetAllEmployees();

        Task<List<AhmadHRMSBackend.Models.Departments.Departments>> GetAllDepartments();
    }
}
