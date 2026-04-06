using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.LeaveRequest;
using AhmadHRMSBackend.dto.LeaveStatus;
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
    }
}
