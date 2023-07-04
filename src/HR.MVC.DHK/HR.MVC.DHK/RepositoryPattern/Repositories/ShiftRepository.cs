using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.RepositoryPattern.Repositories
{
    public class ShiftRepository : Repository<Shift, Guid>, IShiftRepository
    {
        public ShiftRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
