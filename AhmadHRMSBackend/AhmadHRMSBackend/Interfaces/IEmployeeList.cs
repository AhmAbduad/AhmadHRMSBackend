namespace AhmadHRMSBackend.Interfaces
{
    public interface IEmployeeList
    {
        Task<List<AhmadHRMSBackend.Models.EmployeeList.EmployeeList>> GetAllEmployees();

    }
}
