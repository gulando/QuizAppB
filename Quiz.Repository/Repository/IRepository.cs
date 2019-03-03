using System.Collections.Generic;
using System.Linq;
using QuizData;


namespace QuizRepository
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        #region Methods

        TEntity GetById(object id);

        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        void Delete(int id);
        
        #endregion

        #region Properties

        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableNoTracking { get; }

        #endregion
    }
}