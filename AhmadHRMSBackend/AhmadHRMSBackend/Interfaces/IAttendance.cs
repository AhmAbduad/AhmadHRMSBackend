namespace AhmadHRMSBackend.Interfaces
{
    public interface IAttendance
    {
        Task<AhmadHRMSBackend.Models.AttendanceInfo.AttendanceInfo> GetAttendanceInfo();
    }
}
