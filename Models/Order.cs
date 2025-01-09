using System;
using System.ComponentModel.DataAnnotations;

namespace zaliczenie.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; }

        public string UserEmail { get; set; }
        public User User { get; set; }
    }
}
