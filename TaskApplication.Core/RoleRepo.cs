using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskApplication.Core.Interfaces;
using TaskApplication.Data.Common;
using TaskApplication.Data.Entities;

namespace TaskApplication.Core
{
    public class RoleRepo : IRoleRepo
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepo(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<BaseRespone> AssignRole(RoleAssignDto roleAssignDto)
        {
            var result = new BaseRespone();
            var user = await _userManager.FindByIdAsync(roleAssignDto.UserId);

            if (user is null)
            {
                result.IsSuccess = false;
                result.Description = "User not found.";
                return result;
            }

            var role = await _roleManager.FindByIdAsync(roleAssignDto.RoleId);

            if (role is null)
            {
                result.IsSuccess = false;
                result.Description = "Role not found.";
                return result;
            }

            var response = await _userManager.AddToRoleAsync(user, role.Name!);

            if (!response.Succeeded)
            {
                result.IsSuccess = false;
                result.Description = "Role assign failure.";
                return result;
            }

            result.IsSuccess = true;
            result.Description = "Role assigned successfully.";
            return result;
        }

        public async Task<BaseRespone> CreateRole(CreateRoleDto createRoleDto)
        {
            var result = new BaseRespone();
            var roleExist = await _roleManager.RoleExistsAsync(createRoleDto.RoleName);

            if (roleExist)
            {
                result.IsSuccess = false;
                result.Description = "Role already exist.";
                return result;
            }

            var roleResult = await _roleManager.CreateAsync(new IdentityRole(createRoleDto.RoleName));

            if (!roleResult.Succeeded)
            {
                result.IsSuccess = false;
                result.Description = "Role creation failed.";
                return result;
            }
            result.IsSuccess = true;
            result.Description = "Role Created successfully";
            return result;
        }

        public async Task<IEnumerable<RoleResponseDto>> GetRoles()
        {
            return await _roleManager.Roles.Select(r => new RoleResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                TotalUsers = _userManager.GetUsersInRoleAsync(r.Name!).Result.Count
            }).ToListAsync();
        }
    }
}
