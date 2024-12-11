using TaskApplication.Data.Common;
using TaskApplication.Data.Entities;

namespace TaskApplication.Core.Interfaces
{
    public interface IRoleRepo
    {
        public Task<BaseRespone> CreateRole(CreateRoleDto createRoleDto);
        public Task<IEnumerable<RoleResponseDto>> GetRoles();
        public Task<BaseRespone> AssignRole(RoleAssignDto roleAssignDto);
    }
}
