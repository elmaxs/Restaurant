using Restaurant.Enums;
using Restaurant.Models;

namespace Restaurant.Services.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Guid userId, ICollection<OrderItem> orderItems, PaymentMethod paymentMethod);
        Task<List<Order>> GetAllOrdersByUserAsync(Guid userId);
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task<bool> CanceledOrderByIdAsync(Guid orderId, Guid userId);
        Task<List<Order>> GetAllOrdersAsync(); // Додано отримання всіх замовлень
        Task<Order?> UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus); // Додано оновлення статусу замовлення
    }
}
