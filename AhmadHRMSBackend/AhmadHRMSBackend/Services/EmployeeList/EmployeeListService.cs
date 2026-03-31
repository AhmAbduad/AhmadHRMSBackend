using AhmadHRMSBackend.dto.EmployeeList;
using AhmadHRMSBackend.UnitofWork;

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
    }
}
