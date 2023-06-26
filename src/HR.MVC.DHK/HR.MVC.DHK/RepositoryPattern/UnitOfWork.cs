using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.RepositoryPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbContext;


        public IEmployeeRepository Employee { get; private set; }






        public UnitOfWork(ApplicationDbContext dbContext,

            IEmployeeRepository EmployeeRepository
           
            )
        {
             _dbContext = dbContext;

            Employee = EmployeeRepository;
        } 

        public virtual void Dispose() => _dbContext?.Dispose();
        public virtual void Save() => _dbContext?.SaveChanges();

    }
}
