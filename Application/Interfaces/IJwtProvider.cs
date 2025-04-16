namespace Hecoto.Backend.Application.Interfaces;
public interface IJwtProvider
{
    public string GenerateToken(int userId, string email, string role);
    // decodificar el token y obtener el id del usuario, el email y el rol
    (int userId, string email, string role) DecodeToken(string token);

    public bool ValidateToken(string token);
    

}
