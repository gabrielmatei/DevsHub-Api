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
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevsHub.Controllers.V1
{
    [ApiController]
    [Produces("application/json")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementsService _announcements;
        private readonly IMapper _mapper;

        public AnnouncementController(IAnnouncementsService announcements, IMapper mapper)
        {
            _announcements = announcements;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Announcement.GetAll)]
        [ProducesResponseType(typeof(AnnouncementListResponse), 200)]
        public async Task<IActionResult> GetAnnouncements()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var categories = await _announcements.GetAnnouncementsAsync(showAll: userRole == Role.Admin);
            return Ok(_mapper.Map<AnnouncementListResponse>(categories));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost(ApiRoutes.Announcement.Create)]
        [ProducesResponseType(typeof(AnnouncementResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateOrUpdateAnnouncementRequest request)
        {
            var createdCategory = await _announcements.CreateAnnouncementAsync(request);
            if (createdCategory != null)
                return Created(ApiHelper.GetResourceUri(HttpContext.Request, ApiRoutes.Announcement.GetAll), _mapper.Map<AnnouncementResponse>(createdCategory));
            return BadRequest();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut(ApiRoutes.Announcement.Update)]
        [ProducesResponseType(typeof(AnnouncementResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAnnouncement([FromRoute] Guid id, [FromBody] CreateOrUpdateAnnouncementRequest request)
        {
            var updatedCategory = await _announcements.UpdateAnnouncementAsync(id, request);
            if (updatedCategory != null)
                return Ok(_mapper.Map<AnnouncementResponse>(updatedCategory));
            return NotFound();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete(ApiRoutes.Announcement.Delete)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute] Guid id)
        {
            var deleted = await _announcements.DeleteAnnouncementAsync(id);
            if (deleted)
                return NoContent();
            return NotFound();
        }
    }
}
