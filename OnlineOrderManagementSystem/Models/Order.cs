namespace OnlineOrderManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = OrderStatus.Pending.ToString();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    enum OrderStatus { Pending, Processing, Shipped, Delivered }
}
