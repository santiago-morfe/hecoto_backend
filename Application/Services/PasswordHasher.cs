using BCrypt.Net; // Importar la librería BCrypt.Net
using Hecoto.Backend.Application.Interfaces;

namespace Hecoto.Backend.Application.Services;
public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        // Usar BCrypt para generar un hash seguro
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        // Usar BCrypt para verificar la contraseña
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}