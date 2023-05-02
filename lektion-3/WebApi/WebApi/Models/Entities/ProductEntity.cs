using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class ProductEntity
    {
        [Key]
        public string ArticleNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Specification { get; set; }
        public decimal Price { get; set; }

        public string PartitionKey { get; set; } = "Product";
    }
}
