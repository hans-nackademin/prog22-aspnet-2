using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using WebApi.Helpers.Services;

namespace WebApi.Models.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;

}

public class UserHash
{
    [Key]
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public string SecurityConcurrencyKey { get; set; } = null!;

    public UserEntity User { get; set; } = null!;
}