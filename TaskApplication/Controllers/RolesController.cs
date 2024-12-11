using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskApplication.Core.Interfaces;
using TaskApplication.Data.Entities;

namespace TaskApplication.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController(IRoleRepo roleRepo) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            if (string.IsNullOrEmpty(createRoleDto.RoleName))
                return BadRequest("Role name is required");

            var result = await roleRepo.CreateRole(createRoleDto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleResponseDto>>> GetRoles()
        {
            var roles = await roleRepo.GetRoles();
            return Ok(roles);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] RoleAssignDto roleAssignDto)
        {
            var result = await roleRepo.AssignRole(roleAssignDto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}