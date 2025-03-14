using Restaurant.Enums;
using Restaurant.Models;

namespace Restaurant.Services.Interface
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Guid userId, ICollection<OrderItem> orderItems, PaymentMethod paymentMethod);
        Task<List<Order>> GetAllOrdersByUserAsync(Guid userId);
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task<bool> CanceledOrderByIdAsync(Guid orderId, Guid userId);
    }
}
