using System.ComponentModel.DataAnnotations.Schema;

namespace Click_Buy1.Models
{
    [Table("AddToCart")]
    public class AddToCart
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }

    }
}