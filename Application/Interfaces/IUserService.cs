namespace Hecoto.Backend.Application.Interfaces;
using Hecoto.Backend.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    Task<bool> RegisterUserAsync(string userName, string password,string role);
    Task<bool> UpdateUserAsync(string userId, string userName, string role);
    Task<bool> DeleteUserAsync(string userId);
    Task<User?> GetUserByIdAsync(string userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
}