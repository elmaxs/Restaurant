using Restaurant.Enums;

namespace Restaurant.Models.DTOs
{
    public class OrderRequest
    {
        public ICollection<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class OrderItemRequest
    {
        public Guid MenuItemId { get; set; } // ID страви
        public int Quantity { get; set; } // Кількість
    }
}
