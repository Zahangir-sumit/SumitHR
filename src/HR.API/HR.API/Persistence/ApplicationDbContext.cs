using HR.MVC.DHK.Models;
using HR.MVC.DHK.Models.VM;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace HR.MVC.DHK.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Triggers
            modelBuilder.Entity<Employee>()
                     .ToTable(tb => tb.HasTrigger("calculateBasic"));


            // Keys
            modelBuilder.Entity<Employee>()
                .HasKey((x) => new { x.EmpId, x.ComId });

            modelBuilder.Entity<Department>()
                .HasKey((x) => new { x.DeptId, x.ComId });

            modelBuilder.Entity<Designation>()
                .HasKey((x) => new { x.DesigId, x.ComId });

            modelBuilder.Entity<Shift>()
                .HasKey((x) => new { x.ShiftId, x.ComId });


            modelBuilder.Entity<AttendanceSummary>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Salary>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWID()");
            //modelBuilder.Entity<Attendance>()
            //    .HasIndex(u => new { u.ComId, u.EmpId, u.DtDate })
            //    .IsUnique();


            //modelBuilder.Entity<AttendanceSummary>()
            //    .HasIndex(u => new { u.ComId, u.EmpId, u.DtYear, u.DtMonth })
            //    .IsUnique();

            //modelBuilder.Entity<Attendance>()
            //    .HasKey((x) => new { x.EmpId, x.ComId, x.DtDate });

            //modelBuilder.Entity<AttendanceSummary>()
            //    .HasKey((x) => new { x.EmpId, x.ComId, x.DtMonth, x.DtYear });

            //modelBuilder.Entity<Salary>()
            //    .HasKey((x) => new { x.EmpId, x.ComId, x.DtMonth, x.DtYear });


            modelBuilder.Entity<Attendance>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Attendance>()
                .HasIndex(a => new { a.ComId, a.EmpId, a.DtDate })
                .IsUnique();

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Company)
                .WithMany()
                .HasForeignKey(a => a.ComId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => new { a.EmpId, a.ComId })
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Salary>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Salary>()
                .HasIndex(u => new { u.ComId, u.EmpId, u.DtYear, u.DtMonth })
                .IsUnique();

            modelBuilder.Entity<Salary>()
                .HasOne(a => a.Company)
                .WithMany()
                .HasForeignKey(a => a.ComId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Salary>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => new { a.EmpId, a.ComId })
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AttendanceSummary>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<AttendanceSummary>()
                .HasIndex(u => new { u.ComId, u.EmpId, u.DtYear, u.DtMonth })
                .IsUnique();

            modelBuilder.Entity<AttendanceSummary>()
                .HasOne(a => a.Company)
                .WithMany()
                .HasForeignKey(a => a.ComId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AttendanceSummary>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => new { a.EmpId, a.ComId })
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Company>()
                .HasIndex(u => u.ComName)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasIndex(u => u.DeptName)
                .IsUnique();

            modelBuilder.Entity<Designation>()
                .HasIndex(u => u.DesigName)
                .IsUnique();

            modelBuilder.Entity<Company>()
                .HasMany(x => x.Departments)
                .WithOne(y => y.Company)
                .HasForeignKey(z => z.ComId);

            modelBuilder.Entity<Company>()
                .HasMany(x => x.Designations)
                .WithOne(y => y.Company)
                .HasForeignKey(z => z.ComId);

            modelBuilder.Entity<Company>()
                .HasMany(x => x.Employees)
                .WithOne(y => y.Company)
                .HasForeignKey(z => z.ComId);

            modelBuilder.Entity<Company>()
                .HasMany(x => x.Shifts)
                .WithOne(y => y.Company)
                .HasForeignKey(z => z.ComId);

            //modelBuilder.Entity<Company>()
            //    .HasMany(x => x.Attendances)
            //    .WithOne(y => y.Company)
            //    .HasForeignKey(z => z.ComId);

            //modelBuilder.Entity<Company>()
            //    .HasMany(x => x.AttendanceSummaries)
            //    .WithOne(y => y.Company)
            //    .HasForeignKey(z => z.ComId);

            //modelBuilder.Entity<Company>()
            //    .HasMany(x => x.Salaries)
            //    .WithOne(y => y.Company)
            //    .HasForeignKey(z => z.ComId);

            modelBuilder.Entity<Department>()
                .HasMany(x => x.Employees)
                .WithOne(y => y.Department)
                .HasForeignKey(z => new { z.DeptId, z.ComId });

            modelBuilder.Entity<Designation>()
                .HasMany(x => x.Employees)
                .WithOne(y => y.Designation)
                .HasForeignKey(z => new { z.DesigId, z.ComId });

            modelBuilder.Entity<Shift>()
                .HasMany(x => x.Employees)
                .WithOne(y => y.Shift)
                .HasForeignKey(z => new { z.ShiftId, z.ComId });

            //modelBuilder.Entity<Employee>()
            //    .HasMany(x => x.Attendances)
            //    .WithOne(y => y.Employee)
            //    .HasForeignKey(z => z.EmpId);

            //modelBuilder.Entity<Employee>()
            //    .HasMany(x => x.AttendanceSummaries)
            //    .WithOne(y => y.Employee)
            //    .HasForeignKey(z => z.EmpId);

            //modelBuilder.Entity<Employee>()
            //    .HasMany(x => x.Salaries)
            //    .WithOne(y => y.Employee)
            //    .HasForeignKey(z => z.EmpId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<AttendanceSummary> AttendanceSummary { get; set; }
        public DbSet<Salary> Salary { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<SalaryReport> SalaryReport { get; set; }
        public DbSet<SalarySummaryReport> SalarySummaryReport { get; set; }
        public DbSet<DailyAttendance> DailyAttendances { get; set; }
        public DbSet<MonthlyAttendance> MonthlyAttendances { get; set; }
    }
}
