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
    public class ContestController : ControllerBase
    {
        private readonly IContestService _contestService;
        private readonly IMapper _mapper;

        public ContestController(IContestService contestService, IMapper mapper)
        {
            _contestService = contestService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Contest.GetAll)]
        [ProducesResponseType(typeof(ContestShortListResponse), 200)]
        public async Task<IActionResult> GetContests()
        {
            var contests = await _contestService.GetContestsAsync();
            return Ok(_mapper.Map<ContestShortListResponse>(contests));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Contest.Get)]
        [ProducesResponseType(typeof(ContestResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var contest = await _contestService.GetContestAsync(id);
            if (contest != null)
                return Ok(_mapper.Map<ContestResponse>(contest));
            return NotFound();
        }

        [Authorize(Roles = Role.Admin + "," + Role.Organizer)]
        [HttpPost(ApiRoutes.Contest.Create)]
        [ProducesResponseType(typeof(ContestShortResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateContest([FromBody] CreateOrUpdateContestRequest request)
        {
            Guid.TryParse(User.FindFirst("id")?.Value, out var userId);

            var createdContest = await _contestService.CreateContestAsync(userId, request);
            if (createdContest != null)
                return Created(ApiHelper.GetResourceUri(HttpContext.Request, ApiRoutes.Contest.Get, createdContest.Id), _mapper.Map<ContestShortResponse>(createdContest));
            return BadRequest();
        }

        [Authorize(Roles = Role.Admin + "," + Role.Organizer)]
        [HttpPut(ApiRoutes.Contest.Update)]
        [ProducesResponseType(typeof(ContestShortResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateContest([FromRoute] Guid id, [FromBody] CreateOrUpdateContestRequest request)
        {
            Guid.TryParse(User.FindFirst("id")?.Value, out var userId);

            var updatedContest = await _contestService.UpdateContestAsync(id, userId, request);
            if (updatedContest != null)
                return Ok(_mapper.Map<ContestShortResponse>(updatedContest));
            return NotFound();
        }

        [Authorize(Roles = Role.Admin + "," + Role.Organizer)]
        [HttpDelete(ApiRoutes.Contest.Delete)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteContest([FromRoute] Guid id)
        {
            Guid.TryParse(User.FindFirst("id")?.Value, out var userId);

            var deleted = await _contestService.DeleteContestAsync(id, userId);
            if (deleted)
                return NoContent();
            return NotFound();
        }
    }
}
