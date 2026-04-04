namespace AhmadHRMSBackend.Interfaces
{
    public interface IMarkAttendance
    {
        Task<List<AhmadHRMSBackend.Models.AttendanceRecord.AttendanceRecord>> GetMarkAttendanceRecord(DateTime date,int departmentId);

        Task<List<AhmadHRMSBackend.Models.Departments.Departments>> GetDepartments();
    }
}
