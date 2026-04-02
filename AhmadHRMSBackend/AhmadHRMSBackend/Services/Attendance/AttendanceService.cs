using AhmadHRMSBackend.dto.EmployeeList;
using AhmadHRMSBackend.dto.GetAttendanceInfo;
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
    }
}
