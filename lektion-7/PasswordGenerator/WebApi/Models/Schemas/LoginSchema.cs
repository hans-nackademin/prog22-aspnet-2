namespace WebApi.Models.Schemas
{
    public class LoginSchema
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
