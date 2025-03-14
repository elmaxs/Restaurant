using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Enums;
using Restaurant.Models;
using Restaurant.Services.Interface;

namespace Restaurant.Services.RealizeService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OrderService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> CanceledOrderByIdAsync(Guid orderId, Guid userId)
        {
            var order = await _applicationDbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId && o.ApplicationUserId == userId);

            if (order == null || order.OrderStatus != OrderStatus.New)
                return false;

            order.OrderStatus = OrderStatus.Cancelled;
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetAllOrdersByUserAsync(Guid userId)
        {
            return await _applicationDbContext.Orders
                .Where(o => o.ApplicationUserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .OrderByDescending(o => o.OrderTime)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _applicationDbContext.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync();
        }

        public async Task CreateOrderAsync(Guid userId, ICollection<OrderItem> orderItems, PaymentMethod paymentMethod)
        {
            var totalPrice = orderItems.Sum(s => s.Price * s.Quantity);

            var order = new Order
            {
                Id = new Guid(),
                ApplicationUserId = userId,
                OrderItems = orderItems,
                PaymentMethod = paymentMethod,
                TotalPrice = totalPrice
            };

            await _applicationDbContext.AddAsync(order);
            await _applicationDbContext.SaveChangesAsync();
            //return order;
        }
    }
}
