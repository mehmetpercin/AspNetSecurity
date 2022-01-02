using System.ComponentModel.DataAnnotations.Schema;

namespace DataProtection.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [NotMapped]
        public string EncryptedId { get; set; } = string.Empty;
    }
}
