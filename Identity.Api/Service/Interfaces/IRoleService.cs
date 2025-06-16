using Identity.Api.Model;
using Identity.Api.Model.DTOs;

namespace Identity.Api.Service.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRoles();
        Task DeleteUserRole(int id);
        Task<Role> GetRoleById(int id);
        Task<UserDTO> GetUserByRoleId(int id);

    }
}
