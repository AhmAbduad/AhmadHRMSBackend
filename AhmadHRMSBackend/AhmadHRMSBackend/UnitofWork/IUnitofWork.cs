using AhmadHRMSBackend.Interfaces;

namespace AhmadHRMSBackend.UnitofWork
{
    public interface IUnitofWork:IDisposable
    {
        IEmployeeList EmployeeList { get; }

        IAttendance Attendance { get; }



        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
