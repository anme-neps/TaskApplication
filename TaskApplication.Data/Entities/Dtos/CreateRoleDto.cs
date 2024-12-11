using System.ComponentModel.DataAnnotations;

namespace TaskApplication.Data.Entities
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "Role Name is required.")]
        public string RoleName { get; set;} = null!;
    }
}