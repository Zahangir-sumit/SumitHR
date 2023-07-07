using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.RepositoryPattern.Repositories
{
    public class EmployeeRepository : Repository<Employee, Guid>,IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public List<Employee> GetEmployees()
        {
            var employees = _context.Employee
                                .Include(c => c.Company)
                                .Include(d => d.Department)
                                .ToList();

            return employees;
        }
    }
}
