using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Enums;
using Restaurant.Models;
using Restaurant.Models.DTOs;
using Restaurant.Services.Interface;
using System.Security.Claims;

namespace Restaurant.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly ApplicationDbContext _applicationDbContext;

        public OrderController(UserManager<ApplicationUser> userManager, IOrderService orderService, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _orderService = orderService;
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderRequest request)
        {
            if (request == null || !request.OrderItems.Any())
                return BadRequest("Any corrent items");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("You are not authorized");

            var menuItemIds = request.OrderItems.Select(m => m.MenuItemId).ToList();

            var menuItems = await _applicationDbContext.MenuItems
                .Where(m => menuItemIds.Contains(m.Id))
                .ToListAsync();

            var orderItems = request.OrderItems.Select(item =>
            {
                var menuItem = menuItems.First(i => i.Id == item.MenuItemId);
                return new OrderItem
                {
                    Id = new Guid(),
                    MenuItemId = menuItem.Id,
                    Quantity = item.Quantity,
                    Price = menuItem.Price * item.Quantity
                };
            }).ToList();

            if (orderItems.Count != request.OrderItems.Count)
                return BadRequest("Not all dishes loaded");

            var order = await _orderService.CreateOrderAsync(Guid.Parse(userId), orderItems, request.PaymentMethod);
            
            return Ok(order);
        }

        [HttpGet("historyOrders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            if (orders == null)
                return NotFound("Orders not found");

            return Ok(orders);
        }

        [HttpGet("userOrders")]
        public async Task<IActionResult> GetAllOrdersByUserAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("You are not authorize");

            var orders = await _orderService.GetAllOrdersByUserAsync(Guid.Parse(userId));
            if (orders == null)
                return NotFound("Orders not found");

            return Ok(orders);
        }

        [HttpGet("allOrders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Not correct id");

            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound("Order not found");

            return Ok(order);
        }

        [HttpPut("updateStatus")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderByIdAsync(Guid orderId, OrderStatus newStatus)
        {
            if (orderId == Guid.Empty)
                return BadRequest("Not correct order id");

            var order = await _orderService.UpdateOrderStatusAsync(orderId, newStatus);
            if (order == null)
                return BadRequest("Order not found or he cant be update");

            return Ok("Order status update");
        }

        [HttpDelete("cancelOrder")]
        public async Task<IActionResult> CancelOrderAsync(Guid orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("You are not authorize");

            var status = await _orderService.CanceledOrderByIdAsync(orderId, Guid.Parse(userId));
            if (!status)
                return BadRequest("Order not found or cant be update");

            return Ok("Order was cancelled");
        }
    }
}
