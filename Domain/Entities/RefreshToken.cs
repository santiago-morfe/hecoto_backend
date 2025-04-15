namespace Hecoto.Backend.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public int UserId { get; set; } // Cambiado de Guid a int para que coincida con la clave primaria de User
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }

    // Navigation property
    public User User { get; set; } = null!;
}