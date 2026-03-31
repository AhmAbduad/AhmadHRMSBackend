using AhmadHRMSBackend.Models.Departments;
using AhmadHRMSBackend.Models.EmployeeList;
using AhmadHRMSBackend.Models.Position;
using AhmadHRMSBackend.Models.Status;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee → Department
            modelBuilder.Entity<EmployeeList>()
                .HasOne(e => e.Departments)
                .WithMany(d => d.EmployeeList)
                .HasForeignKey(e => e.DepartmentID)
               .OnDelete(DeleteBehavior.Cascade);

            // Employee → Position
            modelBuilder.Entity<EmployeeList>()
                .HasOne(e => e.Position)
                .WithMany(p => p.EmployeeList)
                .HasForeignKey(e => e.PositionID)
                .OnDelete(DeleteBehavior.Cascade);

            // Employee → Status
            modelBuilder.Entity<EmployeeList>()
                .HasOne(e => e.Status)
                .WithMany(s => s.EmployeeList)
                .HasForeignKey(e => e.StatusID)
                .OnDelete(DeleteBehavior.Cascade);


        }



    }
}
