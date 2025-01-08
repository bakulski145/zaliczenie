using System;
using System.Collections.Generic;
using zaliczenie.Models;

namespace zaliczenie.Models // Zmień na faktyczny namespace projektu
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        New,
        Processing,
        Completed,
        Cancelled
    }
}
