using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.RepositoryPattern.Repositories
{
    public class CompanyRepository : Repository<Employee, Guid>,IEmployeeRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
