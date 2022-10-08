using System.Linq.Expressions;

namespace Pharmacy.WebApi.IRepositories
{
    public interface IGenericRepository<TSource> where TSource : class
    {

        Task<TSource> CreateAsync(TSource source);
        Task<TSource> UpdateAsync(TSource source);
        void DeleteRange(IEnumerable<TSource> sources);
        Task<TSource?> GetAsync(Expression<Func<TSource, bool>> expression);
        IQueryable<TSource> GetAll(Expression<Func<TSource, bool>>? expression = null);
    }
}
