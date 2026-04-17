using AhmadHRMSBackend.Interfaces;

namespace AhmadHRMSBackend.UnitofWork
{
    public interface IUnitofWork:IDisposable
    {
        IEmployeeList EmployeeList { get; }

        IAttendance Attendance { get; }

        IMarkAttendance MarkAttendance { get; }

        ILeave Leave { get; }

        ITimeSheet TimeSheet { get; }

        IPayroll Payroll { get; }

        IPerformances Performances { get; }

        IReports Reports { get; }

        IDashboard Dashboard { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
