using Microsoft.AspNetCore.Mvc;
using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.ViewModels.Users;

namespace Pharmacy.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] UserCreateModel userCreate)
        {
            var result = await _userService.UpdateAsync(id, userCreate);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateIamgeAsync(long id, [FromForm] UserImageUpdateViewModel userCreateViewModel)
        {
            return Ok(await _userService.ImageUpdate(id, userCreateViewModel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var res = await _userService.DeleteAsync(p => p.Id == id);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var res = await _userService.GetAsync(p => p.Id == id);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var res = await _userService.GetAllAsync(null, @params);

            return Ok(res);
        }

    }
}
