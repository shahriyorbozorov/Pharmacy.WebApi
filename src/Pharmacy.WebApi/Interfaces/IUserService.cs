using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.Models;
using Pharmacy.WebApi.ViewModels.Users;
using System.Linq.Expressions;

namespace Pharmacy.WebApi.Interfaces
{
    public interface IUserService
    {
        Task<bool> UpdateAsync(long id, UserCreateModel userCreate);

        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);

        Task<UserViewModel?> GetAsync(Expression<Func<User, bool>> expression);

        Task<IEnumerable<UserViewModel>> GetAllAsync(Expression<Func<User, bool>>? expression = null,
            PaginationParams? @params = null);
        Task<bool> ImageUpdate(long id, UserImageUpdateViewModel model);
    }
}
