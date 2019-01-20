using System;
using System.Linq;
using QuizData.EntityBase;
using QuizData.Models;


namespace QuizData.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BussinessEntityBase
    {
        
        private ApplicationDbContext dbContext  { get; }
 
        public Repository(ApplicationDbContext repositoryContext)
        {
            dbContext = repositoryContext;
        }
        
        private bool disposed;

        protected void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
                {
                    dbContext.Dispose();
                }
            }
            disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public TEntity GetObjByID(int objID)
        {
            try
            {
                return dbContext.Set<TEntity>().Find(objID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IQueryable<TEntity> GetObjList()
        {
            try
            {
                return dbContext.Set<TEntity>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }   
        }

        public TEntity AddObj(TEntity obj)
        {
            try
            {
                dbContext.Set<TEntity>().Add(obj);
                dbContext.SaveChanges();
                return obj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public TEntity UpdateObj(TEntity obj)
        {
            try
            {
                dbContext.Set<TEntity>().Update(obj);
                dbContext.SaveChanges();
                return obj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public TEntity DeleteObj(int objID)
        {
            try
            {
                var dbEntry = dbContext.Set<TEntity>().FirstOrDefault(p => p.ID == objID);

                if (dbEntry != null)
                {
                    dbContext.Remove(dbEntry);
                    dbContext.SaveChanges();
                }

                return dbEntry;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}