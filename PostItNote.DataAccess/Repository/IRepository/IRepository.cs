using System.Linq.Expressions;

namespace PostItNote.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> Filter, string? includeproperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? Filter = null, string? includeproperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
