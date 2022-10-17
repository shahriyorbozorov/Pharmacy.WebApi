using Microsoft.AspNetCore.Mvc;
using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.ViewModels.Orders;

namespace Pharmacy.WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrdersController(IOrderService orderService)
        {
            _service = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] OrderCreateModel createModel)
        {
            var userId = long.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");
            return Ok(await _service.CreateAsync(userId, createModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] OrderCreateModel orderCreate)
            => Ok(await _service.UpdateAsync(id, orderCreate));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _service.DeleteAsync(o => o.Id == id));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
            => Ok(await _service.GetAsync(o => o.Id == id));

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
            => Ok(await _service.GetAllAsync(null, @params));



    }
}
