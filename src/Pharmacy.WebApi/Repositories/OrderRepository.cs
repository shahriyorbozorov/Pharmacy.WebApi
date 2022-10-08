using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Models;

namespace Pharmacy.WebApi.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
