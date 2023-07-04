using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.RepositoryPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbContext;


        public IEmployeeRepository Employee { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IDesignationRepository Designation { get; private set; }
        public IShiftRepository Shift { get; private set; }
        public IAttendanceRepository Attendance { get; private set; }
        public IReportRepository Report { get; private set; }





        public UnitOfWork(ApplicationDbContext dbContext,

            IEmployeeRepository EmployeeRepository,
            ICompanyRepository CompanyRepository,
            IDepartmentRepository DepartmentRepository,
            IDesignationRepository DesignationRepository,
            IShiftRepository ShiftRepository,
            IAttendanceRepository AttendanceRepository,
            IReportRepository ReportRepository
            )
        {
             _dbContext = dbContext;

            Employee = EmployeeRepository;
            Company = CompanyRepository;
            Department = DepartmentRepository;
            Designation = DesignationRepository;
            Shift = ShiftRepository;
            Attendance = AttendanceRepository;
            Report = ReportRepository;
        } 

        public virtual void Dispose() => _dbContext?.Dispose();
        public virtual void Save() => _dbContext?.SaveChanges();

    }
}
