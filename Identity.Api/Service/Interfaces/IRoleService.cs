using Identity.Api.Model;
using Identity.Api.Model.DTOs;

namespace Identity.Api.Service.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRoles();
        Task<int> DeleteUserRole(int id);
        Task<Role> GetRoleById(int id);
        Task<List<UserDTO>> GetUsersByRoleId(int id);
        Task<int> UpdateRole(Role role);
        Task<Role> createRole(RawRole role);

    }
}
