using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using cqrscommon;

namespace cqrswebwritedataacccess
{
    

    public interface IReadEntities
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : Entity;
    }

    public interface IWriteEntities : IReadEntities, IUnitOfWork
    {
        IQueryable<TEntity> Load<TEntity>() where TEntity : Entity;
        void Create<TEntity>(TEntity entity) where TEntity : Entity;
    }

    public interface IUnitOfWork
    {
        int Commit();
    }

    public class WriteDataContext : DbContext
    {
        public WriteDataContext()
        {
            
        }

        public WriteDataContext(string connectionStringOrName) : base(connectionStringOrName)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventRecord>();
            base.OnModelCreating(modelBuilder);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : Entity
        {
            return Set<TEntity>().AsNoTracking(); // detach results from context
        }

        public IQueryable<TEntity> Load<TEntity>() where TEntity : Entity
        {
            return Set<TEntity>();
        }

        public void Create<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (Entry(entity).State == EntityState.Detached)
                Set<TEntity>().Add(entity);
        }

        public int Commit()
        {
            return this.SaveChanges();
        }
    }
}
