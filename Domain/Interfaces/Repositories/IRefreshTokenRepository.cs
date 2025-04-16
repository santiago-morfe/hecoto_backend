namespace Hecoto.Backend.Domain.Interfaces.Repositories;
using Hecoto.Backend.Domain.Entities;
using System;

public interface IRefreshTokenRepository
{
    Task AddRefreshTokenAsync(RefreshToken token);
    Task DeleteRefreshTokenAsync(int id);
    Task<IEnumerable<RefreshToken>> GetRefreshTokensByUserIdAsync(int userId);
    Task<RefreshToken?> GetRefreshTokenByIdAsync(int id);
    Task<RefreshToken?> GetRefreshTokenByUserIdAsync(int userId);
    Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token);
}