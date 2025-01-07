namespace zaliczenie.Models
{
    public class Order
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Address { get; set; }
    }
}