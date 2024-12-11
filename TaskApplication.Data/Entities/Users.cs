using Microsoft.AspNetCore.Identity;

namespace TaskApplication.Data.Entities
{
    public class Users : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
