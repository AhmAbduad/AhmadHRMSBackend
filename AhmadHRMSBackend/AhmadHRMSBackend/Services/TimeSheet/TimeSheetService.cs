using AhmadHRMSBackend.dto.TimeSheetDetails;
using AhmadHRMSBackend.UnitofWork;

namespace AhmadHRMSBackend.Services.TimeSheet
{
    public class TimeSheetService
    {
        private readonly IUnitofWork _unitOfWork;

        public TimeSheetService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TimeSheetDetailsDto>> GetTimeSheetDetail(GetTimesheetDto dto)
        {
            var result =await _unitOfWork.TimeSheet.GetTimeSheetDetail(dto);

            return result;
        }
    }
}
