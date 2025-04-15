namespace Hecoto.Backend.Application.Services;
using Hecoto.Backend.Application.Interfaces;
using Hecoto.Backend.Domain.Entities;
using Hecoto.Backend.Domain.Enums;
using Hecoto.Backend.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

public class AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IRefreshTokenRepository refreshTokenRepository) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<string> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || !_passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }
        // generar token de refresh
            var refreshToken = Guid.NewGuid().ToString();
            
        var token = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7) // 7 días de expiración
        };
        await _refreshTokenRepository.AddRefreshTokenAsync(token);

        return refreshToken;
    }

    public async Task<string> RefreshTokenAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(refreshToken);
        if (token == null || token.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        var user = await _userRepository.GetByIdAsync(token.UserId);
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        return _jwtProvider.GenerateToken(Guid.NewGuid(), user.Username, user.Role.ToString());
    }

    // public async Task RevokeRefreshTokenAsync(string refreshToken)
    // {
    //     var token = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(refreshToken);
    //     if (token == null)
    //     {
    //         throw new UnauthorizedAccessException("Invalid refresh token");
    //     }

    //     await _refreshTokenRepository.DeleteRefreshTokenAsync(token.Id);
    // }
}