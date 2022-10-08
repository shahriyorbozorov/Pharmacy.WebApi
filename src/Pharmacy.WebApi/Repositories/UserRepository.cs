using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Models;

namespace Pharmacy.WebApi.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
