using Listive.Messages;
using Logins.Api.Requests;
using Logins.Api.Responses;
using Logins.Api.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IPublishEndpoint _publisher;

    public AuthenticationController(
        ILogger<AuthenticationController> logger,
        UserManager<IdentityUser> userManager,
        IJwtService jwtService,
        IPublishEndpoint publisher)
    {
        _logger = logger;
        _userManager = userManager;
        _jwtService = jwtService;
        _publisher = publisher;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest("Bad credentials");

        _logger.LogInformation("Generating new token for {Email}", request.Email);

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null) return BadRequest("Bad credentials");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            await _publisher.Publish(new LoginAttemptEvent(request.Email, Request.HttpContext.Connection.RemoteIpAddress.ToString()));

            return BadRequest("Bad credentials");
        }

        var token = _jwtService.CreateToken(user);

        return Ok(token);
    }

    [Authorize]
    [HttpGet("verify")]
    public async Task<IActionResult> Verify()
    {
        var user = await _userManager.GetUserAsync(User);

        return Ok(new UserResponse(user.UserName, user.Email));
    }
}
