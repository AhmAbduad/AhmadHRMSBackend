namespace AhmadHRMSBackend.Interfaces
{
    public interface IAttendance
    {
        Task<AhmadHRMSBackend.Models.AttendanceInfo.AttendanceInfo> GetAttendanceInfo();

        Task<List<AhmadHRMSBackend.Models.AttendanceRecord.AttendanceRecord>> GetAttendanceRecord();

        Task<AhmadHRMSBackend.Models.AttendanceSummary.AttendanceSummary> GetAttendanceSummary(int id);
    }
}
