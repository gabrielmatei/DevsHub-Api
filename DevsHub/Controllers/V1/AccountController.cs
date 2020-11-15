using AutoMapper;
using DevsHub.Contracts.V1;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Contracts.V1.Responses;
using DevsHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DevsHub.Controllers.V1
{
    [ApiController]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Account.Register)]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _accountService.RegisterAsync(request);
            if (response != null)
                return Ok(response);
            return BadRequest();
        }

        [HttpPost(ApiRoutes.Account.Login)]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _accountService.LoginAsync(request);
            if (response != null)
                return Ok(response);
            return BadRequest();
        }

        [Authorize]
        [HttpGet(ApiRoutes.Account.Get)]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAccount()
        {
            Guid.TryParse(User.FindFirst("id")?.Value, out var userId);

            var response = await _accountService.GetAccountAsync(userId);
            if (response != null)
                return Ok(_mapper.Map<AccountResponse>(response));
            return BadRequest();
        }
    }
}
