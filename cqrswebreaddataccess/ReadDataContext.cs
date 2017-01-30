using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cqrswebreaddataccess
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
    
    public interface IReader
    {
        IQueryable<T> Query<T>() where T : Entity;
    }
    public class ReadDataContext : DbContext, IReader
    {
        public ReadDataContext()
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Television>();
            base.OnModelCreating(modelBuilder);
        }

        public ReadDataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            
        }

        public IQueryable<T> Query<T>() where T: Entity
        {
            return Set<T>().AsNoTracking();
        }
    }
}
