using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.RepositoryPattern.Repositories
{
    public class EmployeeRepository : Repository<Employee, Guid>,IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
