using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizData;

namespace QuizRepository
{

    public interface IDbContext
    {
        #region Methods

        DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        string GenerateCreateScript();

        IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class;

        IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : EntityBase;

        int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        void Detach<TEntity>(TEntity entity) where TEntity : EntityBase;

        #endregion
    }
}