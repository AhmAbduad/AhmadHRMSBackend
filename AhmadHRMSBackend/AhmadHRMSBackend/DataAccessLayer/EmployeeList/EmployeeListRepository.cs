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
              .Include(e => e.Departments)
              .Include(e => e.Position)
              .Include(e => e.Status)
              .ToListAsync();
        }

        public async Task<List<AhmadHRMSBackend.Models.Departments.Departments>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<List<AhmadHRMSBackend.Models.Position.Position>> GetAllPosition()
        {
            return await _context.Position.ToListAsync();
        }

        public async Task<List<AhmadHRMSBackend.Models.Status.Status>> GetAllStatus()
        {
            return await _context.Status.ToListAsync();
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
            var employee = await _context.EmployeeList
                .FirstOrDefaultAsync(e => e.EmployeeID == id);

            if (employee == null)
            {
                return false; // Employee not found
            }

            _context.EmployeeList.Remove(employee);

            return true; // Successfully deleted
        }
    }
}
