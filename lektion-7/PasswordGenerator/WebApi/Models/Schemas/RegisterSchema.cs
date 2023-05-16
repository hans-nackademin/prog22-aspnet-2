using WebApi.Models.Entities;

namespace WebApi.Models.Schemas
{
    public class RegisterSchema
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public static implicit operator UserEntity(RegisterSchema schema)
        {
            return new UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = schema.FirstName,
                LastName = schema.LastName,
                Email = schema.Email
            };
        }
    }
}
