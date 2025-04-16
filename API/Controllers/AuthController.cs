namespace Hecoto.Backend.API.Controllers;
using Hecoto.Backend.Application.Interfaces;
using Hecoto.Backend.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, IUserService userService ) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Username and password are required.");
        }

        try
        {
            var  (accessToken, refreshToken) = await _authService.AuthenticateAsync(request.Username, request.Password);
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(new LoginResponseDTO { AccessToken = accessToken, RefreshToken = refreshToken });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid credentials");
        }
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO request)
    {
        if (string.IsNullOrEmpty(request.RefreshToken) || string.IsNullOrEmpty(request.AccessToken))
        {
            return BadRequest("Refresh token and access token are required.");
        }

        try
        {
            var newAccessToken = await _authService.RefreshTokenAsync(request.RefreshToken, request.AccessToken);
            return Ok(new RefreshTokenResponseDTO { AccessToken = newAccessToken });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid or expired refresh token");
        }
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
    {
        
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Username and password are required.");
        }

        try
        {
            var result = await _userService.RegisterUserAsync(request.Username, request.Password, "User");
            if (!result)
            {
                return BadRequest("User already exists.");
            }
            
            var ( accessToken, refreshToken) = await _authService.AuthenticateAsync(request.Username, request.Password);

            return Ok(new RegisterResponseDTO { AccessToken = accessToken, RefreshToken = refreshToken });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}