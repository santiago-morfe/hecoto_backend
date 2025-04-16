namespace Hecoto.Backend.Application.Services;
using Hecoto.Backend.Application.Interfaces;
using Hecoto.Backend.Domain.Entities;
using Hecoto.Backend.Domain.Enums;
using Hecoto.Backend.Domain.Interfaces.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IRefreshTokenRepository refreshTokenRepository) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<(string accessToken, string refreshToken)> AuthenticateAsync(string username, string password)
    {
        // verificar si el usuario existe
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }
        // verificar si la contraseña es correcta
        if (!_passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }
        // generar token de acceso
        var accessToken = _jwtProvider.GenerateToken(user.Id, user.Username, user.Role.ToString());

        // generar token de refresco
        var refreshToken = Guid.NewGuid().ToString();
        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7), // el token de refresco expira en 7 dias
            IsRevoked = false
        };
        // guardar el token de refresco en la base de datos 
        await _refreshTokenRepository.AddRefreshTokenAsync(refreshTokenEntity);
        // regresar el token de acceso y el de refresco

        return (accessToken, refreshToken);
    }

    public async Task<string> RefreshTokenAsync(string refreshToken, string accessToken)
    {
        // verificar si el token de acceso es valido
        if(_jwtProvider.ValidateToken(accessToken))
        {
            throw new UnauthorizedAccessException("Invalid access token");
        }
        // optener id del usuario a partir del token de acceso que es un JWT
        var userId = _jwtProvider.DecodeToken(accessToken).userId;

        // optener el usuario a partir del id
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid user");
        }

        // optener los tokens de refresco del usuario
        var tokens = await _refreshTokenRepository.GetRefreshTokensByUserIdAsync(userId);

        // verificar si el token de refresco es valido
        var token = tokens.FirstOrDefault(t => t.Token == refreshToken);

        if (token == null || token.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid refresh token");
        }
        
        // generar nuevo token de acceso
        var newAccessToken = _jwtProvider.GenerateToken(user.Id, user.Username, user.Role.ToString());
        
        // regresar el nuevo token de acceso
        return newAccessToken;
    }
}