using DevsHub.Contracts.V1;
using DevsHub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevsHub.Controllers.V1
{
    [ApiController]
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
        public async Task<IActionResult> GetAll()
        {
            var values = await _valueService.GetValuesAsync();
            return Ok(values);
        }
    }
}
