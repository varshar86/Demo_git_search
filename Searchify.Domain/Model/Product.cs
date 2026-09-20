using System.ComponentModel.DataAnnotations;

namespace Searchify.Domain.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
    }
}
