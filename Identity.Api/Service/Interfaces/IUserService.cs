﻿using Identity.Api.Model;
using Identity.Api.Model.DTOs;
using Identity.Model;
using Identity.Service.Model;

namespace Identity.Api.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<UserDTO> CreateUser(RawUserDTO user);
        Task<UserDTO> UpdateUser(UserDTO user);
        Task<UserDTO> GetUserById(int id);
        Task DeleteUser(int id);
        Task<List<Role>> GetUserRoles(int id);
    }
}
