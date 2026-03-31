using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.EmployeeList;
using AhmadHRMSBackend.UnitofWork;
using System.Linq;

namespace AhmadHRMSBackend.Services.EmployeeList
{
    public class EmployeeListService
    {
        private readonly IUnitofWork _unitOfWork;

        public EmployeeListService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EmployeeListDto>> GetAllEmployees()
        {
            var employees = await _unitOfWork.EmployeeList.GetAllEmployees();

            return employees.Select(e => new EmployeeListDto
            {
                EmployeeID = e.EmployeeID,
                Name = e.Name,
                Email = e.Email,
                DepartmentName = e.Departments?.Label,   // ✅ FIXED
                PositionName = e.Position?.PositionName,
                StatusName = e.Status?.StatusName,
                JoinDate = e.JoinDate,
                Avatar = e.avatar
            }).ToList();
        }

        public async Task<List<DepartmentDto>> GetAllDepartments()
        {
            var departmemnts = await _unitOfWork.EmployeeList.GetAllDepartments();

            return departmemnts.Select(e => new DepartmentDto
            {
                id = e.DepartmentsID,
                Value = e.Value,
                Label = e.Label
            }).ToList();
        }
    }
}
