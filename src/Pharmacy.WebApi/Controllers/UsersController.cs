using Microsoft.AspNetCore.Mvc;
using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.ViewModels.Users;

namespace Pharmacy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] UserCreateModel userCreate)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
        {
            return Ok();
        }

    }
}
