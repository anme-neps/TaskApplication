using TaskApplication.Data.Common;
using TaskApplication.Data.Entities;

namespace TaskApplication.Core.Interfaces
{
    public interface IAccountRepo
    {
        public Task<BaseRespone> Register(RegisterDto registerDto);
        public Task<AuthResponseDto> Login(LoginDto loginDto);
        public Task<UserDetailDto> GetUserDetail(string userId);
        public Task<IEnumerable<UserDetailDto>> GetUsers();
        public Task<BaseRespone> DeleteUser(string id);
    }
}
