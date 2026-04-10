using AhmadHRMSBackend.Models.AttendanceInfo;
using AhmadHRMSBackend.Models.AttendanceRecord;
using AhmadHRMSBackend.Models.AttendanceSummary;
using AhmadHRMSBackend.Models.Departments;
using AhmadHRMSBackend.Models.EmployeeList;
using AhmadHRMSBackend.Models.LeaveRequests;
using AhmadHRMSBackend.Models.LeaveStatus;
using AhmadHRMSBackend.Models.LeaveTypes;
using AhmadHRMSBackend.Models.PayrollRequests;
using AhmadHRMSBackend.Models.PayrollStatus;
using AhmadHRMSBackend.Models.Position;
using AhmadHRMSBackend.Models.Status;
using AhmadHRMSBackend.Models.TimesheetDetails;
using AhmadHRMSBackend.Models.Timesheets;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace AhmadHRMSBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }



        public DbSet<EmployeeList> EmployeeList { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Status> Status { get; set; }

        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

        public DbSet<AttendanceSummary> AttendanceSummary { get; set; }

        public DbSet<AttendanceInfo> AttendanceInfo { get; set; }

        public DbSet<LeaveRequests> LeaveRequests { get; set; }

        public DbSet<LeaveTypes> LeaveTypes { get; set; }

        public DbSet<LeaveStatus> LeaveStatus { get; set; }

        public DbSet<TimesheetDetails> TimesheetDetails { get; set; }

        public DbSet<Timesheets> Timesheets { get; set; }

        public DbSet<PayrollRequests> PayrollRequests { get; set; }

        public DbSet<PayrollStatus> PayrollStatus { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee → Department
            modelBuilder.Entity<EmployeeList>()
                .HasOne(e => e.Departments)
                .WithMany(d => d.EmployeeList)
                .HasForeignKey(e => e.DepartmentID)
               .OnDelete(DeleteBehavior.NoAction);

            // Employee → Position
            modelBuilder.Entity<EmployeeList>()
                .HasOne(e => e.Position)
                .WithMany(p => p.EmployeeList)
                .HasForeignKey(e => e.PositionID)
                .OnDelete(DeleteBehavior.NoAction);

            // Employee → Status
            modelBuilder.Entity<EmployeeList>()
                .HasOne(e => e.Status)
                .WithMany(s => s.EmployeeList)
                .HasForeignKey(e => e.StatusID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AttendanceRecord>()
                .HasOne(e=>e.Employee)
                .WithMany(p => p.AttendanceRecords)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<AttendanceSummary>()
                .HasIndex(a => new { a.EmployeeId, a.Month, a.Year })
                .IsUnique();


            modelBuilder.Entity<AttendanceSummary>()
                .HasOne(e => e.Employee)
                .WithMany(p => p.AttendanceSummary)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<LeaveRequests>()
                .HasOne(e=>e.Employee)
                .WithMany(p=>p.LeaveRequests)
                .HasForeignKey(e=>e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);


            // LeaveType → LeaveRequest
            modelBuilder.Entity<LeaveRequests>()
                .HasOne(l => l.LeaveType)
                .WithMany(t => t.LeaveRequests)
                .HasForeignKey(l => l.LeaveTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            // LeaveStatus → LeaveRequest
            modelBuilder.Entity<LeaveRequests>()
                .HasOne(l => l.LeaveStatus)
                .WithMany(s => s.LeaveRequests)
                .HasForeignKey(l => l.LeaveStatusId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Timesheets>()
                .HasOne(l=>l.Employee)
                .WithMany(s=>s.Timesheets)
                .HasForeignKey(l=>l.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TimesheetDetails>()
                .HasOne(l=>l.Timesheet)
                .WithMany(s=>s.TimesheetDetails)
                .HasForeignKey(l=>l.TimesheetId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Timesheets>()
                .Property(t => t.TotalHours)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Timesheets>()
                .Property(t => t.RegularHours)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Timesheets>()
                .Property(t => t.OvertimeHours)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PayrollRequests>()
                .Property(p => p.Amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PayrollRequests>()
                .HasOne(l=>l.Employee)
                .WithMany(s=>s.PayrollRequests)
                .HasForeignKey(l=>l.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PayrollRequests>()
                .HasOne(l=>l.PayrollStatus)
                .WithMany(s=>s.PayrollRequests)
                .HasForeignKey(l=>l.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
