using Microsoft.EntityFrameworkCore;
using PostItNote.DataAccess.Data;
using PostItNote.DataAccess.Repository.IRepository;
using System.Linq.Expressions;
using System.Linq;

namespace PostItNote.DataAccess.Repository
{
    public class Repository<T>: IRepository<T> where T :class
    { 
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbset.Add(entity);
            _db.SaveChanges();
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? Filter = null, string? includeproperties = null)
        {
            IQueryable<T> query = dbset;
            if (Filter != null)
            {
                query = query.Where(Filter);
            }
            if (includeproperties != null)
            {
                foreach (var inclideprop in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {   
                    query = query.Include(inclideprop);
                }
            }
            return query.ToList();
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> Filter, string? includeproperties = null)
        {
            IQueryable<T> query = dbset;
            query = query.Where(Filter);
            if (includeproperties != null)
            {
                foreach (var inclideprop in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inclideprop);
                }
            }
            return query.FirstOrDefault();
        }
        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }
    }
}
