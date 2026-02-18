using System.ComponentModel.DataAnnotations.Schema;
namespace Click_Buy1.Models
{
    [Table("RemovedFromCart")]
    public class RemovedFromCart
    {

        public int Id { get; set; }
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public DateTime RemovedAt { get; set; } = DateTime.Now;
    }
}
