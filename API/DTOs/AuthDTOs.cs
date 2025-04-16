namespace Hecoto.Backend.API.DTOs;

public class RegisterRequestDTO
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterResponseDTO
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

public class LoginRequestDTO
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDTO
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

public class RefreshTokenRequestDTO
{
    public string RefreshToken { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
}

public class RefreshTokenResponseDTO
{
    public string AccessToken { get; set; } = string.Empty;
}