using System.ComponentModel.DataAnnotations;
namespace Hecoto.Backend.Domain.Entities;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }
    public required string Token { get; set; }
    public required int UserId { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; } = false;

    // Navigation property
    public User User { get; set; } = null!;
}