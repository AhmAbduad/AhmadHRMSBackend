using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AhmadHRMSBackend.DataAccessLayer.EmployeeList
{
    public class EmployeeListRepository: IEmployeeList
    {
        private readonly AppDbContext _context;

        public EmployeeListRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AhmadHRMSBackend.Models.EmployeeList.EmployeeList>> GetAllEmployees()
        {
            return await _context.EmployeeList
             .Where(e => !e.IsDeleted) // 🔹 Only non-deleted records
             .Include(e => e.Departments)
             .Include(e => e.Position)
             .Include(e => e.Status)
             .ToListAsync();
        }

        public async Task<List<AhmadHRMSBackend.Models.Departments.Departments>> GetAllDepartments()
        {
            return await _context.Departments
            .Where(d => !d.IsDeleted) // 🔹 Only non-deleted departments
            .ToListAsync();
        }

        public async Task<List<AhmadHRMSBackend.Models.Position.Position>> GetAllPosition()
        {
            return await _context.Position
            .Where(p => !p.IsDeleted) // 🔹 Only non-deleted positions
            .ToListAsync();
        }

        public async Task<List<AhmadHRMSBackend.Models.Status.Status>> GetAllStatus()
        {
            return await _context.Status
            .Where(s => !s.IsDeleted) // 🔹 Only non-deleted statuses
            .ToListAsync();
        }

        public async Task<AhmadHRMSBackend.Models.EmployeeList.EmployeeList> CreateEmployee(string Email, string Name, int DepartmentId, int PositionId, int StatusId, DateTime JoinDate, string Avatar)
        {
            var request = new AhmadHRMSBackend.Models.EmployeeList.EmployeeList
            {
                Email = Email,
                Name = Name,
                DepartmentID = DepartmentId,
                PositionID = PositionId,
                StatusID = StatusId,
                avatar = Avatar,
                JoinDate = JoinDate,
                IsDeleted=false
            };



            _context.EmployeeList.Add(request);

            return request;
            
        }

        public async Task<AhmadHRMSBackend.Models.EmployeeList.EmployeeList> UpdateEmployee(int Id, string Email, string Name, int DepartmentId, int PositionId, int StatusId, DateTime JoinDate, string Avatar)
        {
            var employee = await _context.EmployeeList
                            .FirstOrDefaultAsync(e => e.EmployeeID == Id);

            if (employee == null)
            {
                return null; // Employee not found
            }

            // Update the employee properties
            employee.Name = Name;
            employee.Email = Email;
            employee.DepartmentID = DepartmentId;
            employee.PositionID = PositionId;
            employee.StatusID = StatusId;
            employee.JoinDate = JoinDate;
            employee.avatar = Avatar;

            

            return employee;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            //var employee = await _context.EmployeeList
            //.FirstOrDefaultAsync(e => e.EmployeeID == id);

            //if (employee == null)
            //{
            //    return false; // Employee not found
            //}

            //// 🔹 Soft Delete instead of Remove
            //employee.IsDeleted = true;

            //// Optional: updated date bhi rakh sakte ho
            //// employee.UpdatedAt = DateTime.UtcNow;

            //return true; // Successfully marked as deleted

            var employee = await _context.EmployeeList
            .Include(e => e.AttendanceRecords)
            .Include(e => e.AttendanceSummary)
            .Include(e => e.LeaveRequests)
            .Include(e => e.Timesheets)
            .Include(e => e.PayrollRequests)
            .Include(e => e.Performance)
            // .Include(e => e.Users)  // ❌ Remove this - Users table nahi touch karna
            .FirstOrDefaultAsync(e => e.EmployeeID == id);

            if (employee == null)
            {
                return false;
            }

            // 🔹 Soft Delete Employee only
            employee.IsDeleted = true;

            // 🔹 Soft Delete related records (except Users)
            foreach (var record in employee.AttendanceRecords)
            {
                record.IsDeleted = true;
            }

            foreach (var summary in employee.AttendanceSummary)
            {
                summary.IsDeleted = true;
            }

            foreach (var leave in employee.LeaveRequests)
            {
                leave.IsDeleted = true;
            }

            foreach (var timesheet in employee.Timesheets)
            {
                timesheet.IsDeleted = true;
            }

            foreach (var payroll in employee.PayrollRequests)
            {
                payroll.IsDeleted = true;
            }

            foreach (var performance in employee.Performance)
            {
                performance.IsDeleted = true;
            }

            // ❌ DO NOT touch employee.Users
            // Users table remains unchanged

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
