using HR.MVC.DHK.Models;

namespace HR.MVC.DHK.RepositoryPattern.IRepositories
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        List<Employee> GetEmployees();
    }
}
