using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskApplication.Core.Interfaces;
using TaskApplication.Data.Common;
using TaskApplication.Data.Entities;

namespace TaskApplication.Core
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<Users> _userManager;
        private readonly IConfiguration _configuration;

        public AccountRepo(UserManager<Users> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<BaseRespone> DeleteUser(string id)
        {
            var result = new BaseRespone();
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
            {
                result.IsSuccess = false;
                result.Description = "User not Found";
                return result;
            }

            var response = await _userManager.DeleteAsync(user);
            if (response.Succeeded)
            {
                result.IsSuccess = true;
                result.Description = "Role deleted successfully.";
                return result;
            }

            result.IsSuccess = false;
            result.Description = "Role deletion failed.";
            return result;
        }

        public async Task<UserDetailDto> GetUserDetail(string userId)
        {
            var result = new UserDetailDto();
            var user = await _userManager.FindByIdAsync(userId!);

            if (user is null)
            {
                result.IsSuccess = false;
                result.Description = "User not found";
                return result;
            }
            
            return new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Roles = [.. await _userManager.GetRolesAsync(user)],
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                AccessFailedCount = user.AccessFailedCount,
                IsSuccess = true
            };
        }

        public async Task<IEnumerable<UserDetailDto>> GetUsers()
        {
            return await _userManager.Users.Select(u => new UserDetailDto
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                Roles = _userManager.GetRolesAsync(u).Result.ToArray(),
            }).ToListAsync();
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var result = new AuthResponseDto();
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
            {
                result.IsSuccess = false;
                result.Description = "User not found with this email.";
                return result;
            }

            var response = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!response)
            {
                result.IsSuccess = false;
                result.Description = "Invalid Password.";
                return result;
            }
            result.IsSuccess = true;
            result.Token = GenerateToken(user);
            result.Description = "user login successful";
            return result;
        }

        public async Task<BaseRespone> Register(RegisterDto registerDto)
        {
            var result = new BaseRespone();
            var user = new Users
            {
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                UserName = registerDto.Email
            };

            var response = await _userManager.CreateAsync(user, registerDto.Password);

            if (!response.Succeeded)
            {
                result.IsSuccess = false;
                result.Description = "Could not create User. Please try again";
                return result;
            }

            if (registerDto.Roles is null)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                foreach (var role in registerDto.Roles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
            result.IsSuccess = true;
            result.Description = "User created successfully";
            return result;
        }

        private string GenerateToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII
            .GetBytes(_configuration.GetSection("JWTSetting").GetSection("securityKey").Value!);

            var roles = _userManager.GetRolesAsync(user).Result;

            List<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new(JwtRegisteredClaimNames.Name, user.FullName ?? ""),
                new(JwtRegisteredClaimNames.NameId, user.Id ?? ""),
                new(JwtRegisteredClaimNames.Aud,
                 _configuration.GetSection("JWTSetting").GetSection("validAudience").Value!),
                new(JwtRegisteredClaimNames.Iss,
                 _configuration.GetSection("JWTSetting").GetSection("validIssuer").Value!),
            ];

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
