using DevsHub.Contracts.V1;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DevsHub.Controllers.V1
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet(ApiRoutes.Users.GetAll)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _usersService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet(ApiRoutes.Users.Get)]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await _usersService.GetUserAsync(id);
            if (user != null)
                return Ok(user);
            return NotFound();
        }

        [HttpPost(ApiRoutes.Users.Create)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var createdUser = await _usersService.CreateUserAsync(request);
            if (createdUser != null)
            {
                var locationUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}/{ApiRoutes.Users.Get.Replace("{id}", createdUser.Id.ToString())}";
                return Created(locationUri, createdUser);
            }
            return NotFound();
        }

        [HttpPut(ApiRoutes.Users.Update)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
        {
            var updatedUser = await _usersService.UpdateUserAsync(id, request);
            if (updatedUser != null)
                return Ok();
            return NotFound();
        }

        [HttpDelete(ApiRoutes.Users.Delete)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var deleted = await _usersService.DeleteUserAsync(id);
            if (deleted)
                return NoContent();
            return NotFound();
        }
    }
}
