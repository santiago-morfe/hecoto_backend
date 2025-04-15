using System.Threading.Tasks;

namespace Hecoto.Backend.Domain.Interfaces.Repositories;
using Hecoto.Backend.Domain.Entities;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
}