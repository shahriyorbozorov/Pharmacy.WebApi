using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.Models;
using Pharmacy.WebApi.ViewModels.Users;
using System.Linq.Expressions;

namespace Pharmacy.WebApi.Services
{
    public class UserService : IUserService
    {
       
        public Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserViewModel>> GetAllAsync(Expression<Func<User, bool>>? expression = null, PaginationParams? parameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<UserViewModel?> GetAsync(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(long id, UserCreateModel userCreate)
        {
            throw new NotImplementedException();
        }
    }
}
