using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.RepositoryPattern.Repositories
{
    public class DepartmentRepository : Repository<Department, Guid>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
