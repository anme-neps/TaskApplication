using TaskApplication.Data.Common;

namespace TaskApplication.Data.Entities
{
    public class AuthResponseDto : BaseRespone
    {

        public string? Token { get; set; } = string.Empty;

    }
}
