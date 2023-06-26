using HR.MVC.DHK.RepositoryPattern.IRepositories;

namespace HR.MVC.DHK.RepositoryPattern
{
    public interface IUnitOfWork : IDisposable
    {

        #region Repositories

        IEmployeeRepository Employee { get; }





        #endregion Repositories


        void Save();
    }
}
