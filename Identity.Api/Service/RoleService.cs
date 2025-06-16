using Identity.Api.Model;
using Identity.Api.Model.DTOs;
using Identity.Api.Service.Interfaces;
using Identity.Service.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Service
{
    public class RoleService : IRoleService
    {
        private readonly IdentityContext _context;

        public RoleService(IdentityContext context)
        {
            _context = context;
        }

        public async Task<Role> createRole(RawRole role)
        {
            var newRole = new Role
            {
                Type = role.Type
            };
            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync();
            return newRole;
        }

        public async Task<int> DeleteUserRole(int id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole == null)
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }
            _context.UserRoles.Remove(userRole);
            try
            {
                await _context.SaveChangesAsync();
                return id;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException($"Role with ID {id} could not be deleted due to concurrency issues.");
            }
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }
            return role;
        }

        public async Task<List<UserDTO>> GetUsersByRoleId(int id)
        {
            var userList = await _context.UserRoles
                .Where(ur => ur.RoleId == id)
                .Include(ur => ur.User)
                .Select(ur => new UserDTO
                {
                    Id = ur.User.Id,
                    FirstName = ur.User.FirstName,
                    LastName = ur.User.LastName,
                    Email = ur.User.Email
                })
                .ToListAsync();
            return userList;
        }

        public async Task<int> UpdateRole(Role role)
        {
            try
            {
                var entity = await FindSingleRole(role.Id);
                if (!string.IsNullOrWhiteSpace(role.Type)) entity.Type = role.Type;
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException($"Role with ID {role.Id} could not be updated due to a database error.");
            }
        }

        private async Task<Role> FindSingleRole(int roleId)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == roleId);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with Id {roleId} not found.");
            }
            return role;
        }
    }
}
