using AhmadHRMSBackend.Models.AttendanceInfo;
using AhmadHRMSBackend.Models.AttendanceRecord;
using AhmadHRMSBackend.Models.AttendanceSummary;
using AhmadHRMSBackend.Models.Departments;
using AhmadHRMSBackend.Models.EmployeeList;
using AhmadHRMSBackend.Models.LeaveRequests;
using AhmadHRMSBackend.Models.LeaveStatus;
using AhmadHRMSBackend.Models.LeaveTypes;
using AhmadHRMSBackend.Models.Login;
using AhmadHRMSBackend.Models.PayrollRequests;
using AhmadHRMSBackend.Models.PayrollStatus;
using AhmadHRMSBackend.Models.Performance;
using AhmadHRMSBackend.Models.Position;
using AhmadHRMSBackend.Models.Reports;
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

        public DbSet<Performance> Performance { get; set; }

        public DbSet<PerformanceGoal> PerformanceGoal { get; set; }

        public DbSet<PerformanceAchievement> PerformanceAchievement { get; set; }

        public DbSet<PerformancePeriod> PerformancePeriod { get; set; }

        public DbSet<ReportTypes> ReportTypes { get; set; }

        public DbSet<ReportPeriods> ReportPeriods { get; set; }

        public DbSet<ReportStatus> ReportStatus { get; set; }

        public DbSet<Reports>  Reports { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<Roles> Roles { get; set; }


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


            modelBuilder.Entity<Performance>()
                .HasOne(l=>l.Employee)
                .WithMany(s=>s.Performance)
                .HasForeignKey(l=>l.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PerformanceGoal>()
                .HasOne(l=>l.Performance)
                .WithMany(s=>s.PerformanceGoal)
                .HasForeignKey(l=>l.PerformanceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PerformanceAchievement>()
                .HasOne(l=>l.Performance)
                .WithMany(s=>s.PerformanceAcheivement)
                .HasForeignKey(l=>l.PerformanceId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Performance>()
                .Property(p => p.Rating)
                .HasPrecision(3, 2);

            modelBuilder.Entity<Performance>()
                .HasOne(l=>l.PerformancePeriod)
                .WithMany(s=>s.Performances)
                .HasForeignKey(l=>l.PeriodId)  
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<PerformancePeriod>().HasData(
                    new PerformancePeriod { PerformancePeriodId = 1, PeriodName = "Q1 2026", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 2, PeriodName = "Q2 2026", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 3, PeriodName = "Q3 2026", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 4, PeriodName = "Q4 2026", IsDeleted = false },

                    new PerformancePeriod { PerformancePeriodId = 5, PeriodName = "Q1 2027", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 6, PeriodName = "Q2 2027", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 7, PeriodName = "Q3 2027", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 8, PeriodName = "Q4 2027", IsDeleted = false },


                    new PerformancePeriod { PerformancePeriodId = 9, PeriodName =  "Q1 2028", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 10, PeriodName = "Q2 2028", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 11, PeriodName = "Q3 2028", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 12, PeriodName = "Q4 2028", IsDeleted = false },

                    new PerformancePeriod { PerformancePeriodId = 13, PeriodName = "Q1 2029", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 14, PeriodName = "Q2 2029", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 15, PeriodName = "Q3 2029", IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 16, PeriodName = "Q4 2029", IsDeleted = false },


                    new PerformancePeriod { PerformancePeriodId = 17, PeriodName = "Q1 2030" , IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 18, PeriodName = "Q2 2030" , IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 19, PeriodName = "Q3 2030" , IsDeleted = false },
                    new PerformancePeriod { PerformancePeriodId = 20, PeriodName = "Q4 2030" , IsDeleted = false }
                );

            modelBuilder.Entity<Reports>()
                .HasOne(r => r.ReportType)
                .WithMany(t => t.Reports)
                .HasForeignKey(r => r.ReportTypeId)
                .OnDelete(DeleteBehavior.NoAction);


             modelBuilder.Entity<Reports>()
                .HasOne(r => r.ReportPeriod)
                .WithMany(p => p.Reports)
                .HasForeignKey(r => r.ReportPeriodId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reports>()
                .HasOne(r => r.Departments)
                .WithMany(d => d.Reports) 
                .HasForeignKey(r => r.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reports>()
                .HasOne(r => r.ReportStatus)
                .WithMany(s => s.Reports)
                .HasForeignKey(r => r.ReportStatusId)
                .OnDelete(DeleteBehavior.NoAction);


            // 🔹 ReportTypes Seed
            modelBuilder.Entity<ReportTypes>().HasData(
                new ReportTypes { ReportTypeId = 1, ReportTypeName = "Attendance", IsDeleted = false },
                new ReportTypes { ReportTypeId = 2, ReportTypeName = "Payroll", IsDeleted = false },
                new ReportTypes { ReportTypeId = 3, ReportTypeName = "Performance", IsDeleted = false },
                new ReportTypes { ReportTypeId = 4, ReportTypeName = "Leave", IsDeleted = false },
                new ReportTypes { ReportTypeId = 5, ReportTypeName = "Timesheet", IsDeleted = false }
            );

            // 🔹 ReportPeriods Seed
            modelBuilder.Entity<ReportPeriods>().HasData(
                new ReportPeriods { ReportPeriodId = 1, ReportPeriodName = "Daily", IsDeleted = false },
                new ReportPeriods { ReportPeriodId = 2, ReportPeriodName = "Weekly", IsDeleted = false },
                new ReportPeriods { ReportPeriodId = 3, ReportPeriodName = "Monthly", IsDeleted = false },
                new ReportPeriods { ReportPeriodId = 4, ReportPeriodName = "Quarterly", IsDeleted = false },
                new ReportPeriods { ReportPeriodId = 5, ReportPeriodName = "Yearly", IsDeleted = false }
            );

            // 🔹 ReportStatus Seed
            modelBuilder.Entity<ReportStatus>().HasData(
                new ReportStatus { ReportStatusId = 1, ReportStatusName = "Completed", IsDeleted = false },
                new ReportStatus { ReportStatusId = 2, ReportStatusName = "Scheduled", IsDeleted = false },
                new ReportStatus { ReportStatusId = 3, ReportStatusName = "Draft", IsDeleted = false }
            );

            modelBuilder.Entity<Users>()
                .HasOne(u=>u.Employee)
                .WithOne(e => e.Users)
                .HasForeignKey<Users>(u => u.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Users>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Roles>().HasData(
                new Roles
                {
                    RoleId = 1,
                    RoleName = "Admin",
                    IsDeleted = false
                },
                new Roles
                {
                    RoleId = 2,
                    RoleName = "HR",
                    IsDeleted = false
                },
                new Roles
                {
                    RoleId = 3,
                    RoleName = "Employee",
                    IsDeleted = false
                }
            );


        }
    }
}
