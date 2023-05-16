using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities
{
    public class UserHashEntity
    {
        [Key]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public string SecurityHash { get; set; } = null!;
    
        public UserEntity User { get; set; } = null!;
    }
}
