using System.ComponentModel.DataAnnotations.Schema;

namespace Click_Buy1.Models
{
    [Table("Jeans")]
    public class Jeans
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
