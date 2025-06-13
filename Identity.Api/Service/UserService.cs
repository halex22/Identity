using Identity.Api.Model.DTOs;
using Identity.Api.Service.Interfaces;
using Identity.Model;
using Identity.Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Service
{
    public class UserService : IUserService
    {
        private readonly IdentityContext _context;

        public UserService(IdentityContext context)
        {
            _context = context;
        }

        public Task<User> CreateUser(User user)
        {
            var userToCreate = user;
            _context.Users.Add(userToCreate);
            return _context.SaveChangesAsync().ContinueWith(t => userToCreate);
        }

        public Task<UserDTO> CreateUser(RawUserDTO user)
        {
            var userToCreate = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password
            };
            _context.Users.Add(userToCreate);
            return _context.SaveChangesAsync()
                .ContinueWith(t => new UserDTO
                {
                    Id = userToCreate.Id,
                    FirstName = userToCreate.FirstName,
                    LastName = userToCreate.LastName,
                    Email = userToCreate.Email
                }); ;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            _context.Users.Remove(user);
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.Include(u => u.Requests).ToListAsync();
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null
                ? throw new KeyNotFoundException($"User with ID {id} not found.")
                : new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
        }

        public Task<UserDTO> UpdateUser(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
