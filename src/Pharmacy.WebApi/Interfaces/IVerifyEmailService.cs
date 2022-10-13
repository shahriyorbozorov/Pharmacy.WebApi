using Pharmacy.WebApi.ViewModels.Emails;
using Pharmacy.WebApi.ViewModels.Users;

namespace Pharmacy.WebApi.Interfaces
{
    public interface IVerifyEmailService
    {
        Task SendCodeAsync(SendCodeToEmailViewModel email);

        Task<bool> VerifyEmail(EmailVerifyViewModel emailVerify);

        Task<bool> VerifyPasswordAsync(UserResetPasswordViewModel model);
    }
}
