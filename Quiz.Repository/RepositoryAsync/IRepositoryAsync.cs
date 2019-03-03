using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizData;


namespace QuizRepository
{
    public interface IRepositoryAsync<TEntity> where TEntity : EntityBase
    {
        #region Methods

        Task<TEntity> GetByIdAsync(object id);

        Task InsertAsync(TEntity entity);

        Task InsertAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);

        Task UpdateAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(int id);
        
        #endregion

        #region Properties

        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableNoTracking { get; }

        #endregion
    }
}