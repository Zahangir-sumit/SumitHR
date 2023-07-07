using HR.MVC.DHK.Models;

namespace HR.MVC.DHK.RepositoryPattern.IRepositories
{
    public interface IAttendanceRepository : IRepository<Attendance, Guid>
    {
        Task Process(AttProcess attProcess);
        Task MonthlyProcess(MonthlyAttProcess monthlyAttProcess);
    }
}
