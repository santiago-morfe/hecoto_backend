namespace Hecoto.Backend.Domain.Entities;
using Hecoto.Backend.Domain.Enums;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}