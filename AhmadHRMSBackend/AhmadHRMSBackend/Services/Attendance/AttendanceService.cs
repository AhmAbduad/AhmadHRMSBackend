using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.EmployeeList;
using AhmadHRMSBackend.dto.GetAttendanceInfo;
using AhmadHRMSBackend.dto.GetAttendanceRecord;
using AhmadHRMSBackend.dto.GetAttendanceSummary;
using AhmadHRMSBackend.UnitofWork;

namespace AhmadHRMSBackend.Services.Attendance
{
    public class AttendanceService
    {
        private readonly IUnitofWork _unitOfWork;

        public AttendanceService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAttendanceInfoDto> GetAttendanceInfo()
        {
            var e = await _unitOfWork.Attendance.GetAttendanceInfo();

            if (e == null)
                return null;

            return new GetAttendanceInfoDto
            {
                AttendanceInfoId = e.AttendanceInfoId,
                CheckInTime = e.CheckInTime,
                CheckOutTime = e.CheckOutTime,
                Status = e.Status
            };
        }


        public async Task<List<GetAttendanceRecordDto>> GetAttendanceRecord(AttendanceRecordMonthDto dto)
        {
            var attendancerecord = await _unitOfWork.Attendance.GetAttendanceRecord(dto);

            return attendancerecord;
        }

        public async Task<GetAttendanceSummaryDto> GetAttendanceSummary(int id)
        {
            var e = await _unitOfWork.Attendance.GetAttendanceSummary(id);

            if (e == null)
                return null;

            return new GetAttendanceSummaryDto
            {
                EmployeeName = e.Employee.Name,
                Month = e.Month,
                Year = e.Year,
                Present = e.Present,
                Absent = e.Absent,
                Late = e.Late,
                Leave = e.Leave
            };
        }

        public async Task<List<DepartmentDto>> GetDepartmentForAttendance()
        {
            var result = await _unitOfWork.Attendance.GetDepartmentForAttendance();
            return result;
        }
    }
}
