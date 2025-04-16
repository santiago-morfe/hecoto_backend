
namespace Hecoto.Backend.Application.Interfaces;

// Interfaz mejorada de IAuthService
public interface IAuthService
{
    // amvaos metodos regresan el token de acceso y el de refresco
    Task<(string accessToken, string refreshToken)> AuthenticateAsync(string username, string password);
    Task<string> RefreshTokenAsync(string refreshToken, string accessToken);

    // Task<string> RevokeTokenAsync(string refreshToken, string accessToken);

}