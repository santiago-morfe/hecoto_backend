namespace Hecoto.Backend.Application.Interfaces;
public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string hash, string password);
}
