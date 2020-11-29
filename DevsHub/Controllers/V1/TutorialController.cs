using AutoMapper;
using DevsHub.Contracts.V1;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Contracts.V1.Responses;
using DevsHub.Data;
using DevsHub.Helpers;
using DevsHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DevsHub.Controllers.V1
{
    [ApiController]
    [Produces("application/json")]
    public class TutorialController : ControllerBase
    {
        private readonly ITutorialService _tutorialService;
        private readonly IMapper _mapper;

        public TutorialController(ITutorialService tutorialService, IMapper mapper)
        {
            _tutorialService = tutorialService;
            _mapper = mapper;
        }

        #region Tutorials
        [HttpGet(ApiRoutes.Tutorial.GetAll)]
        [ProducesResponseType(typeof(TutorialListResponse), 200)]
        public async Task<IActionResult> GetTutorials()
        {
            var tutorials = await _tutorialService.GetTutorialsAsync();
            return Ok(_mapper.Map<TutorialListResponse>(tutorials));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Tutorial.Get)]
        [ProducesResponseType(typeof(TutorialResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTutorial([FromRoute] Guid id)
        {
            var tutorial = await _tutorialService.GetTutorialAsync(id);
            if (tutorial != null)
                return Ok(_mapper.Map<TutorialResponse>(tutorial));
            return NotFound();
        }

        [Authorize]
        [HttpPost(ApiRoutes.Tutorial.Create)]
        [ProducesResponseType(typeof(TutorialShortResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateTutorial([FromBody] CreateOrUpdateTutorialRequest request)
        {
            Guid.TryParse(User.FindFirst("id")?.Value, out var userId);

            var createdTutorial = await _tutorialService.CreateTutorialAsync(userId, request);
            if (createdTutorial != null)
                return Created(ApiHelper.GetResourceUri(HttpContext.Request, ApiRoutes.Tutorial.Get, createdTutorial.Id), _mapper.Map<TutorialShortResponse>(createdTutorial));
            return BadRequest();
        }

        [Authorize]
        [HttpPut(ApiRoutes.Tutorial.Update)]
        [ProducesResponseType(typeof(TutorialShortResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTutorial([FromRoute] Guid id, [FromBody] CreateOrUpdateTutorialRequest request)
        {
            Guid.TryParse(User.FindFirst("id")?.Value, out var userId);

            var updatedTutorial = await _tutorialService.UpdateTutorialAsync(id, userId, request);
            if (updatedTutorial != null)
                return Ok(_mapper.Map<TutorialShortResponse>(updatedTutorial));
            return NotFound();
        }

        [Authorize]
        [HttpDelete(ApiRoutes.Tutorial.Delete)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTutorial([FromRoute] Guid id)
        {
            Guid.TryParse(User.FindFirst("id")?.Value, out var userId);

            var deleted = await _tutorialService.DeleteTutorialAsync(id, userId);
            if (deleted)
                return NoContent();
            return NotFound();
        }
        #endregion

        #region Categories
        [HttpGet(ApiRoutes.Tutorial.Category.GetAll)]
        [ProducesResponseType(typeof(TutorialCategoryListResponse), 200)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _tutorialService.GetTutorialCategoriesAsync();
            return Ok(_mapper.Map<TutorialCategoryListResponse>(categories));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost(ApiRoutes.Tutorial.Category.Create)]
        [ProducesResponseType(typeof(TutorialCategoryResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateOrUpdateTutorialCategoryRequest request)
        {
            var createdCategory = await _tutorialService.CreateTutorialCategoryAsync(request);
            if (createdCategory != null)
                return Created(ApiHelper.GetResourceUri(HttpContext.Request, ApiRoutes.Tutorial.Category.GetAll), _mapper.Map<TutorialCategoryResponse>(createdCategory));
            return BadRequest();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut(ApiRoutes.Tutorial.Category.Update)]
        [ProducesResponseType(typeof(TutorialCategoryResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] CreateOrUpdateTutorialCategoryRequest request)
        {
            var updatedCategory = await _tutorialService.UpdateTutorialCategoryAsync(id, request);
            if (updatedCategory != null)
                return Ok(_mapper.Map<TutorialCategoryResponse>(updatedCategory));
            return NotFound();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete(ApiRoutes.Tutorial.Category.Delete)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var deleted = await _tutorialService.DeleteTutorialCategoryAsync(id);
            if (deleted)
                return NoContent();
            return NotFound();
        }
        #endregion
    }
}
