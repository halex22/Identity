using Identity.Api.Model;
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

        public async Task DeleteUserRole(int id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole == null)
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }

            throw new NotImplementedException();
        }

        public async Task<List<Role>> GetAllUserRoles()
        {
           return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role= await _context.Roles.FindAsync(id);
            if (role== null)
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }
            return role;
        }
    }
}
