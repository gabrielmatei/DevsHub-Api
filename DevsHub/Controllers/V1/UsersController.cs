using AutoMapper;
using DevsHub.Contracts.V1;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Contracts.V1.Responses;
using DevsHub.Domain;
using DevsHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DevsHub.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Users.GetAll)]
        [ProducesResponseType(typeof(UserListResponse), 200)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _usersService.GetUsersAsync();
            return Ok(_mapper.Map<UserListResponse>(users));
        }

        [HttpGet(ApiRoutes.Users.Get)]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await _usersService.GetUserAsync(id);
            if (user != null)
                return Ok(_mapper.Map<UserResponse>(user));
            return NotFound();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost(ApiRoutes.Users.Create)]
        [ProducesResponseType(typeof(UserResponse), 20)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var createdUser = await _usersService.CreateUserAsync(request);
            if (createdUser != null)
            {
                var locationUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/{ApiRoutes.Users.Get.Replace("{id}", createdUser.Id.ToString())}";
                return Created(locationUri, _mapper.Map<UserResponse>(createdUser));
            }
            return BadRequest();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut(ApiRoutes.Users.Update)]
        [ProducesResponseType(typeof(UserResponse), 20)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
        {
            var updatedUser = await _usersService.UpdateUserAsync(id, request);
            if (updatedUser != null)
                return Ok(_mapper.Map<UserResponse>(updatedUser));
            return NotFound();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete(ApiRoutes.Users.Delete)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var deleted = await _usersService.DeleteUserAsync(id);
            if (deleted)
                return NoContent();
            return NotFound();
        }
    }
}
