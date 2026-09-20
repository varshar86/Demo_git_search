using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Searchify.Application.Commands;
using Searchify.Domain.Model;

namespace Searchify.API.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        try
        {
            var token = await _mediator.Send(new AuthenticateCommand(user));

            if (string.IsNullOrWhiteSpace(token) ||
                token.StartsWith("Username or Password incorrect", StringComparison.OrdinalIgnoreCase))
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            return Ok(new
            {
                Token = token,
                UserName = user.UserName,
                ExpiresInMinutes = 60
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Message = "An unexpected error occurred.",
                Detail = ex.Message
            });
        }
    }
}
