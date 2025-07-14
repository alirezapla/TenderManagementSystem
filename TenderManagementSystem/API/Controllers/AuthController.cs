using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenderManagementSystem.Application.Commands.Auth;
using TenderManagementSystem.Application.DTOs;

namespace TenderManagementSystem.API.Controllers;

[Route("[controller]")]
public class AuthController : BaseApiController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
    {
        var command = new RegisterUserCommand {Model = model};
        var result = await _mediator.Send(command);
        if (result.Succeeded)
        {
            return Ok(new {userId = result.UserId, Message = "User account created successfully"});
        }
        else
        {
            return BadRequest(new {Errors = result.Errors});
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        var command = new LoginUserCommand() {Model = model};

        var token = await _mediator.Send(command);
        return Ok(new {Token = token});
    }
}