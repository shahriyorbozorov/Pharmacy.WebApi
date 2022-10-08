using Microsoft.AspNetCore.Mvc;
using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.ViewModels.Orders;

namespace Pharmacy.WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] OrderCreateModel createModel)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] OrderCreateModel orderCreate)
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
