using Microsoft.EntityFrameworkCore;
using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Models;

namespace Pharmacy.WebApi.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }
       
        public async Task DeleteAsync(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                throw new Exception("not found");
            _context.Users.Remove(user);
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Email.Equals(email));

            return user is null ? null : user;
        }
    }
}
