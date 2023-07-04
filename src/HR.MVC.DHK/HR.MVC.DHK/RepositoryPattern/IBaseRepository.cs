using HR.MVC.DHK.Models.IEntity;
using System.Linq.Expressions;

namespace HR.MVC.DHK.RepositoryPattern
{
    public interface IBaseRepository<TEntity, TKey>
            where TEntity : class
    {
        void Add(TEntity entityToAdd);
        void Edit(TEntity entityToEdit);
        void Remove(TKey id);
        void Remove(TEntity entityToRemove);
        void Remove(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity FindWhere(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entityToAdd);
        Task EditAsync(TEntity entityToEdit);
        Task RemoveAsync(TKey id);
        Task RemoveAsync(TEntity entityToRemove);
        Task RemoveAsync(Expression<Func<TEntity, bool>> filter);

        TEntity GetById(TKey id);
        IList<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(TKey id);
        Task<IList<TEntity>> GetAllAsync();

        int GetCount(Expression<Func<TEntity, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);

    }
}
