using DevsHub.Contracts.V1;
using DevsHub.Domain;
using DevsHub.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevsHub.Controllers.V1
{
    [ApiController]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        private readonly IValueService _valueService;

        public ValuesController(IValueService valueService)
        {
            _valueService = valueService;
        }

        /// <summary>
        /// Returns all values
        /// </summary>
        /// <response code="200">Returns all values</response>
        [HttpGet(ApiRoutes.Values.GetAll)]
        [ProducesResponseType(typeof(List<Value>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var values = await _valueService.GetValuesAsync();
            return Ok(values);
        }

        [HttpGet(ApiRoutes.Values.Get)]
        [ProducesResponseType(typeof(Value), 200)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var value = await _valueService.GetValueByIdAsync(id);
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        [HttpPost(ApiRoutes.Values.Create)]
        public async Task<IActionResult> Create([FromBody] Value request)
        {
            var value = new Value
            {
                Name = request.Name
            };

            await _valueService.CreateValueAsync(value);

            var locationUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/{ApiRoutes.Values.Get.Replace("{id}", value.Id.ToString())}";

            var response = new Value { Id = value.Id };

            return Created(locationUri, response);
        }

        [HttpPut(ApiRoutes.Values.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Value request)
        {
            var value = await _valueService.GetValueByIdAsync(id);
            value.Name = request.Name;

            var updated = await _valueService.UpdateValueAsync(value);
            if (updated)
                return Ok(value);
            return NotFound();
        }

        [HttpDelete(ApiRoutes.Values.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _valueService.DeleteValueAsync(id);
            if (deleted)
                return NoContent();
            return NotFound();
        }
    }
}
