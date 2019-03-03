using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizData;
using QuizRepository;


namespace QuizRepository
{
    public class QuizDBContext : DbContext, IDbContext
    {
        #region Ctor

        public QuizDBContext(DbContextOptions<QuizDBContext> options) : base(options)
        {
            
        }

        #endregion

        #region Utilities

        protected virtual string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is DbParameter parameter))
                    continue;

                sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";

                if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                    sql = $"{sql} output";
            }

            return sql;
        }

        #endregion

        #region Methods

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase
        {
            return base.Set<TEntity>();
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public virtual string GenerateCreateScript()
        {
            return this.Database.GenerateCreateScript();
        }

        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class
        {
            return this.Query<TQuery>().FromSql(sql);
        }

        public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : EntityBase
        {
            return this.Set<TEntity>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        }

        public virtual int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            //set specific command timeout
            var previousTimeout = this.Database.GetCommandTimeout();
            this.Database.SetCommandTimeout(timeout);

            var result = 0;
            if (!doNotEnsureTransaction)
            {
                //use with transaction
                using (var transaction = this.Database.BeginTransaction())
                {
                    result = this.Database.ExecuteSqlCommand(sql, parameters);
                    transaction.Commit();
                }
            }
            else
                result = this.Database.ExecuteSqlCommand(sql, parameters);
            
            //return previous timeout back
            this.Database.SetCommandTimeout(previousTimeout);
            
            return result;
        }

        public virtual void Detach<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityEntry = this.Entry(entity);
            if (entityEntry == null)
                return;
            
            //set the entity is not being tracked by the context
            entityEntry.State = EntityState.Detached;
        }

        #endregion
        
        #region tables
        
        public DbSet<Quiz> Quizes { get; set; }
        
        public DbSet<Question> Questions { get; set; }
        
        public DbSet<Answer> Answers { get; set; }
        
        public DbSet<QuizTheme> QuizThemes { get; set; }
        
        public DbSet<QuestionType> QuestionTypes { get; set; }
        
        public DbSet<AnswerType> AnswerTypes { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Right> Rights { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }
        
        public DbSet<UserRight> UserRights { get; set; }
        
        public DbSet<RoleRight> RoleRights { get; set; }
        
        #endregion
    }
}