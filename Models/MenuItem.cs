using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class MenuItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlImageAdress { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
