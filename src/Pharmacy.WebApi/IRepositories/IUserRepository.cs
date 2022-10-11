using Pharmacy.WebApi.Models;

namespace Pharmacy.WebApi.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task DeleteAsync(long id);
        Task<User?> FindByEmailAsync(string email);
    }
}
