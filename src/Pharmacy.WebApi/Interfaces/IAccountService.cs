using Pharmacy.WebApi.ViewModels.Users;

namespace Pharmacy.WebApi.Interfaces

{
    public interface IAccountService
    {
        Task<bool> RegistrAsync(UserCreateModel userCreateModel);
        Task<string?> LoginAsync(UserLoginViewModel userLoginViewModel);
        Task<bool> VerifyPasswordAsync(UserResetPasswordViewModel password);

        
    }
}
