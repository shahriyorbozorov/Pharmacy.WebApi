using Microsoft.AspNetCore.Mvc;
using Pharmacy.WebApi.ViewModels.Users;

namespace Pharmacy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("registr")]
        public async Task<IActionResult> RegistrAsync([FromForm] UserCreateModel userCreateModel)
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] UserLoginViewModel userLoginView)
        {
            return Ok();
        }
    }
}
