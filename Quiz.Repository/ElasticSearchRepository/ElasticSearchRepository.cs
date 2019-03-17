using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using QuizData;


namespace QuizRepository
{
    public class ElasticSearchRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        #region Fields

        private IElasticClient _elasticClient;

        public IQueryable<TEntity> Table { get; }
        
        public IQueryable<TEntity> TableNoTracking { get; }
        
        #endregion

        #region Ctor
        
        public ElasticSearchRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        #endregion
        
        #region methods
        
        public TEntity GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            
            //This will delete whole type(table).
            _elasticClient.DeleteByQuery<TEntity>(d => d.MatchAll());
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _elasticClient.DeleteMany(entities);
        }

        public void Delete(int id)
        {
            _elasticClient.Delete<TEntity>(id);
        }

        #endregion
                
        #region async methods
        
        public Task<TEntity> GetByIdAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAsync(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
        
        #endregion
    }
}