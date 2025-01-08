using System;

namespace zaliczenie.Models // Zmień namespace na właściwy
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public string ProductName { get; set; } // Nazwa produktu
        public int Quantity { get; set; } // Ilość produktu
        public decimal Price { get; set; } // Cena jednostkowa produktu
    }
}
