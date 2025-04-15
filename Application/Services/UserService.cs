using Hecoto.Backend.Application.Interfaces;
using Hecoto.Backend.Domain.Entities;
using Hecoto.Backend.Domain.Interfaces.Repositories;
using Hecoto.Backend.Domain.Enums;
using System.Data;

namespace Hecoto.Backend.Application.Services
{
    public class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<bool> RegisterUserAsync(string userName, string password, string role)
        {
            var hashedPassword = _passwordHasher.Hash(password);
            var user = new User
            {
                Username = userName,
                PasswordHash = hashedPassword,
                Role = (UserRole)Enum.Parse<UserRole>(role, true) // Convertir el string a enum correctamente
            };

            if (await _userRepository.GetByUsernameAsync(userName) != null)
            {
                return false; // El usuario ya existe
            }

            await _userRepository.AddAsync(user);
            return true; // Registro exitoso
        }

        public async Task<bool> UpdateUserAsync(string userId, string userName, string role)
        {

            var user = await _userRepository.GetByIdAsync(int.Parse(userId));
            if (user == null)
            {
                return false; // Usuario no encontrado
            }

            user.Username = userName;
            user.Role = (UserRole)Enum.Parse<UserRole>(role, true); // Convertir el string a enum correctamente

            await _userRepository.UpdateAsync(user);
            return true; // Actualización exitosa
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {

            var user = await _userRepository.GetByIdAsync(int.Parse(userId));
            if (user == null)
            {
                return false; // Usuario no encontrado
            }

            await _userRepository.DeleteAsync(int.Parse(userId));
            return true; // Eliminación exitosa
        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {

            return await _userRepository.GetByIdAsync(int.Parse(userId));
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}