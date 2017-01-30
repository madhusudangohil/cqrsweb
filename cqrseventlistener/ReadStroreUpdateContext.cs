using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace cqrseventlistener
{
    public class Entity
    {
        public Guid Id { get; set; }
    }

    public class Television : Entity
    {
        public bool IsOn { get; set; }
        public int FromChannel { get; set; }
        public int ToChannel { get; set; }
        public int Version { get; set; }
    }
    public interface IWriteEntities : IUnitOfWork
    {
        IQueryable<TEntity> Load<TEntity>() where TEntity : Entity;
        void Update<TEntity>(TEntity entity) where TEntity : Entity;
    }

    public interface IUnitOfWork
    {
        int Commit();
    }

    class ReadStroreUpdateContext : DbContext, IWriteEntities
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Television>();
            base.OnModelCreating(modelBuilder);
        }

        public ReadStroreUpdateContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            
        }
        public int Commit()
        {
            return SaveChanges();
        }

        public IQueryable<TEntity> Load<TEntity>() where TEntity : Entity
        {
            return Set<TEntity>();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            Set<TEntity>().AddOrUpdate(entity);
        }

        public void Update<TEntity>(TEntity entity, params Expression<Func<TEntity, Object>>[] properties) where TEntity : Entity
        {
            Entry(entity).State = EntityState.Unchanged;
            foreach (var property in properties)
            {
                //var propertyName = ExpressionHelper.GetExpressionText(property);
                Entry(entity).Property(property).IsModified = true;
            }
        }
    }
}
