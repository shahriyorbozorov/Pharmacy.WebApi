using Microsoft.AspNetCore.Mvc;
using Pharmacy.WebApi.Common.Exceptions;
using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.ViewModels.Drugs;
using System.Net;

namespace Pharmacy.WebApi.Controllers
{
    [Route("api/drugs")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugService _service;
        public DrugsController(IDrugService drugService)
        {
            _service = drugService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] DrugCreateModel createModel)
            => Ok(await _service.CreateAsync(createModel));


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] long id)
            => Ok(await _service.GetAsync(d => d.Id == id));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _service.GetAllAsync(@params));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _service.DeleteAsync(drug => drug.Id == id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] DrugCreateModel createModel)
            => Ok(await _service.UpdateAsync(id, createModel));


    }
}
