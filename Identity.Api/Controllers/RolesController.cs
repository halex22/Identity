using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Model;
using Identity.Service.Model;
using Identity.Api.Service.Interfaces;
using Identity.Api.Model.DTOs;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _service.GetAllRoles();
            return Ok(roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole([FromRoute]int id)
        {
            try
            {
                var role = await _service.GetRoleById(id);
                return Ok(role);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Role with ID {id} not found.");
            }

        }

        // GET: api/Roles/1/User
        // tutti gli utenti che hanno il role 1
        [HttpGet("{id}/users")]
        public async Task<IActionResult> GetUsersByRoleId([FromRoute]int id)
        {
            try
            {
                var users = await _service.GetUsersByRoleId(id);
                return Ok(users);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole([FromRoute]int id, [FromBody]Role role)
        {
            if (id != role.Id) return BadRequest();


            try
            {
                var updatedRole = await _service.UpdateRole(role);
                return Ok(updatedRole);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex);
            }
            catch (InvalidOperationException ex)
            {

                    return BadRequest(ex);
            }

        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] RawRole role)
        {
            if (string.IsNullOrWhiteSpace(role.Type))
            {
                return BadRequest("Role type cannot be null or empty.");
            }

            var createdRole = await _service.createRole(role);

            return CreatedAtAction("GetRole", new { id = createdRole.Id }, createdRole);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var deletedRoleId = await _service.DeleteUserRole(id);
                return Ok(deletedRoleId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
