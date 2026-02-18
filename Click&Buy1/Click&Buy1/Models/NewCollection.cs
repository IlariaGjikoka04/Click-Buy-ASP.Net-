using System.ComponentModel.DataAnnotations.Schema;

namespace Click_Buy1.Models
{
    [Table("NewCollection")]
    public class NewCollection
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; } // Foto e parë
        public string? ImageUrl2 { get; set; } // Foto e dytë
        public decimal Price { get; set; }
    }
}
