using AhmadHRMSBackend.dto.ChangeStatus;
using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.LeaveEmployee;
using AhmadHRMSBackend.dto.LeaveRequest;
using AhmadHRMSBackend.dto.LeaveStats;
using AhmadHRMSBackend.dto.LeaveStatus;
using AhmadHRMSBackend.dto.LeaveTypes;
using AhmadHRMSBackend.dto.SubmitLeaveRequest;
using AhmadHRMSBackend.UnitofWork;

namespace AhmadHRMSBackend.Services.Leave
{
    public class LeaveService
    {

        private readonly IUnitofWork _unitOfWork;

        public LeaveService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DepartmentDto>> GetDepartmentsForLeave()
        {
            var departments = await _unitOfWork.Leave.GetDepartmentsForLeave();

            if (departments == null)
                return null;

            return departments.Select(e => new DepartmentDto
            {
                id = e.DepartmentsID,
                Value = e.Value,
                Label = e.Label
            }).ToList();
        }

        public async Task<List<LeaveRequestDto>> GetLeaveRequest()
        {
            var leaverequest = await _unitOfWork.Leave.GetLeaveRequest();

            if (leaverequest == null)
                return null;

            return leaverequest;
        }

        public async Task<List<LeaveStatusDto>> GetStatusForLeave()
        {
            var ss = await _unitOfWork.Leave.GetStatusForLeave();
            if (ss == null)
                return null;

            return ss.Select(e => new LeaveStatusDto
            {
                statusid = e.StatusId,
                value = e.StatusName,
                label = e.StatusName
            }).ToList();
        }

        public async Task<LeaveStatsDto> GetLeaveStats()
        {
            var leavestats = await _unitOfWork.Leave.GetLeaveStats();

            return leavestats;
        }

        public async Task<List<LeaveTypesDto>> GetLeaveTypes()
        {
            var leavetypes = await _unitOfWork.Leave.GetLeaveTypes();
            return leavetypes;

        }

        public async Task<bool> SubmitLeaveRequest(SubmitLeaveRequestDto dto)
        {
            var submitleaverequest = await _unitOfWork.Leave.SaveMarkAttendance(dto);
            if (submitleaverequest == true)
                return true;
            else
            {
                return false;
            }
        }

        public async Task<List<LeaveEmployeeDto>> GetEmployeesForLeave()
        {
            var employeelist = await _unitOfWork.Leave.GetEmployeesForLeave();

            return employeelist;
        }

        public async Task<bool> ChangeLeaveRequestStatus(ChangeStatusDto dto)
        {
            var result = await _unitOfWork.Leave.ChangeLeaveRequestStatus(dto);
            if (result == true) 
                return true;
            else
            {
                return false;
            }
        }

    }
}
