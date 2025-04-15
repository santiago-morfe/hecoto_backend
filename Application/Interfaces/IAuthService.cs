
namespace Hecoto.Backend.Application.Interfaces;

// Interfaz mejorada de IAuthService
public interface IAuthService
{
    Task<string> AuthenticateAsync(string username, string password);
    Task<string> RefreshTokenAsync(string refreshToken);

}