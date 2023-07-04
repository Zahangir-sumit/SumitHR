using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HR.MVC.DHK.RepositoryPattern.Repositories
{
    public class AttendanceRepository : Repository<Attendance, Guid>, IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Process(AttProcess attProcess)
        {
            var SpParameter = new List<SqlParameter>();
            SpParameter.Add(new SqlParameter("@ComId", attProcess.ComId));
            SpParameter.Add(new SqlParameter("@dtDate", attProcess.DtDate));


            var result = await Task.Run(() => _context.Database
            .ExecuteSqlRawAsync(@"exec UpdateAttendanceStatus @ComId, @dtDate", SpParameter.ToArray()));
        }

        public async Task MonthlyProcess(MonthlyAttProcess monthlyAttProcess)
        {

            var SpParameter = new List<SqlParameter>();
            SpParameter.Add(new SqlParameter("@ComId", monthlyAttProcess.ComId));
            SpParameter.Add(new SqlParameter("@Month", monthlyAttProcess.DtMonth));
            SpParameter.Add(new SqlParameter("@Year", monthlyAttProcess.DtYear));


            var result = await Task.Run(() => _context.Database
            .ExecuteSqlRawAsync(@"exec monthlyAttendance @ComId,@Month, @Year", SpParameter.ToArray()));
        }
    }
}
