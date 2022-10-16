using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.ViewModels.Emails;
using Pharmacy.WebApi.ViewModels.Users;

namespace Pharmacy.WebApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IVerifyEmailService _verifyEmailService;

        public AccountController(IAccountService accountService, IVerifyEmailService verifyEmailService)
        {
            _accountService = accountService;
            _verifyEmailService = verifyEmailService;
        }

        [HttpPost("registr"), AllowAnonymous]
        public async Task<IActionResult> RegistrAsync([FromBody] UserCreateModel userCreateViewModel)
            => Ok(await _accountService.RegistrAsync(userCreateViewModel));

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginViewModel logInViewModel)
            => Ok(new { Token = (await _accountService.LoginAsync(logInViewModel)) });

        [HttpPost("verify-email"), AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromBody] EmailVerifyViewModel email)
            => Ok(await _verifyEmailService.VerifyEmail(email));

        [HttpPost("send-code-to-email"), AllowAnonymous]
        public async Task<IActionResult> SendToEmail([FromBody] SendCodeToEmailViewModel email)
        {
            await _verifyEmailService.SendCodeAsync(email);
            return Ok();
        }

        [HttpPost("reset-password"), AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] UserResetPasswordViewModel forgotPassword)
        {
            return Ok(await _accountService.VerifyPasswordAsync(forgotPassword));
        }
    }
}
