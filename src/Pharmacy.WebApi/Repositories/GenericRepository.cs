using Microsoft.EntityFrameworkCore;
using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.IRepositories;
using System.Linq.Expressions;

namespace Pharmacy.WebApi.Repositories
{
    public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TSource> _dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
            _dbSet = _dbContext.Set<TSource>();
        }
        public async Task<TSource> CreateAsync(TSource source)
        {
            var entity = await _dbSet.AddAsync(source);
            return entity.Entity;
        }

        public void DeleteRange(IEnumerable<TSource> sources)
        {
            _dbSet.RemoveRange(sources);
        }

        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>>? expression = null)
        {
            return expression is null ? _dbSet : _dbSet.Where(expression);
        }

        public Task<TSource?> GetAsync(System.Linq.Expressions.Expression<Func<TSource, bool>> expression)
        {
            return _dbSet.FirstOrDefaultAsync(expression);
        }

        public Task<TSource> UpdateAsync(TSource source)
        {
            return Task.FromResult(_dbSet.Update(source).Entity);
        }
    }
}
