using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Order Order { get; set; }
        public Guid OrderId { get; set; }

        public MenuItem MenuItem { get; set; }
        public Guid MenuItemId { get; set; }

        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
