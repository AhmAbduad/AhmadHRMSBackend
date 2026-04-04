using AhmadHRMSBackend.dto.CreateEmployee;
using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.EmployeeList;
using AhmadHRMSBackend.dto.Position;
using AhmadHRMSBackend.dto.Status;
using AhmadHRMSBackend.dto.UpdateEmployee;
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
            var departments = await _unitOfWork.EmployeeList.GetAllDepartments();

            return departments.Select(e => new DepartmentDto
            {
                id = e.DepartmentsID,
                Value = e.Value,
                Label = e.Label
            }).ToList();
        }

        public async Task<List<PositionDto>> GetAllPosition()
        {
            var position = await _unitOfWork.EmployeeList.GetAllPosition();

            return position.Select(e => new PositionDto
            {
                id = e.PositionID,
                PositionName= e.PositionName,
            }).ToList();
        }

        public async Task<List<StatusDto>> GetAllStatus()
        {
            var status = await _unitOfWork.EmployeeList.GetAllStatus();

            return status.Select(e => new StatusDto
            {
                id = e.StatusID,
                StatusName = e.StatusName
            }).ToList();
        }

        public async Task<AhmadHRMSBackend.Models.EmployeeList.EmployeeList> CreateEmployee(CreateEmployeeDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var request = await _unitOfWork.EmployeeList.CreateEmployee(
                    dto.Email,
                    dto.Name,
                    dto.DepartmentId,
                    dto.PositionId,
                    dto.StatusId,
                    dto.JoinDate,
                    dto.Avatar
                    );

                // 🔹 Save changes
                await _unitOfWork.SaveChangesAsync();

                // 🔹 Commit transaction
                await _unitOfWork.CommitTransactionAsync();

                return request;

            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw; // Bubble up the exception

            }
        }

        public async Task<AhmadHRMSBackend.Models.EmployeeList.EmployeeList> UpdateEmployee(UpdateEmployeeDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var request = await _unitOfWork.EmployeeList.UpdateEmployee(
                    dto.Id,
                    dto.Email,
                    dto.Name,
                    dto.DepartmentId,
                    dto.PositionId,
                    dto.StatusId,
                    dto.JoinDate,
                    dto.Avatar
                    );

                // 🔹 Save changes
                await _unitOfWork.SaveChangesAsync();

                // 🔹 Commit transaction
                await _unitOfWork.CommitTransactionAsync();

                return request;

            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw; // Bubble up the exception

            }

        }

        public async Task<bool> DeleteEmployee(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try{
                var request =await _unitOfWork.EmployeeList.DeleteEmployee(id);

                await _unitOfWork.SaveChangesAsync();

                // 🔹 Commit transaction
                await _unitOfWork.CommitTransactionAsync();

                return request;
            }
            catch 
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw; // Bubble up the exception
            }
        }


    }
}
