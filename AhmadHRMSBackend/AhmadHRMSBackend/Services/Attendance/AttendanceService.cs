using AhmadHRMSBackend.dto.EmployeeList;
using AhmadHRMSBackend.dto.GetAttendanceInfo;
using AhmadHRMSBackend.dto.GetAttendanceRecord;
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


        public async Task<List<GetAttendanceRecordDto>> GetAttendanceRecord()
        {
            var attendancerecord = await _unitOfWork.Attendance.GetAttendanceRecord();

            if (attendancerecord == null)
                return null;

            return attendancerecord.Select(e => new GetAttendanceRecordDto
            {
                Id = e.EmployeeId,
                EmployeeName = e.Employee.Name,
                Department = e.Employee.Departments.Label,
                Date = e.Date,
                CheckIn = e.CheckIn, 
                CheckOut = e.CheckOut,
                Status = e.Status,
                Avatar = e.Employee.avatar
            }).ToList();
        }
    }
}
