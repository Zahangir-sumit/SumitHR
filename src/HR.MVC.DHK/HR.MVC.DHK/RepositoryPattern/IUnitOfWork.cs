using HR.MVC.DHK.RepositoryPattern.IRepositories;

namespace HR.MVC.DHK.RepositoryPattern
{
    public interface IUnitOfWork : IDisposable
    {

        #region Repositories

        IEmployeeRepository Employee { get; }
        ICompanyRepository Company { get; }
        IDepartmentRepository Department { get; }
        IDesignationRepository Designation { get; }
        IShiftRepository Shift { get; }
        IAttendanceRepository Attendance { get; }
        IReportRepository Report { get; }




        #endregion Repositories


        void Save();
    }
}
