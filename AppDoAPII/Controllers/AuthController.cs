using AppDoAPI.Models.Requests;
using AppDoAPI.Models.Responses;
using AppDoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppDoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginRequest request)
    {
        LoginResponse response =
            await _authService.LoginAsync(request);

        if (!response.Exitoso)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RegisterResponse>> Register(
        [FromBody] RegisterRequest request)
    {
        RegisterResponse response =
            await _authService.RegisterAsync(request);

        if (!response.Exitoso)
        {
            return Conflict(response);
        }

        return StatusCode(
            StatusCodes.Status201Created,
            response);
    }
}