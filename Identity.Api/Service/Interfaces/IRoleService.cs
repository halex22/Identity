using Identity.Api.Model;

namespace Identity.Api.Service.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllUserRoles();
        Task DeleteUserRole(int id);
        Task<Role> GetRoleById(int id);
    }
}
