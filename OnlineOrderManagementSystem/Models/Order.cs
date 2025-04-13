using System.Text.Json.Serialization;

namespace OnlineOrderManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime? StatusUpdatedAt { get; set; } 
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    public enum OrderStatus { Pending, Processing, Shipped, Delivered }
}
