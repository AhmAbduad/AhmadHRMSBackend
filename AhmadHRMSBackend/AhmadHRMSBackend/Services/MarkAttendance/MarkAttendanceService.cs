using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.GetAttendanceRecord;
using AhmadHRMSBackend.dto.GetMarkAttendance;
using AhmadHRMSBackend.Models.Departments;
using AhmadHRMSBackend.UnitofWork;

namespace AhmadHRMSBackend.Services.MarkAttendance
{
    public class MarkAttendanceService
    {
        private readonly IUnitofWork _unitOfWork;

        public MarkAttendanceService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetAttendanceRecordDto>> GetMarkAttendanceRecord(GetMarkAttendanceDto dto)
        {
            var markattendancerecord = await _unitOfWork.MarkAttendance.GetMarkAttendanceRecord(dto.date,dto.departmentId);

            if (markattendancerecord == null)
                return null;

            return markattendancerecord.Select(e => new GetAttendanceRecordDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.Employee.Name,
                Department = e.Employee.Departments.Label,
                Date = e.Date,
                CheckIn = e.CheckIn,
                CheckOut = e.CheckOut,
                Status = e.Status,
                Avatar = e.Employee.avatar
            }).ToList();
        }

        public async Task<List<DepartmentDto>> GetDepartments()
        {
            var departments = await _unitOfWork.MarkAttendance.GetDepartments();

            if (departments == null)
                return null;

            return departments.Select(e => new DepartmentDto
            {
                id = e.DepartmentsID,
                Value = e.Value,
                Label = e.Label
            }).ToList();
        }
    }
}
