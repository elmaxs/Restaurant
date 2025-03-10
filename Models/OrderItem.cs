﻿namespace Restaurant.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Order Order { get; set; }
        public Guid OrderId { get; set; }

        public MenuItem MenuItem { get; set; }
        public Guid MenuItemId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
