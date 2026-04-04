using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AhmadHRMSBackend.UnitofWork
{
    public class UnitofWork:IUnitofWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;
        public IEmployeeList EmployeeList { get; }
        public IAttendance Attendance { get; }

        public IMarkAttendance MarkAttendance { get; }

        // this is unit of work constructor
        public UnitofWork(
                        AppDbContext context,
                        IEmployeeList employeeList,
                        IAttendance attendance,
                        IMarkAttendance markAttendance)
        {
            _context = context;
            EmployeeList = employeeList;
            Attendance = attendance;
            MarkAttendance = markAttendance;
        }



        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            //await _transaction.CommitAsync();

            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            // await _transaction.RollbackAsync();
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }



        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}
