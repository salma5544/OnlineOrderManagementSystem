using OnlineOrderManagementSystem.Models;
using System.Text.Json.Serialization;

namespace OnlineOrderManagementSystem.DTOs
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; } = new();
        public DateTime? StatusUpdatedAt { get; set; } 
        public List<OrderItemDetailsDto> Items { get; set; } = new();
    }

}
